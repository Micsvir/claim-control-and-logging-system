namespace ExecutorModule
{
    partial class ClaimClosingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClaimClosingForm));
            this.gbClosingClaimInfo = new System.Windows.Forms.GroupBox();
            this.mtbAffectedObjectsCount = new System.Windows.Forms.MaskedTextBox();
            this.lObjectsCountCaption = new System.Windows.Forms.Label();
            this.lInternalCategoryCaption = new System.Windows.Forms.Label();
            this.lCommonCategoryCaption = new System.Windows.Forms.Label();
            this.tbRoom = new System.Windows.Forms.TextBox();
            this.lRoomCaption = new System.Windows.Forms.Label();
            this.tbClaimCreationDate = new System.Windows.Forms.TextBox();
            this.tbClaimSenderInfo = new System.Windows.Forms.TextBox();
            this.tbClaimTypeOfIssue = new System.Windows.Forms.TextBox();
            this.lClaimCreationDateCaption = new System.Windows.Forms.Label();
            this.cbInternalCategories = new System.Windows.Forms.ComboBox();
            this.cbCommonCategories = new System.Windows.Forms.ComboBox();
            this.tbClaimAddInfo = new System.Windows.Forms.TextBox();
            this.lClaimAddInfoCaption = new System.Windows.Forms.Label();
            this.lSenderInfoCaption = new System.Windows.Forms.Label();
            this.lTypeOfIssueCaption = new System.Windows.Forms.Label();
            this.gbSolvingInfo = new System.Windows.Forms.GroupBox();
            this.bCancel = new System.Windows.Forms.Button();
            this.bOK = new System.Windows.Forms.Button();
            this.tbExecutionInformation = new System.Windows.Forms.TextBox();
            this.gbClosingClaimInfo.SuspendLayout();
            this.gbSolvingInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbClosingClaimInfo
            // 
            this.gbClosingClaimInfo.Controls.Add(this.mtbAffectedObjectsCount);
            this.gbClosingClaimInfo.Controls.Add(this.lObjectsCountCaption);
            this.gbClosingClaimInfo.Controls.Add(this.lInternalCategoryCaption);
            this.gbClosingClaimInfo.Controls.Add(this.lCommonCategoryCaption);
            this.gbClosingClaimInfo.Controls.Add(this.tbRoom);
            this.gbClosingClaimInfo.Controls.Add(this.lRoomCaption);
            this.gbClosingClaimInfo.Controls.Add(this.tbClaimCreationDate);
            this.gbClosingClaimInfo.Controls.Add(this.tbClaimSenderInfo);
            this.gbClosingClaimInfo.Controls.Add(this.tbClaimTypeOfIssue);
            this.gbClosingClaimInfo.Controls.Add(this.lClaimCreationDateCaption);
            this.gbClosingClaimInfo.Controls.Add(this.cbInternalCategories);
            this.gbClosingClaimInfo.Controls.Add(this.cbCommonCategories);
            this.gbClosingClaimInfo.Controls.Add(this.tbClaimAddInfo);
            this.gbClosingClaimInfo.Controls.Add(this.lClaimAddInfoCaption);
            this.gbClosingClaimInfo.Controls.Add(this.lSenderInfoCaption);
            this.gbClosingClaimInfo.Controls.Add(this.lTypeOfIssueCaption);
            this.gbClosingClaimInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbClosingClaimInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbClosingClaimInfo.Location = new System.Drawing.Point(0, 0);
            this.gbClosingClaimInfo.Name = "gbClosingClaimInfo";
            this.gbClosingClaimInfo.Size = new System.Drawing.Size(780, 294);
            this.gbClosingClaimInfo.TabIndex = 0;
            this.gbClosingClaimInfo.TabStop = false;
            this.gbClosingClaimInfo.Text = "Информация о закрываемой заявке";
            // 
            // mtbAffectedObjectsCount
            // 
            this.mtbAffectedObjectsCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mtbAffectedObjectsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mtbAffectedObjectsCount.Location = new System.Drawing.Point(556, 253);
            this.mtbAffectedObjectsCount.Name = "mtbAffectedObjectsCount";
            this.mtbAffectedObjectsCount.Size = new System.Drawing.Size(100, 19);
            this.mtbAffectedObjectsCount.TabIndex = 22;
            this.mtbAffectedObjectsCount.TextChanged += new System.EventHandler(this.mtbAffectedObjectsCount_TextChanged);
            // 
            // lObjectsCountCaption
            // 
            this.lObjectsCountCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lObjectsCountCaption.Location = new System.Drawing.Point(553, 211);
            this.lObjectsCountCaption.Name = "lObjectsCountCaption";
            this.lObjectsCountCaption.Size = new System.Drawing.Size(176, 35);
            this.lObjectsCountCaption.TabIndex = 21;
            this.lObjectsCountCaption.Text = "Укажите кол-во затронутых элементов";
            // 
            // lInternalCategoryCaption
            // 
            this.lInternalCategoryCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lInternalCategoryCaption.Location = new System.Drawing.Point(283, 211);
            this.lInternalCategoryCaption.Name = "lInternalCategoryCaption";
            this.lInternalCategoryCaption.Size = new System.Drawing.Size(219, 35);
            this.lInternalCategoryCaption.TabIndex = 20;
            this.lInternalCategoryCaption.Text = "Укажите внутренную категорию системы";
            // 
            // lCommonCategoryCaption
            // 
            this.lCommonCategoryCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lCommonCategoryCaption.Location = new System.Drawing.Point(13, 211);
            this.lCommonCategoryCaption.Name = "lCommonCategoryCaption";
            this.lCommonCategoryCaption.Size = new System.Drawing.Size(188, 35);
            this.lCommonCategoryCaption.TabIndex = 19;
            this.lCommonCategoryCaption.Text = "Укажите общепринятую категорию";
            // 
            // tbRoom
            // 
            this.tbRoom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbRoom.Location = new System.Drawing.Point(142, 87);
            this.tbRoom.Name = "tbRoom";
            this.tbRoom.ReadOnly = true;
            this.tbRoom.Size = new System.Drawing.Size(632, 19);
            this.tbRoom.TabIndex = 18;
            // 
            // lRoomCaption
            // 
            this.lRoomCaption.AutoSize = true;
            this.lRoomCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lRoomCaption.Location = new System.Drawing.Point(12, 87);
            this.lRoomCaption.Name = "lRoomCaption";
            this.lRoomCaption.Size = new System.Drawing.Size(79, 20);
            this.lRoomCaption.TabIndex = 17;
            this.lRoomCaption.Text = "Комната:";
            // 
            // tbClaimCreationDate
            // 
            this.tbClaimCreationDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbClaimCreationDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbClaimCreationDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbClaimCreationDate.Location = new System.Drawing.Point(142, 112);
            this.tbClaimCreationDate.Name = "tbClaimCreationDate";
            this.tbClaimCreationDate.ReadOnly = true;
            this.tbClaimCreationDate.Size = new System.Drawing.Size(632, 19);
            this.tbClaimCreationDate.TabIndex = 16;
            // 
            // tbClaimSenderInfo
            // 
            this.tbClaimSenderInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbClaimSenderInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbClaimSenderInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbClaimSenderInfo.Location = new System.Drawing.Point(142, 62);
            this.tbClaimSenderInfo.Name = "tbClaimSenderInfo";
            this.tbClaimSenderInfo.ReadOnly = true;
            this.tbClaimSenderInfo.Size = new System.Drawing.Size(632, 19);
            this.tbClaimSenderInfo.TabIndex = 15;
            // 
            // tbClaimTypeOfIssue
            // 
            this.tbClaimTypeOfIssue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbClaimTypeOfIssue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbClaimTypeOfIssue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbClaimTypeOfIssue.Location = new System.Drawing.Point(142, 37);
            this.tbClaimTypeOfIssue.Name = "tbClaimTypeOfIssue";
            this.tbClaimTypeOfIssue.ReadOnly = true;
            this.tbClaimTypeOfIssue.Size = new System.Drawing.Size(632, 19);
            this.tbClaimTypeOfIssue.TabIndex = 14;
            // 
            // lClaimCreationDateCaption
            // 
            this.lClaimCreationDateCaption.AutoSize = true;
            this.lClaimCreationDateCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lClaimCreationDateCaption.Location = new System.Drawing.Point(12, 112);
            this.lClaimCreationDateCaption.Name = "lClaimCreationDateCaption";
            this.lClaimCreationDateCaption.Size = new System.Drawing.Size(128, 20);
            this.lClaimCreationDateCaption.TabIndex = 13;
            this.lClaimCreationDateCaption.Text = "Дата создания:";
            // 
            // cbInternalCategories
            // 
            this.cbInternalCategories.DropDownWidth = 400;
            this.cbInternalCategories.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbInternalCategories.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbInternalCategories.FormattingEnabled = true;
            this.cbInternalCategories.Location = new System.Drawing.Point(286, 250);
            this.cbInternalCategories.Name = "cbInternalCategories";
            this.cbInternalCategories.Size = new System.Drawing.Size(220, 28);
            this.cbInternalCategories.TabIndex = 8;
            this.cbInternalCategories.SelectedIndexChanged += new System.EventHandler(this.cbInternalCategories_SelectedIndexChanged);
            this.cbInternalCategories.Click += new System.EventHandler(this.cbInternalCategories_Click);
            // 
            // cbCommonCategories
            // 
            this.cbCommonCategories.DropDownWidth = 700;
            this.cbCommonCategories.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCommonCategories.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbCommonCategories.FormattingEnabled = true;
            this.cbCommonCategories.Location = new System.Drawing.Point(16, 250);
            this.cbCommonCategories.Name = "cbCommonCategories";
            this.cbCommonCategories.Size = new System.Drawing.Size(220, 28);
            this.cbCommonCategories.TabIndex = 7;
            this.cbCommonCategories.SelectedIndexChanged += new System.EventHandler(this.cbCommonCategories_SelectedIndexChanged);
            this.cbCommonCategories.Click += new System.EventHandler(this.cbCommonCategories_Click);
            // 
            // tbClaimAddInfo
            // 
            this.tbClaimAddInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbClaimAddInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbClaimAddInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbClaimAddInfo.Location = new System.Drawing.Point(142, 137);
            this.tbClaimAddInfo.Multiline = true;
            this.tbClaimAddInfo.Name = "tbClaimAddInfo";
            this.tbClaimAddInfo.ReadOnly = true;
            this.tbClaimAddInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbClaimAddInfo.Size = new System.Drawing.Size(632, 56);
            this.tbClaimAddInfo.TabIndex = 5;
            // 
            // lClaimAddInfoCaption
            // 
            this.lClaimAddInfoCaption.AutoSize = true;
            this.lClaimAddInfoCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lClaimAddInfoCaption.Location = new System.Drawing.Point(12, 137);
            this.lClaimAddInfoCaption.Name = "lClaimAddInfoCaption";
            this.lClaimAddInfoCaption.Size = new System.Drawing.Size(124, 20);
            this.lClaimAddInfoCaption.TabIndex = 4;
            this.lClaimAddInfoCaption.Text = "Доп. сведения:";
            // 
            // lSenderInfoCaption
            // 
            this.lSenderInfoCaption.AutoSize = true;
            this.lSenderInfoCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lSenderInfoCaption.Location = new System.Drawing.Point(12, 62);
            this.lSenderInfoCaption.Name = "lSenderInfoCaption";
            this.lSenderInfoCaption.Size = new System.Drawing.Size(97, 20);
            this.lSenderInfoCaption.TabIndex = 2;
            this.lSenderInfoCaption.Text = "Заявитель:";
            // 
            // lTypeOfIssueCaption
            // 
            this.lTypeOfIssueCaption.AutoSize = true;
            this.lTypeOfIssueCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lTypeOfIssueCaption.Location = new System.Drawing.Point(12, 37);
            this.lTypeOfIssueCaption.Name = "lTypeOfIssueCaption";
            this.lTypeOfIssueCaption.Size = new System.Drawing.Size(68, 20);
            this.lTypeOfIssueCaption.TabIndex = 0;
            this.lTypeOfIssueCaption.Text = "Заявка:";
            // 
            // gbSolvingInfo
            // 
            this.gbSolvingInfo.Controls.Add(this.bCancel);
            this.gbSolvingInfo.Controls.Add(this.bOK);
            this.gbSolvingInfo.Controls.Add(this.tbExecutionInformation);
            this.gbSolvingInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSolvingInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbSolvingInfo.Location = new System.Drawing.Point(0, 294);
            this.gbSolvingInfo.Name = "gbSolvingInfo";
            this.gbSolvingInfo.Size = new System.Drawing.Size(780, 291);
            this.gbSolvingInfo.TabIndex = 1;
            this.gbSolvingInfo.TabStop = false;
            this.gbSolvingInfo.Text = "Доп. сведения и предпринятые действия";
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bCancel.Location = new System.Drawing.Point(647, 255);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(127, 30);
            this.bCancel.TabIndex = 2;
            this.bCancel.Text = "Отмена";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // bOK
            // 
            this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.Enabled = false;
            this.bOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bOK.Location = new System.Drawing.Point(6, 255);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(127, 30);
            this.bOK.TabIndex = 1;
            this.bOK.Text = "Подтвердить";
            this.bOK.UseVisualStyleBackColor = true;
            // 
            // tbExecutionInformation
            // 
            this.tbExecutionInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbExecutionInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbExecutionInformation.Location = new System.Drawing.Point(6, 30);
            this.tbExecutionInformation.Multiline = true;
            this.tbExecutionInformation.Name = "tbExecutionInformation";
            this.tbExecutionInformation.Size = new System.Drawing.Size(768, 219);
            this.tbExecutionInformation.TabIndex = 0;
            this.tbExecutionInformation.TextChanged += new System.EventHandler(this.tbExecutionInformation_TextChanged);
            // 
            // ClaimClosingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(780, 585);
            this.Controls.Add(this.gbSolvingInfo);
            this.Controls.Add(this.gbClosingClaimInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "ClaimClosingForm";
            this.Text = "Закрытие заявки";
            this.gbClosingClaimInfo.ResumeLayout(false);
            this.gbClosingClaimInfo.PerformLayout();
            this.gbSolvingInfo.ResumeLayout(false);
            this.gbSolvingInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbClosingClaimInfo;
        private System.Windows.Forms.Label lTypeOfIssueCaption;
        private System.Windows.Forms.GroupBox gbSolvingInfo;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Label lSenderInfoCaption;
        private System.Windows.Forms.Label lClaimAddInfoCaption;
        public System.Windows.Forms.TextBox tbClaimAddInfo;
        private System.Windows.Forms.Label lClaimCreationDateCaption;
        public System.Windows.Forms.TextBox tbClaimCreationDate;
        public System.Windows.Forms.TextBox tbClaimSenderInfo;
        public System.Windows.Forms.TextBox tbClaimTypeOfIssue;
        private System.Windows.Forms.Label lRoomCaption;
        public System.Windows.Forms.TextBox tbRoom;
        private System.Windows.Forms.Label lObjectsCountCaption;
        private System.Windows.Forms.Label lInternalCategoryCaption;
        private System.Windows.Forms.Label lCommonCategoryCaption;
        public System.Windows.Forms.TextBox tbExecutionInformation;
        public System.Windows.Forms.MaskedTextBox mtbAffectedObjectsCount;
        public System.Windows.Forms.ComboBox cbCommonCategories;
        public System.Windows.Forms.ComboBox cbInternalCategories;
    }
}