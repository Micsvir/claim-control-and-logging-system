﻿namespace ControllerModule
{
    partial class NewClaimNotification
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
            this.lNotification = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lNotification
            // 
            this.lNotification.AutoSize = true;
            this.lNotification.Location = new System.Drawing.Point(125, 106);
            this.lNotification.Name = "lNotification";
            this.lNotification.Size = new System.Drawing.Size(35, 13);
            this.lNotification.TabIndex = 0;
            this.lNotification.Text = "label1";
            // 
            // NewClaimNotification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.lNotification);
            this.Name = "NewClaimNotification";
            this.Text = "NewClaimNotification";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lNotification;
    }
}