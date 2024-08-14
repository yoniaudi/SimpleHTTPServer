namespace SimpleHTTPServer
{
    partial class ConnectionForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonStartServer = new Button();
            buttonStopServer = new Button();
            labelServerPort = new Label();
            textBoxServerPort = new TextBox();
            labelServerLogs = new Label();
            textBoxServerLogs = new TextBox();
            SuspendLayout();
            // 
            // buttonStartServer
            // 
            buttonStartServer.Location = new Point(12, 59);
            buttonStartServer.Name = "buttonStartServer";
            buttonStartServer.Size = new Size(100, 23);
            buttonStartServer.TabIndex = 0;
            buttonStartServer.Text = "Start Server";
            buttonStartServer.UseVisualStyleBackColor = true;
            buttonStartServer.Click += buttonStartServer_Click;
            // 
            // buttonStopServer
            // 
            buttonStopServer.Location = new Point(12, 88);
            buttonStopServer.Name = "buttonStopServer";
            buttonStopServer.Size = new Size(100, 23);
            buttonStopServer.TabIndex = 1;
            buttonStopServer.Text = "Stop Server";
            buttonStopServer.UseVisualStyleBackColor = true;
            buttonStopServer.Click += buttonStopServer_Click;
            // 
            // labelServerPort
            // 
            labelServerPort.AutoSize = true;
            labelServerPort.Location = new Point(12, 12);
            labelServerPort.Name = "labelServerPort";
            labelServerPort.Size = new Size(64, 15);
            labelServerPort.TabIndex = 2;
            labelServerPort.Text = "Server Port";
            // 
            // textBoxServerPort
            // 
            textBoxServerPort.Location = new Point(12, 30);
            textBoxServerPort.Name = "textBoxServerPort";
            textBoxServerPort.Size = new Size(100, 23);
            textBoxServerPort.TabIndex = 3;
            textBoxServerPort.Text = "80";
            textBoxServerPort.Validating += textBoxServerPort_Validating;
            // 
            // labelServerLogs
            // 
            labelServerLogs.AutoSize = true;
            labelServerLogs.Location = new Point(117, 12);
            labelServerLogs.Name = "labelServerLogs";
            labelServerLogs.Size = new Size(67, 15);
            labelServerLogs.TabIndex = 4;
            labelServerLogs.Text = "Server Logs";
            // 
            // textBoxServerLogs
            // 
            textBoxServerLogs.Location = new Point(117, 30);
            textBoxServerLogs.Multiline = true;
            textBoxServerLogs.Name = "textBoxServerLogs";
            textBoxServerLogs.ReadOnly = true;
            textBoxServerLogs.ScrollBars = ScrollBars.Both;
            textBoxServerLogs.Size = new Size(223, 268);
            textBoxServerLogs.TabIndex = 5;
            // 
            // ConnectionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(349, 304);
            Controls.Add(textBoxServerLogs);
            Controls.Add(labelServerLogs);
            Controls.Add(textBoxServerPort);
            Controls.Add(labelServerPort);
            Controls.Add(buttonStopServer);
            Controls.Add(buttonStartServer);
            MaximizeBox = false;
            Name = "ConnectionForm";
            Text = "Simple HTTP Server";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonStartServer;
        private Button buttonStopServer;
        private Label labelServerPort;
        private TextBox textBoxServerPort;
        private Label labelServerLogs;
        private TextBox textBoxServerLogs;
    }
}
