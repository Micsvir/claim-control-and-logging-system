using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetSubSys;
using UtilSubSys;

namespace ControllerModule
{
    public partial class ExecSelectionForm : Form
    {
        public ExecSelectionForm()
        {
            InitializeComponent();
        }

        private void cbExecutorsList_Click(object sender, EventArgs e)
        {

        }

        private void cbExecutorsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbExecutorsList.SelectedIndex != -1)
            {
                bOK.Enabled = true;
            }
            else
            {
                bOK.Enabled = false;
            }
        }
    }
}
