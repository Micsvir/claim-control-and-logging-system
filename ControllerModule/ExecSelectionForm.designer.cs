namespace ControllerModule
{
    partial class ExecSelectionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExecSelectionForm));
            this.cbExecutorsList = new System.Windows.Forms.ComboBox();
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbExecutorsList
            // 
            this.cbExecutorsList.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbExecutorsList.DropDownWidth = 400;
            this.cbExecutorsList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbExecutorsList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbExecutorsList.FormattingEnabled = true;
            this.cbExecutorsList.Location = new System.Drawing.Point(12, 39);
            this.cbExecutorsList.Name = "cbExecutorsList";
            this.cbExecutorsList.Size = new System.Drawing.Size(303, 28);
            this.cbExecutorsList.TabIndex = 0;
            this.cbExecutorsList.SelectedIndexChanged += new System.EventHandler(this.cbExecutorsList_SelectedIndexChanged);
            this.cbExecutorsList.Click += new System.EventHandler(this.cbExecutorsList_Click);
            // 
            // bOK
            // 
            this.bOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.Enabled = false;
            this.bOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bOK.Location = new System.Drawing.Point(12, 154);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(93, 31);
            this.bOK.TabIndex = 1;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            // 
            // bCancel
            // 
            this.bCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bCancel.Location = new System.Drawing.Point(222, 154);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(93, 31);
            this.bCancel.TabIndex = 2;
            this.bCancel.Text = "Отмена";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // ExecSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(334, 211);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.cbExecutorsList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExecSelectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Выберите отвественную группу";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
        public System.Windows.Forms.ComboBox cbExecutorsList;
    }
}