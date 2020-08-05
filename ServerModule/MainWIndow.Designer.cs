namespace NetSubSysTEST
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.bStartServer = new System.Windows.Forms.Button();
            this.bStopServer = new System.Windows.Forms.Button();
            this.lbClients = new System.Windows.Forms.ListBox();
            this.lbMessages = new System.Windows.Forms.ListBox();
            this.gbInfo = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lConnectedClientsCaption = new System.Windows.Forms.Label();
            this.lEventsCaption = new System.Windows.Forms.Label();
            this.lConnectedClientsCount = new System.Windows.Forms.Label();
            this.lConnectedCllientsCountCaption = new System.Windows.Forms.Label();
            this.lPort = new System.Windows.Forms.Label();
            this.lPortCaption = new System.Windows.Forms.Label();
            this.lIP = new System.Windows.Forms.Label();
            this.lIPCaption = new System.Windows.Forms.Label();
            this.lStatus = new System.Windows.Forms.Label();
            this.lStatusCaption = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbCreateDB = new System.Windows.Forms.ToolStripButton();
            this.tsbCheckDBConnection = new System.Windows.Forms.ToolStripButton();
            this.gbControl = new System.Windows.Forms.GroupBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.gbInfo.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.gbControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // bStartServer
            // 
            this.bStartServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bStartServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bStartServer.Location = new System.Drawing.Point(60, 35);
            this.bStartServer.Name = "bStartServer";
            this.bStartServer.Size = new System.Drawing.Size(137, 38);
            this.bStartServer.TabIndex = 3;
            this.bStartServer.Text = "Start Server";
            this.bStartServer.UseVisualStyleBackColor = true;
            this.bStartServer.Click += new System.EventHandler(this.bStartServer_Click);
            // 
            // bStopServer
            // 
            this.bStopServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bStopServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bStopServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bStopServer.Location = new System.Drawing.Point(655, 35);
            this.bStopServer.Name = "bStopServer";
            this.bStopServer.Size = new System.Drawing.Size(137, 38);
            this.bStopServer.TabIndex = 4;
            this.bStopServer.Text = "Stop Server";
            this.bStopServer.UseVisualStyleBackColor = true;
            this.bStopServer.Click += new System.EventHandler(this.bStopServer_Click);
            // 
            // lbClients
            // 
            this.lbClients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbClients.BackColor = System.Drawing.SystemColors.Control;
            this.lbClients.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbClients.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbClients.ItemHeight = 16;
            this.lbClients.Location = new System.Drawing.Point(7, 23);
            this.lbClients.Name = "lbClients";
            this.lbClients.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lbClients.Size = new System.Drawing.Size(830, 82);
            this.lbClients.TabIndex = 5;
            // 
            // lbMessages
            // 
            this.lbMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbMessages.BackColor = System.Drawing.SystemColors.Control;
            this.lbMessages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbMessages.ItemHeight = 16;
            this.lbMessages.Location = new System.Drawing.Point(7, 23);
            this.lbMessages.Name = "lbMessages";
            this.lbMessages.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lbMessages.Size = new System.Drawing.Size(830, 162);
            this.lbMessages.TabIndex = 6;
            // 
            // gbInfo
            // 
            this.gbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbInfo.Controls.Add(this.splitContainer1);
            this.gbInfo.Controls.Add(this.lConnectedClientsCount);
            this.gbInfo.Controls.Add(this.lConnectedCllientsCountCaption);
            this.gbInfo.Controls.Add(this.lPort);
            this.gbInfo.Controls.Add(this.lPortCaption);
            this.gbInfo.Controls.Add(this.lIP);
            this.gbInfo.Controls.Add(this.lIPCaption);
            this.gbInfo.Controls.Add(this.lStatus);
            this.gbInfo.Controls.Add(this.lStatusCaption);
            this.gbInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbInfo.Location = new System.Drawing.Point(0, 132);
            this.gbInfo.Name = "gbInfo";
            this.gbInfo.Size = new System.Drawing.Size(852, 491);
            this.gbInfo.TabIndex = 2;
            this.gbInfo.TabStop = false;
            this.gbInfo.Text = "Информация";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(6, 166);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lConnectedClientsCaption);
            this.splitContainer1.Panel1.Controls.Add(this.lbClients);
            this.splitContainer1.Panel1MinSize = 55;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lEventsCaption);
            this.splitContainer1.Panel2.Controls.Add(this.lbMessages);
            this.splitContainer1.Panel2MinSize = 50;
            this.splitContainer1.Size = new System.Drawing.Size(840, 319);
            this.splitContainer1.SplitterDistance = 119;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 16;
            // 
            // lConnectedClientsCaption
            // 
            this.lConnectedClientsCaption.AutoSize = true;
            this.lConnectedClientsCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lConnectedClientsCaption.Location = new System.Drawing.Point(3, 0);
            this.lConnectedClientsCaption.Name = "lConnectedClientsCaption";
            this.lConnectedClientsCaption.Size = new System.Drawing.Size(188, 20);
            this.lConnectedClientsCaption.TabIndex = 6;
            this.lConnectedClientsCaption.Text = "Подключенные модули";
            // 
            // lEventsCaption
            // 
            this.lEventsCaption.AutoSize = true;
            this.lEventsCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lEventsCaption.Location = new System.Drawing.Point(3, 0);
            this.lEventsCaption.Name = "lEventsCaption";
            this.lEventsCaption.Size = new System.Drawing.Size(76, 20);
            this.lEventsCaption.TabIndex = 7;
            this.lEventsCaption.Text = "События";
            // 
            // lConnectedClientsCount
            // 
            this.lConnectedClientsCount.AutoSize = true;
            this.lConnectedClientsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lConnectedClientsCount.Location = new System.Drawing.Point(199, 132);
            this.lConnectedClientsCount.Name = "lConnectedClientsCount";
            this.lConnectedClientsCount.Size = new System.Drawing.Size(18, 20);
            this.lConnectedClientsCount.TabIndex = 15;
            this.lConnectedClientsCount.Text = "0";
            // 
            // lConnectedCllientsCountCaption
            // 
            this.lConnectedCllientsCountCaption.AutoSize = true;
            this.lConnectedCllientsCountCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lConnectedCllientsCountCaption.Location = new System.Drawing.Point(12, 132);
            this.lConnectedCllientsCountCaption.Name = "lConnectedCllientsCountCaption";
            this.lConnectedCllientsCountCaption.Size = new System.Drawing.Size(181, 20);
            this.lConnectedCllientsCountCaption.TabIndex = 14;
            this.lConnectedCllientsCountCaption.Text = "Подключено модулей:\r\n";
            // 
            // lPort
            // 
            this.lPort.AutoSize = true;
            this.lPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lPort.Location = new System.Drawing.Point(199, 100);
            this.lPort.Name = "lPort";
            this.lPort.Size = new System.Drawing.Size(35, 20);
            this.lPort.TabIndex = 13;
            this.lPort.Text = "N/A";
            // 
            // lPortCaption
            // 
            this.lPortCaption.AutoSize = true;
            this.lPortCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lPortCaption.Location = new System.Drawing.Point(12, 100);
            this.lPortCaption.Name = "lPortCaption";
            this.lPortCaption.Size = new System.Drawing.Size(52, 20);
            this.lPortCaption.TabIndex = 12;
            this.lPortCaption.Text = "Порт:";
            // 
            // lIP
            // 
            this.lIP.AutoSize = true;
            this.lIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lIP.Location = new System.Drawing.Point(199, 68);
            this.lIP.Name = "lIP";
            this.lIP.Size = new System.Drawing.Size(35, 20);
            this.lIP.TabIndex = 11;
            this.lIP.Text = "N/A";
            // 
            // lIPCaption
            // 
            this.lIPCaption.AutoSize = true;
            this.lIPCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lIPCaption.Location = new System.Drawing.Point(12, 68);
            this.lIPCaption.Name = "lIPCaption";
            this.lIPCaption.Size = new System.Drawing.Size(28, 20);
            this.lIPCaption.TabIndex = 10;
            this.lIPCaption.Text = "IP:";
            // 
            // lStatus
            // 
            this.lStatus.AutoSize = true;
            this.lStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lStatus.Location = new System.Drawing.Point(199, 36);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(87, 20);
            this.lStatus.TabIndex = 9;
            this.lStatus.Text = "Отключен";
            this.lStatus.TextChanged += new System.EventHandler(this.lStatus_TextChanged);
            // 
            // lStatusCaption
            // 
            this.lStatusCaption.AutoSize = true;
            this.lStatusCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lStatusCaption.Location = new System.Drawing.Point(12, 36);
            this.lStatusCaption.Name = "lStatusCaption";
            this.lStatusCaption.Size = new System.Drawing.Size(66, 20);
            this.lStatusCaption.TabIndex = 8;
            this.lStatusCaption.Text = "Статус:";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCreateDB,
            this.tsbCheckDBConnection});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(852, 26);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbCreateDB
            // 
            this.tsbCreateDB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbCreateDB.Image = ((System.Drawing.Image)(resources.GetObject("tsbCreateDB.Image")));
            this.tsbCreateDB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCreateDB.Name = "tsbCreateDB";
            this.tsbCreateDB.Size = new System.Drawing.Size(97, 23);
            this.tsbCreateDB.Text = "Создать БД";
            // 
            // tsbCheckDBConnection
            // 
            this.tsbCheckDBConnection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbCheckDBConnection.Image = ((System.Drawing.Image)(resources.GetObject("tsbCheckDBConnection.Image")));
            this.tsbCheckDBConnection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCheckDBConnection.Name = "tsbCheckDBConnection";
            this.tsbCheckDBConnection.Size = new System.Drawing.Size(234, 23);
            this.tsbCheckDBConnection.Text = "Проверить подключение к БД";
            this.tsbCheckDBConnection.Click += new System.EventHandler(this.tsbCheckDBConnection_Click);
            // 
            // gbControl
            // 
            this.gbControl.Controls.Add(this.bStopServer);
            this.gbControl.Controls.Add(this.bStartServer);
            this.gbControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbControl.Location = new System.Drawing.Point(0, 26);
            this.gbControl.Name = "gbControl";
            this.gbControl.Size = new System.Drawing.Size(852, 100);
            this.gbControl.TabIndex = 3;
            this.gbControl.TabStop = false;
            this.gbControl.Text = "Управление";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Сервер";
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 623);
            this.Controls.Add(this.gbControl);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.gbInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(380, 500);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сервер";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.gbInfo.ResumeLayout(false);
            this.gbInfo.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gbControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bStartServer;
        private System.Windows.Forms.Button bStopServer;
        private System.Windows.Forms.ListBox lbClients;
        private System.Windows.Forms.ListBox lbMessages;
        private System.Windows.Forms.GroupBox gbInfo;
        private System.Windows.Forms.Label lConnectedClientsCaption;
        private System.Windows.Forms.Label lEventsCaption;
        private System.Windows.Forms.Label lStatusCaption;
        private System.Windows.Forms.Label lConnectedCllientsCountCaption;
        private System.Windows.Forms.Label lPort;
        private System.Windows.Forms.Label lPortCaption;
        private System.Windows.Forms.Label lIP;
        private System.Windows.Forms.Label lIPCaption;
        private System.Windows.Forms.Label lStatus;
        private System.Windows.Forms.Label lConnectedClientsCount;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbCreateDB;
        private System.Windows.Forms.ToolStripButton tsbCheckDBConnection;
        private System.Windows.Forms.GroupBox gbControl;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

