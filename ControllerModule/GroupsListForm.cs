using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UtilSubSys;

namespace ControllerModule
{
    public partial class GroupsListForm : Form
    {
        public GroupsListForm()
        {
            InitializeComponent();
            List<string> colNames = new List<string>();
            colNames.Add("GroupName:Группа");
            colNames.Add("GroupID:ID");
            Configuration.CreateListView(groupsListView, colNames, 12);
            groupsListView.Height = this.Height - 55;
            Configuration.AddDataToListView(groupsListView, MainForm.receivedData);
        }

        private void GroupsListForm_SizeChanged(object sender, EventArgs e)
        {
            groupsListView.Height = this.Height - 55;
        }

        private void groupsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (groupsListView.SelectedItems.Count == 1)
            {
                bOK.Enabled = true;
            }
            else
            {
                bOK.Enabled = false;
            }
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            MainForm.selectedGroupID = Convert.ToInt32(groupsListView.SelectedItems[0].SubItems[1].Text);
            //MessageBox.Show(MainForm.selectedGroupID.ToString());
        }
    }
}
