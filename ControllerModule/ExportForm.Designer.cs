namespace ControllerModule
{
    partial class ExportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportForm));
            this.gbFileTypeSelection = new System.Windows.Forms.GroupBox();
            this.bPatternSelect = new System.Windows.Forms.Button();
            this.tbPatternFileName = new System.Windows.Forms.TextBox();
            this.chbUsePattern = new System.Windows.Forms.CheckBox();
            this.rbDOC = new System.Windows.Forms.RadioButton();
            this.rbHTML = new System.Windows.Forms.RadioButton();
            this.gbOutputFileName = new System.Windows.Forms.GroupBox();
            this.bFileNameSelect = new System.Windows.Forms.Button();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.gbColumnSelection = new System.Windows.Forms.GroupBox();
            this.ReportColumnSet = new System.Windows.Forms.CheckedListBox();
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.gbReportContent = new System.Windows.Forms.GroupBox();
            this.chbMainInfo = new System.Windows.Forms.CheckBox();
            this.chbDetails = new System.Windows.Forms.CheckBox();
            this.gbFileTypeSelection.SuspendLayout();
            this.gbOutputFileName.SuspendLayout();
            this.gbColumnSelection.SuspendLayout();
            this.gbReportContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFileTypeSelection
            // 
            this.gbFileTypeSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFileTypeSelection.Controls.Add(this.bPatternSelect);
            this.gbFileTypeSelection.Controls.Add(this.tbPatternFileName);
            this.gbFileTypeSelection.Controls.Add(this.chbUsePattern);
            this.gbFileTypeSelection.Controls.Add(this.rbDOC);
            this.gbFileTypeSelection.Controls.Add(this.rbHTML);
            this.gbFileTypeSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbFileTypeSelection.Location = new System.Drawing.Point(0, 101);
            this.gbFileTypeSelection.Name = "gbFileTypeSelection";
            this.gbFileTypeSelection.Size = new System.Drawing.Size(374, 150);
            this.gbFileTypeSelection.TabIndex = 0;
            this.gbFileTypeSelection.TabStop = false;
            this.gbFileTypeSelection.Text = "Выбор типа файла";
            // 
            // bPatternSelect
            // 
            this.bPatternSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bPatternSelect.Enabled = false;
            this.bPatternSelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bPatternSelect.Location = new System.Drawing.Point(346, 120);
            this.bPatternSelect.Name = "bPatternSelect";
            this.bPatternSelect.Size = new System.Drawing.Size(19, 19);
            this.bPatternSelect.TabIndex = 2;
            this.bPatternSelect.UseVisualStyleBackColor = true;
            this.bPatternSelect.Visible = false;
            this.bPatternSelect.Click += new System.EventHandler(this.bPatternSelect_Click);
            // 
            // tbPatternFileName
            // 
            this.tbPatternFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPatternFileName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPatternFileName.Enabled = false;
            this.tbPatternFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbPatternFileName.Location = new System.Drawing.Point(5, 120);
            this.tbPatternFileName.Name = "tbPatternFileName";
            this.tbPatternFileName.Size = new System.Drawing.Size(334, 19);
            this.tbPatternFileName.TabIndex = 4;
            // 
            // chbUsePattern
            // 
            this.chbUsePattern.AutoSize = true;
            this.chbUsePattern.Enabled = false;
            this.chbUsePattern.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chbUsePattern.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chbUsePattern.Location = new System.Drawing.Point(5, 90);
            this.chbUsePattern.Name = "chbUsePattern";
            this.chbUsePattern.Size = new System.Drawing.Size(198, 24);
            this.chbUsePattern.TabIndex = 3;
            this.chbUsePattern.Text = "Использовать шаблон";
            this.chbUsePattern.UseVisualStyleBackColor = true;
            this.chbUsePattern.CheckedChanged += new System.EventHandler(this.chbUsePattern_CheckedChanged);
            // 
            // rbDOC
            // 
            this.rbDOC.AutoSize = true;
            this.rbDOC.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbDOC.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbDOC.Location = new System.Drawing.Point(5, 60);
            this.rbDOC.Name = "rbDOC";
            this.rbDOC.Size = new System.Drawing.Size(92, 24);
            this.rbDOC.TabIndex = 1;
            this.rbDOC.TabStop = true;
            this.rbDOC.Text = "MS Word";
            this.rbDOC.UseVisualStyleBackColor = true;
            this.rbDOC.CheckedChanged += new System.EventHandler(this.rbDOC_CheckedChanged);
            // 
            // rbHTML
            // 
            this.rbHTML.AutoSize = true;
            this.rbHTML.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbHTML.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbHTML.Location = new System.Drawing.Point(5, 30);
            this.rbHTML.Name = "rbHTML";
            this.rbHTML.Size = new System.Drawing.Size(69, 24);
            this.rbHTML.TabIndex = 0;
            this.rbHTML.TabStop = true;
            this.rbHTML.Text = "HTML";
            this.rbHTML.UseVisualStyleBackColor = true;
            // 
            // gbOutputFileName
            // 
            this.gbOutputFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbOutputFileName.Controls.Add(this.bFileNameSelect);
            this.gbOutputFileName.Controls.Add(this.tbFileName);
            this.gbOutputFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbOutputFileName.Location = new System.Drawing.Point(0, 257);
            this.gbOutputFileName.Name = "gbOutputFileName";
            this.gbOutputFileName.Size = new System.Drawing.Size(374, 62);
            this.gbOutputFileName.TabIndex = 1;
            this.gbOutputFileName.TabStop = false;
            this.gbOutputFileName.Text = "Выбор директории и имени файла";
            // 
            // bFileNameSelect
            // 
            this.bFileNameSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bFileNameSelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bFileNameSelect.Location = new System.Drawing.Point(346, 30);
            this.bFileNameSelect.Name = "bFileNameSelect";
            this.bFileNameSelect.Size = new System.Drawing.Size(19, 19);
            this.bFileNameSelect.TabIndex = 1;
            this.bFileNameSelect.UseVisualStyleBackColor = true;
            this.bFileNameSelect.Click += new System.EventHandler(this.bFileNameSelect_Click);
            // 
            // tbFileName
            // 
            this.tbFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFileName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbFileName.Location = new System.Drawing.Point(5, 30);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(335, 19);
            this.tbFileName.TabIndex = 0;
            // 
            // gbColumnSelection
            // 
            this.gbColumnSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbColumnSelection.Controls.Add(this.ReportColumnSet);
            this.gbColumnSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbColumnSelection.Location = new System.Drawing.Point(0, 325);
            this.gbColumnSelection.Name = "gbColumnSelection";
            this.gbColumnSelection.Size = new System.Drawing.Size(374, 184);
            this.gbColumnSelection.TabIndex = 2;
            this.gbColumnSelection.TabStop = false;
            this.gbColumnSelection.Text = "Выбор колонок";
            // 
            // ReportColumnSet
            // 
            this.ReportColumnSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportColumnSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ReportColumnSet.FormattingEnabled = true;
            this.ReportColumnSet.Location = new System.Drawing.Point(3, 27);
            this.ReportColumnSet.Name = "ReportColumnSet";
            this.ReportColumnSet.Size = new System.Drawing.Size(368, 151);
            this.ReportColumnSet.TabIndex = 0;
            // 
            // bOK
            // 
            this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bOK.Location = new System.Drawing.Point(5, 515);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(81, 34);
            this.bOK.TabIndex = 3;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bCancel.Location = new System.Drawing.Point(293, 515);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 34);
            this.bCancel.TabIndex = 4;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "report";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // gbReportContent
            // 
            this.gbReportContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbReportContent.Controls.Add(this.chbMainInfo);
            this.gbReportContent.Controls.Add(this.chbDetails);
            this.gbReportContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbReportContent.Location = new System.Drawing.Point(0, 3);
            this.gbReportContent.Name = "gbReportContent";
            this.gbReportContent.Size = new System.Drawing.Size(374, 92);
            this.gbReportContent.TabIndex = 5;
            this.gbReportContent.TabStop = false;
            this.gbReportContent.Text = "Выбор содержимого отчета";
            // 
            // chbMainInfo
            // 
            this.chbMainInfo.AutoSize = true;
            this.chbMainInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chbMainInfo.Location = new System.Drawing.Point(5, 60);
            this.chbMainInfo.Name = "chbMainInfo";
            this.chbMainInfo.Size = new System.Drawing.Size(290, 24);
            this.chbMainInfo.TabIndex = 1;
            this.chbMainInfo.Text = "Информация о количестве заявок";
            this.chbMainInfo.UseVisualStyleBackColor = true;
            // 
            // chbDetails
            // 
            this.chbDetails.AutoSize = true;
            this.chbDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chbDetails.Location = new System.Drawing.Point(5, 30);
            this.chbDetails.Name = "chbDetails";
            this.chbDetails.Size = new System.Drawing.Size(254, 24);
            this.chbDetails.TabIndex = 0;
            this.chbDetails.Text = "Подробный отчет по заявкам";
            this.chbDetails.UseVisualStyleBackColor = true;
            // 
            // ExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(374, 553);
            this.Controls.Add(this.gbReportContent);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.gbColumnSelection);
            this.Controls.Add(this.gbOutputFileName);
            this.Controls.Add(this.gbFileTypeSelection);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(382, 580);
            this.Name = "ExportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Экспорт результатов";
            this.gbFileTypeSelection.ResumeLayout(false);
            this.gbFileTypeSelection.PerformLayout();
            this.gbOutputFileName.ResumeLayout(false);
            this.gbOutputFileName.PerformLayout();
            this.gbColumnSelection.ResumeLayout(false);
            this.gbReportContent.ResumeLayout(false);
            this.gbReportContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFileTypeSelection;
        private System.Windows.Forms.GroupBox gbOutputFileName;
        private System.Windows.Forms.Button bFileNameSelect;
        private System.Windows.Forms.GroupBox gbColumnSelection;
        private System.Windows.Forms.Button bPatternSelect;
        private System.Windows.Forms.TextBox tbPatternFileName;
        private System.Windows.Forms.CheckBox chbUsePattern;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox gbReportContent;
        public System.Windows.Forms.RadioButton rbDOC;
        public System.Windows.Forms.RadioButton rbHTML;
        public System.Windows.Forms.TextBox tbFileName;
        public System.Windows.Forms.CheckedListBox ReportColumnSet;
        public System.Windows.Forms.CheckBox chbMainInfo;
        public System.Windows.Forms.CheckBox chbDetails;
    }
}