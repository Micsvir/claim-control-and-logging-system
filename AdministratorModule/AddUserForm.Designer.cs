namespace AdministratorModule
{
    partial class AddEditUserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEditUserForm));
            this.tbUserLastName = new System.Windows.Forms.TextBox();
            this.tbUserFirstName = new System.Windows.Forms.TextBox();
            this.tbUserPatronymic = new System.Windows.Forms.TextBox();
            this.lUserFNameCaption = new System.Windows.Forms.Label();
            this.lUserLNameCaption = new System.Windows.Forms.Label();
            this.lUserPatCaption = new System.Windows.Forms.Label();
            this.lUserRoleCaption = new System.Windows.Forms.Label();
            this.cbUserRole = new System.Windows.Forms.ComboBox();
            this.cbUserGroup = new System.Windows.Forms.ComboBox();
            this.lUserGroupCaption = new System.Windows.Forms.Label();
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.lUserStatusCaption = new System.Windows.Forms.Label();
            this.cbUserStatus = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // tbUserLastName
            // 
            this.tbUserLastName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbUserLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbUserLastName.Location = new System.Drawing.Point(16, 32);
            this.tbUserLastName.Name = "tbUserLastName";
            this.tbUserLastName.Size = new System.Drawing.Size(224, 15);
            this.tbUserLastName.TabIndex = 0;
            this.tbUserLastName.TextChanged += new System.EventHandler(this.tbUserLastName_TextChanged);
            // 
            // tbUserFirstName
            // 
            this.tbUserFirstName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbUserFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbUserFirstName.Location = new System.Drawing.Point(16, 97);
            this.tbUserFirstName.Name = "tbUserFirstName";
            this.tbUserFirstName.Size = new System.Drawing.Size(224, 15);
            this.tbUserFirstName.TabIndex = 1;
            this.tbUserFirstName.TextChanged += new System.EventHandler(this.tbUserFirstName_TextChanged);
            // 
            // tbUserPatronymic
            // 
            this.tbUserPatronymic.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbUserPatronymic.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbUserPatronymic.Location = new System.Drawing.Point(16, 162);
            this.tbUserPatronymic.Name = "tbUserPatronymic";
            this.tbUserPatronymic.Size = new System.Drawing.Size(224, 15);
            this.tbUserPatronymic.TabIndex = 2;
            this.tbUserPatronymic.TextChanged += new System.EventHandler(this.tbUserPatronymic_TextChanged);
            // 
            // lUserFNameCaption
            // 
            this.lUserFNameCaption.AutoSize = true;
            this.lUserFNameCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lUserFNameCaption.Location = new System.Drawing.Point(12, 9);
            this.lUserFNameCaption.Name = "lUserFNameCaption";
            this.lUserFNameCaption.Size = new System.Drawing.Size(81, 20);
            this.lUserFNameCaption.TabIndex = 3;
            this.lUserFNameCaption.Text = "Фамилия";
            // 
            // lUserLNameCaption
            // 
            this.lUserLNameCaption.AutoSize = true;
            this.lUserLNameCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lUserLNameCaption.Location = new System.Drawing.Point(12, 74);
            this.lUserLNameCaption.Name = "lUserLNameCaption";
            this.lUserLNameCaption.Size = new System.Drawing.Size(40, 20);
            this.lUserLNameCaption.TabIndex = 4;
            this.lUserLNameCaption.Text = "Имя";
            // 
            // lUserPatCaption
            // 
            this.lUserPatCaption.AutoSize = true;
            this.lUserPatCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lUserPatCaption.Location = new System.Drawing.Point(12, 139);
            this.lUserPatCaption.Name = "lUserPatCaption";
            this.lUserPatCaption.Size = new System.Drawing.Size(83, 20);
            this.lUserPatCaption.TabIndex = 5;
            this.lUserPatCaption.Text = "Отчество";
            // 
            // lUserRoleCaption
            // 
            this.lUserRoleCaption.AutoSize = true;
            this.lUserRoleCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lUserRoleCaption.Location = new System.Drawing.Point(12, 204);
            this.lUserRoleCaption.Name = "lUserRoleCaption";
            this.lUserRoleCaption.Size = new System.Drawing.Size(47, 20);
            this.lUserRoleCaption.TabIndex = 7;
            this.lUserRoleCaption.Text = "Роль";
            // 
            // cbUserRole
            // 
            this.cbUserRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbUserRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbUserRole.FormattingEnabled = true;
            this.cbUserRole.Items.AddRange(new object[] {
            "Исполнитель",
            "Контролер",
            "Администратор",
            "Старший группы"});
            this.cbUserRole.Location = new System.Drawing.Point(16, 227);
            this.cbUserRole.Name = "cbUserRole";
            this.cbUserRole.Size = new System.Drawing.Size(224, 24);
            this.cbUserRole.TabIndex = 8;
            this.cbUserRole.TextChanged += new System.EventHandler(this.cbUserRole_TextChanged);
            // 
            // cbUserGroup
            // 
            this.cbUserGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbUserGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbUserGroup.FormattingEnabled = true;
            this.cbUserGroup.Location = new System.Drawing.Point(16, 301);
            this.cbUserGroup.Name = "cbUserGroup";
            this.cbUserGroup.Size = new System.Drawing.Size(224, 24);
            this.cbUserGroup.TabIndex = 9;
            this.cbUserGroup.TextChanged += new System.EventHandler(this.cbUserGroup_TextChanged);
            // 
            // lUserGroupCaption
            // 
            this.lUserGroupCaption.AutoSize = true;
            this.lUserGroupCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lUserGroupCaption.Location = new System.Drawing.Point(12, 278);
            this.lUserGroupCaption.Name = "lUserGroupCaption";
            this.lUserGroupCaption.Size = new System.Drawing.Size(61, 20);
            this.lUserGroupCaption.TabIndex = 10;
            this.lUserGroupCaption.Text = "Группа";
            // 
            // bOK
            // 
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bOK.Location = new System.Drawing.Point(16, 481);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(99, 36);
            this.bOK.TabIndex = 11;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bCancel.Location = new System.Drawing.Point(141, 481);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(99, 36);
            this.bCancel.TabIndex = 12;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // lUserStatusCaption
            // 
            this.lUserStatusCaption.AutoSize = true;
            this.lUserStatusCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lUserStatusCaption.Location = new System.Drawing.Point(12, 352);
            this.lUserStatusCaption.Name = "lUserStatusCaption";
            this.lUserStatusCaption.Size = new System.Drawing.Size(62, 20);
            this.lUserStatusCaption.TabIndex = 14;
            this.lUserStatusCaption.Text = "Статус";
            // 
            // cbUserStatus
            // 
            this.cbUserStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbUserStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbUserStatus.FormattingEnabled = true;
            this.cbUserStatus.Items.AddRange(new object[] {
            "Работает",
            "Сменил профиль деятельности",
            "Уволен"});
            this.cbUserStatus.Location = new System.Drawing.Point(16, 375);
            this.cbUserStatus.Name = "cbUserStatus";
            this.cbUserStatus.Size = new System.Drawing.Size(224, 24);
            this.cbUserStatus.TabIndex = 10;
            // 
            // AddEditUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(261, 529);
            this.Controls.Add(this.lUserStatusCaption);
            this.Controls.Add(this.cbUserStatus);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.lUserGroupCaption);
            this.Controls.Add(this.cbUserGroup);
            this.Controls.Add(this.cbUserRole);
            this.Controls.Add(this.lUserRoleCaption);
            this.Controls.Add(this.lUserPatCaption);
            this.Controls.Add(this.lUserLNameCaption);
            this.Controls.Add(this.lUserFNameCaption);
            this.Controls.Add(this.tbUserPatronymic);
            this.Controls.Add(this.tbUserFirstName);
            this.Controls.Add(this.tbUserLastName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AddEditUserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавление нового пользователя";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox tbUserLastName;
        public System.Windows.Forms.TextBox tbUserFirstName;
        public System.Windows.Forms.TextBox tbUserPatronymic;
        public System.Windows.Forms.Label lUserFNameCaption;
        public System.Windows.Forms.Label lUserLNameCaption;
        public System.Windows.Forms.Label lUserPatCaption;
        public System.Windows.Forms.Label lUserRoleCaption;
        public System.Windows.Forms.Label lUserGroupCaption;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
        public System.Windows.Forms.ComboBox cbUserRole;
        public System.Windows.Forms.ComboBox cbUserGroup;
        public System.Windows.Forms.Label lUserStatusCaption;
        public System.Windows.Forms.ComboBox cbUserStatus;
    }
}