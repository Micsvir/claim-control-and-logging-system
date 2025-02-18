﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UtilSubSys;
using NetSubSys;

namespace ExecutorModule
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void cbUsersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainWindow.selectedUserChanged = true;
            MainWindow.groupActiveClaims.Clear();
            MainWindow.userActiveClaims.Clear();

            if (cbUsersList.SelectedIndex != -1)
            {
                //bOK.Enabled = true;
                mtbUserPassword.Visible = true;
            }
            else
            {
                //bOK.Enabled = false;
                mtbUserPassword.Visible = false;
            }
        }

        private void cbUsersList_Click(object sender, EventArgs e)
        {

        }

        private void mtbUserPassword_TextChanged(object sender, EventArgs e)
        {
            if (mtbUserPassword.Text.Length > 0)
            {
                bOK.Enabled = true;
            }
            else
            {
                bOK.Enabled = false;
            }
        }

        private void mtbUserPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) //Convert.ToChar(0x13)
            {
                if (mtbUserPassword.Text.Length > 0)
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Пароль не может быть пустым");
                }
            }
        }

        private void cbUsersList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) //Convert.ToChar(0x13)
            {
                if (mtbUserPassword.Text.Length > 0)
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Пароль не может быть пустым");
                }
            }
        }
    }
}
