using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExecutorModule
{
    public partial class PswdChangingForm : Form
    {
        public PswdChangingForm()
        {
            InitializeComponent();
        }

        private void mtbOldPswd_TextChanged(object sender, EventArgs e)
        {
            if (mtbNewPswd.Text.Length > 0 && mtbOldPswd.Text.Length > 0)
            {
                bOK.Enabled = true;
            }
            else
            {
                bOK.Enabled = false;
            }
        }

        private void mtbNewPswd_TextChanged(object sender, EventArgs e)
        {
            if (mtbNewPswd.Text.Length > 0 && mtbOldPswd.Text.Length > 0)
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
