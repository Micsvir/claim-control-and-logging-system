namespace ControllerModule
{
    partial class ReportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbGetReport = new System.Windows.Forms.ToolStripButton();
            this.tsbExport = new System.Windows.Forms.ToolStripButton();
            this.gbReportSettings = new System.Windows.Forms.GroupBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgvParametersSelection = new System.Windows.Forms.DataGridView();
            this.tbHelp = new System.Windows.Forms.TextBox();
            this.tbRequestResult = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.gbReport = new System.Windows.Forms.GroupBox();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cmsParametersSelectionMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.parametersRowDeleting = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.gbReportSettings.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParametersSelection)).BeginInit();
            this.gbReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.cmsParametersSelectionMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbGetReport,
            this.tsbExport});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1216, 36);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbGetReport
            // 
            this.tsbGetReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbGetReport.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tsbGetReport.Image = ((System.Drawing.Image)(resources.GetObject("tsbGetReport.Image")));
            this.tsbGetReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbGetReport.Name = "tsbGetReport";
            this.tsbGetReport.Size = new System.Drawing.Size(118, 33);
            this.tsbGetReport.Text = "Посмотреть отчет";
            this.tsbGetReport.Click += new System.EventHandler(this.tsbGetReport_Click);
            // 
            // tsbExport
            // 
            this.tsbExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbExport.Image = ((System.Drawing.Image)(resources.GetObject("tsbExport.Image")));
            this.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.Size = new System.Drawing.Size(56, 33);
            this.tsbExport.Text = "Экспорт";
            this.tsbExport.Click += new System.EventHandler(this.tsbExport_Click);
            // 
            // gbReportSettings
            // 
            this.gbReportSettings.Controls.Add(this.splitContainer2);
            this.gbReportSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbReportSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbReportSettings.Location = new System.Drawing.Point(0, 0);
            this.gbReportSettings.Name = "gbReportSettings";
            this.gbReportSettings.Size = new System.Drawing.Size(1216, 208);
            this.gbReportSettings.TabIndex = 1;
            this.gbReportSettings.TabStop = false;
            this.gbReportSettings.Text = "Параметры";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 27);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgvParametersSelection);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tbHelp);
            this.splitContainer2.Panel2.Controls.Add(this.tbRequestResult);
            this.splitContainer2.Size = new System.Drawing.Size(1210, 178);
            this.splitContainer2.SplitterDistance = 711;
            this.splitContainer2.TabIndex = 14;
            // 
            // dgvParametersSelection
            // 
            this.dgvParametersSelection.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvParametersSelection.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvParametersSelection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParametersSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvParametersSelection.Location = new System.Drawing.Point(0, 0);
            this.dgvParametersSelection.Name = "dgvParametersSelection";
            this.dgvParametersSelection.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvParametersSelection.Size = new System.Drawing.Size(711, 178);
            this.dgvParametersSelection.TabIndex = 12;
            this.dgvParametersSelection.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvParametersSelection_MouseClick);
            this.dgvParametersSelection.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvParametersSelection_RowsAdded);
            // 
            // tbHelp
            // 
            this.tbHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbHelp.Location = new System.Drawing.Point(0, 0);
            this.tbHelp.Multiline = true;
            this.tbHelp.Name = "tbHelp";
            this.tbHelp.ReadOnly = true;
            this.tbHelp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbHelp.Size = new System.Drawing.Size(495, 178);
            this.tbHelp.TabIndex = 14;
            this.tbHelp.Text = resources.GetString("tbHelp.Text");
            // 
            // tbRequestResult
            // 
            this.tbRequestResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbRequestResult.Location = new System.Drawing.Point(0, 63);
            this.tbRequestResult.Multiline = true;
            this.tbRequestResult.Name = "tbRequestResult";
            this.tbRequestResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbRequestResult.Size = new System.Drawing.Size(238, 23);
            this.tbRequestResult.TabIndex = 13;
            this.tbRequestResult.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 564);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1216, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // gbReport
            // 
            this.gbReport.Controls.Add(this.dgvReport);
            this.gbReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbReport.Location = new System.Drawing.Point(0, 0);
            this.gbReport.Name = "gbReport";
            this.gbReport.Size = new System.Drawing.Size(1216, 316);
            this.gbReport.TabIndex = 4;
            this.gbReport.TabStop = false;
            this.gbReport.Text = "Отчет";
            // 
            // dgvReport
            // 
            this.dgvReport.AllowUserToAddRows = false;
            this.dgvReport.AllowUserToDeleteRows = false;
            this.dgvReport.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvReport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReport.Location = new System.Drawing.Point(3, 27);
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.ReadOnly = true;
            this.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReport.Size = new System.Drawing.Size(1210, 286);
            this.dgvReport.TabIndex = 0;
            this.dgvReport.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvReport_MouseDoubleClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 36);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gbReportSettings);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gbReport);
            this.splitContainer1.Size = new System.Drawing.Size(1216, 528);
            this.splitContainer1.SplitterDistance = 208;
            this.splitContainer1.TabIndex = 5;
            // 
            // cmsParametersSelectionMenu
            // 
            this.cmsParametersSelectionMenu.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmsParametersSelectionMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parametersRowDeleting});
            this.cmsParametersSelectionMenu.Name = "cmsParametersSelectionMenu";
            this.cmsParametersSelectionMenu.Size = new System.Drawing.Size(167, 26);
            // 
            // parametersRowDeleting
            // 
            this.parametersRowDeleting.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.parametersRowDeleting.Name = "parametersRowDeleting";
            this.parametersRowDeleting.Size = new System.Drawing.Size(166, 22);
            this.parametersRowDeleting.Text = "Удалить строку";
            this.parametersRowDeleting.Click += new System.EventHandler(this.parametersRowDeleting_Click);
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 586);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1224, 400);
            this.Name = "ReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Отчет";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gbReportSettings.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvParametersSelection)).EndInit();
            this.gbReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.cmsParametersSelectionMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.GroupBox gbReportSettings;
        private System.Windows.Forms.ToolStripButton tsbGetReport;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox gbReport;
        private System.Windows.Forms.DataGridView dgvParametersSelection;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ContextMenuStrip cmsParametersSelectionMenu;
        private System.Windows.Forms.ToolStripMenuItem parametersRowDeleting;
        private System.Windows.Forms.ToolStripButton tsbExport;
        public System.Windows.Forms.DataGridView dgvReport;
        public System.Windows.Forms.TextBox tbRequestResult;
        private System.Windows.Forms.TextBox tbHelp;
    }
}