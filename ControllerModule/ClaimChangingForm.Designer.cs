namespace ControllerModule
{
    partial class ClaimChangingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClaimChangingForm));
            this.lRoom = new System.Windows.Forms.Label();
            this.tbRoom = new System.Windows.Forms.TextBox();
            this.tbClaim = new System.Windows.Forms.TextBox();
            this.lClaim = new System.Windows.Forms.Label();
            this.tbAddInfo = new System.Windows.Forms.TextBox();
            this.lAddInfo = new System.Windows.Forms.Label();
            this.lClaimIDCaption = new System.Windows.Forms.Label();
            this.lClaimID = new System.Windows.Forms.Label();
            this.lClaimDateCaption = new System.Windows.Forms.Label();
            this.lClaimDate = new System.Windows.Forms.Label();
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.tbSenderName = new System.Windows.Forms.TextBox();
            this.lSenderNameCaption = new System.Windows.Forms.Label();
            this.tbSenderPhone = new System.Windows.Forms.TextBox();
            this.lSenderPhoneCaption = new System.Windows.Forms.Label();
            this.cbExecGroupsList = new System.Windows.Forms.ComboBox();
            this.lExecGroupCaption = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lRoom
            // 
            this.lRoom.AutoSize = true;
            this.lRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lRoom.Location = new System.Drawing.Point(12, 79);
            this.lRoom.Name = "lRoom";
            this.lRoom.Size = new System.Drawing.Size(79, 20);
            this.lRoom.TabIndex = 0;
            this.lRoom.Text = "Комната:";
            // 
            // tbRoom
            // 
            this.tbRoom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbRoom.Location = new System.Drawing.Point(184, 82);
            this.tbRoom.Name = "tbRoom";
            this.tbRoom.Size = new System.Drawing.Size(154, 15);
            this.tbRoom.TabIndex = 1;
            // 
            // tbClaim
            // 
            this.tbClaim.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbClaim.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbClaim.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbClaim.Location = new System.Drawing.Point(16, 192);
            this.tbClaim.Multiline = true;
            this.tbClaim.Name = "tbClaim";
            this.tbClaim.Size = new System.Drawing.Size(462, 86);
            this.tbClaim.TabIndex = 3;
            // 
            // lClaim
            // 
            this.lClaim.AutoSize = true;
            this.lClaim.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lClaim.Location = new System.Drawing.Point(12, 169);
            this.lClaim.Name = "lClaim";
            this.lClaim.Size = new System.Drawing.Size(68, 20);
            this.lClaim.TabIndex = 2;
            this.lClaim.Text = "Заявка:";
            // 
            // tbAddInfo
            // 
            this.tbAddInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAddInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbAddInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbAddInfo.Location = new System.Drawing.Point(16, 315);
            this.tbAddInfo.Multiline = true;
            this.tbAddInfo.Name = "tbAddInfo";
            this.tbAddInfo.Size = new System.Drawing.Size(462, 86);
            this.tbAddInfo.TabIndex = 5;
            // 
            // lAddInfo
            // 
            this.lAddInfo.AutoSize = true;
            this.lAddInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lAddInfo.Location = new System.Drawing.Point(12, 292);
            this.lAddInfo.Name = "lAddInfo";
            this.lAddInfo.Size = new System.Drawing.Size(223, 20);
            this.lAddInfo.TabIndex = 4;
            this.lAddInfo.Text = "Дополнительные сведения:";
            // 
            // lClaimIDCaption
            // 
            this.lClaimIDCaption.AutoSize = true;
            this.lClaimIDCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lClaimIDCaption.Location = new System.Drawing.Point(12, 19);
            this.lClaimIDCaption.Name = "lClaimIDCaption";
            this.lClaimIDCaption.Size = new System.Drawing.Size(86, 20);
            this.lClaimIDCaption.TabIndex = 6;
            this.lClaimIDCaption.Text = "ID заявки:";
            // 
            // lClaimID
            // 
            this.lClaimID.AutoSize = true;
            this.lClaimID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lClaimID.Location = new System.Drawing.Point(181, 22);
            this.lClaimID.Name = "lClaimID";
            this.lClaimID.Size = new System.Drawing.Size(15, 16);
            this.lClaimID.TabIndex = 7;
            this.lClaimID.Text = "0";
            // 
            // lClaimDateCaption
            // 
            this.lClaimDateCaption.AutoSize = true;
            this.lClaimDateCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lClaimDateCaption.Location = new System.Drawing.Point(12, 49);
            this.lClaimDateCaption.Name = "lClaimDateCaption";
            this.lClaimDateCaption.Size = new System.Drawing.Size(153, 20);
            this.lClaimDateCaption.TabIndex = 8;
            this.lClaimDateCaption.Text = "Дата поступления:";
            // 
            // lClaimDate
            // 
            this.lClaimDate.AutoSize = true;
            this.lClaimDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lClaimDate.Location = new System.Drawing.Point(181, 52);
            this.lClaimDate.Name = "lClaimDate";
            this.lClaimDate.Size = new System.Drawing.Size(58, 16);
            this.lClaimDate.TabIndex = 9;
            this.lClaimDate.Text = "00-00-00";
            // 
            // bOK
            // 
            this.bOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bOK.Location = new System.Drawing.Point(16, 515);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(88, 34);
            this.bOK.TabIndex = 10;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            // 
            // bCancel
            // 
            this.bCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bCancel.Location = new System.Drawing.Point(390, 515);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(88, 34);
            this.bCancel.TabIndex = 11;
            this.bCancel.Text = "Отмена";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // tbSenderName
            // 
            this.tbSenderName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbSenderName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbSenderName.Location = new System.Drawing.Point(184, 112);
            this.tbSenderName.Name = "tbSenderName";
            this.tbSenderName.Size = new System.Drawing.Size(154, 15);
            this.tbSenderName.TabIndex = 13;
            // 
            // lSenderNameCaption
            // 
            this.lSenderNameCaption.AutoSize = true;
            this.lSenderNameCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lSenderNameCaption.Location = new System.Drawing.Point(12, 109);
            this.lSenderNameCaption.Name = "lSenderNameCaption";
            this.lSenderNameCaption.Size = new System.Drawing.Size(129, 20);
            this.lSenderNameCaption.TabIndex = 12;
            this.lSenderNameCaption.Text = "Имя заявителя:";
            // 
            // tbSenderPhone
            // 
            this.tbSenderPhone.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbSenderPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbSenderPhone.Location = new System.Drawing.Point(184, 142);
            this.tbSenderPhone.Name = "tbSenderPhone";
            this.tbSenderPhone.Size = new System.Drawing.Size(154, 15);
            this.tbSenderPhone.TabIndex = 15;
            // 
            // lSenderPhoneCaption
            // 
            this.lSenderPhoneCaption.AutoSize = true;
            this.lSenderPhoneCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lSenderPhoneCaption.Location = new System.Drawing.Point(12, 139);
            this.lSenderPhoneCaption.Name = "lSenderPhoneCaption";
            this.lSenderPhoneCaption.Size = new System.Drawing.Size(168, 20);
            this.lSenderPhoneCaption.TabIndex = 14;
            this.lSenderPhoneCaption.Text = "Телефон заявителя:";
            // 
            // cbExecGroupsList
            // 
            this.cbExecGroupsList.DropDownWidth = 400;
            this.cbExecGroupsList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbExecGroupsList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbExecGroupsList.FormattingEnabled = true;
            this.cbExecGroupsList.Location = new System.Drawing.Point(16, 438);
            this.cbExecGroupsList.Name = "cbExecGroupsList";
            this.cbExecGroupsList.Size = new System.Drawing.Size(462, 24);
            this.cbExecGroupsList.TabIndex = 16;
            this.cbExecGroupsList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbExecGroupsList_MouseClick);
            this.cbExecGroupsList.SelectedIndexChanged += new System.EventHandler(this.cbExecGroupsList_SelectedIndexChanged);
            // 
            // lExecGroupCaption
            // 
            this.lExecGroupCaption.AutoSize = true;
            this.lExecGroupCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lExecGroupCaption.Location = new System.Drawing.Point(12, 415);
            this.lExecGroupCaption.Name = "lExecGroupCaption";
            this.lExecGroupCaption.Size = new System.Drawing.Size(186, 20);
            this.lExecGroupCaption.TabIndex = 17;
            this.lExecGroupCaption.Text = "Ответственная группа:";
            // 
            // ClaimChangingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 563);
            this.Controls.Add(this.lExecGroupCaption);
            this.Controls.Add(this.cbExecGroupsList);
            this.Controls.Add(this.tbSenderPhone);
            this.Controls.Add(this.lSenderPhoneCaption);
            this.Controls.Add(this.tbSenderName);
            this.Controls.Add(this.lSenderNameCaption);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.lClaimDate);
            this.Controls.Add(this.lClaimDateCaption);
            this.Controls.Add(this.lClaimID);
            this.Controls.Add(this.lClaimIDCaption);
            this.Controls.Add(this.tbAddInfo);
            this.Controls.Add(this.lAddInfo);
            this.Controls.Add(this.tbClaim);
            this.Controls.Add(this.lClaim);
            this.Controls.Add(this.tbRoom);
            this.Controls.Add(this.lRoom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ClaimChangingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Изменение заявки";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lRoom;
        private System.Windows.Forms.Label lClaim;
        private System.Windows.Forms.Label lAddInfo;
        private System.Windows.Forms.Label lClaimIDCaption;
        private System.Windows.Forms.Label lClaimDateCaption;
        public System.Windows.Forms.Button bOK;
        public System.Windows.Forms.Button bCancel;
        public System.Windows.Forms.TextBox tbRoom;
        public System.Windows.Forms.TextBox tbClaim;
        public System.Windows.Forms.TextBox tbAddInfo;
        public System.Windows.Forms.Label lClaimID;
        public System.Windows.Forms.Label lClaimDate;
        public System.Windows.Forms.TextBox tbSenderName;
        private System.Windows.Forms.Label lSenderNameCaption;
        public System.Windows.Forms.TextBox tbSenderPhone;
        private System.Windows.Forms.Label lSenderPhoneCaption;
        public System.Windows.Forms.ComboBox cbExecGroupsList;
        private System.Windows.Forms.Label lExecGroupCaption;
    }
}