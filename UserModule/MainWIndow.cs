using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using NetSubSys;
using UtilSubSys;
using System.Diagnostics;
using System.Threading;

namespace UserModule
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            //--подгонка элементов формы при изменениях размеров
            tabControl1.ItemSize = new Size((this.Size.Width - 30) / tabControl1.TabPages.Count, 50);
            for (int i = 0; i < 4; i++)
            {
                statusStrip1.Items[i].Size = new Size(this.Size.Width / 4 - 10, statusStrip1.Items[i].Size.Height);
            }
            lSendClaim.Left = this.Width / 2 - lSendClaim.Width / 2;
            lSendClaim.Visible = false;
            //--конец подгонки

            //сокрытие элемента dgvActiveClaims
            dgvActiveClaims.Visible = false;

            //Подписка на событие получения сообщения
            Server.MessageRecieved += new Server.MessageWatcher(AddClaimsToDGV);
            Client.AnswerRecievedFromServer += new Client.AnswerReciever(AddClientsInfoToDGV);


            //получение информации о системе
            GetSystemInfo();
            //загрузка настроек из файла .exe.conf
            Configuration.GetSettings();
            //проверка, что настройки загрузились корректно
            if (Configuration.status == Configuration.GetSettingsResult.OK)
            {
                AppList = Configuration.GetValuesBySettingsName("AppList");
                EquipList = Configuration.GetValuesBySettingsName("EquipList");
                AnotherClaims = Configuration.GetValuesBySettingsName("AnotherClaims");
                Rooms = Configuration.GetValuesBySettingsName("RoomsList");
                serverIP = Configuration.GetValuesBySettingsName("ServerIP")[0];
                serverPort = Convert.ToInt32(Configuration.GetValuesBySettingsName("ServerPort")[0]);
                phoneNum = Configuration.GetValuesBySettingsName("PhoneNum")[0];
                lInfo.Text += phoneNum;
                lInfo.Left = (this.Width / 2) - (lInfo.Width / 2);

                //Добавление номеров комнат в список cbStep3
                foreach (string curRoom in Rooms)
                {
                    cbStep3.Items.Add(curRoom);
                }

                //настройка колонок dgvActiveClaims
                Configuration.CreateDGVColumns(dgvActiveClaims, Configuration.GetValuesBySettingsName("ActiveClaimsColumnsSet"));
                try
                {
                    dgvActiveClaims.Columns["ClaimID"].Visible = false;
                }
                catch { }

                //Заполнение списка пар "строка, цвет", чтобы раскрашивать ячейки dgvActiveClaims
                SetDataToPaintDGV(ref dataToPaintDGV);

                //инициализация объекта класса Server
                server = new Server(Convert.ToInt32(Configuration.GetValuesBySettingsName("ThisServerPort")[0]));

                //инициализация объекта класса Client
                client = new Client(Client.ClientType.UserModule);

                //попытка запустить сервер на прослушивание указанного в файле конфигурации порта
                try
                {
                    server.Start(true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //присваивание значений полям объекта класса Client
                client.serverIP = serverIP;
                client.serverPort = serverPort;
                client.clientSideServerPort = server.port;
                //попытка подключиться к серверу
                try
                {
                    client.ConnectToServer(serverIP, serverPort);
                    try
                    {
                        NetMessage msg = new NetMessage(NetMessage.commandType.GetInfo);
                        client.SendMessage(msg, false);

                        usersDataRequest = true;
                        NetMessage usersDataRequestMsg = new NetMessage(NetMessage.commandType.GetInfo, "GetUsers");
                        client.SendMessage(usersDataRequestMsg, false);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                catch
                {
                    MessageBox.Show("Подключиться к серверу не удалось. Проверьте подключение");
                }
            }
            else
            {
                MessageBox.Show(Configuration.status.ToString());
            }
        }

        //Глобальные переменные
        List<string> AppList = new List<string>();
        List<string> EquipList = new List<string>();
        List<string> AnotherClaims = new List<string>();
        List<string> Rooms = new List<string>();
        string serverIP = "";
        int serverPort = 0;
        DataTable dt = new DataTable();

        Client client = null;
        Server server = null;

        bool usersDataRequest = false;
        DataTable usersData = new DataTable();
        int userID = -1;

        //Номер телефона, по которому следует обращаться в случае возникших проблем (указан в файле настроек)
        string phoneNum = "";

        //Список пар строк и цветов, в которые надо расскрасить строки (dgvActiveClaims.Rows)
        public List<StringAndColorPair> dataToPaintDGV = new List<StringAndColorPair>();

        public int GetLVSubitemIndexByName(ListView targetListView, string subItemName)
        {
            int result = -1;
            for (int i = 0; i < targetListView.Columns.Count; i++)
            {
                if (targetListView.Columns[i].Name == subItemName)
                {
                    result = i;
                }
            }
            return result;
        }

        //Удаление заявки
        public void DeleteClaim()
        {
            //если выбрана одна или более заявок
            if (dgvActiveClaims.SelectedRows.Count > 0)
            {
                //Создание сообщения
                NetMessage deleteClaimsMsg = new NetMessage(NetMessage.commandType.DeleteClaim);

                //формирование списка ID
                deleteClaimsMsg.dataToSend.Columns.Add("ClaimID");
                deleteClaimsMsg.dataToSend.Columns.Add("ClaimDeletionReceivedDate");
                deleteClaimsMsg.dataToSend.Columns.Add("ClaimDeletionReceivedTime");
                deleteClaimsMsg.dataToSend.Columns.Add("ClaimDeletedByID");
                deleteClaimsMsg.dataToSend.Columns.Add("ClaimStatus");

                for (int i = 0; i < dgvActiveClaims.SelectedRows.Count; i++)
                {
                    DataRow newRow = deleteClaimsMsg.dataToSend.NewRow();
                    newRow["ClaimID"] = Convert.ToInt32(dgvActiveClaims.SelectedRows[0].Cells["ClaimID"].Value);
                    newRow["ClaimDeletionReceivedDate"] = "CONVERT(VARCHAR(50), GETDATE(), 102)";
                    newRow["ClaimDeletionReceivedTime"] = "CONVERT(VARCHAR(50), GETDATE(), 8)";
                    newRow["ClaimDeletedByID"] = userID;
                    newRow["ClaimStatus"] = "Отменена";
                    deleteClaimsMsg.dataToSend.Rows.Add(newRow);
                }

                try
                {
                    client.SendMessage(deleteClaimsMsg, false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Необходимо выбрать по крайней мере одну заявку");
            }
        }

        //Получение версии Windows, IP, имени хоста и имени пользователя
        public void GetSystemInfo()
        {
            string IP = "";
            string OSVer = "";
            string hostName = "";
            string userName = "";

            hostName = System.Net.Dns.GetHostName();
            if (hostName == "")
            {
                hostName = "N/A";
            }
            IP = System.Net.Dns.GetHostByName(hostName).AddressList[0].ToString();
            if (IP == "")
            {
                IP = "N/A";
            }
            userName = System.Environment.UserName;
            if (userName == "")
            {
                userName = "N/A";
            }
            OSVer = System.Environment.OSVersion.VersionString;
            statusStrip1.Items[1].Text = hostName;
            statusStrip1.Items[2].Text = IP;
            statusStrip1.Items[3].Text = userName;
            statusStrip1.Items[0].Text = OSVer;
        }
        
        //Отправка заявки на сервер
        public void SendClaim()
        {
            NetMessage claimMessage = new NetMessage();
            claimMessage.command = NetMessage.commandType.AddClaim;
            string caption = "Отправка заявки";

            claimMessage.dataToSend.Columns.Add("ClaimSenderUserName");
            claimMessage.dataToSend.Columns.Add("ClaimSenderHostName");
            claimMessage.dataToSend.Columns.Add("ClaimSenderHostIP");
            claimMessage.dataToSend.Columns.Add("ClaimSenderRoom");
            claimMessage.dataToSend.Columns.Add("ClaimSenderName");
            claimMessage.dataToSend.Columns.Add("ClaimSenderPhone");
            claimMessage.dataToSend.Columns.Add("TypeOfIssue");
            claimMessage.dataToSend.Columns.Add("ClaimDiscription");
            claimMessage.dataToSend.Columns.Add("ClaimReceivedDate");
            claimMessage.dataToSend.Columns.Add("ClaimReceivedTime");
            claimMessage.dataToSend.Columns.Add("ClaimStatus");
            DataRow newRow = claimMessage.dataToSend.NewRow();
            

            if (cbStep1.SelectedIndex != 0)
            {
                if (cbStep1.Items[cbStep1.SelectedIndex].ToString().IndexOf("программ") != -1)
                {
                    if (cbStep1.Items[cbStep1.SelectedIndex].ToString().IndexOf("ребуется") != -1)
                    {
                        newRow["TypeOfIssue"] = "Требуется настройка программы " + cbStep2.Items[cbStep2.SelectedIndex].ToString();
                        newRow["ClaimSenderRoom"] = cbStep3.Items[cbStep3.SelectedIndex].ToString();
                        if (tbStep5.Text.IndexOf("Здесь Вы можете сообщить дополнительную информацию") == -1)
                        {
                            newRow["ClaimDiscription"] = tbStep5.Text;
                        }
                        else
                        {
                            
                        }
                    }
                    if (cbStep1.Items[cbStep1.SelectedIndex].ToString().IndexOf("работает") != -1)
                    {
                        newRow["TypeOfIssue"] = "Была обнаружена некорректная работы программы " + cbStep2.Items[cbStep2.SelectedIndex].ToString();
                        newRow["ClaimSenderRoom"] = cbStep3.Items[cbStep3.SelectedIndex].ToString();
                        if (tbStep5.Text.IndexOf("Здесь Вы можете сообщить дополнительную информацию") == -1)
                        {
                            newRow["ClaimDiscription"] = tbStep5.Text;
                        }
                        else
                        {
                            
                        }
                    }
                }
                if (cbStep1.Items[cbStep1.SelectedIndex].ToString().IndexOf("оборудов") != -1)
                {
                    if (cbStep1.Items[cbStep1.SelectedIndex].ToString().IndexOf("ребуется") != -1)
                    {
                        newRow["TypeOfIssue"] = "Требуется настроить " + cbStep2.Items[cbStep2.SelectedIndex].ToString().ToLower();
                        newRow["ClaimSenderRoom"] = cbStep3.Items[cbStep3.SelectedIndex].ToString();
                        if (tbStep5.Text.IndexOf("Здесь Вы можете сообщить дополнительную информацию") == -1)
                        {
                            newRow["ClaimDiscription"] = tbStep5.Text;
                        }
                        else
                        {

                        }
                    }
                    if (cbStep1.Items[cbStep1.SelectedIndex].ToString().IndexOf("работает") != -1)
                    {
                        newRow["TypeOfIssue"] = "Некорректно работает " + cbStep2.Items[cbStep2.SelectedIndex].ToString().ToLower();
                        newRow["ClaimSenderRoom"] = cbStep3.Items[cbStep3.SelectedIndex].ToString();
                        if (tbStep5.Text.IndexOf("Здесь Вы можете сообщить дополнительную информацию") == -1)
                        {
                            newRow["ClaimDiscription"] = tbStep5.Text;
                        }
                        else
                        {

                        }
                    }
                }
                if (cbStep1.Items[cbStep1.SelectedIndex].ToString().IndexOf("Иное") != -1)
                {
                    newRow["TypeOfIssue"] = cbStep2.Items[cbStep2.SelectedIndex].ToString();
                    newRow["ClaimSenderRoom"] = cbStep3.Items[cbStep3.SelectedIndex].ToString();
                    if (tbStep5.Text.IndexOf("Здесь Вы можете сообщить дополнительную информацию") == -1)
                    {
                        newRow["ClaimDiscription"] = tbStep5.Text;
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                newRow["TypeOfIssue"] = "Причину установить не удалось";
                newRow["ClaimSenderRoom"] = cbStep3.Items[cbStep3.SelectedIndex].ToString();
                if (tbStep5.Text.IndexOf("Здесь Вы можете сообщить дополнительную информацию") == -1)
                {
                    newRow["ClaimDiscription"] = tbStep5.Text;
                }
                else
                {

                }
            }

            newRow["ClaimSenderHostName"] = client.hostName;
            newRow["ClaimSenderUserName"] = client.userName;
            newRow["ClaimSenderName"] = tbName.Text;
            newRow["ClaimSenderPhone"] = tbPhone.Text;

            claimMessage.dataToSend.Rows.Add(newRow);

            string textForMessageBox = "Отправитель: " + claimMessage.dataToSend.Rows[0]["ClaimSenderName"] +
                "\nСодержание заявки: " + claimMessage.dataToSend.Rows[0]["TypeOfIssue"] + " (" + claimMessage.dataToSend.Rows[0]["ClaimSenderRoom"] + " комната, тел.: " + 
                claimMessage.dataToSend.Rows[0]["ClaimSenderPhone"] + ")" + 
                "\nДополнительные сведения: " + claimMessage.dataToSend.Rows[0]["ClaimDiscription"];
            if (MessageBox.Show(textForMessageBox, caption, MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    if (!client.connectedToServer)
                    {
                        client.ConnectToServer(serverIP, serverPort);
                    }
                    //Попытка отправки сообщения с заявкой на сервер. На сервере информация о заявке добавляется в базу данных.
                    //Заявка должна быть добавлена в активные заявки только в том случае, если информация была не только отправлена на сервер системы,
                    //но еще и добавлена в базу данных. Поэтому здесь добавлена конструкция try..catch, а в классе Client в методе SendMessage добавлен 
                    //метод throw exeption для случая, когда данные от модуля пользователя были получены на сервере, но не смогли быть добавлены
                    //в базу данных
                    this.client.SendMessage(claimMessage, false);

                    MessageBox.Show("Заявка была отправлена на сервер");
                    
                    //обнуление формы
                    tbStep5.Text = "";
                    cbStep1.SelectedIndex = -1;
                    cbStep2.SelectedIndex = -1;
                    cbStep3.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show("Отправить сообщение на сервер не удалось." +
                        "\n\n" + ex.Message + 
                        "\n\nПовторить попытку?", "Ошибка", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        SendClaim();
                    }
                    else
                    {
                        //обнуление формы
                        tbStep5.Text = "";
                        tbName.Text = "";
                        tbPhone.Text = "";
                        cbStep2.SelectedIndex = -1;
                        cbStep1.SelectedIndex = -1;
                        cbStep3.SelectedIndex = -1;
                    }
                }
            }
        }

        //делегат для заполнения lwAcitveClaims
        public delegate void lwActiveClaimsDeleagate(NetMessage data);

        //обработчик события сервера
        public void AddClaimsToDGV(NetMessage data)
        {
            if (dgvActiveClaims.InvokeRequired)
            {
                lwActiveClaimsDeleagate newDel = new lwActiveClaimsDeleagate(AddClaimsToDGV);
                this.Invoke(newDel, new object[] { data });
            }
            else
            {
                Configuration.AddDataToDGV(dgvActiveClaims, data.dataToSend, true, "ClaimID", "ClaimID");
                Configuration.PaintDGVCells(dgvActiveClaims, GetDGVColumnIndexToColorCells(dgvActiveClaims, "ClaimStatus"), dataToPaintDGV);

                if (dgvActiveClaims.Rows.Count > 0)
                {
                    dgvActiveClaims.Visible = true;
                    lNoActiveClaims.Visible = false;
                }
            }
        }

        //обработчик события клиента
        public void AddClientsInfoToDGV(NetMessage data)
        {
            dt = data.dataToSend;
            if (dgvActiveClaims.InvokeRequired)
            {
                lwActiveClaimsDeleagate newDel = new lwActiveClaimsDeleagate(AddClaimsToDGV);
                this.Invoke(newDel, new object[] { data });
            }
            else
            {
                if (!usersDataRequest)
                {
                    Configuration.AddDataToDGV(dgvActiveClaims, data.dataToSend, true, "ClaimID", "ClaimID");
                    Configuration.PaintDGVCells(dgvActiveClaims, GetDGVColumnIndexToColorCells(dgvActiveClaims, "ClaimStatus"), dataToPaintDGV);

                    if (dgvActiveClaims.Rows.Count > 0)
                    {
                        dgvActiveClaims.Visible = true;
                        lNoActiveClaims.Visible = false;
                    }
                }
                else
                {
                    usersDataRequest = false;
                    usersData = data.dataToSend;
                    for (int i = 0; i < usersData.Rows.Count; i++)
                    {
                        if (usersData.Rows[i]["PersFirstName"].ToString() == "Пользователь")
                        {
                            userID = Convert.ToInt32(usersData.Rows[i]["PersID"]);
                        }
                    }
                }
            }
        }

        //Заполнение списка пар строк и цветов для раскрашивания dgvActiveClaims 
        public void SetDataToPaintDGV(ref List<StringAndColorPair> targetList)
        {
            StringAndColorPair pair = new StringAndColorPair();
            pair.stringToFind = "Ожидает назначения ответственной группы";
            pair.colorToSet = Color.Red;
            targetList.Add(pair);

            pair.stringToFind = "Ожидает назначения ответственного исполнителя";
            pair.colorToSet = Color.Orange;
            targetList.Add(pair);

            pair.stringToFind = "Ожидает выполнения";
            pair.colorToSet = Color.Yellow;
            targetList.Add(pair);

            pair.stringToFind = "Выполняется";
            pair.colorToSet = Color.LightBlue;
            targetList.Add(pair);

            pair.stringToFind = "Выполнена";
            pair.colorToSet = Color.LightGreen;
            targetList.Add(pair);

            pair.stringToFind = "Удалена";
            pair.colorToSet = Color.MediumPurple;
            targetList.Add(pair);

            pair.stringToFind = "Отменена";
            pair.colorToSet = Color.MediumPurple;
            targetList.Add(pair);
        }

        //Вычисление индекса столбца dgvActiveClaims со строками, по которым определяется цвет ячеек
        public int GetDGVColumnIndexToColorCells(DataGridView targetDGV, string columnName)
        {
            int index = -1;
            for (int dgvCol = 0; dgvCol < targetDGV.Columns.Count; dgvCol++)
            {
                if (targetDGV.Columns[dgvCol].Name == columnName)
                {
                    index = dgvCol;
                    return index;
                }
            }
            return index;
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            //растяжение вкладок по ширине окна
            tabControl1.ItemSize = new Size((this.Size.Width - 30) / tabControl1.TabPages.Count, 50);
            //растяжение элементов статус-бара по ширине окна
            for (int i = 0; i < 4; i++)
            {
                statusStrip1.Items[i].Size = new Size(this.Size.Width / 4 - 10, statusStrip1.Items[i].Size.Height);
            }
            //центровка lSendClaim
            lSendClaim.Left = this.Width / 2 - lSendClaim.Width / 2;

            //центровка lNoActiveClaims
            lNoActiveClaims.Top =  this.Height / 2 - lNoActiveClaims.Height - 50;
            lNoActiveClaims.Left = this.Width / 2 - lNoActiveClaims.Width / 2;
        }

        private void lSendClaim_MouseEnter(object sender, EventArgs e)
        {
            lSendClaim.Font = new Font(lSendClaim.Font.FontFamily, lSendClaim.Font.Size, FontStyle.Underline);
            lSendClaim.ForeColor = Color.Blue;
        }

        private void lSendClaim_MouseLeave(object sender, EventArgs e)
        {
            lSendClaim.Font = new Font(lSendClaim.Font.FontFamily, lSendClaim.Font.Size, FontStyle.Regular);
            lSendClaim.ForeColor = Color.Black;
        }

        //при изменении выбранного элемента в cbStep1 обнуляется содержимое cbStep2
        //и заполняется в соответствии с тем, что выбрано в cbStep1
        private void cbStep1_SelectedIndexChanged(object sender, EventArgs e)
        {
            gbStep2.Enabled = false;
            gbStep3.Enabled = false;
            gbStep4.Enabled = false;
            gbStep5.Enabled = false;
            cbStep3.SelectedIndex = -1;
            cbStep2.SelectedIndex = -1;
            cbStep2.Items.Clear();
            tbName.Text = "";
            tbPhone.Text = "";

            if (cbStep1.SelectedIndex != -1)
            {
                if (cbStep1.SelectedIndex != 0)
                {
                    gbStep2.Enabled = true;
                    if (cbStep1.Items[cbStep1.SelectedIndex].ToString().IndexOf("программ") != -1)
                    {
                        foreach (string curStr in AppList)
                        {
                            cbStep2.Items.Add(curStr);
                        }
                        lStep2Tip.Text = "Укажите оборудование/программу";
                    }
                    if (cbStep1.Items[cbStep1.SelectedIndex].ToString().IndexOf("оборудован") != -1)
                    {
                        foreach (string curStr in EquipList)
                        {
                            cbStep2.Items.Add(curStr);
                        }
                        lStep2Tip.Text = "Укажите оборудование/программу";
                    }
                    if (cbStep1.Items[cbStep1.SelectedIndex].ToString().IndexOf("программ") == -1 && cbStep1.Items[cbStep1.SelectedIndex].ToString().IndexOf("оборудован") == -1)
                    {
                        foreach (string curStr in AnotherClaims)
                        {
                            cbStep2.Items.Add(curStr);
                        }
                        lStep2Tip.Text = "Уточните заявку";
                    }
                }
                if (cbStep1.SelectedIndex == 0)
                {
                    gbStep2.Enabled = false;
                    cbStep2.SelectedIndex = -1;
                    gbStep3.Enabled = true;
                    gbStep5.Enabled = true;
                    if ((cbStep3.SelectedIndex != -1))
                    {
                        lSendClaim.Visible = true;
                    }
                }
            }
            else
            {
                gbStep2.Enabled = false;
                gbStep3.Enabled = false;
            }
        }

        private void cbStep2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStep2.SelectedIndex != -1)
            {
                gbStep3.Enabled = true;
            }
            else
            {
                gbStep3.Enabled = false;
            }
            if (cbStep1.SelectedIndex != -1 && cbStep2.SelectedIndex != -1 && cbStep3.SelectedIndex != -1)
            {
                lSendClaim.Visible = true;
            }
            else
            {
                lSendClaim.Visible = false;
            }
        }

        private void cbStep3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cbStep1.SelectedIndex == 0) || ((cbStep3.SelectedIndex != -1) && (cbStep2.SelectedIndex != -1)))
            {
                gbStep4.Enabled = true;               
            }
            else
            {
                gbStep4.Enabled = false;
            }
        }

        private void lSendClaim_Click(object sender, EventArgs e)
        {
            SendClaim();
        }

        private void cbStep3_TextChanged(object sender, EventArgs e)
        {
            lSendClaim.Visible = false;
            gbStep4.Enabled = false;
            for (int i = 0; i < cbStep3.Items.Count; i++)
            {
                if (cbStep3.Text == cbStep3.Items[i].ToString())
                {
                    cbStep3.SelectedIndex = i;
                    break;
                }
            }
        }

        private void tbStep4_EnabledChanged(object sender, EventArgs e)
        {
            if (tbStep5.Enabled && !tbStep5.Focused)
            {
                Color cGrey = Color.Gray;
                tbStep5.ForeColor = cGrey;
                tbStep5.Text = //"Здесь Вы можете указать свою фамилию, уточнить детали выявленных неисправностей или сообщить "
                "Здесь Вы можете сообщить дополнительную информацию о выявленных неисправностях, а также указать Вашу фамилию, наименование подразделения или рабочие часы, чтобы сотрудники, устраняющие неисправность, смогли сориентироваться по месту и времени";
            }
            else
            {
                tbStep5.Clear();
                tbStep5.ForeColor = Color.Black;
            }
        }

        private void tbStep4_Enter(object sender, EventArgs e)
        {
            if (tbStep5.Text.IndexOf("Здесь Вы можете сообщить дополнительную информацию") != -1)
            {
                tbStep5.ForeColor = Color.Black;
                tbStep5.Clear();
            }
        }

        private void cbStep2_TextChanged(object sender, EventArgs e)
        {
            lSendClaim.Visible = false;
        }

        private void cbStep1_TextChanged(object sender, EventArgs e)
        {
            lSendClaim.Visible = false;
        }

        private void tbStep4_Leave(object sender, EventArgs e)
        {
            if (tbStep5.Text == "")
            {
                tbStep5.ForeColor = Color.Gray;
                tbStep5.Text = "Здесь Вы можете сообщить дополнительную информацию о выявленных неисправностях, а также указать Вашу фамилию, наименование подразделения или рабочие часы, чтобы сотрудники, устраняющие неисправность, смогли сориентироваться по месту и времени";
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client.connectedToServer)
            {
                try
                {
                    client.DisconectFromServer();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Отключиться от сервера не удалось.\n" + ex.Message);
                }
            }
        }

        private void lwActiveClaims_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                activeClaimsContextMenu.Show(MousePosition);
            }
        }

        private void deleteClaim_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить заявку?", "Подтверждение удаления заявки", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DeleteClaim();
            }
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            if (cbStep1.SelectedIndex == 0)
            {
                if (tbName.Text.Length > 0 && tbPhone.Text.Length > 0)
                {
                    lSendClaim.Visible = true;
                }
                else
                {
                    lSendClaim.Visible = false;
                }
            }
            else
            {
                if (tbName.Text.Length > 0 && tbPhone.Text.Length > 0)
                {
                    lSendClaim.Visible = true;
                    gbStep5.Enabled = true;
                }
                else
                {
                    lSendClaim.Visible = false;
                    gbStep5.Enabled = false;
                }
            }
        }

        private void tbPhone_TextChanged(object sender, EventArgs e)
        {
            if (cbStep1.SelectedIndex == 0)
            {
                if (tbName.Text.Length > 0 && tbPhone.Text.Length > 0)
                {
                    lSendClaim.Visible = true;
                }
                else
                {
                    lSendClaim.Visible = false;
                }
            }
            else
            {
                if (tbName.Text.Length > 0 && tbPhone.Text.Length > 0)
                {
                    lSendClaim.Visible = true;
                    gbStep5.Enabled = true;
                }
                else
                {
                    lSendClaim.Visible = false;
                    gbStep5.Enabled = false;
                }
            }
        }

        private void dgvActiveClaims_Leave(object sender, EventArgs e)
        {
            if (dgvActiveClaims.SelectedRows.Count > 0)
            {
                dgvActiveClaims.SelectedRows[0].Selected = false;
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (dgvActiveClaims.SelectedRows.Count > 0)
            {
                dgvActiveClaims.SelectedRows[0].Selected = false;
            }
        }

        private void dgvActiveClaims_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                activeClaimsContextMenu.Show(MousePosition);
            }
        }
    }
}
