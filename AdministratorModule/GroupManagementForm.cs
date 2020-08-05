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
    public partial class GroupManagementForm : Form
    {
        public GroupManagementForm()
        {
            InitializeComponent();

            if (tbGroupName.Text.Length > 0)
            {
                bOK.Enabled = true;
            }
            else
            {
                bOK.Enabled = false;
            }
        }

        private void tbGroupName_TextChanged(object sender, EventArgs e)
        {
            if (tbGroupName.Text.Length > 0)
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
