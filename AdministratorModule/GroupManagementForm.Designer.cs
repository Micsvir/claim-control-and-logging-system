namespace AdministratorModule
{
    partial class GroupManagementForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupManagementForm));
            this.bCancel = new System.Windows.Forms.Button();
            this.bOK = new System.Windows.Forms.Button();
            this.lGroupNameCaption = new System.Windows.Forms.Label();
            this.tbGroupName = new System.Windows.Forms.TextBox();
            this.lGroupCommentCaption = new System.Windows.Forms.Label();
            this.tbGroupComment = new System.Windows.Forms.TextBox();
            this.lGroupVisibilityCaption = new System.Windows.Forms.Label();
            this.chbGroupVisibility = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bCancel.Location = new System.Drawing.Point(376, 490);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(99, 36);
            this.bCancel.TabIndex = 16;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // bOK
            // 
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bOK.Location = new System.Drawing.Point(16, 490);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(99, 36);
            this.bOK.TabIndex = 15;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            // 
            // lGroupNameCaption
            // 
            this.lGroupNameCaption.AutoSize = true;
            this.lGroupNameCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lGroupNameCaption.Location = new System.Drawing.Point(12, 9);
            this.lGroupNameCaption.Name = "lGroupNameCaption";
            this.lGroupNameCaption.Size = new System.Drawing.Size(139, 20);
            this.lGroupNameCaption.TabIndex = 14;
            this.lGroupNameCaption.Text = "Название группы";
            // 
            // tbGroupName
            // 
            this.tbGroupName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbGroupName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbGroupName.Location = new System.Drawing.Point(16, 33);
            this.tbGroupName.Name = "tbGroupName";
            this.tbGroupName.Size = new System.Drawing.Size(459, 15);
            this.tbGroupName.TabIndex = 13;
            this.tbGroupName.TextChanged += new System.EventHandler(this.tbGroupName_TextChanged);
            // 
            // lGroupCommentCaption
            // 
            this.lGroupCommentCaption.AutoSize = true;
            this.lGroupCommentCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lGroupCommentCaption.Location = new System.Drawing.Point(12, 102);
            this.lGroupCommentCaption.Name = "lGroupCommentCaption";
            this.lGroupCommentCaption.Size = new System.Drawing.Size(253, 20);
            this.lGroupCommentCaption.TabIndex = 18;
            this.lGroupCommentCaption.Text = "Описание группы (опционально)";
            // 
            // tbGroupComment
            // 
            this.tbGroupComment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbGroupComment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbGroupComment.Location = new System.Drawing.Point(16, 125);
            this.tbGroupComment.Multiline = true;
            this.tbGroupComment.Name = "tbGroupComment";
            this.tbGroupComment.Size = new System.Drawing.Size(459, 133);
            this.tbGroupComment.TabIndex = 17;
            // 
            // lGroupVisibilityCaption
            // 
            this.lGroupVisibilityCaption.AutoSize = true;
            this.lGroupVisibilityCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lGroupVisibilityCaption.Location = new System.Drawing.Point(12, 308);
            this.lGroupVisibilityCaption.Name = "lGroupVisibilityCaption";
            this.lGroupVisibilityCaption.Size = new System.Drawing.Size(169, 20);
            this.lGroupVisibilityCaption.TabIndex = 19;
            this.lGroupVisibilityCaption.Text = "Отображение группы";
            // 
            // chbGroupVisibility
            // 
            this.chbGroupVisibility.AutoSize = true;
            this.chbGroupVisibility.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chbGroupVisibility.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chbGroupVisibility.Location = new System.Drawing.Point(16, 331);
            this.chbGroupVisibility.Name = "chbGroupVisibility";
            this.chbGroupVisibility.Size = new System.Drawing.Size(457, 24);
            this.chbGroupVisibility.TabIndex = 20;
            this.chbGroupVisibility.Text = "Отображать группу в списке групп на назначение заявок";
            this.chbGroupVisibility.UseVisualStyleBackColor = true;
            // 
            // GroupManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 545);
            this.Controls.Add(this.chbGroupVisibility);
            this.Controls.Add(this.lGroupVisibilityCaption);
            this.Controls.Add(this.lGroupCommentCaption);
            this.Controls.Add(this.tbGroupComment);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.lGroupNameCaption);
            this.Controls.Add(this.tbGroupName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GroupManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GroupManagementForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bOK;
        public System.Windows.Forms.Label lGroupNameCaption;
        public System.Windows.Forms.TextBox tbGroupName;
        public System.Windows.Forms.Label lGroupCommentCaption;
        public System.Windows.Forms.TextBox tbGroupComment;
        public System.Windows.Forms.Label lGroupVisibilityCaption;
        public System.Windows.Forms.CheckBox chbGroupVisibility;
    }
}