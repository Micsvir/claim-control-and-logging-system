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
            /*
            cbExecGroupsList.Items.Clear();
            try
            {
                //Запрос списка групп
                MainForm.getGroupsButtonClick = true;
                NetMessage getGroupsListMsg = new NetMessage(NetMessage.commandType.GetInfo, "GetGroups");
                MainForm.client.SendMessage(getGroupsListMsg, false);

                MainForm.groupsData = MainForm.receivedData;

                //заполнение cbExecutorsList
                for (int row = 0; row < MainForm.receivedData.Rows.Count; row++)
                {
                    cbExecGroupsList.Items.Add(MainForm.receivedData.Rows[row]["GroupName"]);
                }
                MainForm.getGroupsButtonClick = false;
            }
            catch (Exception ex)
            {
                MainForm.getGroupsButtonClick = false;
                MessageBox.Show(ex.Message);
            }
            */
        }

        private void cbExecGroupsList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
