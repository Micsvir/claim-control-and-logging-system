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
            /*
            cbExecutorsList.Items.Clear();
            try
            {
                //Запрос списка групп
                NetMessage getGroupsListMsg = new NetMessage(NetMessage.commandType.GetInfo, "GetGroups");
                MainForm.client.SendMessage(getGroupsListMsg, false);

                //заполнение cbExecutorsList
                for (int row = 0; row < MainForm.receivedData.Rows.Count; row++)
                {
                    cbExecutorsList.Items.Add(MainForm.receivedData.Rows[row]["GroupName"]);
                }

                MainForm.groupsData = MainForm.receivedData;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            */
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
