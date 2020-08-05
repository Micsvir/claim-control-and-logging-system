namespace ExecutorModule
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbSelectUser = new System.Windows.Forms.ToolStripButton();
            this.tsbSelectExecutor = new System.Windows.Forms.ToolStripButton();
            this.tsbEditClaim = new System.Windows.Forms.ToolStripButton();
            this.tsbStartExecuteTheClaim = new System.Windows.Forms.ToolStripButton();
            this.tsbCloseTheClaim = new System.Windows.Forms.ToolStripButton();
            this.tsbShowReport = new System.Windows.Forms.ToolStripButton();
            this.tsbChangePswd = new System.Windows.Forms.ToolStripButton();
            this.gbUserInfo = new System.Windows.Forms.GroupBox();
            this.gbUserActiveClaims = new System.Windows.Forms.GroupBox();
            this.lvUserAcitveClaims = new System.Windows.Forms.ListView();
            this.lUserName = new System.Windows.Forms.Label();
            this.gbActiveClaims = new System.Windows.Forms.GroupBox();
            this.lvGroupActiveClaims = new System.Windows.Forms.ListView();
            this.chDate = new System.Windows.Forms.ColumnHeader();
            this.chTime = new System.Windows.Forms.ColumnHeader();
            this.chInfo = new System.Windows.Forms.ColumnHeader();
            this.chStatus = new System.Windows.Forms.ColumnHeader();
            this.gbGroupInfo = new System.Windows.Forms.GroupBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.gbGroupMembers = new System.Windows.Forms.GroupBox();
            this.lvGroupMembers = new System.Windows.Forms.ListView();
            this.chID = new System.Windows.Forms.ColumnHeader();
            this.chGroupMemberLastName = new System.Windows.Forms.ColumnHeader();
            this.chGroupMemberFirstName = new System.Windows.Forms.ColumnHeader();
            this.chActiveClaims = new System.Windows.Forms.ColumnHeader();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cmsGroupActiveClaimsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setExecutor = new System.Windows.Forms.ToolStripMenuItem();
            this.editGroupClaim = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsUserActiveClaimsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startToExecute = new System.Windows.Forms.ToolStripMenuItem();
            this.editUserClaim = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.closeTheClaim = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.gbUserInfo.SuspendLayout();
            this.gbUserActiveClaims.SuspendLayout();
            this.gbActiveClaims.SuspendLayout();
            this.gbGroupInfo.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.gbGroupMembers.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.cmsGroupActiveClaimsMenu.SuspendLayout();
            this.cmsUserActiveClaimsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSelectUser,
            this.tsbSelectExecutor,
            this.tsbEditClaim,
            this.tsbStartExecuteTheClaim,
            this.tsbCloseTheClaim,
            this.tsbShowReport,
            this.tsbChangePswd});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(737, 36);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbSelectUser
            // 
            this.tsbSelectUser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSelectUser.Image = ((System.Drawing.Image)(resources.GetObject("tsbSelectUser.Image")));
            this.tsbSelectUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSelectUser.Name = "tsbSelectUser";
            this.tsbSelectUser.Size = new System.Drawing.Size(148, 33);
            this.tsbSelectUser.Text = "Выбрать пользователя";
            this.tsbSelectUser.Click += new System.EventHandler(this.tsbSelectUser_Click);
            // 
            // tsbSelectExecutor
            // 
            this.tsbSelectExecutor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSelectExecutor.Image = ((System.Drawing.Image)(resources.GetObject("tsbSelectExecutor.Image")));
            this.tsbSelectExecutor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSelectExecutor.Name = "tsbSelectExecutor";
            this.tsbSelectExecutor.Size = new System.Drawing.Size(153, 33);
            this.tsbSelectExecutor.Text = "Назначить исполнителя";
            this.tsbSelectExecutor.Click += new System.EventHandler(this.tsbSelectExecutor_Click);
            // 
            // tsbEditClaim
            // 
            this.tsbEditClaim.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbEditClaim.Image = ((System.Drawing.Image)(resources.GetObject("tsbEditClaim.Image")));
            this.tsbEditClaim.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEditClaim.Name = "tsbEditClaim";
            this.tsbEditClaim.Size = new System.Drawing.Size(111, 33);
            this.tsbEditClaim.Text = "Изменить заявку";
            this.tsbEditClaim.Click += new System.EventHandler(this.tsbEditClaim_Click);
            // 
            // tsbStartExecuteTheClaim
            // 
            this.tsbStartExecuteTheClaim.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbStartExecuteTheClaim.Image = ((System.Drawing.Image)(resources.GetObject("tsbStartExecuteTheClaim.Image")));
            this.tsbStartExecuteTheClaim.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStartExecuteTheClaim.Name = "tsbStartExecuteTheClaim";
            this.tsbStartExecuteTheClaim.Size = new System.Drawing.Size(168, 33);
            this.tsbStartExecuteTheClaim.Text = "Приступить к выполнению";
            this.tsbStartExecuteTheClaim.Click += new System.EventHandler(this.tsbStartExecuteTheClaim_Click);
            // 
            // tsbCloseTheClaim
            // 
            this.tsbCloseTheClaim.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbCloseTheClaim.Image = ((System.Drawing.Image)(resources.GetObject("tsbCloseTheClaim.Image")));
            this.tsbCloseTheClaim.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCloseTheClaim.Name = "tsbCloseTheClaim";
            this.tsbCloseTheClaim.Size = new System.Drawing.Size(104, 33);
            this.tsbCloseTheClaim.Text = "Закрыть заявку";
            this.tsbCloseTheClaim.Click += new System.EventHandler(this.tsbCloseTheClaim_Click);
            // 
            // tsbShowReport
            // 
            this.tsbShowReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbShowReport.Image = ((System.Drawing.Image)(resources.GetObject("tsbShowReport.Image")));
            this.tsbShowReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowReport.Name = "tsbShowReport";
            this.tsbShowReport.Size = new System.Drawing.Size(118, 20);
            this.tsbShowReport.Text = "Посмотреть отчет";
            this.tsbShowReport.Click += new System.EventHandler(this.tsbShowReport_Click);
            // 
            // tsbChangePswd
            // 
            this.tsbChangePswd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbChangePswd.Image = ((System.Drawing.Image)(resources.GetObject("tsbChangePswd.Image")));
            this.tsbChangePswd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbChangePswd.Name = "tsbChangePswd";
            this.tsbChangePswd.Size = new System.Drawing.Size(108, 20);
            this.tsbChangePswd.Text = "Сменить пароль";
            this.tsbChangePswd.Click += new System.EventHandler(this.tsbChangePswd_Click);
            // 
            // gbUserInfo
            // 
            this.gbUserInfo.Controls.Add(this.gbUserActiveClaims);
            this.gbUserInfo.Controls.Add(this.lUserName);
            this.gbUserInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbUserInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbUserInfo.Location = new System.Drawing.Point(0, 0);
            this.gbUserInfo.Name = "gbUserInfo";
            this.gbUserInfo.Size = new System.Drawing.Size(737, 180);
            this.gbUserInfo.TabIndex = 0;
            this.gbUserInfo.TabStop = false;
            this.gbUserInfo.Text = "Исполнитель";
            // 
            // gbUserActiveClaims
            // 
            this.gbUserActiveClaims.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbUserActiveClaims.Controls.Add(this.lvUserAcitveClaims);
            this.gbUserActiveClaims.Location = new System.Drawing.Point(3, 50);
            this.gbUserActiveClaims.Name = "gbUserActiveClaims";
            this.gbUserActiveClaims.Size = new System.Drawing.Size(731, 124);
            this.gbUserActiveClaims.TabIndex = 2;
            this.gbUserActiveClaims.TabStop = false;
            this.gbUserActiveClaims.Text = "Активные заявки исполнителя";
            // 
            // lvUserAcitveClaims
            // 
            this.lvUserAcitveClaims.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvUserAcitveClaims.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvUserAcitveClaims.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lvUserAcitveClaims.FullRowSelect = true;
            this.lvUserAcitveClaims.GridLines = true;
            this.lvUserAcitveClaims.Location = new System.Drawing.Point(3, 27);
            this.lvUserAcitveClaims.Name = "lvUserAcitveClaims";
            this.lvUserAcitveClaims.Size = new System.Drawing.Size(725, 94);
            this.lvUserAcitveClaims.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.lvUserAcitveClaims.TabIndex = 0;
            this.lvUserAcitveClaims.UseCompatibleStateImageBehavior = false;
            this.lvUserAcitveClaims.View = System.Windows.Forms.View.Details;
            this.lvUserAcitveClaims.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvUserAcitveClaims_MouseDoubleClick);
            this.lvUserAcitveClaims.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvUserAcitveClaims_MouseClick);
            this.lvUserAcitveClaims.Click += new System.EventHandler(this.lvUserAcitveClaims_Click);
            // 
            // lUserName
            // 
            this.lUserName.AutoSize = true;
            this.lUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lUserName.Location = new System.Drawing.Point(12, 27);
            this.lUserName.Name = "lUserName";
            this.lUserName.Size = new System.Drawing.Size(85, 20);
            this.lUserName.TabIndex = 1;
            this.lUserName.Text = "UserName";
            // 
            // gbActiveClaims
            // 
            this.gbActiveClaims.Controls.Add(this.lvGroupActiveClaims);
            this.gbActiveClaims.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbActiveClaims.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbActiveClaims.Location = new System.Drawing.Point(0, 0);
            this.gbActiveClaims.Name = "gbActiveClaims";
            this.gbActiveClaims.Size = new System.Drawing.Size(539, 282);
            this.gbActiveClaims.TabIndex = 0;
            this.gbActiveClaims.TabStop = false;
            this.gbActiveClaims.Text = "Активные заявки группы";
            // 
            // lvGroupActiveClaims
            // 
            this.lvGroupActiveClaims.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvGroupActiveClaims.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDate,
            this.chTime,
            this.chInfo,
            this.chStatus});
            this.lvGroupActiveClaims.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvGroupActiveClaims.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lvGroupActiveClaims.FullRowSelect = true;
            this.lvGroupActiveClaims.GridLines = true;
            this.lvGroupActiveClaims.HideSelection = false;
            this.lvGroupActiveClaims.Location = new System.Drawing.Point(3, 27);
            this.lvGroupActiveClaims.MultiSelect = false;
            this.lvGroupActiveClaims.Name = "lvGroupActiveClaims";
            this.lvGroupActiveClaims.Size = new System.Drawing.Size(533, 252);
            this.lvGroupActiveClaims.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.lvGroupActiveClaims.TabIndex = 0;
            this.lvGroupActiveClaims.UseCompatibleStateImageBehavior = false;
            this.lvGroupActiveClaims.View = System.Windows.Forms.View.Details;
            this.lvGroupActiveClaims.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvGroupActiveClaims_MouseDoubleClick);
            this.lvGroupActiveClaims.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvGroupActiveClaims_MouseClick);
            this.lvGroupActiveClaims.Click += new System.EventHandler(this.lvGroupActiveClaims_Click);
            // 
            // chDate
            // 
            this.chDate.Text = "Дата";
            this.chDate.Width = 146;
            // 
            // chTime
            // 
            this.chTime.Text = "Время";
            this.chTime.Width = 100;
            // 
            // chInfo
            // 
            this.chInfo.Text = "Описание";
            this.chInfo.Width = 100;
            // 
            // chStatus
            // 
            this.chStatus.Text = "Статус";
            this.chStatus.Width = 100;
            // 
            // gbGroupInfo
            // 
            this.gbGroupInfo.Controls.Add(this.splitContainer2);
            this.gbGroupInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbGroupInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbGroupInfo.Location = new System.Drawing.Point(0, 0);
            this.gbGroupInfo.Name = "gbGroupInfo";
            this.gbGroupInfo.Size = new System.Drawing.Size(737, 312);
            this.gbGroupInfo.TabIndex = 0;
            this.gbGroupInfo.TabStop = false;
            this.gbGroupInfo.Text = "Группа";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 27);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.gbActiveClaims);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.gbGroupMembers);
            this.splitContainer2.Size = new System.Drawing.Size(731, 282);
            this.splitContainer2.SplitterDistance = 539;
            this.splitContainer2.TabIndex = 1;
            // 
            // gbGroupMembers
            // 
            this.gbGroupMembers.Controls.Add(this.lvGroupMembers);
            this.gbGroupMembers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbGroupMembers.Location = new System.Drawing.Point(0, 0);
            this.gbGroupMembers.Name = "gbGroupMembers";
            this.gbGroupMembers.Size = new System.Drawing.Size(188, 282);
            this.gbGroupMembers.TabIndex = 0;
            this.gbGroupMembers.TabStop = false;
            this.gbGroupMembers.Text = "Сотрудники";
            // 
            // lvGroupMembers
            // 
            this.lvGroupMembers.AllowDrop = true;
            this.lvGroupMembers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chID,
            this.chGroupMemberLastName,
            this.chGroupMemberFirstName,
            this.chActiveClaims});
            this.lvGroupMembers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvGroupMembers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lvGroupMembers.FullRowSelect = true;
            this.lvGroupMembers.GridLines = true;
            this.lvGroupMembers.ImeMode = System.Windows.Forms.ImeMode.On;
            this.lvGroupMembers.Location = new System.Drawing.Point(3, 27);
            this.lvGroupMembers.Name = "lvGroupMembers";
            this.lvGroupMembers.Size = new System.Drawing.Size(182, 252);
            this.lvGroupMembers.TabIndex = 0;
            this.lvGroupMembers.UseCompatibleStateImageBehavior = false;
            this.lvGroupMembers.View = System.Windows.Forms.View.Details;
            // 
            // chID
            // 
            this.chID.Text = "ID";
            // 
            // chGroupMemberLastName
            // 
            this.chGroupMemberLastName.Text = "Фамилия";
            this.chGroupMemberLastName.Width = 100;
            // 
            // chGroupMemberFirstName
            // 
            this.chGroupMemberFirstName.Text = "Имя";
            this.chGroupMemberFirstName.Width = 100;
            // 
            // chActiveClaims
            // 
            this.chActiveClaims.Text = "Активные заявки";
            this.chActiveClaims.Width = 100;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 36);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gbUserInfo);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gbGroupInfo);
            this.splitContainer1.Size = new System.Drawing.Size(737, 496);
            this.splitContainer1.SplitterDistance = 180;
            this.splitContainer1.TabIndex = 1;
            // 
            // cmsGroupActiveClaimsMenu
            // 
            this.cmsGroupActiveClaimsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setExecutor,
            this.editGroupClaim});
            this.cmsGroupActiveClaimsMenu.Name = "cmsGroupActiveClaimsMenu";
            this.cmsGroupActiveClaimsMenu.Size = new System.Drawing.Size(208, 48);
            // 
            // setExecutor
            // 
            this.setExecutor.Name = "setExecutor";
            this.setExecutor.Size = new System.Drawing.Size(207, 22);
            this.setExecutor.Text = "Назначить исполнителя";
            this.setExecutor.Click += new System.EventHandler(this.setExecutor_Click);
            // 
            // editGroupClaim
            // 
            this.editGroupClaim.Name = "editGroupClaim";
            this.editGroupClaim.Size = new System.Drawing.Size(207, 22);
            this.editGroupClaim.Text = "Изменить заявку";
            this.editGroupClaim.Click += new System.EventHandler(this.editGroupClaim_Click);
            // 
            // cmsUserActiveClaimsMenu
            // 
            this.cmsUserActiveClaimsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToExecute,
            this.closeTheClaim,
            this.editUserClaim});
            this.cmsUserActiveClaimsMenu.Name = "cmsUserActiveClaimsMenu";
            this.cmsUserActiveClaimsMenu.Size = new System.Drawing.Size(225, 92);
            // 
            // startToExecute
            // 
            this.startToExecute.Name = "startToExecute";
            this.startToExecute.Size = new System.Drawing.Size(224, 22);
            this.startToExecute.Text = "Приступить к выполнению";
            this.startToExecute.Click += new System.EventHandler(this.startToExecute_Click);
            // 
            // editUserClaim
            // 
            this.editUserClaim.Name = "editUserClaim";
            this.editUserClaim.Size = new System.Drawing.Size(224, 22);
            this.editUserClaim.Text = "Изменить заявку";
            this.editUserClaim.Click += new System.EventHandler(this.editUserClaim_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Модуль исполнителя";
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // closeTheClaim
            // 
            this.closeTheClaim.Name = "closeTheClaim";
            this.closeTheClaim.Size = new System.Drawing.Size(224, 22);
            this.closeTheClaim.Text = "Закрыть заявку";
            this.closeTheClaim.Click += new System.EventHandler(this.closeTheClaim_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 532);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "Модуль исполнителя";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gbUserInfo.ResumeLayout(false);
            this.gbUserInfo.PerformLayout();
            this.gbUserActiveClaims.ResumeLayout(false);
            this.gbActiveClaims.ResumeLayout(false);
            this.gbGroupInfo.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.gbGroupMembers.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.cmsGroupActiveClaimsMenu.ResumeLayout(false);
            this.cmsUserActiveClaimsMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.GroupBox gbUserInfo;
        private System.Windows.Forms.GroupBox gbActiveClaims;
        private System.Windows.Forms.GroupBox gbGroupInfo;
        private System.Windows.Forms.Label lUserName;
        private System.Windows.Forms.ColumnHeader chDate;
        private System.Windows.Forms.ColumnHeader chTime;
        private System.Windows.Forms.ColumnHeader chInfo;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.GroupBox gbUserActiveClaims;
        private System.Windows.Forms.ListView lvUserAcitveClaims;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox gbGroupMembers;
        private System.Windows.Forms.ListView lvGroupMembers;
        private System.Windows.Forms.ColumnHeader chID;
        private System.Windows.Forms.ColumnHeader chGroupMemberLastName;
        private System.Windows.Forms.ColumnHeader chGroupMemberFirstName;
        private System.Windows.Forms.ColumnHeader chActiveClaims;
        private System.Windows.Forms.ToolStripButton tsbSelectUser;
        public System.Windows.Forms.ListView lvGroupActiveClaims;
        private System.Windows.Forms.ToolStripButton tsbSelectExecutor;
        private System.Windows.Forms.ToolStripButton tsbCloseTheClaim;
        private System.Windows.Forms.ToolStripButton tsbStartExecuteTheClaim;
        private System.Windows.Forms.ToolStripButton tsbShowReport;
        private System.Windows.Forms.ContextMenuStrip cmsGroupActiveClaimsMenu;
        private System.Windows.Forms.ToolStripMenuItem setExecutor;
        private System.Windows.Forms.ContextMenuStrip cmsUserActiveClaimsMenu;
        private System.Windows.Forms.ToolStripMenuItem startToExecute;
        private System.Windows.Forms.ToolStripButton tsbChangePswd;
        private System.Windows.Forms.ToolStripButton tsbEditClaim;
        private System.Windows.Forms.ToolStripMenuItem editGroupClaim;
        private System.Windows.Forms.ToolStripMenuItem editUserClaim;
        public System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripMenuItem closeTheClaim;
    }
}

