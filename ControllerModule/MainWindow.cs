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
using System.Diagnostics;
using System.IO;

namespace ControllerModule
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Server.MessageRecieved += new Server.MessageWatcher(getDataFromServer);
            Client.AnswerRecievedFromServer += new Client.AnswerReciever(getDataFromServer);
            Configuration.GetSettings();
            if (Configuration.status == Configuration.GetSettingsResult.OK)
            {
                //Если выбрана опция проверки доступных обновлений
                if (Configuration.GetValuesBySettingsName("CheckUpdates")[0] == "true")
                {
                    //получение директории-источника обновлений
                    string updatesSource = Configuration.GetValuesBySettingsName("UpdatesSource")[0];
                    
                    //получение PID текущего процесса, чтобы ModulesUpdater при необходимости смог его завершить
                    int processID = Process.GetCurrentProcess().Id;

                    //string argString = processID.ToString() + " " + updatesSource + " " + Process.GetCurrentProcess().MainModule.FileName;
                    
                    //создание и запуск процесса ModulesUpdater.exe
                    System.Diagnostics.Process updater = new Process();
                    updater.StartInfo.FileName = @"ModulesUpdater.exe";
                    updater.StartInfo.Arguments = "/pid " + processID.ToString() + " /source " + updatesSource + " /proc " + Process.GetCurrentProcess().MainModule.FileName;
                    updater.Start();
                    updater.WaitForExit();

                    //Если был обновлен и сам ModulesUpdater, выполняются процедуры по замене старого файла на новый
                    FileInfo newUpdaterFile = new FileInfo("newModulesUpdater.exe");
                    if (newUpdaterFile.Exists)
                    {
                        bool unableToDelete = true;
                        int numberOfAttempts = 0;
                        while (unableToDelete && numberOfAttempts < 10000) //чтобы не уйти в бесконечный цикл
                        {
                            try
                            {
                                numberOfAttempts++;
                                newUpdaterFile.CopyTo("ModulesUpdater.exe", true);
                                newUpdaterFile.Delete();
                                unableToDelete = false;
                            }
                            catch { }
                        }
                    }
                }

                Configuration.CreateListView(lvActiveClaims, Configuration.GetValuesBySettingsName("IncomingClaimsColumnsSet"), 12);
                server = new Server(Convert.ToInt32(Configuration.GetValuesBySettingsName("ThisServerPort")[0]));
                try
                {
                    server.Start(true);
                }
                catch
                {
                    MessageBox.Show("Не удалось запустить сервер");
                }

                client = new Client(Client.ClientType.ControllerModule);
                client.clientSideServerPort = server.port;
                client.serverIP = Configuration.GetValuesBySettingsName("ServerIP")[0];
                client.serverPort = Convert.ToInt32(Configuration.GetValuesBySettingsName("ServerPort")[0]);


                object myObject = new object();
                EventArgs e = new EventArgs();
                tsbSelectUser_Click(myObject, e);
            }
            else
            {
                MessageBox.Show(Configuration.status.ToString());
            }
        }

        void MainForm_lvAcitveClaimsItemsCountIncreased()
        {
            notificationForm = new NewClaimNotification();
            notificationForm.Show();
        }

        public static Server server = null;
        public static Client client = null;

        //в этой переменной хранится получаемая по сети информация
        public static DataTable receivedData = new DataTable();

        //в этой переменной хранится список активных заявок, отображаемый в lvActiveClaims
        //public static DataTable activeClaims = new DataTable();
        //в этой переменной хранится ID выбранной на исполнение заявки(ок) группы
        public static int selectedGroupID;

        //переменная формы для логина (LoginForm)
        LoginForm loginForm = new LoginForm();

        //Переменная формы для отображения на экране сообщения о новой поступившей заявке
        NewClaimNotification notificationForm = new NewClaimNotification();

        //Переменная хранит кол-во активных заявок, в т.ч. выполненных, 
        //Необходима для работы метода ShowNotification
        int prevClaimsCount = 0;

        //Отображает на экране сообщение о поступлении новой заявки
        bool firstTimeDataReceivedAfterLogin = false;
        public delegate void ShowNotificationDelegate();
        public void ShowNotificationAndPlaySound()
        {
            //Подсчитывается кол-во заявок в ListView.
            //Т.к. этот метод вызывается в методе GetDataFromServer после метода AddClaimsToListView,
            //будут посчитаны заявки, включая те, что были получены (если они были получены) с только что
            //принятым сообщением от сервера
            int currentClaimsCount = lvActiveClaims.Items.Count;
 
            //сравниваются значения prevClaimsCount и currentClaimsCount.
            //Если currentClaimsCount оказывается больше prevClaimsCount, на экране отображается сообщение
            //о поступлении новой заявки
            if (currentClaimsCount > prevClaimsCount)
            {
                //проигрывание звукового оповещения
                if (Configuration.GetValuesBySettingsName("AudioNotification")[0] != "0")
                {
                    if (Configuration.GetValuesBySettingsName("AudioFile")[0] != "")
                    {
                        System.Media.SoundPlayer soundPlayer = new System.Media.SoundPlayer(Configuration.GetValuesBySettingsName("AudioFile")[0]);
                        soundPlayer.Play();
                    }
                }

                if (this.InvokeRequired)
                {
                    ShowNotificationDelegate snd = new ShowNotificationDelegate(ShowNotificationAndPlaySound);
                    this.Invoke(snd);
                }
                else
                {
                    //показ визуального оповещения
                    notificationForm = new NewClaimNotification();
                    notificationForm.Show();
                }
            }

            //после чего значению prevClaimsCount присваивается значение currentClaimsCount
            //для дальнейших сравнений
            prevClaimsCount = currentClaimsCount;
        }
        public void CloseNotification()
        {
            if (this.InvokeRequired)
            {
                ShowNotificationDelegate snd = new ShowNotificationDelegate(CloseNotification);
                this.Invoke(snd);
            }
            else
            {
                notificationForm.Close();
            }
        }

        //индикатор, что была нажата кнопка получения списка ответственных групп
        public static bool getGroupsButtonClick = false;

        //индикатор, что была нажата кнопка получения списка сотрудников
        public static bool getUsersButtonClick = false;

        //индикатор, что была нажата кнопка смены пароля
        public static bool changePswdButtonClick = false;

        //true, если была произведена процедура логина
        static bool  logined = false;

        //когда открывается форма для просмотра отчета, осуществляется запрос к БД (непосредственно в момент инициализации самой формы),
        //чтобы получить список столбоцов представления ClaimsView
        static public bool getReportColumnsRequest = false;

        //индикатор, что была нажата кнопка просмотра отчета
        public static bool viewReportButtonClicked = false;

        //переменная, в которой хранится информация о всех пользователях, полученных с сервера
        public static DataTable usersData = new DataTable();

        //переменная, в которой хранится информация об активном пользователе
        public static DataTable currentUserData = new DataTable();

        //переменная, в которой хранится информация о группах
        public static DataTable groupsData = new DataTable();

        delegate void SetDataToWinCtrl(NetMessage msg);
        public void addClaimToListView(NetMessage data)
        {
            if (lvActiveClaims.InvokeRequired)
            //if (dgvIncomingClaims.InvokeRequired)
            {
                SetDataToWinCtrl sdtwc = new SetDataToWinCtrl(addClaimToListView);
                this.Invoke(sdtwc, new object[] { data });
            }
            else
            {
                if (!getGroupsButtonClick)
                {
                    Configuration.AddDataToListView(lvActiveClaims, data.dataToSend);
                }
            }
        }
        public void calculateActiveClaimsAmount(NetMessage data)
        {
            if (lActiveClaimsCount.InvokeRequired)
            {
                SetDataToWinCtrl sdtwc = new SetDataToWinCtrl(calculateActiveClaimsAmount);
                this.Invoke(sdtwc, new object[] { data });
            }
            else
            {
                int activeClaimsCounter = 0;
                int claimStatusColumnIndex = -1;
                for (int col = 0; col < lvActiveClaims.Columns.Count; col++)
                {
                    if (lvActiveClaims.Columns[col].Name == "ClaimStatus")
                    {
                        claimStatusColumnIndex = col;
                    }
                }
                if (claimStatusColumnIndex != -1)
                {
                    foreach (ListViewItem curLVI in lvActiveClaims.Items)
                    {
                        if (curLVI.SubItems[claimStatusColumnIndex].Text != "Выполнена")
                        {
                            activeClaimsCounter++;
                        }
                    }
                }
                lActiveClaimsCount.Text = activeClaimsCounter.ToString();
            }
        }

        public void getDataFromServer(NetMessage data)
        {
            //если входящее сообщение содержит в поле text строку "reportClaimsCount",
            //следовательно, это второе сообщение из двух, отправленных сервером, 
            //в качестве ответа на запрос на получение отчета. Это сообщение содержит таблицу 
            //с перечислением всех типов заявок и указания их количества в запрошенном отчете
            if (data.text == "reportClaimsCount")
            {
                ReportForm.reportClaimsCount = data.dataToSend;
                MainForm.viewReportButtonClicked = false;
            }
            //в противном случае, входящее сообщение обрабатывается стандартным способом
            else
            {
                receivedData = data.dataToSend;

                if (!getGroupsButtonClick && !getUsersButtonClick && !viewReportButtonClicked && !getReportColumnsRequest && !changePswdButtonClick)
                {
                    addClaimToListView(data);

                    //если это не получение данных сразу после того, как был выполнен вход новым пользователем,
                    //тогда следует выполнить проверку на наличие новых поступивших заявок.
                    //значение true устанавливается в методе tsbSelectUser_Click.
                    if (!firstTimeDataReceivedAfterLogin)
                    {
                        ShowNotificationAndPlaySound();
                    }
                    else
                    {
                        prevClaimsCount = lvActiveClaims.Items.Count;
                    }
                    calculateActiveClaimsAmount(data);
                    firstTimeDataReceivedAfterLogin = false;
                }
            }
        }

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

        public void DeleteClaim()
        {
            //если выбрана одна или более заявок
            if (lvActiveClaims.SelectedItems.Count > 0)
            {
                //Создание сообщения
                NetMessage deleteClaimsMsg = new NetMessage(NetMessage.commandType.DeleteClaim);

                //формирование списка ID
                deleteClaimsMsg.dataToSend.Columns.Add("ClaimID");
                deleteClaimsMsg.dataToSend.Columns.Add("ClaimDeletionReceivedDate");
                deleteClaimsMsg.dataToSend.Columns.Add("ClaimDeletionReceivedTime");
                deleteClaimsMsg.dataToSend.Columns.Add("ClaimDeletedByID");
                deleteClaimsMsg.dataToSend.Columns.Add("ClaimStatus");

                for (int i = 0; i < lvActiveClaims.SelectedItems.Count; i++)
                {
                    DataRow newRow = deleteClaimsMsg.dataToSend.NewRow();
                    newRow["ClaimID"] = Convert.ToInt32(lvActiveClaims.SelectedItems[i].SubItems[GetLVSubitemIndexByName(lvActiveClaims, "ClaimID")].Text);
                    newRow["ClaimDeletionReceivedDate"] = "CONVERT(VARCHAR(50), GETDATE(), 102)";
                    newRow["ClaimDeletionReceivedTime"] = "CONVERT(VARCHAR(50), GETDATE(), 8)";
                    newRow["ClaimDeletedByID"] = currentUserData.Rows[0]["PersID"];
                    newRow["ClaimStatus"] = "Удалена контролером";
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

        public void AddClaim()
        {
            //Проверка, что пользователь авторизовался
            if (logined)
            {
                //создание экземпляра формы
                ClaimChangingForm ccf = new ClaimChangingForm();
                
                //инициализация некоторых параметров
                ccf.lClaimID.Text = "N/A";
                ccf.lClaimDate.Text = "N/A";
                ccf.cbExecGroupsList.Enabled = false;

                if (ccf.ShowDialog() == DialogResult.OK)
                {
                    //если заполнены хотя бы основные сведения
                    if (
                        ccf.tbClaim.Text.Length > 0 &&
                        ccf.tbRoom.Text.Length > 0 &&
                        ccf.tbSenderName.Text.Length > 0 &&
                        ccf.tbSenderPhone.Text.Length > 0
                       )
                    {
                        //Создание сообщения с информацией о новой заявке для отправки на сервер
                        NetMessage newClaimMsg = new NetMessage(NetMessage.commandType.AddClaim);

                        //формирование столбцов, а затем и строк для хранения информации о создаваемой заявке
                        newClaimMsg.dataToSend.Columns.Add("ClaimSenderUserName");
                        newClaimMsg.dataToSend.Columns.Add("ClaimSenderHostName");
                        newClaimMsg.dataToSend.Columns.Add("ClaimSenderHostIP");
                        newClaimMsg.dataToSend.Columns.Add("ClaimSenderRoom");
                        newClaimMsg.dataToSend.Columns.Add("ClaimSenderName");
                        newClaimMsg.dataToSend.Columns.Add("ClaimSenderPhone");
                        newClaimMsg.dataToSend.Columns.Add("TypeOfIssue");
                        newClaimMsg.dataToSend.Columns.Add("ClaimDiscription");
                        newClaimMsg.dataToSend.Columns.Add("ClaimReceivedDate");
                        newClaimMsg.dataToSend.Columns.Add("ClaimReceivedTime");
                        newClaimMsg.dataToSend.Columns.Add("ClaimStatus");

                        DataRow newRow = newClaimMsg.dataToSend.NewRow();

                        newRow["ClaimSenderUserName"] = client.userName;
                        newRow["ClaimSenderHostName"] = client.hostName;
                        newRow["ClaimSenderHostIP"] = client.IP;
                        newRow["ClaimSenderRoom"] = ccf.tbRoom.Text;
                        newRow["ClaimSenderName"] = ccf.tbSenderName.Text;
                        newRow["ClaimSenderPhone"] = ccf.tbSenderPhone.Text;
                        newRow["TypeOfIssue"] = ccf.tbClaim.Text;
                        newRow["ClaimDiscription"] = ccf.tbAddInfo.Text;

                        newClaimMsg.dataToSend.Rows.Add(newRow);

                        try
                        {
                            client.SendMessage(newClaimMsg, false);
                            MessageBox.Show("Заявка успешно создана");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Не удалось создать заявку или отправить ее на сервер\n" + ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Для создания заявки необходимо заполнить:\n- Имя заявителя\n- Комнату\n- Телефон\n- Текст заявки");
                        AddClaim();
                    }
                }
            }
        }

        public int GetGroupIDByName(string groupName, DataTable groupsData, string groupNameColumnName, string idColumnName)
        {
            for (int i = 0; i < groupsData.Rows.Count; i++)
            {
                if (groupsData.Rows[i][groupNameColumnName].ToString() == groupName)
                {
                    return Convert.ToInt32(groupsData.Rows[i][idColumnName]);
                }
            }
            return -1;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
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

        //Запрос списка ответственных за исполнение заявок групп
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //если выбраны 1 или более заявок
            if (lvActiveClaims.SelectedItems.Count > 0)
            {
                //проверка условия, что среди выбранных заявок нет заявки со статусом "Выполнена"
                int statusColumnIndex = -1;
                for (int col = 0; col < lvActiveClaims.Columns.Count; col++)
                {
                    if (lvActiveClaims.Columns[col].Name == "ClaimStatus")
                    {
                        statusColumnIndex = col;
                    }
                }
                bool executedClaimSelected = false;
                foreach (ListViewItem curSelLVI in lvActiveClaims.SelectedItems)
                {
                    if (curSelLVI.SubItems[statusColumnIndex].Text == "Выполнена")
                    {
                        executedClaimSelected = true;
                    }
                }
                //если среди выбранных заявок отсутствует заявка со статусом "Выполнена",
                //можно приступить к проверке у выбранных заявок наличия исполнителей, которые были назначены ранее
                //для того, чтобы предупредить пользователя о возможно ошибочных действиях
                if (!executedClaimSelected)
                {
                    //проверка заявок на наличие у них назначенных ответственных исполнителях
                    //В этом случае, статус заявки - "Ожидает выполнения"
                    bool claimWithExecSelected = false;
                    foreach (ListViewItem curSelLVI in lvActiveClaims.SelectedItems)
                    {
                        if (curSelLVI.SubItems[statusColumnIndex].Text == "Ожидает выполнения")
                        {
                            claimWithExecSelected = true;
                        }
                    }

                    //если среди выбранных заявок есть заявка, у которой есть отвественный исполнитель,
                    //необходимо предупредить пользователя о возможно ошибочных действиях
                    if (claimWithExecSelected)
                    {
                        if (MessageBox.Show("Среди выбраннных заявок есть заявка с назначенным ответственным исполнителем\nВы уверены, что хотите переназначить группу?", "Внимание!", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {

                            //значение переменной-маркера для метода getDataFromServer(NetMessage data)
                            //устанавливается в true
                            getGroupsButtonClick = true;

                            if (client.connectedToServer)
                            {
                                try
                                {
                                    //Запрос списка групп
                                    NetMessage getGroupsListMsg = new NetMessage(NetMessage.commandType.GetInfo, "GetGroups");
                                    client.SendMessage(getGroupsListMsg, false);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                            else
                            {
                                try
                                {
                                    //тот же запрос списка групп, только с коннектом к серверу, в случае, если
                                    //клиент оказался не подключен
                                    lvActiveClaims.Clear();
                                    client.ConnectToServer(client.serverIP, client.serverPort);
                                    NetMessage msg = new NetMessage(NetMessage.commandType.GetInfo);
                                    client.SendMessage(msg, false);
                                    try
                                    {
                                        NetMessage getGroupsListMsg = new NetMessage(NetMessage.commandType.GetInfo, "GetGroups");
                                        client.SendMessage(getGroupsListMsg, false);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }

                            groupsData = receivedData;
                            
                            //заполнение cbExecutorsList
                            ExecSelectionForm execSelForm = new ExecSelectionForm();
                            execSelForm.cbExecutorsList.Items.Clear();
                            for (int row = 0; row < groupsData.Rows.Count; row++)
                            {
                                execSelForm.cbExecutorsList.Items.Add(groupsData.Rows[row]["GroupName"]);
                            }

                            if (execSelForm.ShowDialog() == DialogResult.OK)
                            {
                                //создание сообщения для отправки на сервер
                                NetMessage updateClaims = new NetMessage(NetMessage.commandType.UpdateClaim);
                                //формирование DataTable
                                updateClaims.dataToSend.Columns.Add("ClaimID");
                                updateClaims.dataToSend.Columns.Add("ExecGroupID");
                                updateClaims.dataToSend.Columns.Add("ClaimStatus");
                                updateClaims.dataToSend.Columns.Add("ExecGroupOrdererID");
                                //эти поля нужно обнулить, т.к. одна из заявок уже была назначена какому-то исполнителю
                                updateClaims.dataToSend.Columns.Add("ExecID");
                                updateClaims.dataToSend.Columns.Add("ExecOrdererID");
                                updateClaims.dataToSend.Columns.Add("ExecOrderReceivedDate");
                                updateClaims.dataToSend.Columns.Add("ExecOrderReceivedTime");
                                updateClaims.dataToSend.Columns.Add("ClaimExecStartReceivedDate");
                                updateClaims.dataToSend.Columns.Add("ClaimExecStartReceivedTime");
                                //присваиваемый заявке статус
                                string status = "Ожидает назначения ответственного исполнителя";

                                int selectedIndex = execSelForm.cbExecutorsList.SelectedIndex;
                                //int selectedGroupID = Convert.ToInt32(receivedData.Rows[selectedIndex]["GroupID"]);
                                int selectedGroupID = GetGroupIDByName(execSelForm.cbExecutorsList.Items[execSelForm.cbExecutorsList.SelectedIndex].ToString(), groupsData, "GroupName", "GroupID");

                                //Вычисление номера столбца lvActiveClaims, в котором хранятся ID заявок
                                int idIndex = -1;
                                for (int i = 0; i < lvActiveClaims.Columns.Count; i++)
                                {
                                    if (lvActiveClaims.Columns[i].Name == "ClaimID")
                                    {
                                        idIndex = i;
                                    }
                                }

                                //Заполнение DataTable
                                foreach (ListViewItem curlvi in lvActiveClaims.SelectedItems)
                                {
                                    DataRow dr = updateClaims.dataToSend.NewRow();
                                    dr["ClaimID"] = curlvi.SubItems[idIndex].Text;
                                    dr["ExecGroupID"] = selectedGroupID;
                                    dr["ClaimStatus"] = status;
                                    dr["ExecGroupOrdererID"] = currentUserData.Rows[0]["PersID"];
                                    //обнуление
                                    dr["ExecID"] = DBNull.Value;
                                    dr["ExecOrdererID"] = DBNull.Value;
                                    dr["ExecOrderReceivedDate"] = DBNull.Value;
                                    dr["ExecOrderReceivedTime"] = DBNull.Value;
                                    dr["ClaimExecStartReceivedDate"] = DBNull.Value;
                                    dr["ClaimExecStartReceivedTime"] = DBNull.Value;
                                    updateClaims.dataToSend.Rows.Add(dr);
                                }

                                try
                                {
                                    client.SendMessage(updateClaims, false);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }

                                //снятие флага, что была нажата кнопка выбора группы
                                getGroupsButtonClick = false;

                            }
                        }
                    }
                    else
                    {
                        //Тот же самый код


                        //значение переменной-маркера для метода getDataFromServer(NetMessage data)
                        //устанавливается в true
                        getGroupsButtonClick = true;
                        if (client.connectedToServer)
                        {
                            try
                            {
                                //Запрос списка групп
                                NetMessage getGroupsListMsg = new NetMessage(NetMessage.commandType.GetInfo, "GetGroups");
                                client.SendMessage(getGroupsListMsg, false);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        else
                        {
                            try
                            {
                                //тот же запрос списка групп, только с коннектом к серверу, в случае, если
                                //клиент оказался не подключен
                                lvActiveClaims.Clear();
                                client.ConnectToServer(client.serverIP, client.serverPort);
                                NetMessage msg = new NetMessage(NetMessage.commandType.GetInfo);
                                client.SendMessage(msg, false);
                                try
                                {
                                    NetMessage getGroupsListMsg = new NetMessage(NetMessage.commandType.GetInfo, "GetGroups");
                                    client.SendMessage(getGroupsListMsg, false);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }

                        groupsData = receivedData;

                        //заполнение cbExecutorsList
                        ExecSelectionForm execSelForm = new ExecSelectionForm();
                        execSelForm.cbExecutorsList.Items.Clear();
                        for (int row = 0; row < groupsData.Rows.Count; row++)
                        {
                            execSelForm.cbExecutorsList.Items.Add(groupsData.Rows[row]["GroupName"]);
                        }

                        if (execSelForm.ShowDialog() == DialogResult.OK)
                        {
                            //создание сообщения для отправки на сервер
                            NetMessage updateClaims = new NetMessage(NetMessage.commandType.UpdateClaim);
                            //формирование DataTable
                            updateClaims.dataToSend.Columns.Add("ClaimID");
                            updateClaims.dataToSend.Columns.Add("ExecGroupID");
                            updateClaims.dataToSend.Columns.Add("ClaimStatus");
                            updateClaims.dataToSend.Columns.Add("ExecGroupOrdererID");
                            //присваиваемый заявке статус
                            string status = "Ожидает назначения ответственного исполнителя";

                            int selectedIndex = execSelForm.cbExecutorsList.SelectedIndex;
                            //int selectedGroupID = Convert.ToInt32(receivedData.Rows[selectedIndex]["GroupID"]);
                            int selectedGroupID = GetGroupIDByName(execSelForm.cbExecutorsList.Items[execSelForm.cbExecutorsList.SelectedIndex].ToString(), groupsData, "GroupName", "GroupID");

                            //Вычисление номера столбца lvActiveClaims, в котором хранятся ID заявок
                            int idIndex = -1;
                            for (int i = 0; i < lvActiveClaims.Columns.Count; i++)
                            {
                                if (lvActiveClaims.Columns[i].Name == "ClaimID")
                                {
                                    idIndex = i;
                                }
                            }

                            //Заполнение DataTable
                            foreach (ListViewItem curlvi in lvActiveClaims.SelectedItems)
                            {
                                DataRow dr = updateClaims.dataToSend.NewRow();
                                dr["ClaimID"] = curlvi.SubItems[idIndex].Text;
                                dr["ExecGroupID"] = selectedGroupID;
                                dr["ClaimStatus"] = status;
                                dr["ExecGroupOrdererID"] = currentUserData.Rows[0]["PersID"];
                                updateClaims.dataToSend.Rows.Add(dr);
                            }

                            try
                            {
                                client.SendMessage(updateClaims, false);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Среди выбранных заявок есть заявка со статусом \"Выполнена\". Назначение ответственной группы невозможно");
                }
            }
            else
            {
                MessageBox.Show("Выберите как минимум одну заявку для назначения ответственной группы");
            }
            //снятие флага, что была нажата кнопка выбора группы
            getGroupsButtonClick = false;
        }

        //запрос списка сотрудников для "аутентификации"
        private void tsbSelectUser_Click(object sender, EventArgs e)
        {
            getUsersButtonClick = true;

            //запрос на сервер, формирование переменной usersData, заполнение cbUsersList значениями
            try
            {
                NetMessage msg = new NetMessage(NetMessage.commandType.GetInfo, "GetUsers");
                MainForm.client.SendMessage(msg, false);

                usersData = MainForm.receivedData;

                DataView usersDataView = new DataView();
                usersDataView = usersData.DefaultView;
                usersDataView.Sort = "PersLastName asc";
                usersData = usersDataView.ToTable();

                loginForm.cbUsersList.Items.Clear();
                for (int rowCounter = 0; rowCounter < MainForm.usersData.Rows.Count; rowCounter++)
                {
                    if (usersData.Rows[rowCounter]["PersID"].ToString() != (24).ToString())
                    {
                        loginForm.cbUsersList.Items.Add(MainForm.usersData.Rows[rowCounter]["PersLastName"] + " " + MainForm.usersData.Rows[rowCounter]["PersFirstName"] + " " + MainForm.usersData.Rows[rowCounter]["PersPatronymic"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            loginForm.cbUsersList.SelectedIndex = -1;
            loginForm.mtbUserPassword.Text = "";

            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                //------------проверка пароля-------------------------------------
                //------------и заодно формирование переменной currentUserData----

                //введенный пользователем пароль
                string enteredPswd = loginForm.mtbUserPassword.Text;
                //индекс выбранного из списка сотрудника
                int selectedIndex = -1; //loginForm.cbUsersList.SelectedIndex;

                //поиск строки таблицы usersData, содержащий информацию о выбранном пользователе
                for (int i = 0; i < usersData.Rows.Count; i++)
                {
                    if (loginForm.cbUsersList.SelectedItem.ToString().IndexOf(usersData.Rows[i]["PersFirstName"].ToString()) != -1 &&
                        loginForm.cbUsersList.SelectedItem.ToString().IndexOf(usersData.Rows[i]["PersLastName"].ToString()) != -1 &&
                        loginForm.cbUsersList.SelectedItem.ToString().IndexOf(usersData.Rows[i]["PersPatronymic"].ToString()) != -1)
                    {
                        selectedIndex = i;
                    }
                }

                if (selectedIndex == -1)
                {
                    MessageBox.Show("Ошибка!\n\nПользователь не найден");
                }

                //переменная DataTable, c которой информация будет отправлена на сервер
                DataTable loginUserData = new DataTable();
                DataRow newRow = loginUserData.NewRow();

                //заполнение переменной newRow данными
                for (int colCounter = 0; colCounter < usersData.Columns.Count; colCounter++)
                {
                    loginUserData.Columns.Add(usersData.Columns[colCounter].ColumnName);
                    newRow[colCounter] = usersData.Rows[selectedIndex][colCounter];
                }
                newRow["PersPswd"] = enteredPswd;

                //добавление newRow к selectedUserInfo
                loginUserData.Rows.Add(newRow);

                //отправка данных на сервер
                NetMessage msg = new NetMessage(NetMessage.commandType.PswdCheck);
                msg.dataToSend = loginUserData;
                client.SendMessage(msg, false);

                //если количество возвращенных с сервера строк в переменной receivedData равно 1,
                //значит, в результате сравнения паролей в таблице Persons нашлась запись с совпадающими
                //паролями. Следовательно, пароль был введен правильно.
                if (receivedData.Rows.Count == 1)
                {
                    //Проверка роли сотрудника
                    if (loginUserData.Rows[0]["PersRole"].ToString() == "Контролер" || loginUserData.Rows[0]["PersRole"].ToString() == "Администратор")
                    {
                        //переменная-флаг принимает значение true, что свидетельствует о том,
                        //что сейчас будут получены все активные заявки для модуля контролера,
                        //и выполнять проверку на поступление новых заявок с целью показать уведомление не нужно
                        firstTimeDataReceivedAfterLogin = true;

                        //Отключение от сервера (в случае, если клиент был к нему подключен) для последующего переподключения.
                        //Процедура необходима для того, чтобы в списке connectedClients на сервере
                        //оказался данный ExecutorModule с корректным значением свойства dbUserID
                        if (client.connectedToServer)
                        {
                            try
                            {
                                client.DisconectFromServer();
                                lConnectionStatus.Text = "Отключено";
                                lConnectionStatus.ForeColor = Color.Red;
                                logined = false;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Отключиться от сервера не удалось\n\n" + ex.Message);
                            }
                        }
                        
                        currentUserData = loginUserData;

                        //Обновление свойства dbUserID у экземпляра класса Client
                        client.dbUserID = Convert.ToInt32(currentUserData.Rows[0]["PersID"]);

                        logined = true;

                        //отправка информации на сервер с запросом получения информации о заявках
                        try
                        {
                            //и выполнение процедуры "подключения" к серверу
                            client.ConnectToServer(client.serverIP, client.serverPort);

                            //обновление информации о состоянии подключения
                            lConnectionStatus.Text = "Подключено";
                            lConnectionStatus.ForeColor = Color.Green;

                            //обновление информации о активном пользователе
                            lActiveUser.Text = currentUserData.Rows[0]["PersLastName"] + "  " + currentUserData.Rows[0]["PersFirstName"] + " " + currentUserData.Rows[0]["PersPatronymic"];

                            //снятие флага, что была нажата кнопка выбора сотрудника
                            getUsersButtonClick = false;
                            msg = new NetMessage(NetMessage.commandType.GetInfo);
                            msg.dataToSend = currentUserData;
                            client.SendMessage(msg, false);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Отправить данные на сервер не удалось\n\n" + ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Недостаточно прав на использование модуля контролера");
                    }
                }
                else
                {
                    //если количество отправленных сервером строк в переменной receivedData равно 0 (или больше 1),
                    //значит, в результате SQL запроса не удалось найти запись в таблице Persons, пароли с которой 
                    //совпали бы
                    currentUserData.Clear();
                    MessageBox.Show("Неверный пароль");
                }
            }
            getUsersButtonClick = false;
        }

        private void tsbChangePswd_Click(object sender, EventArgs e)
        {
            if (logined)
            {
                changePswdButtonClick = true;

                PswdChangingForm pswdChangingForm = new PswdChangingForm();
                pswdChangingForm.lUserData.Text = currentUserData.Rows[0]["PersLastName"] + " " + currentUserData.Rows[0]["PersFirstName"] + " " + currentUserData.Rows[0]["PersPatronymic"];

                //подгонка ФИО по центру формы
                pswdChangingForm.lUserData.Left = (pswdChangingForm.Width / 2) - (pswdChangingForm.lUserData.Width / 2);
                //конец подгонки

                pswdChangingForm.ShowDialog();

                NetMessage pswdInfo = new NetMessage(NetMessage.commandType.PswdChange);
                pswdInfo.dataToSend.Columns.Add("PersFirstName");
                pswdInfo.dataToSend.Columns.Add("PersLastName");
                pswdInfo.dataToSend.Columns.Add("PersPatronymic");
                pswdInfo.dataToSend.Columns.Add("PersRole");
                pswdInfo.dataToSend.Columns.Add("OldPswd");
                pswdInfo.dataToSend.Columns.Add("NewPswd");

                DataRow newRow = pswdInfo.dataToSend.NewRow();

                newRow["PersFirstName"] = currentUserData.Rows[0]["PersFirstName"];
                newRow["PersLastName"] = currentUserData.Rows[0]["PersLastName"];
                newRow["PersPatronymic"] = currentUserData.Rows[0]["PersPatronymic"];
                newRow["PersRole"] = currentUserData.Rows[0]["PersRole"];
                newRow["OldPswd"] = pswdChangingForm.mtbOldPswd.Text;
                newRow["NewPswd"] = pswdChangingForm.mtbNewPswd.Text;

                pswdInfo.dataToSend.Rows.Add(newRow);

                bool isSuccessful = false;
                try
                {
                    client.SendMessage(pswdInfo, false);
                    isSuccessful = true;
                }
                catch (Exception ex)
                {
                    isSuccessful = false;
                    MessageBox.Show("Сменить пароль не удалось\n\n" + ex.Message);
                }

                if (isSuccessful)
                {
                    MessageBox.Show("Пароль успешно изменен");
                }

                changePswdButtonClick = false;
            }
            else
            {
                MessageBox.Show("Для использования функции необходимо выполнить вход в программу");
            }
        }

        private void tsbViewReport_Click(object sender, EventArgs e)
        {
            if (currentUserData.Rows.Count > 0)
            {
                ReportForm rf = new ReportForm();
                rf.Show();
            }
            else
            {
                MessageBox.Show("Выберите пользователя");
            }
        }

        private void lvActiveClaims_Click(object sender, EventArgs e)
        {
            notificationForm.Close();
        }

        private void SetExecGroup_Click(object sender, EventArgs e)
        {
            toolStripButton1_Click(sender, e);
        }

        private void lvActiveClaims_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                cmsActiveClaimsMenu.Show(MousePosition);
            }
        }

        private void удалитьЗаявкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteClaim();
        }

        private void tsbDeleteClaim_Click(object sender, EventArgs e)
        {
            DeleteClaim();
        }

        private void tsbEditClaim_Click(object sender, EventArgs e)
        {
            if (lvActiveClaims.SelectedItems.Count == 1)
            {
                //если заявка назначена на исполнителя или на ответственную группу или находится в стадии выполнения, ее редактирование возможно только
                //из под учетной записи соответствующего сотрудника во избежания путаницы, которая может возникнуть, если в процессе выполнения заявки
                //одним сотрудником ее содержимое будет изменено другим сотрудником 
                if (
                    (lvActiveClaims.SelectedItems[0].SubItems[GetLVSubitemIndexByName(lvActiveClaims, "ClaimStatus")].Text.IndexOf("Выполняется") == -1) &&
                    (lvActiveClaims.SelectedItems[0].SubItems[GetLVSubitemIndexByName(lvActiveClaims, "ClaimStatus")].Text.IndexOf("Выполнена") == -1) &&
                    (lvActiveClaims.SelectedItems[0].SubItems[GetLVSubitemIndexByName(lvActiveClaims, "ClaimStatus")].Text.IndexOf("Удалена") == -1)
                   )
                {
                    //Создание объекта формы
                    ClaimChangingForm ccf = new ClaimChangingForm();
                    
                    //Получение с сервера списка групп и заполнение cbExecGroupList полученными данными
                    ccf.cbExecGroupsList.Items.Clear();
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
                            ccf.cbExecGroupsList.Items.Add(MainForm.receivedData.Rows[row]["GroupName"]);
                        }
                        MainForm.getGroupsButtonClick = false;
                    }
                    catch (Exception ex)
                    {
                        MainForm.getGroupsButtonClick = false;
                        MessageBox.Show(ex.Message);
                    }

                    //Передача в форму данных о выбранной заявке
                    //Выполняется в блоке try, потому что состав столбцов для отображения активных заявок может быть изменен путем редактирования
                    //файла настроек *.exe.conf, в результате чего может не оказаться некоторых необходимых полей
                    try
                    {
                        ccf.lClaimID.Text = lvActiveClaims.SelectedItems[0].SubItems[GetLVSubitemIndexByName(lvActiveClaims, "ClaimID")].Text;
                        ccf.lClaimDate.Text = lvActiveClaims.SelectedItems[0].SubItems[GetLVSubitemIndexByName(lvActiveClaims, "ClaimReceivedDate")].Text + " " +
                                              lvActiveClaims.SelectedItems[0].SubItems[GetLVSubitemIndexByName(lvActiveClaims, "ClaimReceivedTime")].Text;
                        ccf.tbRoom.Text = lvActiveClaims.SelectedItems[0].SubItems[GetLVSubitemIndexByName(lvActiveClaims, "ClaimSenderRoom")].Text;
                        ccf.tbSenderName.Text = lvActiveClaims.SelectedItems[0].SubItems[GetLVSubitemIndexByName(lvActiveClaims, "ClaimSenderName")].Text;
                        ccf.tbSenderPhone.Text = lvActiveClaims.SelectedItems[0].SubItems[GetLVSubitemIndexByName(lvActiveClaims, "ClaimSenderPhone")].Text;
                        ccf.tbClaim.Text = lvActiveClaims.SelectedItems[0].SubItems[GetLVSubitemIndexByName(lvActiveClaims, "TypeOfIssue")].Text;
                        ccf.tbAddInfo.Text = lvActiveClaims.SelectedItems[0].SubItems[GetLVSubitemIndexByName(lvActiveClaims, "ClaimDiscription")].Text;

                        //Выбор в cbExecGroupList текущей группы редактируемой заявки (если группа была назначена)
                        if (lvActiveClaims.SelectedItems[0].SubItems[GetLVSubitemIndexByName(lvActiveClaims, "ExecGroupName")].Text != "")
                        {
                            for (int i = 0; i < ccf.cbExecGroupsList.Items.Count; i++)
                            {
                                if (ccf.cbExecGroupsList.Items[i].ToString() == lvActiveClaims.SelectedItems[0].SubItems[GetLVSubitemIndexByName(lvActiveClaims, "ExecGroupName")].Text)
                                {
                                    ccf.cbExecGroupsList.SelectedIndex = i;
                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n" + "Возможно, отсутствуют необходимые для заполнения формы данные");
                    }

                    //Отображение формы на экране
                    //Если была нажата кнопка OK
                    if (ccf.ShowDialog() == DialogResult.OK)
                    {
                        //Если в окне редактирования заявки не была выбрана ответственная за ее исполнение группа набор отправляемых данных
                        //будет отличаться от набора данных, отправляемых в случае, если ответственная группа была выбрана.
                        if (ccf.cbExecGroupsList.SelectedIndex == -1)
                        {
                            //Подготовка данных для отправки на сервер
                            DataTable dataToSend = new DataTable();
                            dataToSend.Columns.Add("ClaimID");
                            dataToSend.Columns.Add("ClaimSenderRoom");
                            dataToSend.Columns.Add("ClaimSenderName");
                            dataToSend.Columns.Add("ClaimSenderPhone");
                            dataToSend.Columns.Add("TypeOfIssue");
                            dataToSend.Columns.Add("ClaimDiscription");
                            dataToSend.Columns.Add("ClaimChangesReceivedDate");
                            dataToSend.Columns.Add("ClaimChangesReceivedTime");
                            dataToSend.Columns.Add("ClaimChangerID");

                            DataRow newRow = dataToSend.NewRow();
                            newRow["ClaimID"] = Convert.ToInt32(ccf.lClaimID.Text);
                            newRow["ClaimSenderRoom"] = ccf.tbRoom.Text;
                            newRow["ClaimSenderName"] = ccf.tbSenderName.Text;
                            newRow["ClaimSenderPhone"] = ccf.tbSenderPhone.Text;
                            newRow["TypeOfIssue"] = ccf.tbClaim.Text;
                            newRow["ClaimDiscription"] = ccf.tbAddInfo.Text;
                            newRow["ClaimChangesReceivedDate"] = "CONVERT(VARCHAR(50), GETDATE(), 102)";
                            newRow["ClaimChangesReceivedTime"] = "CONVERT(VARCHAR(50), GETDATE(), 8)";
                            newRow["ClaimChangerID"] = currentUserData.Rows[0]["PersID"];

                            dataToSend.Rows.Add(newRow);

                            NetMessage messageToSend = new NetMessage(NetMessage.commandType.UpdateClaim);
                            messageToSend.dataToSend = dataToSend;
                            messageToSend.text = ccf.lClaimID.Text;

                            try
                            {
                                client.SendMessage(messageToSend, false);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        else
                        {
                            //Подготовка данных для отправки на сервер
                            DataTable dataToSend = new DataTable();
                            dataToSend.Columns.Add("ClaimID");
                            dataToSend.Columns.Add("ClaimSenderRoom");
                            dataToSend.Columns.Add("ClaimSenderName");
                            dataToSend.Columns.Add("ClaimSenderPhone");
                            dataToSend.Columns.Add("TypeOfIssue");
                            dataToSend.Columns.Add("ClaimDiscription");
                            dataToSend.Columns.Add("ExecGroupID");
                            dataToSend.Columns.Add("ClaimStatus");
                            dataToSend.Columns.Add("ExecGroupOrdererID");
                            dataToSend.Columns.Add("ClaimChangesReceivedDate");
                            dataToSend.Columns.Add("ClaimChangesReceivedTime");
                            dataToSend.Columns.Add("ClaimChangerID");

                            //присваиваемый заявке статус
                            string status = "Ожидает назначения ответственного исполнителя";

                            DataRow newRow = dataToSend.NewRow();
                            newRow["ClaimID"] = Convert.ToInt32(ccf.lClaimID.Text);
                            newRow["ClaimSenderRoom"] = ccf.tbRoom.Text;
                            newRow["ClaimSenderName"] = ccf.tbSenderName.Text;
                            newRow["ClaimSenderPhone"] = ccf.tbSenderPhone.Text;
                            newRow["TypeOfIssue"] = ccf.tbClaim.Text;
                            newRow["ClaimDiscription"] = ccf.tbAddInfo.Text;
                            newRow["ExecGroupID"] = GetGroupIDByName(ccf.cbExecGroupsList.Items[ccf.cbExecGroupsList.SelectedIndex].ToString(), MainForm.groupsData, "GroupName", "GroupID");
                            newRow["ClaimStatus"] = status;
                            newRow["ExecGroupOrdererID"] = currentUserData.Rows[0]["PersID"];
                            newRow["ClaimChangesReceivedDate"] = "CONVERT(VARCHAR(50), GETDATE(), 102)";
                            newRow["ClaimChangesReceivedTime"] = "CONVERT(VARCHAR(50), GETDATE(), 8)";
                            newRow["ClaimChangerID"] = currentUserData.Rows[0]["PersID"];

                            dataToSend.Rows.Add(newRow);

                            NetMessage messageToSend = new NetMessage(NetMessage.commandType.UpdateClaim);
                            messageToSend.dataToSend = dataToSend;
                            //messageToSend.text = ccf.lClaimID.Text;

                            try
                            {
                                client.SendMessage(messageToSend, false);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Редактирование выполняемых, выполненных и удаленных заявок невозможно. Доступен только просмотр");

                    //Далее тот же код, что используется при редактировании еще не выполненной заявки

                    //Создание объекта формы
                    ClaimChangingForm ccf = new ClaimChangingForm();

                    //Отключение кнопки OK
                    ccf.bOK.Enabled = false;

                    //Получение с сервера списка групп и заполнение cbExecGroupList полученными данными
                    ccf.cbExecGroupsList.Items.Clear();
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
                            ccf.cbExecGroupsList.Items.Add(MainForm.receivedData.Rows[row]["GroupName"]);
                        }
                        MainForm.getGroupsButtonClick = false;
                    }
                    catch (Exception ex)
                    {
                        MainForm.getGroupsButtonClick = false;
                        MessageBox.Show(ex.Message);
                    }

                    //Передача в форму данных о выбранной заявке
                    //Выполняется в блоке try, потому что состав столбцов для отображения активных заявок может быть изменен путем редактирования
                    //файла настроек *.exe.conf, в результате чего может не оказаться некоторых необходимых полей
                    try
                    {
                        ccf.lClaimID.Text = lvActiveClaims.SelectedItems[0].SubItems[GetLVSubitemIndexByName(lvActiveClaims, "ClaimID")].Text;
                        ccf.lClaimDate.Text = lvActiveClaims.SelectedItems[0].SubItems[GetLVSubitemIndexByName(lvActiveClaims, "ClaimReceivedDate")].Text + " " +
                                              lvActiveClaims.SelectedItems[0].SubItems[GetLVSubitemIndexByName(lvActiveClaims, "ClaimReceivedTime")].Text;
                        ccf.tbRoom.Text = lvActiveClaims.SelectedItems[0].SubItems[GetLVSubitemIndexByName(lvActiveClaims, "ClaimSenderRoom")].Text;
                        ccf.tbSenderName.Text = lvActiveClaims.SelectedItems[0].SubItems[GetLVSubitemIndexByName(lvActiveClaims, "ClaimSenderName")].Text;
                        ccf.tbSenderPhone.Text = lvActiveClaims.SelectedItems[0].SubItems[GetLVSubitemIndexByName(lvActiveClaims, "ClaimSenderPhone")].Text;
                        ccf.tbClaim.Text = lvActiveClaims.SelectedItems[0].SubItems[GetLVSubitemIndexByName(lvActiveClaims, "TypeOfIssue")].Text;
                        ccf.tbAddInfo.Text = lvActiveClaims.SelectedItems[0].SubItems[GetLVSubitemIndexByName(lvActiveClaims, "ClaimDiscription")].Text;

                        //Выбор в cbExecGroupList текущей группы редактируемой заявки (если группа была назначена)
                        if (lvActiveClaims.SelectedItems[0].SubItems[GetLVSubitemIndexByName(lvActiveClaims, "ExecGroupName")].Text != "")
                        {
                            for (int i = 0; i < ccf.cbExecGroupsList.Items.Count; i++)
                            {
                                if (ccf.cbExecGroupsList.Items[i].ToString() == lvActiveClaims.SelectedItems[0].SubItems[GetLVSubitemIndexByName(lvActiveClaims, "ExecGroupName")].Text)
                                {
                                    ccf.cbExecGroupsList.SelectedIndex = i;
                                    break;
                                }
                            }
                        }

                        ccf.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n" + "Возможно, отсутствуют необходимые для заполнения формы данные");
                    }
                }
            }
            else
            {
                MessageBox.Show("Необходимо выбрать заявку для редактирования");
            }
        }

        private void EditClaim_Click(object sender, EventArgs e)
        {
            tsbEditClaim_Click(sender, e);
        }

        private void lvActiveClaims_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EditClaim_Click(sender, e);
        }

        private void AddClaim2_Click(object sender, EventArgs e)
        {
            AddClaim();
        }

        private void tsmAddClaimWithForm_Click(object sender, EventArgs e)
        {
            AddClaim();
        }

        private void tsmAddClaimWithModule_Click(object sender, EventArgs e)
        {
            //UserModulePath
            if (Configuration.GetValuesBySettingsName("UserModulePath")[0] != "")
            {
                try
                {
                    Process.Start(Configuration.GetValuesBySettingsName("UserModulePath")[0]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось запустить модуль заявителя\n" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Путь к модулю заявителя не найден в файле конфигурации. Проверьте настройки");
            }
        }
    }
}
