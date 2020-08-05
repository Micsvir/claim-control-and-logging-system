namespace AdministratorModule
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.cbUsersList = new System.Windows.Forms.ComboBox();
            this.mtbUserPassword = new System.Windows.Forms.MaskedTextBox();
            this.lUser = new System.Windows.Forms.Label();
            this.lPassword = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bOK
            // 
            this.bOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.Enabled = false;
            this.bOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bOK.Location = new System.Drawing.Point(23, 168);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(87, 31);
            this.bOK.TabIndex = 1;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bCancel
            // 
            this.bCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bCancel.Location = new System.Drawing.Point(227, 168);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(86, 31);
            this.bCancel.TabIndex = 2;
            this.bCancel.Text = "Отмена";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // cbUsersList
            // 
            this.cbUsersList.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbUsersList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUsersList.DropDownWidth = 333;
            this.cbUsersList.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbUsersList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbUsersList.FormattingEnabled = true;
            this.cbUsersList.Location = new System.Drawing.Point(23, 41);
            this.cbUsersList.Name = "cbUsersList";
            this.cbUsersList.Size = new System.Drawing.Size(290, 28);
            this.cbUsersList.TabIndex = 3;
            this.cbUsersList.SelectedIndexChanged += new System.EventHandler(this.cbUsersList_SelectedIndexChanged);
            this.cbUsersList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbUsersList_KeyPress);
            this.cbUsersList.Click += new System.EventHandler(this.cbUsersList_Click);
            // 
            // mtbUserPassword
            // 
            this.mtbUserPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mtbUserPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mtbUserPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mtbUserPassword.Location = new System.Drawing.Point(24, 106);
            this.mtbUserPassword.Name = "mtbUserPassword";
            this.mtbUserPassword.PasswordChar = '*';
            this.mtbUserPassword.Size = new System.Drawing.Size(290, 26);
            this.mtbUserPassword.TabIndex = 4;
            this.mtbUserPassword.Visible = false;
            this.mtbUserPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mtbUserPassword_KeyPress);
            this.mtbUserPassword.TextChanged += new System.EventHandler(this.mtbUserPassword_TextChanged);
            // 
            // lUser
            // 
            this.lUser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lUser.AutoSize = true;
            this.lUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lUser.Location = new System.Drawing.Point(20, 18);
            this.lUser.Name = "lUser";
            this.lUser.Size = new System.Drawing.Size(121, 20);
            this.lUser.TabIndex = 5;
            this.lUser.Text = "Пользователь";
            // 
            // lPassword
            // 
            this.lPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lPassword.AutoSize = true;
            this.lPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lPassword.Location = new System.Drawing.Point(20, 83);
            this.lPassword.Name = "lPassword";
            this.lPassword.Size = new System.Drawing.Size(67, 20);
            this.lPassword.TabIndex = 6;
            this.lPassword.Text = "Пароль";
            this.lPassword.Visible = false;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(334, 211);
            this.Controls.Add(this.lPassword);
            this.Controls.Add(this.lUser);
            this.Controls.Add(this.mtbUserPassword);
            this.Controls.Add(this.cbUsersList);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LogON";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Label lUser;
        private System.Windows.Forms.Label lPassword;
        public System.Windows.Forms.ComboBox cbUsersList;
        public System.Windows.Forms.MaskedTextBox mtbUserPassword;
    }
}