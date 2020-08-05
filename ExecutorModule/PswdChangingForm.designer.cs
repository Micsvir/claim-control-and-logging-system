namespace ExecutorModule
{
    partial class PswdChangingForm
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
            this.lUserDataCaption = new System.Windows.Forms.Label();
            this.lUserData = new System.Windows.Forms.Label();
            this.lOldPswdCaption = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.mtbOldPswd = new System.Windows.Forms.MaskedTextBox();
            this.mtbNewPswd = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // lUserDataCaption
            // 
            this.lUserDataCaption.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lUserDataCaption.AutoSize = true;
            this.lUserDataCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lUserDataCaption.Location = new System.Drawing.Point(83, 9);
            this.lUserDataCaption.Name = "lUserDataCaption";
            this.lUserDataCaption.Size = new System.Drawing.Size(153, 25);
            this.lUserDataCaption.TabIndex = 0;
            this.lUserDataCaption.Text = "Пользователь";
            // 
            // lUserData
            // 
            this.lUserData.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lUserData.AutoSize = true;
            this.lUserData.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lUserData.Location = new System.Drawing.Point(142, 54);
            this.lUserData.Name = "lUserData";
            this.lUserData.Size = new System.Drawing.Size(18, 20);
            this.lUserData.TabIndex = 1;
            this.lUserData.Text = "1";
            // 
            // lOldPswdCaption
            // 
            this.lOldPswdCaption.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lOldPswdCaption.AutoSize = true;
            this.lOldPswdCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lOldPswdCaption.Location = new System.Drawing.Point(34, 125);
            this.lOldPswdCaption.Name = "lOldPswdCaption";
            this.lOldPswdCaption.Size = new System.Drawing.Size(250, 25);
            this.lOldPswdCaption.TabIndex = 2;
            this.lOldPswdCaption.Text = "Введите старый пароль";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(36, 199);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Введите новый пароль";
            // 
            // bOK
            // 
            this.bOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.Enabled = false;
            this.bOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bOK.Location = new System.Drawing.Point(37, 297);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(88, 34);
            this.bOK.TabIndex = 6;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            // 
            // bCancel
            // 
            this.bCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bCancel.Location = new System.Drawing.Point(194, 297);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(88, 34);
            this.bCancel.TabIndex = 7;
            this.bCancel.Text = "Отмена";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // mtbOldPswd
            // 
            this.mtbOldPswd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mtbOldPswd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mtbOldPswd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mtbOldPswd.Location = new System.Drawing.Point(39, 153);
            this.mtbOldPswd.Name = "mtbOldPswd";
            this.mtbOldPswd.PasswordChar = '*';
            this.mtbOldPswd.Size = new System.Drawing.Size(237, 19);
            this.mtbOldPswd.TabIndex = 8;
            this.mtbOldPswd.TextChanged += new System.EventHandler(this.mtbOldPswd_TextChanged);
            // 
            // mtbNewPswd
            // 
            this.mtbNewPswd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mtbNewPswd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mtbNewPswd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mtbNewPswd.Location = new System.Drawing.Point(39, 227);
            this.mtbNewPswd.Name = "mtbNewPswd";
            this.mtbNewPswd.PasswordChar = '*';
            this.mtbNewPswd.Size = new System.Drawing.Size(237, 19);
            this.mtbNewPswd.TabIndex = 9;
            this.mtbNewPswd.TextChanged += new System.EventHandler(this.mtbNewPswd_TextChanged);
            // 
            // PswdChangingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 357);
            this.Controls.Add(this.mtbNewPswd);
            this.Controls.Add(this.mtbOldPswd);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lOldPswdCaption);
            this.Controls.Add(this.lUserData);
            this.Controls.Add(this.lUserDataCaption);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PswdChangingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Смена пароля";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lUserDataCaption;
        private System.Windows.Forms.Label lOldPswdCaption;
        public System.Windows.Forms.Label lUserData;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button bOK;
        public System.Windows.Forms.Button bCancel;
        public System.Windows.Forms.MaskedTextBox mtbOldPswd;
        public System.Windows.Forms.MaskedTextBox mtbNewPswd;
    }
}