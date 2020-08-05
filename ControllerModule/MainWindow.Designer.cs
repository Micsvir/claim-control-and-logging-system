namespace ControllerModule
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbSelectUser = new System.Windows.Forms.ToolStripButton();
            this.tsbSelectGroup = new System.Windows.Forms.ToolStripButton();
            this.tsbAddClaim = new System.Windows.Forms.ToolStripButton();
            this.tsdbAddClaim = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmAddClaimWithForm = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAddClaimWithModule = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbDeleteClaim = new System.Windows.Forms.ToolStripButton();
            this.tsbEditClaim = new System.Windows.Forms.ToolStripButton();
            this.tsbViewReport = new System.Windows.Forms.ToolStripButton();
            this.tsbChangePswd = new System.Windows.Forms.ToolStripButton();
            this.lvActiveClaims = new System.Windows.Forms.ListView();
            this.chClaimID = new System.Windows.Forms.ColumnHeader();
            this.chDate = new System.Windows.Forms.ColumnHeader();
            this.chTime = new System.Windows.Forms.ColumnHeader();
            this.chSenderIP = new System.Windows.Forms.ColumnHeader();
            this.chSenderHostName = new System.Windows.Forms.ColumnHeader();
            this.chSenderName = new System.Windows.Forms.ColumnHeader();
            this.chSenderRoom = new System.Windows.Forms.ColumnHeader();
            this.chReason = new System.Windows.Forms.ColumnHeader();
            this.chAddInfo = new System.Windows.Forms.ColumnHeader();
            this.gbGeneralInformation = new System.Windows.Forms.GroupBox();
            this.lActiveClaimsCount = new System.Windows.Forms.Label();
            this.lActiveClaimsCountCaption = new System.Windows.Forms.Label();
            this.lActiveUser = new System.Windows.Forms.Label();
            this.lAcitveUserCaption = new System.Windows.Forms.Label();
            this.lConnectionStatus = new System.Windows.Forms.Label();
            this.lConnectionStatusCaption = new System.Windows.Forms.Label();
            this.gbActiveClaims = new System.Windows.Forms.GroupBox();
            this.cmsActiveClaimsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SetExecGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.EditClaim = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьЗаявкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.gbGeneralInformation.SuspendLayout();
            this.gbActiveClaims.SuspendLayout();
            this.cmsActiveClaimsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSelectUser,
            this.tsbSelectGroup,
            this.tsbAddClaim,
            this.tsdbAddClaim,
            this.tsbDeleteClaim,
            this.tsbEditClaim,
            this.tsbViewReport,
            this.tsbChangePswd});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(1239, 36);
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
            // tsbSelectGroup
            // 
            this.tsbSelectGroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSelectGroup.Image = ((System.Drawing.Image)(resources.GetObject("tsbSelectGroup.Image")));
            this.tsbSelectGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSelectGroup.Name = "tsbSelectGroup";
            this.tsbSelectGroup.Size = new System.Drawing.Size(134, 33);
            this.tsbSelectGroup.Text = "Назначить на группу";
            this.tsbSelectGroup.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsbSelectGroup.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tsbAddClaim
            // 
            this.tsbAddClaim.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbAddClaim.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddClaim.Image")));
            this.tsbAddClaim.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddClaim.Name = "tsbAddClaim";
            this.tsbAddClaim.Size = new System.Drawing.Size(103, 33);
            this.tsbAddClaim.Text = "Создать заявку";
            this.tsbAddClaim.Visible = false;
            this.tsbAddClaim.Click += new System.EventHandler(this.AddClaim2_Click);
            // 
            // tsdbAddClaim
            // 
            this.tsdbAddClaim.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsdbAddClaim.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmAddClaimWithForm,
            this.tsmAddClaimWithModule});
            this.tsdbAddClaim.Image = ((System.Drawing.Image)(resources.GetObject("tsdbAddClaim.Image")));
            this.tsdbAddClaim.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsdbAddClaim.Name = "tsdbAddClaim";
            this.tsdbAddClaim.Size = new System.Drawing.Size(112, 33);
            this.tsdbAddClaim.Text = "Создать заявку";
            // 
            // tsmAddClaimWithForm
            // 
            this.tsmAddClaimWithForm.Name = "tsmAddClaimWithForm";
            this.tsmAddClaimWithForm.Size = new System.Drawing.Size(284, 22);
            this.tsmAddClaimWithForm.Text = "Создать заявку с помощью формы";
            this.tsmAddClaimWithForm.Click += new System.EventHandler(this.tsmAddClaimWithForm_Click);
            // 
            // tsmAddClaimWithModule
            // 
            this.tsmAddClaimWithModule.Name = "tsmAddClaimWithModule";
            this.tsmAddClaimWithModule.Size = new System.Drawing.Size(284, 22);
            this.tsmAddClaimWithModule.Text = "Создать заявку с помощью модуля";
            this.tsmAddClaimWithModule.Click += new System.EventHandler(this.tsmAddClaimWithModule_Click);
            // 
            // tsbDeleteClaim
            // 
            this.tsbDeleteClaim.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbDeleteClaim.Image = ((System.Drawing.Image)(resources.GetObject("tsbDeleteClaim.Image")));
            this.tsbDeleteClaim.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDeleteClaim.Name = "tsbDeleteClaim";
            this.tsbDeleteClaim.Size = new System.Drawing.Size(103, 33);
            this.tsbDeleteClaim.Text = "Удалить заявку";
            this.tsbDeleteClaim.Click += new System.EventHandler(this.tsbDeleteClaim_Click);
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
            // tsbViewReport
            // 
            this.tsbViewReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbViewReport.Image = ((System.Drawing.Image)(resources.GetObject("tsbViewReport.Image")));
            this.tsbViewReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbViewReport.Name = "tsbViewReport";
            this.tsbViewReport.Size = new System.Drawing.Size(118, 33);
            this.tsbViewReport.Text = "Посмотреть отчет";
            this.tsbViewReport.Click += new System.EventHandler(this.tsbViewReport_Click);
            // 
            // tsbChangePswd
            // 
            this.tsbChangePswd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbChangePswd.Image = ((System.Drawing.Image)(resources.GetObject("tsbChangePswd.Image")));
            this.tsbChangePswd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbChangePswd.Name = "tsbChangePswd";
            this.tsbChangePswd.Size = new System.Drawing.Size(108, 33);
            this.tsbChangePswd.Text = "Сменить пароль";
            this.tsbChangePswd.Click += new System.EventHandler(this.tsbChangePswd_Click);
            // 
            // lvActiveClaims
            // 
            this.lvActiveClaims.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvActiveClaims.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chClaimID,
            this.chDate,
            this.chTime,
            this.chSenderIP,
            this.chSenderHostName,
            this.chSenderName,
            this.chSenderRoom,
            this.chReason,
            this.chAddInfo});
            this.lvActiveClaims.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvActiveClaims.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lvActiveClaims.FullRowSelect = true;
            this.lvActiveClaims.GridLines = true;
            this.lvActiveClaims.Location = new System.Drawing.Point(3, 27);
            this.lvActiveClaims.Name = "lvActiveClaims";
            this.lvActiveClaims.Size = new System.Drawing.Size(1233, 340);
            this.lvActiveClaims.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.lvActiveClaims.TabIndex = 0;
            this.lvActiveClaims.UseCompatibleStateImageBehavior = false;
            this.lvActiveClaims.View = System.Windows.Forms.View.Details;
            this.lvActiveClaims.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvActiveClaims_MouseDoubleClick);
            this.lvActiveClaims.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvActiveClaims_MouseClick);
            this.lvActiveClaims.Click += new System.EventHandler(this.lvActiveClaims_Click);
            // 
            // chClaimID
            // 
            this.chClaimID.Text = "Номер";
            this.chClaimID.Width = 100;
            // 
            // chDate
            // 
            this.chDate.Text = "Дата";
            this.chDate.Width = 100;
            // 
            // chTime
            // 
            this.chTime.Text = "Время";
            this.chTime.Width = 100;
            // 
            // chSenderIP
            // 
            this.chSenderIP.Text = "IP отправителя";
            this.chSenderIP.Width = 140;
            // 
            // chSenderHostName
            // 
            this.chSenderHostName.Text = "ПК";
            this.chSenderHostName.Width = 100;
            // 
            // chSenderName
            // 
            this.chSenderName.Text = "Имя отправителя";
            this.chSenderName.Width = 150;
            // 
            // chSenderRoom
            // 
            this.chSenderRoom.Text = "Комната";
            this.chSenderRoom.Width = 100;
            // 
            // chReason
            // 
            this.chReason.Text = "Причина обращения";
            this.chReason.Width = 170;
            // 
            // chAddInfo
            // 
            this.chAddInfo.Text = "Доп. информация";
            this.chAddInfo.Width = 180;
            // 
            // gbGeneralInformation
            // 
            this.gbGeneralInformation.Controls.Add(this.lActiveClaimsCount);
            this.gbGeneralInformation.Controls.Add(this.lActiveClaimsCountCaption);
            this.gbGeneralInformation.Controls.Add(this.lActiveUser);
            this.gbGeneralInformation.Controls.Add(this.lAcitveUserCaption);
            this.gbGeneralInformation.Controls.Add(this.lConnectionStatus);
            this.gbGeneralInformation.Controls.Add(this.lConnectionStatusCaption);
            this.gbGeneralInformation.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbGeneralInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbGeneralInformation.Location = new System.Drawing.Point(0, 36);
            this.gbGeneralInformation.Name = "gbGeneralInformation";
            this.gbGeneralInformation.Size = new System.Drawing.Size(1239, 132);
            this.gbGeneralInformation.TabIndex = 1;
            this.gbGeneralInformation.TabStop = false;
            this.gbGeneralInformation.Text = "Общая информация";
            // 
            // lActiveClaimsCount
            // 
            this.lActiveClaimsCount.AutoSize = true;
            this.lActiveClaimsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lActiveClaimsCount.Location = new System.Drawing.Point(207, 100);
            this.lActiveClaimsCount.Name = "lActiveClaimsCount";
            this.lActiveClaimsCount.Size = new System.Drawing.Size(18, 20);
            this.lActiveClaimsCount.TabIndex = 5;
            this.lActiveClaimsCount.Text = "0";
            // 
            // lActiveClaimsCountCaption
            // 
            this.lActiveClaimsCountCaption.AutoSize = true;
            this.lActiveClaimsCountCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lActiveClaimsCountCaption.Location = new System.Drawing.Point(12, 100);
            this.lActiveClaimsCountCaption.Name = "lActiveClaimsCountCaption";
            this.lActiveClaimsCountCaption.Size = new System.Drawing.Size(121, 20);
            this.lActiveClaimsCountCaption.TabIndex = 4;
            this.lActiveClaimsCountCaption.Text = "Кол-во заявок:";
            // 
            // lActiveUser
            // 
            this.lActiveUser.AutoSize = true;
            this.lActiveUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lActiveUser.Location = new System.Drawing.Point(208, 70);
            this.lActiveUser.Name = "lActiveUser";
            this.lActiveUser.Size = new System.Drawing.Size(35, 20);
            this.lActiveUser.TabIndex = 3;
            this.lActiveUser.Text = "N/A";
            // 
            // lAcitveUserCaption
            // 
            this.lAcitveUserCaption.AutoSize = true;
            this.lAcitveUserCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lAcitveUserCaption.Location = new System.Drawing.Point(12, 70);
            this.lAcitveUserCaption.Name = "lAcitveUserCaption";
            this.lAcitveUserCaption.Size = new System.Drawing.Size(96, 20);
            this.lAcitveUserCaption.TabIndex = 2;
            this.lAcitveUserCaption.Text = "Контролер:";
            // 
            // lConnectionStatus
            // 
            this.lConnectionStatus.AutoSize = true;
            this.lConnectionStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lConnectionStatus.ForeColor = System.Drawing.Color.Red;
            this.lConnectionStatus.Location = new System.Drawing.Point(208, 40);
            this.lConnectionStatus.Name = "lConnectionStatus";
            this.lConnectionStatus.Size = new System.Drawing.Size(96, 20);
            this.lConnectionStatus.TabIndex = 1;
            this.lConnectionStatus.Text = "Отключено";
            // 
            // lConnectionStatusCaption
            // 
            this.lConnectionStatusCaption.AutoSize = true;
            this.lConnectionStatusCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lConnectionStatusCaption.Location = new System.Drawing.Point(12, 40);
            this.lConnectionStatusCaption.Name = "lConnectionStatusCaption";
            this.lConnectionStatusCaption.Size = new System.Drawing.Size(190, 20);
            this.lConnectionStatusCaption.TabIndex = 0;
            this.lConnectionStatusCaption.Text = "Состояние соединения:";
            // 
            // gbActiveClaims
            // 
            this.gbActiveClaims.Controls.Add(this.lvActiveClaims);
            this.gbActiveClaims.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbActiveClaims.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbActiveClaims.Location = new System.Drawing.Point(0, 168);
            this.gbActiveClaims.Name = "gbActiveClaims";
            this.gbActiveClaims.Size = new System.Drawing.Size(1239, 370);
            this.gbActiveClaims.TabIndex = 2;
            this.gbActiveClaims.TabStop = false;
            this.gbActiveClaims.Text = "Заявки";
            // 
            // cmsActiveClaimsMenu
            // 
            this.cmsActiveClaimsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SetExecGroup,
            this.EditClaim,
            this.удалитьЗаявкуToolStripMenuItem});
            this.cmsActiveClaimsMenu.Name = "cmsActiveClaimsMenu";
            this.cmsActiveClaimsMenu.Size = new System.Drawing.Size(259, 70);
            // 
            // SetExecGroup
            // 
            this.SetExecGroup.Name = "SetExecGroup";
            this.SetExecGroup.Size = new System.Drawing.Size(258, 22);
            this.SetExecGroup.Text = "Назначить ответственную группу";
            this.SetExecGroup.Click += new System.EventHandler(this.SetExecGroup_Click);
            // 
            // EditClaim
            // 
            this.EditClaim.Name = "EditClaim";
            this.EditClaim.Size = new System.Drawing.Size(258, 22);
            this.EditClaim.Text = "Изменить заявку";
            this.EditClaim.Click += new System.EventHandler(this.EditClaim_Click);
            // 
            // удалитьЗаявкуToolStripMenuItem
            // 
            this.удалитьЗаявкуToolStripMenuItem.Name = "удалитьЗаявкуToolStripMenuItem";
            this.удалитьЗаявкуToolStripMenuItem.Size = new System.Drawing.Size(258, 22);
            this.удалитьЗаявкуToolStripMenuItem.Text = "Удалить заявку";
            this.удалитьЗаявкуToolStripMenuItem.Click += new System.EventHandler(this.удалитьЗаявкуToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1239, 538);
            this.Controls.Add(this.gbActiveClaims);
            this.Controls.Add(this.gbGeneralInformation);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Модуль контролера";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gbGeneralInformation.ResumeLayout(false);
            this.gbGeneralInformation.PerformLayout();
            this.gbActiveClaims.ResumeLayout(false);
            this.cmsActiveClaimsMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ListView lvActiveClaims;
        private System.Windows.Forms.ColumnHeader chDate;
        private System.Windows.Forms.ColumnHeader chTime;
        private System.Windows.Forms.ColumnHeader chSenderIP;
        private System.Windows.Forms.ColumnHeader chSenderName;
        private System.Windows.Forms.ColumnHeader chReason;
        private System.Windows.Forms.ColumnHeader chAddInfo;
        private System.Windows.Forms.ColumnHeader chClaimID;
        private System.Windows.Forms.ColumnHeader chSenderHostName;
        private System.Windows.Forms.ColumnHeader chSenderRoom;
        private System.Windows.Forms.ToolStripButton tsbSelectGroup;
        private System.Windows.Forms.ToolStripButton tsbSelectUser;
        private System.Windows.Forms.GroupBox gbGeneralInformation;
        private System.Windows.Forms.Label lAcitveUserCaption;
        private System.Windows.Forms.Label lConnectionStatus;
        private System.Windows.Forms.Label lConnectionStatusCaption;
        private System.Windows.Forms.Label lActiveClaimsCount;
        private System.Windows.Forms.Label lActiveClaimsCountCaption;
        private System.Windows.Forms.Label lActiveUser;
        private System.Windows.Forms.GroupBox gbActiveClaims;
        private System.Windows.Forms.ToolStripButton tsbViewReport;
        private System.Windows.Forms.ToolStripButton tsbChangePswd;
        private System.Windows.Forms.ContextMenuStrip cmsActiveClaimsMenu;
        private System.Windows.Forms.ToolStripMenuItem SetExecGroup;
        private System.Windows.Forms.ToolStripMenuItem удалитьЗаявкуToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbDeleteClaim;
        private System.Windows.Forms.ToolStripButton tsbEditClaim;
        private System.Windows.Forms.ToolStripMenuItem EditClaim;
        private System.Windows.Forms.ToolStripButton tsbAddClaim;
        private System.Windows.Forms.ToolStripDropDownButton tsdbAddClaim;
        private System.Windows.Forms.ToolStripMenuItem tsmAddClaimWithForm;
        private System.Windows.Forms.ToolStripMenuItem tsmAddClaimWithModule;



    }
}

