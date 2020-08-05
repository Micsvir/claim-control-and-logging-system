namespace AdministratorModule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.gbControls = new System.Windows.Forms.GroupBox();
            this.tbSelector = new System.Windows.Forms.TabControl();
            this.tpUsers = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgvUsersParams = new System.Windows.Forms.DataGridView();
            this.bChangeUserPswd = new System.Windows.Forms.Button();
            this.bChangeUserData = new System.Windows.Forms.Button();
            this.bDeleteUser = new System.Windows.Forms.Button();
            this.bAddUser = new System.Windows.Forms.Button();
            this.bGetUsers = new System.Windows.Forms.Button();
            this.tpGroups = new System.Windows.Forms.TabPage();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.dgvGroupsParams = new System.Windows.Forms.DataGridView();
            this.bChangeGroupData = new System.Windows.Forms.Button();
            this.bDeleteGroup = new System.Windows.Forms.Button();
            this.bAddGroup = new System.Windows.Forms.Button();
            this.bGetGroups = new System.Windows.Forms.Button();
            this.tpClaims = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.dgvClaimsParams = new System.Windows.Forms.DataGridView();
            this.bChangeClaimData = new System.Windows.Forms.Button();
            this.bDeleteClaim = new System.Windows.Forms.Button();
            this.bGetClaims = new System.Windows.Forms.Button();
            this.gbData = new System.Windows.Forms.GroupBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbSelectUser = new System.Windows.Forms.ToolStripButton();
            this.tsbExport = new System.Windows.Forms.ToolStripButton();
            this.tsbPswdChange = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lActiveUser = new System.Windows.Forms.Label();
            this.lStatusCaption = new System.Windows.Forms.Label();
            this.lActiveUserCaption = new System.Windows.Forms.Label();
            this.lStatus = new System.Windows.Forms.Label();
            this.dgvDataMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmEditRow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDeleteRow = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsUsersParams = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteRowFromUsersParams = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsGroupsParams = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteRowFromGroupsParams = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsClaimsParams = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteRowFromClaimsParams = new System.Windows.Forms.ToolStripMenuItem();
            this.gbControls.SuspendLayout();
            this.tbSelector.SuspendLayout();
            this.tpUsers.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsersParams)).BeginInit();
            this.tpGroups.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGroupsParams)).BeginInit();
            this.tpClaims.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClaimsParams)).BeginInit();
            this.gbData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.dgvDataMenuStrip.SuspendLayout();
            this.cmsUsersParams.SuspendLayout();
            this.cmsGroupsParams.SuspendLayout();
            this.cmsClaimsParams.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbControls
            // 
            this.gbControls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbControls.Controls.Add(this.tbSelector);
            this.gbControls.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbControls.Location = new System.Drawing.Point(0, 71);
            this.gbControls.Name = "gbControls";
            this.gbControls.Size = new System.Drawing.Size(1006, 227);
            this.gbControls.TabIndex = 0;
            this.gbControls.TabStop = false;
            this.gbControls.Text = "Управление";
            // 
            // tbSelector
            // 
            this.tbSelector.Controls.Add(this.tpUsers);
            this.tbSelector.Controls.Add(this.tpGroups);
            this.tbSelector.Controls.Add(this.tpClaims);
            this.tbSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSelector.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbSelector.Location = new System.Drawing.Point(3, 27);
            this.tbSelector.Name = "tbSelector";
            this.tbSelector.SelectedIndex = 0;
            this.tbSelector.Size = new System.Drawing.Size(1000, 197);
            this.tbSelector.TabIndex = 0;
            this.tbSelector.SelectedIndexChanged += new System.EventHandler(this.tbSelector_SelectedIndexChanged);
            // 
            // tpUsers
            // 
            this.tpUsers.Controls.Add(this.splitContainer2);
            this.tpUsers.Location = new System.Drawing.Point(4, 29);
            this.tpUsers.Name = "tpUsers";
            this.tpUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tpUsers.Size = new System.Drawing.Size(992, 164);
            this.tpUsers.TabIndex = 0;
            this.tpUsers.Text = "Пользователи";
            this.tpUsers.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgvUsersParams);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.bChangeUserPswd);
            this.splitContainer2.Panel2.Controls.Add(this.bChangeUserData);
            this.splitContainer2.Panel2.Controls.Add(this.bDeleteUser);
            this.splitContainer2.Panel2.Controls.Add(this.bAddUser);
            this.splitContainer2.Panel2.Controls.Add(this.bGetUsers);
            this.splitContainer2.Size = new System.Drawing.Size(986, 158);
            this.splitContainer2.SplitterDistance = 630;
            this.splitContainer2.TabIndex = 14;
            // 
            // dgvUsersParams
            // 
            this.dgvUsersParams.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvUsersParams.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvUsersParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsersParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsersParams.Location = new System.Drawing.Point(0, 0);
            this.dgvUsersParams.Name = "dgvUsersParams";
            this.dgvUsersParams.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsersParams.Size = new System.Drawing.Size(626, 154);
            this.dgvUsersParams.TabIndex = 13;
            this.dgvUsersParams.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvUsersParams_MouseClick);
            // 
            // bChangeUserPswd
            // 
            this.bChangeUserPswd.Dock = System.Windows.Forms.DockStyle.Top;
            this.bChangeUserPswd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bChangeUserPswd.Location = new System.Drawing.Point(0, 124);
            this.bChangeUserPswd.Name = "bChangeUserPswd";
            this.bChangeUserPswd.Size = new System.Drawing.Size(348, 31);
            this.bChangeUserPswd.TabIndex = 4;
            this.bChangeUserPswd.Text = "Сбросить пароль";
            this.bChangeUserPswd.UseVisualStyleBackColor = true;
            this.bChangeUserPswd.Click += new System.EventHandler(this.bChangeUserPswd_Click);
            // 
            // bChangeUserData
            // 
            this.bChangeUserData.Dock = System.Windows.Forms.DockStyle.Top;
            this.bChangeUserData.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bChangeUserData.Location = new System.Drawing.Point(0, 93);
            this.bChangeUserData.Name = "bChangeUserData";
            this.bChangeUserData.Size = new System.Drawing.Size(348, 31);
            this.bChangeUserData.TabIndex = 3;
            this.bChangeUserData.Text = "Изменить данные пользователя";
            this.bChangeUserData.UseVisualStyleBackColor = true;
            this.bChangeUserData.Click += new System.EventHandler(this.bChangeUserData_Click);
            // 
            // bDeleteUser
            // 
            this.bDeleteUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.bDeleteUser.Enabled = false;
            this.bDeleteUser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bDeleteUser.Location = new System.Drawing.Point(0, 62);
            this.bDeleteUser.Name = "bDeleteUser";
            this.bDeleteUser.Size = new System.Drawing.Size(348, 31);
            this.bDeleteUser.TabIndex = 2;
            this.bDeleteUser.Text = "Удалить пользователя";
            this.bDeleteUser.UseVisualStyleBackColor = true;
            // 
            // bAddUser
            // 
            this.bAddUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.bAddUser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bAddUser.Location = new System.Drawing.Point(0, 31);
            this.bAddUser.Name = "bAddUser";
            this.bAddUser.Size = new System.Drawing.Size(348, 31);
            this.bAddUser.TabIndex = 1;
            this.bAddUser.Text = "Добавить пользователя";
            this.bAddUser.UseVisualStyleBackColor = true;
            this.bAddUser.Click += new System.EventHandler(this.bAddUser_Click);
            // 
            // bGetUsers
            // 
            this.bGetUsers.Dock = System.Windows.Forms.DockStyle.Top;
            this.bGetUsers.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bGetUsers.Location = new System.Drawing.Point(0, 0);
            this.bGetUsers.Name = "bGetUsers";
            this.bGetUsers.Size = new System.Drawing.Size(348, 31);
            this.bGetUsers.TabIndex = 0;
            this.bGetUsers.Text = "Загрузить данные";
            this.bGetUsers.UseVisualStyleBackColor = true;
            this.bGetUsers.Click += new System.EventHandler(this.bGetUsers_Click);
            // 
            // tpGroups
            // 
            this.tpGroups.Controls.Add(this.splitContainer4);
            this.tpGroups.Location = new System.Drawing.Point(4, 29);
            this.tpGroups.Name = "tpGroups";
            this.tpGroups.Padding = new System.Windows.Forms.Padding(3);
            this.tpGroups.Size = new System.Drawing.Size(992, 164);
            this.tpGroups.TabIndex = 3;
            this.tpGroups.Text = "Группы";
            this.tpGroups.UseVisualStyleBackColor = true;
            // 
            // splitContainer4
            // 
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(3, 3);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.dgvGroupsParams);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.bChangeGroupData);
            this.splitContainer4.Panel2.Controls.Add(this.bDeleteGroup);
            this.splitContainer4.Panel2.Controls.Add(this.bAddGroup);
            this.splitContainer4.Panel2.Controls.Add(this.bGetGroups);
            this.splitContainer4.Size = new System.Drawing.Size(986, 158);
            this.splitContainer4.SplitterDistance = 625;
            this.splitContainer4.TabIndex = 0;
            // 
            // dgvGroupsParams
            // 
            this.dgvGroupsParams.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvGroupsParams.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvGroupsParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGroupsParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGroupsParams.Location = new System.Drawing.Point(0, 0);
            this.dgvGroupsParams.Name = "dgvGroupsParams";
            this.dgvGroupsParams.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGroupsParams.Size = new System.Drawing.Size(621, 154);
            this.dgvGroupsParams.TabIndex = 14;
            this.dgvGroupsParams.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvGroupsParams_MouseClick);
            // 
            // bChangeGroupData
            // 
            this.bChangeGroupData.Dock = System.Windows.Forms.DockStyle.Top;
            this.bChangeGroupData.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bChangeGroupData.Location = new System.Drawing.Point(0, 93);
            this.bChangeGroupData.Name = "bChangeGroupData";
            this.bChangeGroupData.Size = new System.Drawing.Size(353, 31);
            this.bChangeGroupData.TabIndex = 4;
            this.bChangeGroupData.Text = "Изменить группу";
            this.bChangeGroupData.UseVisualStyleBackColor = true;
            this.bChangeGroupData.Click += new System.EventHandler(this.bChangeGroupData_Click);
            // 
            // bDeleteGroup
            // 
            this.bDeleteGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.bDeleteGroup.Enabled = false;
            this.bDeleteGroup.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bDeleteGroup.Location = new System.Drawing.Point(0, 62);
            this.bDeleteGroup.Name = "bDeleteGroup";
            this.bDeleteGroup.Size = new System.Drawing.Size(353, 31);
            this.bDeleteGroup.TabIndex = 3;
            this.bDeleteGroup.Text = "Удалить группу";
            this.bDeleteGroup.UseVisualStyleBackColor = true;
            // 
            // bAddGroup
            // 
            this.bAddGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.bAddGroup.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bAddGroup.Location = new System.Drawing.Point(0, 31);
            this.bAddGroup.Name = "bAddGroup";
            this.bAddGroup.Size = new System.Drawing.Size(353, 31);
            this.bAddGroup.TabIndex = 2;
            this.bAddGroup.Text = "Добавить группу";
            this.bAddGroup.UseVisualStyleBackColor = true;
            this.bAddGroup.Click += new System.EventHandler(this.bAddGroup_Click);
            // 
            // bGetGroups
            // 
            this.bGetGroups.Dock = System.Windows.Forms.DockStyle.Top;
            this.bGetGroups.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bGetGroups.Location = new System.Drawing.Point(0, 0);
            this.bGetGroups.Name = "bGetGroups";
            this.bGetGroups.Size = new System.Drawing.Size(353, 31);
            this.bGetGroups.TabIndex = 1;
            this.bGetGroups.Text = "Загрузить данные";
            this.bGetGroups.UseVisualStyleBackColor = true;
            this.bGetGroups.Click += new System.EventHandler(this.bGetGroups_Click);
            // 
            // tpClaims
            // 
            this.tpClaims.Controls.Add(this.splitContainer3);
            this.tpClaims.Location = new System.Drawing.Point(4, 29);
            this.tpClaims.Name = "tpClaims";
            this.tpClaims.Padding = new System.Windows.Forms.Padding(3);
            this.tpClaims.Size = new System.Drawing.Size(992, 164);
            this.tpClaims.TabIndex = 1;
            this.tpClaims.Text = "Заявки";
            this.tpClaims.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.dgvClaimsParams);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.bChangeClaimData);
            this.splitContainer3.Panel2.Controls.Add(this.bDeleteClaim);
            this.splitContainer3.Panel2.Controls.Add(this.bGetClaims);
            this.splitContainer3.Size = new System.Drawing.Size(986, 158);
            this.splitContainer3.SplitterDistance = 629;
            this.splitContainer3.TabIndex = 0;
            // 
            // dgvClaimsParams
            // 
            this.dgvClaimsParams.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvClaimsParams.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvClaimsParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClaimsParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvClaimsParams.Location = new System.Drawing.Point(0, 0);
            this.dgvClaimsParams.Name = "dgvClaimsParams";
            this.dgvClaimsParams.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClaimsParams.Size = new System.Drawing.Size(625, 154);
            this.dgvClaimsParams.TabIndex = 14;
            this.dgvClaimsParams.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvClaimsParams_MouseClick);
            // 
            // bChangeClaimData
            // 
            this.bChangeClaimData.Dock = System.Windows.Forms.DockStyle.Top;
            this.bChangeClaimData.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bChangeClaimData.Location = new System.Drawing.Point(0, 62);
            this.bChangeClaimData.Name = "bChangeClaimData";
            this.bChangeClaimData.Size = new System.Drawing.Size(349, 31);
            this.bChangeClaimData.TabIndex = 3;
            this.bChangeClaimData.Text = "Изменить заявку";
            this.bChangeClaimData.UseVisualStyleBackColor = true;
            this.bChangeClaimData.Click += new System.EventHandler(this.bChangeClaimData_Click);
            // 
            // bDeleteClaim
            // 
            this.bDeleteClaim.Dock = System.Windows.Forms.DockStyle.Top;
            this.bDeleteClaim.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bDeleteClaim.Location = new System.Drawing.Point(0, 31);
            this.bDeleteClaim.Name = "bDeleteClaim";
            this.bDeleteClaim.Size = new System.Drawing.Size(349, 31);
            this.bDeleteClaim.TabIndex = 2;
            this.bDeleteClaim.Text = "Удалить заявку";
            this.bDeleteClaim.UseVisualStyleBackColor = true;
            this.bDeleteClaim.Click += new System.EventHandler(this.bDeleteClaim_Click);
            // 
            // bGetClaims
            // 
            this.bGetClaims.Dock = System.Windows.Forms.DockStyle.Top;
            this.bGetClaims.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bGetClaims.Location = new System.Drawing.Point(0, 0);
            this.bGetClaims.Name = "bGetClaims";
            this.bGetClaims.Size = new System.Drawing.Size(349, 31);
            this.bGetClaims.TabIndex = 1;
            this.bGetClaims.Text = "Загрузить данные";
            this.bGetClaims.UseVisualStyleBackColor = true;
            this.bGetClaims.Click += new System.EventHandler(this.bGetClaims_Click);
            // 
            // gbData
            // 
            this.gbData.Controls.Add(this.dgvData);
            this.gbData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbData.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbData.Location = new System.Drawing.Point(0, 0);
            this.gbData.Name = "gbData";
            this.gbData.Size = new System.Drawing.Size(1006, 242);
            this.gbData.TabIndex = 1;
            this.gbData.TabStop = false;
            this.gbData.Text = "Данные";
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(3, 27);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(1000, 212);
            this.dgvData.TabIndex = 0;
            this.dgvData.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvData_MouseClick);
            this.dgvData.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvData_MouseDoubleClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSelectUser,
            this.tsbExport,
            this.tsbPswdChange});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1010, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbSelectUser
            // 
            this.tsbSelectUser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSelectUser.Image = ((System.Drawing.Image)(resources.GetObject("tsbSelectUser.Image")));
            this.tsbSelectUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSelectUser.Name = "tsbSelectUser";
            this.tsbSelectUser.Size = new System.Drawing.Size(135, 22);
            this.tsbSelectUser.Text = "Выбор пользователя";
            this.tsbSelectUser.Click += new System.EventHandler(this.tsbSelectUser_Click);
            // 
            // tsbExport
            // 
            this.tsbExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbExport.Image = ((System.Drawing.Image)(resources.GetObject("tsbExport.Image")));
            this.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.Size = new System.Drawing.Size(59, 22);
            this.tsbExport.Text = "Экспорт";
            this.tsbExport.Visible = false;
            // 
            // tsbPswdChange
            // 
            this.tsbPswdChange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbPswdChange.Image = ((System.Drawing.Image)(resources.GetObject("tsbPswdChange.Image")));
            this.tsbPswdChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPswdChange.Name = "tsbPswdChange";
            this.tsbPswdChange.Size = new System.Drawing.Size(95, 22);
            this.tsbPswdChange.Text = "Смена пароля";
            this.tsbPswdChange.Click += new System.EventHandler(this.tsbPswdChange_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lActiveUser);
            this.splitContainer1.Panel1.Controls.Add(this.gbControls);
            this.splitContainer1.Panel1.Controls.Add(this.lStatusCaption);
            this.splitContainer1.Panel1.Controls.Add(this.lActiveUserCaption);
            this.splitContainer1.Panel1.Controls.Add(this.lStatus);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gbData);
            this.splitContainer1.Size = new System.Drawing.Size(1010, 555);
            this.splitContainer1.SplitterDistance = 305;
            this.splitContainer1.TabIndex = 3;
            // 
            // lActiveUser
            // 
            this.lActiveUser.AutoSize = true;
            this.lActiveUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lActiveUser.Location = new System.Drawing.Point(143, 39);
            this.lActiveUser.Name = "lActiveUser";
            this.lActiveUser.Size = new System.Drawing.Size(35, 20);
            this.lActiveUser.TabIndex = 4;
            this.lActiveUser.Text = "N/A";
            // 
            // lStatusCaption
            // 
            this.lStatusCaption.AutoSize = true;
            this.lStatusCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lStatusCaption.Location = new System.Drawing.Point(12, 10);
            this.lStatusCaption.Name = "lStatusCaption";
            this.lStatusCaption.Size = new System.Drawing.Size(66, 20);
            this.lStatusCaption.TabIndex = 1;
            this.lStatusCaption.Text = "Статус:";
            // 
            // lActiveUserCaption
            // 
            this.lActiveUserCaption.AutoSize = true;
            this.lActiveUserCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lActiveUserCaption.Location = new System.Drawing.Point(12, 39);
            this.lActiveUserCaption.Name = "lActiveUserCaption";
            this.lActiveUserCaption.Size = new System.Drawing.Size(125, 20);
            this.lActiveUserCaption.TabIndex = 3;
            this.lActiveUserCaption.Text = "Пользователь:";
            // 
            // lStatus
            // 
            this.lStatus.AutoSize = true;
            this.lStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lStatus.ForeColor = System.Drawing.Color.Red;
            this.lStatus.Location = new System.Drawing.Point(143, 10);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(148, 20);
            this.lStatus.TabIndex = 2;
            this.lStatus.Text = "Вход не выполнен";
            // 
            // dgvDataMenuStrip
            // 
            this.dgvDataMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmEditRow,
            this.tsmDeleteRow});
            this.dgvDataMenuStrip.Name = "dgvDataMenuStrip";
            this.dgvDataMenuStrip.Size = new System.Drawing.Size(169, 48);
            // 
            // tsmEditRow
            // 
            this.tsmEditRow.Name = "tsmEditRow";
            this.tsmEditRow.Size = new System.Drawing.Size(168, 22);
            this.tsmEditRow.Text = "Изменить запись";
            this.tsmEditRow.Click += new System.EventHandler(this.tsmEditRow_Click);
            // 
            // tsmDeleteRow
            // 
            this.tsmDeleteRow.Name = "tsmDeleteRow";
            this.tsmDeleteRow.Size = new System.Drawing.Size(168, 22);
            this.tsmDeleteRow.Text = "Удалить запись";
            this.tsmDeleteRow.Click += new System.EventHandler(this.tsmDeleteRow_Click);
            // 
            // cmsUsersParams
            // 
            this.cmsUsersParams.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteRowFromUsersParams});
            this.cmsUsersParams.Name = "cmsUsersParams";
            this.cmsUsersParams.Size = new System.Drawing.Size(159, 26);
            // 
            // DeleteRowFromUsersParams
            // 
            this.DeleteRowFromUsersParams.Name = "DeleteRowFromUsersParams";
            this.DeleteRowFromUsersParams.Size = new System.Drawing.Size(158, 22);
            this.DeleteRowFromUsersParams.Text = "Удалить строку";
            this.DeleteRowFromUsersParams.Click += new System.EventHandler(this.DeleteRow_Click);
            // 
            // cmsGroupsParams
            // 
            this.cmsGroupsParams.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteRowFromGroupsParams});
            this.cmsGroupsParams.Name = "cmsGroupsParams";
            this.cmsGroupsParams.Size = new System.Drawing.Size(159, 26);
            // 
            // deleteRowFromGroupsParams
            // 
            this.deleteRowFromGroupsParams.Name = "deleteRowFromGroupsParams";
            this.deleteRowFromGroupsParams.Size = new System.Drawing.Size(158, 22);
            this.deleteRowFromGroupsParams.Text = "Удалить строку";
            this.deleteRowFromGroupsParams.Click += new System.EventHandler(this.deleteRowFromGroupsParams_Click);
            // 
            // cmsClaimsParams
            // 
            this.cmsClaimsParams.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteRowFromClaimsParams});
            this.cmsClaimsParams.Name = "cmsClaimsParams";
            this.cmsClaimsParams.Size = new System.Drawing.Size(159, 26);
            // 
            // DeleteRowFromClaimsParams
            // 
            this.DeleteRowFromClaimsParams.Name = "DeleteRowFromClaimsParams";
            this.DeleteRowFromClaimsParams.Size = new System.Drawing.Size(158, 22);
            this.DeleteRowFromClaimsParams.Text = "Удалить строку";
            this.DeleteRowFromClaimsParams.Click += new System.EventHandler(this.DeleteRowFromClaimsParams_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 580);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1018, 607);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Модуль администратора";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            this.gbControls.ResumeLayout(false);
            this.tbSelector.ResumeLayout(false);
            this.tpUsers.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsersParams)).EndInit();
            this.tpGroups.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGroupsParams)).EndInit();
            this.tpClaims.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClaimsParams)).EndInit();
            this.gbData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.dgvDataMenuStrip.ResumeLayout(false);
            this.cmsUsersParams.ResumeLayout(false);
            this.cmsGroupsParams.ResumeLayout(false);
            this.cmsClaimsParams.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbControls;
        private System.Windows.Forms.GroupBox gbData;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripButton tsbExport;
        private System.Windows.Forms.ToolStripButton tsbPswdChange;
        private System.Windows.Forms.ToolStripButton tsbSelectUser;
        private System.Windows.Forms.Label lActiveUser;
        private System.Windows.Forms.Label lActiveUserCaption;
        private System.Windows.Forms.Label lStatus;
        private System.Windows.Forms.Label lStatusCaption;
        private System.Windows.Forms.TabControl tbSelector;
        private System.Windows.Forms.TabPage tpUsers;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dgvUsersParams;
        private System.Windows.Forms.Button bChangeUserData;
        private System.Windows.Forms.Button bDeleteUser;
        private System.Windows.Forms.Button bAddUser;
        private System.Windows.Forms.Button bGetUsers;
        private System.Windows.Forms.TabPage tpGroups;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.DataGridView dgvGroupsParams;
        private System.Windows.Forms.Button bChangeGroupData;
        private System.Windows.Forms.Button bDeleteGroup;
        private System.Windows.Forms.Button bAddGroup;
        private System.Windows.Forms.Button bGetGroups;
        private System.Windows.Forms.TabPage tpClaims;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DataGridView dgvClaimsParams;
        private System.Windows.Forms.Button bChangeClaimData;
        private System.Windows.Forms.Button bDeleteClaim;
        private System.Windows.Forms.Button bGetClaims;
        public System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button bChangeUserPswd;
        private System.Windows.Forms.ContextMenuStrip dgvDataMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmEditRow;
        private System.Windows.Forms.ToolStripMenuItem tsmDeleteRow;
        private System.Windows.Forms.ContextMenuStrip cmsUsersParams;
        private System.Windows.Forms.ToolStripMenuItem DeleteRowFromUsersParams;
        private System.Windows.Forms.ContextMenuStrip cmsGroupsParams;
        private System.Windows.Forms.ToolStripMenuItem deleteRowFromGroupsParams;
        private System.Windows.Forms.ContextMenuStrip cmsClaimsParams;
        private System.Windows.Forms.ToolStripMenuItem DeleteRowFromClaimsParams;
    }
}

