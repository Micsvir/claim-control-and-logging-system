using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetSubSys;

namespace ControllerModule
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

        private void cbExecGroupsList_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void cbExecGroupsList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
