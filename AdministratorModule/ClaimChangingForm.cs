using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AdministratorModule
{
    public partial class ClaimChangingForm : Form
    {
        public ClaimChangingForm()
        {
            InitializeComponent();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chbGroupOrderDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpGroupOrderDate.Enabled = chbGroupOrderDate.Checked;
        }

        private void chbExecOrderDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpExecOrderDate.Enabled = chbExecOrderDate.Checked;
        }

        private void chbExecStartDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpClaimStartExecDate.Enabled = chbExecStartDate.Checked;
        }

        private void chbExecEndDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpClaimExecEndDate.Enabled = chbExecEndDate.Checked;
        }
    }
}
