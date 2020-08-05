namespace UserModule
{
    partial class MainWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslOSVer = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslHostName = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslHostIP = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssUserName = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbClaimCreation = new System.Windows.Forms.TabPage();
            this.lInfo = new System.Windows.Forms.Label();
            this.gbStep4 = new System.Windows.Forms.GroupBox();
            this.lPhone = new System.Windows.Forms.Label();
            this.lName = new System.Windows.Forms.Label();
            this.tbPhone = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lStep4Tip = new System.Windows.Forms.Label();
            this.gbStep5 = new System.Windows.Forms.GroupBox();
            this.tbStep5 = new System.Windows.Forms.TextBox();
            this.lStep5Tip = new System.Windows.Forms.Label();
            this.lSendClaim = new System.Windows.Forms.Label();
            this.gbStep3 = new System.Windows.Forms.GroupBox();
            this.cbStep3 = new System.Windows.Forms.ComboBox();
            this.lStep3Tip = new System.Windows.Forms.Label();
            this.gbStep2 = new System.Windows.Forms.GroupBox();
            this.cbStep2 = new System.Windows.Forms.ComboBox();
            this.lStep2Tip = new System.Windows.Forms.Label();
            this.gbStep1 = new System.Windows.Forms.GroupBox();
            this.cbStep1 = new System.Windows.Forms.ComboBox();
            this.lStep1Tip = new System.Windows.Forms.Label();
            this.tbActiveClaims = new System.Windows.Forms.TabPage();
            this.dgvActiveClaims = new System.Windows.Forms.DataGridView();
            this.activeClaimsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteClaim = new System.Windows.Forms.ToolStripMenuItem();
            this.lNoActiveClaims = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tbClaimCreation.SuspendLayout();
            this.gbStep4.SuspendLayout();
            this.gbStep5.SuspendLayout();
            this.gbStep3.SuspendLayout();
            this.gbStep2.SuspendLayout();
            this.gbStep1.SuspendLayout();
            this.tbActiveClaims.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActiveClaims)).BeginInit();
            this.activeClaimsContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslOSVer,
            this.tsslHostName,
            this.tsslHostIP,
            this.tssUserName});
            this.statusStrip1.Location = new System.Drawing.Point(0, 567);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(850, 24);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslOSVer
            // 
            this.tsslOSVer.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsslOSVer.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.tsslOSVer.Name = "tsslOSVer";
            this.tsslOSVer.Size = new System.Drawing.Size(68, 19);
            this.tsslOSVer.Text = "OS Version";
            // 
            // tsslHostName
            // 
            this.tsslHostName.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsslHostName.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.tsslHostName.Name = "tsslHostName";
            this.tsslHostName.Size = new System.Drawing.Size(71, 19);
            this.tsslHostName.Text = "Host Name";
            // 
            // tsslHostIP
            // 
            this.tsslHostIP.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsslHostIP.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.tsslHostIP.Name = "tsslHostIP";
            this.tsslHostIP.Size = new System.Drawing.Size(49, 19);
            this.tsslHostIP.Text = "Host IP";
            // 
            // tssUserName
            // 
            this.tssUserName.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tssUserName.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.tssUserName.Name = "tssUserName";
            this.tssUserName.Size = new System.Drawing.Size(69, 19);
            this.tssUserName.Text = "User Name";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tbClaimCreation);
            this.tabControl1.Controls.Add(this.tbActiveClaims);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControl1.ItemSize = new System.Drawing.Size(200, 50);
            this.tabControl1.Location = new System.Drawing.Point(0, 3);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(850, 563);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 6;
            this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
            // 
            // tbClaimCreation
            // 
            this.tbClaimCreation.Controls.Add(this.lInfo);
            this.tbClaimCreation.Controls.Add(this.gbStep4);
            this.tbClaimCreation.Controls.Add(this.gbStep5);
            this.tbClaimCreation.Controls.Add(this.lSendClaim);
            this.tbClaimCreation.Controls.Add(this.gbStep3);
            this.tbClaimCreation.Controls.Add(this.gbStep2);
            this.tbClaimCreation.Controls.Add(this.gbStep1);
            this.tbClaimCreation.Location = new System.Drawing.Point(4, 54);
            this.tbClaimCreation.Name = "tbClaimCreation";
            this.tbClaimCreation.Padding = new System.Windows.Forms.Padding(3);
            this.tbClaimCreation.Size = new System.Drawing.Size(842, 505);
            this.tbClaimCreation.TabIndex = 0;
            this.tbClaimCreation.Text = "Создание новой заявки";
            this.tbClaimCreation.UseVisualStyleBackColor = true;
            // 
            // lInfo
            // 
            this.lInfo.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lInfo.AutoSize = true;
            this.lInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lInfo.ForeColor = System.Drawing.Color.Red;
            this.lInfo.Location = new System.Drawing.Point(121, 476);
            this.lInfo.Name = "lInfo";
            this.lInfo.Size = new System.Drawing.Size(606, 24);
            this.lInfo.TabIndex = 5;
            this.lInfo.Text = "По возникшим вопросам обращайтесь в дежурную смену по тел.: ";
            // 
            // gbStep4
            // 
            this.gbStep4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbStep4.Controls.Add(this.lPhone);
            this.gbStep4.Controls.Add(this.lName);
            this.gbStep4.Controls.Add(this.tbPhone);
            this.gbStep4.Controls.Add(this.tbName);
            this.gbStep4.Controls.Add(this.lStep4Tip);
            this.gbStep4.Enabled = false;
            this.gbStep4.Location = new System.Drawing.Point(8, 223);
            this.gbStep4.Name = "gbStep4";
            this.gbStep4.Size = new System.Drawing.Size(831, 83);
            this.gbStep4.TabIndex = 4;
            this.gbStep4.TabStop = false;
            this.gbStep4.Text = "Шаг 4:";
            // 
            // lPhone
            // 
            this.lPhone.AutoSize = true;
            this.lPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lPhone.Location = new System.Drawing.Point(651, 26);
            this.lPhone.Name = "lPhone";
            this.lPhone.Size = new System.Drawing.Size(79, 20);
            this.lPhone.TabIndex = 5;
            this.lPhone.Text = "Телефон";
            // 
            // lName
            // 
            this.lName.AutoSize = true;
            this.lName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lName.Location = new System.Drawing.Point(436, 28);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(81, 20);
            this.lName.TabIndex = 4;
            this.lName.Text = "Фамилия";
            // 
            // tbPhone
            // 
            this.tbPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbPhone.Location = new System.Drawing.Point(655, 49);
            this.tbPhone.Name = "tbPhone";
            this.tbPhone.Size = new System.Drawing.Size(170, 26);
            this.tbPhone.TabIndex = 3;
            this.tbPhone.TextChanged += new System.EventHandler(this.tbPhone_TextChanged);
            // 
            // tbName
            // 
            this.tbName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbName.Location = new System.Drawing.Point(440, 49);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(170, 26);
            this.tbName.TabIndex = 2;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // lStep4Tip
            // 
            this.lStep4Tip.AutoSize = true;
            this.lStep4Tip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lStep4Tip.Location = new System.Drawing.Point(6, 28);
            this.lStep4Tip.Name = "lStep4Tip";
            this.lStep4Tip.Size = new System.Drawing.Size(284, 40);
            this.lStep4Tip.TabIndex = 0;
            this.lStep4Tip.Text = "Укажите Вашу фамилию и телефон\r\nдля обратной связи";
            // 
            // gbStep5
            // 
            this.gbStep5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbStep5.Controls.Add(this.tbStep5);
            this.gbStep5.Controls.Add(this.lStep5Tip);
            this.gbStep5.Enabled = false;
            this.gbStep5.Location = new System.Drawing.Point(8, 312);
            this.gbStep5.Name = "gbStep5";
            this.gbStep5.Size = new System.Drawing.Size(831, 126);
            this.gbStep5.TabIndex = 3;
            this.gbStep5.TabStop = false;
            this.gbStep5.Text = "Шаг 5:";
            // 
            // tbStep5
            // 
            this.tbStep5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStep5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbStep5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbStep5.Location = new System.Drawing.Point(10, 51);
            this.tbStep5.Multiline = true;
            this.tbStep5.Name = "tbStep5";
            this.tbStep5.Size = new System.Drawing.Size(815, 69);
            this.tbStep5.TabIndex = 1;
            this.tbStep5.Leave += new System.EventHandler(this.tbStep4_Leave);
            this.tbStep5.Enter += new System.EventHandler(this.tbStep4_Enter);
            this.tbStep5.EnabledChanged += new System.EventHandler(this.tbStep4_EnabledChanged);
            // 
            // lStep5Tip
            // 
            this.lStep5Tip.AutoSize = true;
            this.lStep5Tip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lStep5Tip.Location = new System.Drawing.Point(6, 28);
            this.lStep5Tip.Name = "lStep5Tip";
            this.lStep5Tip.Size = new System.Drawing.Size(458, 20);
            this.lStep5Tip.TabIndex = 0;
            this.lStep5Tip.Text = "Укажите дополнительные сведения, если это необходимо";
            // 
            // lSendClaim
            // 
            this.lSendClaim.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lSendClaim.AutoSize = true;
            this.lSendClaim.Location = new System.Drawing.Point(176, 441);
            this.lSendClaim.Name = "lSendClaim";
            this.lSendClaim.Size = new System.Drawing.Size(490, 26);
            this.lSendClaim.TabIndex = 4;
            this.lSendClaim.Text = "Завершить формирование и отправить заявку";
            this.lSendClaim.MouseLeave += new System.EventHandler(this.lSendClaim_MouseLeave);
            this.lSendClaim.Click += new System.EventHandler(this.lSendClaim_Click);
            this.lSendClaim.MouseEnter += new System.EventHandler(this.lSendClaim_MouseEnter);
            // 
            // gbStep3
            // 
            this.gbStep3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbStep3.Controls.Add(this.cbStep3);
            this.gbStep3.Controls.Add(this.lStep3Tip);
            this.gbStep3.Enabled = false;
            this.gbStep3.Location = new System.Drawing.Point(8, 154);
            this.gbStep3.Name = "gbStep3";
            this.gbStep3.Size = new System.Drawing.Size(831, 63);
            this.gbStep3.TabIndex = 3;
            this.gbStep3.TabStop = false;
            this.gbStep3.Text = "Шаг 3:";
            // 
            // cbStep3
            // 
            this.cbStep3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbStep3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbStep3.FormattingEnabled = true;
            this.cbStep3.Location = new System.Drawing.Point(440, 21);
            this.cbStep3.Name = "cbStep3";
            this.cbStep3.Size = new System.Drawing.Size(385, 28);
            this.cbStep3.TabIndex = 1;
            this.cbStep3.SelectedIndexChanged += new System.EventHandler(this.cbStep3_SelectedIndexChanged);
            this.cbStep3.TextChanged += new System.EventHandler(this.cbStep3_TextChanged);
            // 
            // lStep3Tip
            // 
            this.lStep3Tip.AutoSize = true;
            this.lStep3Tip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lStep3Tip.Location = new System.Drawing.Point(6, 28);
            this.lStep3Tip.Name = "lStep3Tip";
            this.lStep3Tip.Size = new System.Drawing.Size(216, 20);
            this.lStep3Tip.TabIndex = 0;
            this.lStep3Tip.Text = "Укажите номер помещения";
            // 
            // gbStep2
            // 
            this.gbStep2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbStep2.Controls.Add(this.cbStep2);
            this.gbStep2.Controls.Add(this.lStep2Tip);
            this.gbStep2.Enabled = false;
            this.gbStep2.Location = new System.Drawing.Point(8, 85);
            this.gbStep2.Name = "gbStep2";
            this.gbStep2.Size = new System.Drawing.Size(831, 63);
            this.gbStep2.TabIndex = 2;
            this.gbStep2.TabStop = false;
            this.gbStep2.Text = "Шаг 2:";
            // 
            // cbStep2
            // 
            this.cbStep2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbStep2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbStep2.FormattingEnabled = true;
            this.cbStep2.Location = new System.Drawing.Point(440, 21);
            this.cbStep2.Name = "cbStep2";
            this.cbStep2.Size = new System.Drawing.Size(385, 28);
            this.cbStep2.TabIndex = 1;
            this.cbStep2.SelectedIndexChanged += new System.EventHandler(this.cbStep2_SelectedIndexChanged);
            this.cbStep2.TextChanged += new System.EventHandler(this.cbStep2_TextChanged);
            // 
            // lStep2Tip
            // 
            this.lStep2Tip.AutoSize = true;
            this.lStep2Tip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lStep2Tip.Location = new System.Drawing.Point(6, 28);
            this.lStep2Tip.Name = "lStep2Tip";
            this.lStep2Tip.Size = new System.Drawing.Size(271, 20);
            this.lStep2Tip.TabIndex = 0;
            this.lStep2Tip.Text = "Укажите оборудование/программу";
            // 
            // gbStep1
            // 
            this.gbStep1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbStep1.Controls.Add(this.cbStep1);
            this.gbStep1.Controls.Add(this.lStep1Tip);
            this.gbStep1.Location = new System.Drawing.Point(8, 15);
            this.gbStep1.Name = "gbStep1";
            this.gbStep1.Size = new System.Drawing.Size(831, 64);
            this.gbStep1.TabIndex = 1;
            this.gbStep1.TabStop = false;
            this.gbStep1.Text = "Шаг 1:";
            // 
            // cbStep1
            // 
            this.cbStep1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbStep1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbStep1.FormattingEnabled = true;
            this.cbStep1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbStep1.Items.AddRange(new object[] {
            "Причину неисправности установить не удалось",
            "Не работает оборудование",
            "Не работает программное обеспечение (ПО)",
            "Требуется настройка оборудования",
            "Требуется настройка программного обеспечения (ПО)",
            "Иное"});
            this.cbStep1.Location = new System.Drawing.Point(440, 21);
            this.cbStep1.Name = "cbStep1";
            this.cbStep1.Size = new System.Drawing.Size(385, 28);
            this.cbStep1.TabIndex = 1;
            this.cbStep1.SelectedIndexChanged += new System.EventHandler(this.cbStep1_SelectedIndexChanged);
            this.cbStep1.TextChanged += new System.EventHandler(this.cbStep1_TextChanged);
            // 
            // lStep1Tip
            // 
            this.lStep1Tip.AutoSize = true;
            this.lStep1Tip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lStep1Tip.Location = new System.Drawing.Point(6, 28);
            this.lStep1Tip.Name = "lStep1Tip";
            this.lStep1Tip.Size = new System.Drawing.Size(422, 20);
            this.lStep1Tip.TabIndex = 0;
            this.lStep1Tip.Text = "Укажите неисправность или причину создания заявки";
            // 
            // tbActiveClaims
            // 
            this.tbActiveClaims.Controls.Add(this.lNoActiveClaims);
            this.tbActiveClaims.Controls.Add(this.dgvActiveClaims);
            this.tbActiveClaims.Location = new System.Drawing.Point(4, 54);
            this.tbActiveClaims.Name = "tbActiveClaims";
            this.tbActiveClaims.Padding = new System.Windows.Forms.Padding(3);
            this.tbActiveClaims.Size = new System.Drawing.Size(842, 505);
            this.tbActiveClaims.TabIndex = 1;
            this.tbActiveClaims.Text = "Активные заявки";
            this.tbActiveClaims.UseVisualStyleBackColor = true;
            // 
            // dgvActiveClaims
            // 
            this.dgvActiveClaims.AllowUserToAddRows = false;
            this.dgvActiveClaims.AllowUserToDeleteRows = false;
            this.dgvActiveClaims.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvActiveClaims.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvActiveClaims.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvActiveClaims.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvActiveClaims.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvActiveClaims.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvActiveClaims.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvActiveClaims.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvActiveClaims.Location = new System.Drawing.Point(3, 3);
            this.dgvActiveClaims.MultiSelect = false;
            this.dgvActiveClaims.Name = "dgvActiveClaims";
            this.dgvActiveClaims.ReadOnly = true;
            this.dgvActiveClaims.RowHeadersVisible = false;
            this.dgvActiveClaims.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvActiveClaims.Size = new System.Drawing.Size(836, 499);
            this.dgvActiveClaims.TabIndex = 0;
            this.dgvActiveClaims.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvActiveClaims_MouseClick);
            this.dgvActiveClaims.Leave += new System.EventHandler(this.dgvActiveClaims_Leave);
            // 
            // activeClaimsContextMenu
            // 
            this.activeClaimsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteClaim});
            this.activeClaimsContextMenu.Name = "activeClaimsContextMenu";
            this.activeClaimsContextMenu.Size = new System.Drawing.Size(167, 26);
            // 
            // deleteClaim
            // 
            this.deleteClaim.Name = "deleteClaim";
            this.deleteClaim.Size = new System.Drawing.Size(166, 22);
            this.deleteClaim.Text = "Отменить заявку";
            this.deleteClaim.Click += new System.EventHandler(this.deleteClaim_Click);
            // 
            // lNoActiveClaims
            // 
            this.lNoActiveClaims.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lNoActiveClaims.Location = new System.Drawing.Point(222, 218);
            this.lNoActiveClaims.Name = "lNoActiveClaims";
            this.lNoActiveClaims.Size = new System.Drawing.Size(399, 68);
            this.lNoActiveClaims.TabIndex = 1;
            this.lNoActiveClaims.Text = "Активные заявки отсутствуют";
            this.lNoActiveClaims.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 591);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(826, 618);
            this.Name = "MainWindow";
            this.Text = "Модуль заявителя";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tbClaimCreation.ResumeLayout(false);
            this.tbClaimCreation.PerformLayout();
            this.gbStep4.ResumeLayout(false);
            this.gbStep4.PerformLayout();
            this.gbStep5.ResumeLayout(false);
            this.gbStep5.PerformLayout();
            this.gbStep3.ResumeLayout(false);
            this.gbStep3.PerformLayout();
            this.gbStep2.ResumeLayout(false);
            this.gbStep2.PerformLayout();
            this.gbStep1.ResumeLayout(false);
            this.gbStep1.PerformLayout();
            this.tbActiveClaims.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvActiveClaims)).EndInit();
            this.activeClaimsContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslOSVer;
        private System.Windows.Forms.ToolStripStatusLabel tsslHostName;
        private System.Windows.Forms.ToolStripStatusLabel tsslHostIP;
        private System.Windows.Forms.ToolStripStatusLabel tssUserName;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbClaimCreation;
        private System.Windows.Forms.TabPage tbActiveClaims;
        private System.Windows.Forms.GroupBox gbStep1;
        private System.Windows.Forms.Label lStep1Tip;
        private System.Windows.Forms.ComboBox cbStep1;
        private System.Windows.Forms.GroupBox gbStep2;
        private System.Windows.Forms.ComboBox cbStep2;
        private System.Windows.Forms.Label lStep2Tip;
        private System.Windows.Forms.GroupBox gbStep3;
        private System.Windows.Forms.Label lStep5Tip;
        private System.Windows.Forms.TextBox tbStep5;
        private System.Windows.Forms.Label lSendClaim;
        private System.Windows.Forms.GroupBox gbStep5;
        private System.Windows.Forms.ComboBox cbStep3;
        private System.Windows.Forms.Label lStep3Tip;
        private System.Windows.Forms.ContextMenuStrip activeClaimsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteClaim;
        private System.Windows.Forms.GroupBox gbStep4;
        private System.Windows.Forms.Label lStep4Tip;
        private System.Windows.Forms.Label lInfo;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lPhone;
        private System.Windows.Forms.Label lName;
        private System.Windows.Forms.TextBox tbPhone;
        public System.Windows.Forms.DataGridView dgvActiveClaims;
        private System.Windows.Forms.Label lNoActiveClaims;

    }
}

