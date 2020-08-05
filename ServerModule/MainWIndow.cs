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

namespace NetSubSysTEST
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Server.MessageRecieved += new Server.MessageWatcher(AddRecievedMessageToLB);
            Server.ErrorOccured += new Server.MessageWatcher(AddRecievedMessageToLB);
            Server.OperationIsSuccessfullyDone += new Server.MessageWatcher(AddRecievedMessageToLB);
            Server.ClientConnected += new Server.ClientEventHandler(AddConnectedClientToLB);
            Server.ClientDisconnected += new Server.ClientEventHandler(RemoveDisconnectedClientFromLB);
            Server.ConnectionToClientIsLost += new Server.ClientEventHandler(LostConnectionMessage);
            Configuration.GetSettings();
            if (Configuration.status == Configuration.GetSettingsResult.OK)
            {
                server = new Server(Convert.ToInt32(Configuration.GetValuesBySettingsName("ServerPort")[0]));

                //т.к. метод GetValuesBySettingsName(string settingsName) класса Configuration возвращает List<string> разделяя строку на подстроки,
                //используя разделитель ';', возникает ситуация, когда строку подключения к SQL серверу этот метод так же разделит на подстроки, так как
                //в ней содержится символ ';'. Следовательно, строку нужно вернуть в исходное состояние
                List<string> tempList = Configuration.GetValuesBySettingsName("connectionString");
                if (tempList.Count > 1)
                {

                    string tempString = "";
                    foreach (string subString in tempList)
                    {
                        tempString += subString + ";";
                    }
                    tempString = tempString.Substring(0, tempString.Length - 1);
                    server.sqlConnectionString = tempString;
                }
                else
                {
                    server.sqlConnectionString = tempList[0];
                }

                try
                {
                    maxRowCount = Convert.ToInt32(Configuration.GetValuesBySettingsName("maxLogRowsCount")[0]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось получить данные о максимально допустимом количестве строк в логе\nПроверьте корректность заполнения файла настроек\n" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show(Configuration.status.ToString());
            }
            client = new Client(Client.ClientType.ServerModule);

            lStatus.ForeColor = Color.Red;
        }

        Server server = null;
        Client client = null;

        bool askAboutMinimizeMode = true;
        bool minimizeToTray = false;

        int maxRowCount = 0;

        public delegate void AddClientsDataToLB(Client dataToAdd);

        public delegate void setDataCallback(NetMessage dataToSet);

        public void AddConnectedClientToLB(Client clientData)
        {
            if (lbClients.InvokeRequired)
            {
                AddClientsDataToLB d = new AddClientsDataToLB(AddConnectedClientToLB);
                Invoke(d, new object[] { clientData });
            }
            else
            {
                lbClients.Items.Add(clientData.IP + " " + clientData.hostName + " " + clientData.userName + " " + clientData.clientType.ToString());
                lbMessages.Items.Add(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " " + clientData.clientType.ToString() + " with IP " + clientData.IP + " on " + clientData.hostName + " (" + clientData.userName + ") has been connected to the server");
                lConnectedClientsCount.Text = lbClients.Items.Count.ToString();
            }
        }

        public void AddRecievedMessageToLB(NetMessage resmes)
        {
            string textToShow = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " " + resmes.client.clientType.ToString() + ", " + resmes.client.IP.ToString() + ": " + resmes.text;
            if (resmes.command == NetMessage.commandType.Message || resmes.command == NetMessage.commandType.BroadcastMessage)
            {
                if (lbMessages.InvokeRequired)
                {
                    setDataCallback d = new setDataCallback(AddRecievedMessageToLB);
                    Invoke(d, new object[] {resmes});
                }
                else
                {
                    if (lbMessages.Items.Count < maxRowCount)
                    {
                        lbMessages.Items.Add(textToShow);
                    }
                    else
                    {
                        lbMessages.Items.RemoveAt(0);
                        lbMessages.Items.Add(textToShow);
                    }
                }
            }
        }

        public void RemoveDisconnectedClientFromLB(Client disconnectingClient)
        {
            if (lbClients.InvokeRequired)
            {
                AddClientsDataToLB acd = new AddClientsDataToLB(RemoveDisconnectedClientFromLB);
                this.Invoke(acd, new object[] { disconnectingClient });
            }
            else
            {
                lbClients.Items.Clear();
                foreach (Client curConnectedClient in server.connectedClients)
                {
                    lbClients.Items.Add(curConnectedClient.IP + " " + curConnectedClient.hostName + " " + curConnectedClient.userName + " " + curConnectedClient.clientType.ToString()); 
                }
                lbMessages.Items.Add(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " " + disconnectingClient.clientType.ToString() + " with IP " + disconnectingClient.IP + " on " + disconnectingClient.hostName + " (" + disconnectingClient.userName + ") has been disconnected from the server");
                lConnectedClientsCount.Text = lbClients.Items.Count.ToString();
            }
        }

        public void LostConnectionMessage(Client ClientData)
        {
            if (this.InvokeRequired)
            {
                AddClientsDataToLB d = new AddClientsDataToLB(LostConnectionMessage);
                Invoke(d, new object[] { ClientData });
            }
            else
            {
                lbMessages.Items.Add(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " Connection with " + ClientData.clientType.ToString() + " with IP " + ClientData.IP.ToString() + " is lost");
                lbMessages.Items.Add(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " " + ClientData.clientType.ToString() + "(" + ClientData.IP.ToString() + ")" + " has been deleted from connectedClients list");
                lbClients.Items.Clear();
                foreach (Client curConnectedClient in server.connectedClients)
                {
                    lbClients.Items.Add(curConnectedClient.IP + " " + curConnectedClient.hostName + " " + curConnectedClient.userName + " " + curConnectedClient.clientType.ToString());
                    lConnectedClientsCount.Text = lbClients.Items.Count.ToString();
                }
            }
        }

        private void bStartServer_Click(object sender, EventArgs e)
        {
            if (!server.isAcitve)
            {
                try
                {
                    server.Start(true);
                    if (server.isAcitve)
                    {
                        lbMessages.Items.Add(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " Server has been started");
                        lStatus.Text = "Включен";
                        lPort.Text = server.port.ToString();
                        lIP.Text = "";

                        for (int i = 0; i < System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.Length; i++)
                        {
                            lIP.Text += System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList[i].ToString() + ";  ";
                        }
                    }
                    else
                    {
                        lbMessages.Items.Add(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " Something went wrong");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void bShow_Click(object sender, EventArgs e)
        {
            string result="";
            for (int i = 0; i < server.recievedMessages.Count; i++)
            {
                result += server.recievedMessages[i].text + "\n" ;
            }
            MessageBox.Show(result);
        }

        private void bStopServer_Click(object sender, EventArgs e)
        {
            if (server.isAcitve)
            {
                server.Stop();
                //MessageBox.Show("Server has been stoped");
                lbMessages.Items.Add(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " Server has been stoped");
                lStatus.Text = "Отключен";
                lIP.Text = "N/A";
                lPort.Text = "N/A";
            }
        }

        private void bForDiffShit_Click(object sender, EventArgs e)
        {
            string text = "";
            int i = 0;
            for (i = 0; i < System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.Length; i++)
            {
                text += System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList[i].ToString() + "\n";
            }
            MessageBox.Show(text);
        }

        private void showConClientsList_Click(object sender, EventArgs e)
        {
            string text = "";
            foreach (Client curClientData in server.connectedClients)
            {
                text += curClientData.IP + ", " + curClientData.hostName + ", " + curClientData.userName + "\n";
            }
            MessageBox.Show(text);
        }

        private void lStatus_TextChanged(object sender, EventArgs e)
        {
            if (lStatus.Text == "Отключен")
            {
                lStatus.ForeColor = Color.Red;
            }
            if (lStatus.Text == "Включен")
            {
                lStatus.ForeColor = Color.Green;
            }
        }

        private void tsbCheckDBConnection_Click(object sender, EventArgs e)
        {
            List<string> testResult = server.ConnectionToDBTest();
            string resultString = "";
            foreach (string curResultString in testResult)
            {
                resultString += curResultString + "\n\n";
            }
            MessageBox.Show(resultString);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                if (!minimizeToTray)
                {
                    if (askAboutMinimizeMode)
                    {
                        if (MessageBox.Show("Сворачивать программу в трей?", "Настройка программы", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            minimizeToTray = true;
                            askAboutMinimizeMode = false;

                            this.ShowInTaskbar = false;
                            notifyIcon1.Visible = true;
                        }
                        else
                        {
                            askAboutMinimizeMode = false;
                        }
                    }
                }
                else
                {
                    this.ShowInTaskbar = false;
                    notifyIcon1.Visible = true;
                }
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
            this.ShowInTaskbar = true;
        }

    }
}
