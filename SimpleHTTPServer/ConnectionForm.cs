using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SimpleHTTPServer
{
    public partial class ConnectionForm : Form
    {
        private Socket? m_HttpServer = null;
        private Thread? m_Thread = null;
        private int m_Port = 80;
        private bool m_IsServerRunning = false;
        private ManualResetEvent m_StopEvent = new ManualResetEvent(false);

        public ConnectionForm()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            buttonStopServer.Enabled = false;
        }

        private void buttonStartServer_Click(object sender, EventArgs e)
        {
            startServer();
        }

        private void startServer()
        {
            try
            {
                textBoxServerLogs.Invoke(() => textBoxServerLogs.Text = "");
                m_HttpServer = new Socket(SocketType.Stream, ProtocolType.Tcp);

                if (m_Port <= 0 || m_Port > 65535)
                {
                    throw new Exception("Server Port is not in range.");
                }
            }
            catch (Exception ex)
            {
                string errorMsg = $"Error while starting the server.{Environment.NewLine}Exception: {ex.Message}.";

                textBoxServerLogs.Invoke(() => textBoxServerLogs.Text = $"Server Starting Failed.{Environment.NewLine}{errorMsg}");

                return;
            }

            m_IsServerRunning = true;
            m_StopEvent.Reset();
            textBoxServerLogs.Invoke(() => textBoxServerLogs.Text = "Server Started...");
            m_Thread = new Thread(() => connectionThreadMethod());
            m_Thread.Start();
            buttonStartServer.Enabled = false;
            buttonStopServer.Enabled = true;
            textBoxServerPort.Enabled = false;
        }

        private void buttonStopServer_Click(object sender, EventArgs e)
        {
            stopServer();
        }

        private void stopServer()
        {
            try
            {
                m_IsServerRunning = false;
                m_StopEvent.Set();
                m_HttpServer?.Close();
                m_HttpServer = null;

                if (m_Thread != null && m_Thread.IsAlive)
                {
                    m_Thread.Join();
                }

                buttonStartServer.Invoke(() => buttonStartServer.Enabled = true);
                buttonStopServer.Invoke(() => buttonStopServer.Enabled = false);
                textBoxServerPort.Invoke(() => textBoxServerPort.Enabled = true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Stopping the server failed.{Environment.NewLine}Exception: {ex.Message}.");
            }
        }

        private void connectionThreadMethod()
        {
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, m_Port);

                m_HttpServer?.Bind(endPoint);
                m_HttpServer?.Listen(10);

                while (m_IsServerRunning)
                {
                    List<Socket> checkRead = new List<Socket> { m_HttpServer };

                    Socket.Select(checkRead, null, null, 1000000);

                    if (checkRead.Count > 0)
                    {
                        Socket client = m_HttpServer.Accept();
                        ThreadPool.QueueUserWorkItem(handleClient, client);
                    }
                }
            }
            catch (SocketException ex)
            {
                if (m_IsServerRunning)
                {
                    textBoxServerLogs.Invoke(() => textBoxServerLogs.Text += $"Socket error: {ex.Message}\r\n");
                }
            }
            catch (Exception ex)
            {
                textBoxServerLogs.Invoke(() => textBoxServerLogs.Text += $"General error: {ex.Message}\r\n");
            }
        }

        private void handleClient(object? clientObj)
        {
            if (clientObj is Socket client)
            {
                try
                {
                    byte[] bytes = new byte[2048];
                    string data = readInboundConnectionData(bytes, client);

                    textBoxServerLogs.Invoke(() => textBoxServerLogs.Text += $"\r\n\r\n{data}\n\n----- End of Request -----");
                    sendResponseBack(DateTime.Now, client);
                }
                catch (Exception ex)
                {
                    textBoxServerLogs.Invoke(() => textBoxServerLogs.Text += $"Error handling client: {ex.Message}\r\n");
                }
                finally
                {
                    client.Close();
                }
            }
        }

        private string readInboundConnectionData(byte[] bytes, Socket client)
        {
            StringBuilder data = new StringBuilder();

            do
            {
                int bytesCount = client.Receive(bytes);
                data.Append(Encoding.ASCII.GetString(bytes, 0, bytesCount));
            }
            while (data.ToString().IndexOf("\r\n") <= -1);

            return data.ToString();
        }

        private static void sendResponseBack(DateTime time, Socket client)
        {
            string resultHeader = string.Format("HTTP/1.1 200 Everything is Running{0}Server: my_csharp_server{0}Content-Type: text/html; charset: UTF-8{0}{0}",
                                Environment.NewLine);
            string resultBody = $"<!DOCTYPE html><html><head><title>My Server</title></head><body><h4>Server Time is: {time}</h4></body></html>";
            string resultString = $"{resultHeader}{resultBody}";
            byte[] resultData = Encoding.ASCII.GetBytes(resultString);

            client.Send(resultData);
        }

        private void textBoxServerPort_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string port = ((TextBox)sender).Text;

            if (int.TryParse(port, out m_Port) == false)
            {
                MessageBox.Show("Textbox must contain digits only.");
                e.Cancel = true;
            }
        }
    }
}
