using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UtilSubSys;
using NetSubSys;
using System.Diagnostics;
using System.IO;

namespace ExecutorModule
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            lUserName.Text = "N/A";

            Server.MessageRecieved += new Server.MessageWatcher(ReceiveDataFromServer);
            Client.AnswerRecievedFromServer += new Client.AnswerReciever(ReceiveDataFromServer);
            try
            {
                Configuration.GetSettings();
            }
            catch
            {
                MessageBox.Show("Не удалось загрузить настройки");
            }
            if (Configuration.status == Configuration.GetSettingsResult.OK)
            {
                //Если выбрана опция проверки доступных обновлений
                if (Configuration.GetValuesBySettingsName("CheckUpdates")[0] == "true")
                {
                    //получение директории-источника обновлений
                    string updatesSource = Configuration.GetValuesBySettingsName("UpdatesSource")[0];

                    //получение PID текущего процесса, чтобы ModulesUpdater при необходимости смог его завершить
                    int processID = Process.GetCurrentProcess().Id;

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

                if (Configuration.GetValuesBySettingsName("minimizeToTray")[0] == "true")
                {
                    minimizeToTray = true;
                }
                else
                {
                    minimizeToTray = false;
                }
                
                if (Configuration.GetValuesBySettingsName("askAboutMinimizeMode")[0] == "true")
                {
                    askAboutMinimizeMode = true;
                }
                else
                {
                    askAboutMinimizeMode = false;
                }

                Configuration.CreateListView(lvGroupActiveClaims, Configuration.GetValuesBySettingsName("GroupActiveClaims"), 12);
                Configuration.CreateListView(lvUserAcitveClaims, Configuration.GetValuesBySettingsName("UserActiveClaims"), 12);
                Configuration.CreateListView(lvGroupMembers, Configuration.GetValuesBySettingsName("GroupMembers"), 12);

                server = new Server(Convert.ToInt32(Configuration.GetValuesBySettingsName("ThisServerPort")[0]));
                client = new Client(Client.ClientType.ExecutorModule);
                client.serverIP = Configuration.GetValuesBySettingsName("ServerIP")[0];
                client.serverPort = Convert.ToInt32(Configuration.GetValuesBySettingsName("ServerPort")[0]);
                client.clientSideServerPort = server.port;
                try
                {
                    server.Start(true);

                    object myObject = this;
                    EventArgs e = new EventArgs();
                    tsbSelectUser_Click(myObject, e);
                }
                catch
                {
                    MessageBox.Show("Не удалось запустить сервер");
                }

                //подключение к серверу будет выполнено позднее, после того, как будет выбран конкретный пользователь базы данных.
                //Это необходимо для того, чтобы в списке server.connectedClients была информация об ID пользователя, который отправляет сообщения
            }
            else
            {
                MessageBox.Show(Configuration.status.ToString());
            }
        }

        //Глобальные переменные

        public static Server server = null;
        public static Client client = null;
        //хранение получаемой информации от сервера
        public static DataTable receivedData = new DataTable();
        //хранение всех активных заявок группы
        public static DataTable groupActiveClaims = new DataTable();
        //хранение всех активных заявок исполнителя
        public static DataTable userActiveClaims = new DataTable();
        //переменная, в которой хранится информация о всех пользователях, полученных с сервера
        public static DataTable usersData = new DataTable();
        //информация о текущем пользователе, представленная в виде DataTable.
        //В этой таблице одна строка
        public static DataTable currentUserData = new DataTable();
        //хранение инфорации об имени группы
        public static string currentGroupName = "";
        //хранение информации о составе группы
        public static DataTable groupMembers = new DataTable();
        //хранение информации о общепринятых категориях
        public static DataTable commonCategories = new DataTable();
        //хранение информации о внутренних категориях
        public static DataTable internalCategories = new DataTable();
        //переменная-флаг. True, если прогу нужно сворачивать в трей
        public static bool minimizeToTray = false;
        //переменная-флаг. True, если нужно спросить, как сворачивать прогу - в трей или нет
        public static bool askAboutMinimizeMode = true;

        LoginForm loginForm = new LoginForm();
        ExecSelectionForm execSelForm = new ExecSelectionForm();
        ClaimClosingForm claimClosingForm = new ClaimClosingForm();
        public static bool selectUserButtonClicked = false;
        public static bool selectedUserChanged = false;
        public static int closingClaimID = -1;
        public static bool selectCategoryButtonsClicked = false;
        public static int selectedCommonCategoryID = -1;
        public static int selectedInternalCategoryID = -1;
        public static bool pswdChangeButtonClicked = false;
        public static bool logined = false;

        //переменные, использующиеся формой ReportForm
        public static bool getReportColumnsRequest = false;
        public static bool viewReportButtonClicked = false;
        public static bool getGroupNameRequest = false;

        //форма уведомления о поступлении новой заявки
        NewClaimNotification groupNotificationForm = new NewClaimNotification();
        NewClaimNotification userNotificationForm = new NewClaimNotification();
        //флаг, свидетельствующий о том, что в данный момент получение всех активных заявок группы,
        //и показывать уведомление не нужно
        bool firstTimeDataReceivedAfterLogin = false;
        //переменная хранит кол-во активных заявок группы, полученное при последнем получении данных от сервера
        int prevGroupClaimsCount = 0;
        int prevUserClaimsCount = 0;
        public static string notificationString = "";

        //Процедуры, функции

        public delegate void ListViewCallBack(NetMessage msg);
        public void AddClaimsToGroupLV(NetMessage data)
        {
            if (lvGroupActiveClaims.InvokeRequired)
            {
                ListViewCallBack callback = new ListViewCallBack(AddClaimsToGroupLV);
                this.Invoke(callback, new object[] { data });
            }
            else
            {
                lvGroupActiveClaims.Items.Clear();
                if (selectedUserChanged)
                {
                    lvGroupActiveClaims.Items.Clear();
                }
                Configuration.AddDataToListView(lvGroupActiveClaims, data.dataToSend);
            }
        }
        public void AddGroupMembersToLV(NetMessage data)
        {
            if (lvGroupMembers.InvokeRequired)
            {
                ListViewCallBack lvcb = new ListViewCallBack(AddGroupMembersToLV);
                this.Invoke(lvcb, new object[] { data });
            }
            else
            {
                if (selectedUserChanged)
                {
                    lvGroupMembers.Items.Clear();
                    //очередность действий такова, что при смене пользователя отправляется сначала запрос на получение активных заявок группы,
                    //а затем с сервера отправляется дополнительная информация о составе группы. Поэтому при смене пользователя ВСЕГДА
                    //сначала будет выполнен метод AddClaimsToGroupLV, а затем AddGroupMembersToLV, поэтому присваивать значение false
                    //переменной selectedUserChanged следует именно здесь
                    selectedUserChanged = false;
                }
                Configuration.AddDataToListView(lvGroupMembers, data.dataToSend);
            }
        }

        public delegate void ListViewEmptyCallBack();
        public void AddClaimsToUserLV()
        {
            if (lvUserAcitveClaims.InvokeRequired)
            {
                ListViewEmptyCallBack lvcb = new ListViewEmptyCallBack(AddClaimsToUserLV);
                this.Invoke(lvcb, new object[] { });
            }
            else
            {
                lvUserAcitveClaims.Items.Clear();
                Configuration.AddDataToListView(lvUserAcitveClaims, userActiveClaims);
            }
        }

        //Отображает на экране сообщение о поступлении новой заявки
        public delegate void ShowNotificationDelegate();

        //метод осуществляет и звуковое сопровождение, если выставлены соответствующие настройки в файле настроек
        public void ShowGroupNotification()
        {
            //Подсчитывается кол-во заявок в ListView.
            //Т.к. этот метод вызывается в методе GetDataFromServer после метода AddClaimsToListView,
            //будут посчитаны заявки, включая те, что были получены (если они были получены) с только что
            //принятым сообщением от сервера
            int currentClaimsCount = groupActiveClaims.Rows.Count;

            //сравниваются значения prevClaimsCount и currentClaimsCount.
            //Если currentClaimsCount оказывается больше prevClaimsCount, на экране отображается сообщение
            //о поступлении новой заявки
            if (currentClaimsCount > prevGroupClaimsCount)
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
                    ShowNotificationDelegate snd = new ShowNotificationDelegate(ShowGroupNotification);
                    this.Invoke(snd);
                }
                else
                {
                    groupNotificationForm = new NewClaimNotification();
                    groupNotificationForm.Show();
                }
            }

            //после чего значению prevClaimsCount присваивается значение currentClaimsCount
            //для дальнейших сравнений
            prevGroupClaimsCount = currentClaimsCount;
        }
        public void CloseGroupNotification()
        {
            if (this.InvokeRequired)
            {
                ShowNotificationDelegate snd = new ShowNotificationDelegate(CloseGroupNotification);
                this.Invoke(snd);
            }
            else
            {
                groupNotificationForm.Close();
            }
        }

        //метод осуществляет и звуковое сопровождение, если выставлены соответствующие настройки в файле настроек
        public void ShowUserNotification()
        {
            //Подсчитывается кол-во заявок в ListView.
            //Т.к. этот метод вызывается в методе GetDataFromServer после метода AddClaimsToListView,
            //будут посчитаны заявки, включая те, что были получены (если они были получены) с только что
            //принятым сообщением от сервера
            int currentClaimsCount = userActiveClaims.Rows.Count;

            //сравниваются значения prevClaimsCount и currentClaimsCount.
            //Если currentClaimsCount оказывается больше prevClaimsCount, на экране отображается сообщение
            //о поступлении новой заявки
            if (currentClaimsCount > prevUserClaimsCount)
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
                    ShowNotificationDelegate snd = new ShowNotificationDelegate(ShowUserNotification);
                    this.Invoke(snd);
                }
                else
                {
                    userNotificationForm = new NewClaimNotification();
                    userNotificationForm.Show();
                }
            }

            //после чего значению prevClaimsCount присваивается значение currentClaimsCount
            //для дальнейших сравнений
            prevUserClaimsCount = currentClaimsCount;
        }
        public void CloseUserNotification()
        {
            if (this.InvokeRequired)
            {
                ShowNotificationDelegate snd = new ShowNotificationDelegate(CloseUserNotification);
                this.Invoke(snd);
            }
            else
            {
                userNotificationForm.Close();
            }
        }

        //Заполнение переменной groupActiveClaims актуальными значениями, полученными
        //от сервера и помещенными в момент получения в переменную receivedData.
        //По этой причине данный метод вызывается сразу же после получения данных от сервера и заполнения lvGroupActiveClaims.
        //groupAcitveClaims обнуляется в момент изменения значения SelectedIndex у объекта cbUsersList (метод cbUsersList_SelectedIndexChanged класса LoginForm)
        public void UpdateGroupActiveClaims(DataTable data)
        {
            //формирование столбцов
            for (int col = 0; col < data.Columns.Count; col++)
            {
                //если столбец с именем data.Columns[col].ColumnName уже существует в таблице groupActiveClaims
                //то создать его не удасться. Поэтому операция добавления столбцов осуществляется в блоке try..catch
                try
                {
                    groupActiveClaims.Columns.Add(data.Columns[col].ColumnName);
                }
                catch { }
            }

            //обнуление переменной.
            //необходимо для того, чтобы снятые с группы и с исполнителя заявки, которые были переназначены на другую группу, удалялись из списков этой группы
            groupActiveClaims.Rows.Clear();

            //заполнение groupActiveClaims данными из receivedData
            for (int row = 0; row < data.Rows.Count; row++)
            {
                //если список groupActiveClaims не пуст, необходимо выполнить проверку на наличие
                //заявки с ID из текущей строки data в groupAciveClaims
                if (groupActiveClaims.Rows.Count > 0)
                {
                    bool idExists = false;
                    //хранит индекс строки с совпадающим ID
                    int rowIndex = -1;

                    for (int gacRow = 0; gacRow < groupActiveClaims.Rows.Count; gacRow++)
                    {
                        if (Convert.ToInt32(groupActiveClaims.Rows[gacRow]["ClaimID"]) == Convert.ToInt32(data.Rows[row]["ClaimID"]))
                        {
                            idExists = true;
                            rowIndex = gacRow;
                        }
                    }

                    //если ID заявки из текущей строки data обнаруживается среди строк groupActiveClaims,
                    //значение этой строки в groupActiveClaims необходимо заменить на значения из data
                    if (idExists)
                    {
                        for (int col = 0; col < data.Columns.Count; col++)
                        {
                            groupActiveClaims.Rows[rowIndex][col] = data.Rows[row][col];
                        }
                    }
                    //если ID заявки из текущей строки data отсутствует в groupAcitveClaims, необходимо создать новую строку,
                    //заполнить ее данными из data и добавить к groupActiveClaims
                    else
                    {
                        DataRow newRow = groupActiveClaims.NewRow();
                        for (int col = 0; col < data.Columns.Count; col++)
                        {
                            newRow[col] = data.Rows[row][col];
                        }
                        groupActiveClaims.Rows.Add(newRow);
                    }
                }
                //если же groupAcitveClaims пуст, необходимо просто добавить в него новую строку с данными
                else
                {
                    DataRow newRow = groupActiveClaims.NewRow();
                    for (int col = 0; col < data.Columns.Count; col++)
                    {
                        newRow[col] = data.Rows[row][col];
                    }
                    groupActiveClaims.Rows.Add(newRow);
                }
            }
        }

        //Добавление заявок исполнителя в ListView userAcitveClaims
        public void UpdateExecutorActiveClaims()
        {
            userActiveClaims.Rows.Clear();
            //формирование столбцов в userActiveClaims
            for (int col = 0; col < groupActiveClaims.Columns.Count; col++)
            {
                try
                {
                    userActiveClaims.Columns.Add(groupActiveClaims.Columns[col].ColumnName);
                }
                catch { }
            }
            //наполнение userActiveClaims заявками с учетом ID текущего пользователя (client.dbUserID)
            for (int row = 0; row < groupActiveClaims.Rows.Count; row++)
            {
                //так как среди заявок группы может встретиться заявка, у которой еще нет исполнителя,
                //возникнет ситуация, при которой выполнение метода Convert.ToInt32() привет к ошибке,
                //т.к. в этом случае ExecID будет равен NULL. Во избежание возникновения подобной ситуации необходимо
                //выполнить проверку
                if (groupActiveClaims.Rows[row]["ExecID"] != DBNull.Value)
                {
                    if (Convert.ToInt32(groupActiveClaims.Rows[row]["ExecID"]) == client.dbUserID && groupActiveClaims.Rows[row]["ClaimStatus"].ToString() != "Выполнена")
                    {
                        DataRow newRow = userActiveClaims.NewRow();
                        for (int col = 0; col < groupActiveClaims.Columns.Count; col++)
                        {
                            newRow[col] = groupActiveClaims.Rows[row][col];
                        }
                        userActiveClaims.Rows.Add(newRow);
                    }
                }
            }
        }

        //Вычисление количества назначенных заявок на каждого сотрудника группы
        public void UpdateExecutorsActiveClaimsCount()
        {
            //добавление к таблице groupMembers еще одного столбца: ActiveClaimsCount
            try
            {
                groupMembers.Columns.Add("ActiveClaimsCount");
            }
                //если не вышло, значит, такой столбец был уже однажды добавлен
            catch { }

            //счетчик активных задач исполнителя
            int activeClaimsCounter = 0;
            //каждая строка из groupMembers сравнивается с каждой строкой из groupActiveClaims по столбцам PersID и ExecID соответственно.
            //Если ID совпадают, к кол-ву активных задач текущего PersID из groupMembers прибавляется 1.
            for (int membersRow = 0; membersRow < groupMembers.Rows.Count; membersRow++)
            {
                activeClaimsCounter = 0;
                for (int claimsRow = 0; claimsRow < groupActiveClaims.Rows.Count; claimsRow++)
                {
                    if (groupActiveClaims.Rows[claimsRow]["ExecID"] != DBNull.Value)
                    {
                        //не знаю, почему, но без конвертации в int сравнение двух значений DataTable не всегда проходит корректно
                        if (Convert.ToInt32(groupMembers.Rows[membersRow]["PersID"]) == Convert.ToInt32(groupActiveClaims.Rows[claimsRow]["ExecID"]) && groupActiveClaims.Rows[claimsRow]["ClaimStatus"].ToString() != "Выполнена")
                        {
                            activeClaimsCounter++;
                        }
                    }
                }
                groupMembers.Rows[membersRow]["ActiveClaimsCount"] = activeClaimsCounter;
            }
        }

        //получение данных от сервера и от клиента на стороне сервера
        public void ReceiveDataFromServer(NetMessage data)
        {
            if (selectUserButtonClicked || selectCategoryButtonsClicked || getReportColumnsRequest || viewReportButtonClicked || getGroupNameRequest || pswdChangeButtonClicked)
            {
                //если входящее сообщение содержит в поле text строку "reportClaimsCount",
                //следовательно, это второе сообщение из двух, отправленных сервером, 
                //в качестве ответа на запрос на получение отчета. Это сообщение содержит таблицу 
                //с перечислением всех типов заявок и указания их количества в запрошенном отчете
                if (data.text == "reportClaimsCount")
                {
                    ReportForm.reportClaimsCount = data.dataToSend;
                    MainWindow.viewReportButtonClicked = false;
                }
                else
                {
                    receivedData = data.dataToSend;
                }
            }
            else
            {
                if (data.text == "groupMembersList")
                {
                    groupMembers = data.dataToSend;
                    AddGroupMembersToLV(data);
                }
                else
                {
                    //Добавление списка сотрудников группы в lvGroupMembers
                    AddClaimsToGroupLV(data);

                    //Поддержание в актуальном состоянии переменную groupActiveClaims типа DataTable
                    UpdateGroupActiveClaims(data.dataToSend);

                    //Поддержание в актуальном состоянии переменную userActiveClaims типа DataTable
                    UpdateExecutorActiveClaims();

                    //Добавление userActiveClaims в lvUserActiveClaims
                    AddClaimsToUserLV();

                    //Поддержание в актуальном состоянии информации о количестве активных заявок у каждого сотрудника
                    UpdateExecutorsActiveClaimsCount();

                    //Обновление lvGroupMembers информацией о кол-ве активных заявок каждого члена группы
                    NetMessage justSomeMessage = new NetMessage();
                    justSomeMessage.dataToSend = groupMembers;
                    AddGroupMembersToLV(justSomeMessage);
                }
            }

            //после отправки на сервер информации об изменении заявки (например, ее статуса, что и происходит при назначении ответственного исполнителя)
            //сервер отправляет ответное сообщение с текстом "OK", чтобы подтвердить получение данных от клиента, а затем, уже в качестве клиента,
            //отправляет обновленные данные из базы данных обратно этому клиенту, который выступает в роли сервера (т.е. слушает входящее соединение).
            //Т.о. происходит 2 события: server.MessageRecieved  и client.MessageReceivedFromServer. При событии client.MessageReceivedFromServer
            //на самом деле никакой полезной информации, с которой можно было бы работать, клиент не получает - это всего лишь слово "OK", подтверждающее
            //получение информации сервером, поэтому никаких действий по отображению уведомлений в этом случае предпринимать не нужно.
            if (data.text != "OK")
            {
                //если данные, поступающие с сервера, не являются данными, которые получает модуль исполнителя
                //сразу после входа нового пользователя, необходимо выполнить проверку на поступление новых заявок
                //и показать уведомление, если это необходимо
                if (firstTimeDataReceivedAfterLogin)
                {
                    prevUserClaimsCount = userActiveClaims.Rows.Count;
                    prevGroupClaimsCount = groupActiveClaims.Rows.Count;
                }
                else
                {
                    notificationString = "ВАМ НАЗНАЧЕНА НОВАЯ ЗАЯВКА";
                    ShowUserNotification();

                    notificationString = "НА ГРУППУ НАЗНАЧЕНА НОВАЯ ЗАЯВКА";
                    ShowGroupNotification();
                }
                firstTimeDataReceivedAfterLogin = false;
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

        //Процедура изменения выбранной заявки. В качестве параметра получает переменную типа ListView.
        //Это сделано для того, чтобы была возможность редактировать заявки как из заявок группы, так и из заявок пользователя
        public void EditClaim(ListView targetListView)
        {
            if (targetListView.SelectedItems.Count == 1)
            {
                //Создание объекта формы
                ClaimChangingForm ccf = new ClaimChangingForm();

                //Передача в форму данных о выбранной заявке
                //Выполняется в блоке try, потому что состав столбцов для отображения активных заявок может быть изменен путем редактирования
                //файла настроек *.exe.conf, в результате чего может не оказаться некоторых необходимых полей
                try
                {
                    ccf.lClaimID.Text = targetListView.SelectedItems[0].SubItems[GetLVSubitemIndexByName(targetListView, "ClaimID")].Text;
                    ccf.lClaimDate.Text = targetListView.SelectedItems[0].SubItems[GetLVSubitemIndexByName(targetListView, "ClaimReceivedDate")].Text + " " +
                                          targetListView.SelectedItems[0].SubItems[GetLVSubitemIndexByName(targetListView, "ClaimReceivedTime")].Text;
                    ccf.tbRoom.Text = targetListView.SelectedItems[0].SubItems[GetLVSubitemIndexByName(targetListView, "ClaimSenderRoom")].Text;
                    ccf.tbSenderName.Text = targetListView.SelectedItems[0].SubItems[GetLVSubitemIndexByName(targetListView, "ClaimSenderName")].Text;
                    ccf.tbSenderPhone.Text = targetListView.SelectedItems[0].SubItems[GetLVSubitemIndexByName(targetListView, "ClaimSenderPhone")].Text;
                    ccf.tbSenderUserName.Text = targetListView.SelectedItems[0].SubItems[GetLVSubitemIndexByName(targetListView, "ClaimSenderUserName")].Text;
                    ccf.tbHostName.Text = targetListView.SelectedItems[0].SubItems[GetLVSubitemIndexByName(targetListView, "ClaimSenderHostName")].Text;
                    ccf.tbHostIP.Text = targetListView.SelectedItems[0].SubItems[GetLVSubitemIndexByName(targetListView, "ClaimSenderHostIP")].Text;
                    ccf.tbClaim.Text = targetListView.SelectedItems[0].SubItems[GetLVSubitemIndexByName(targetListView, "TypeOfIssue")].Text;
                    ccf.tbAddInfo.Text = targetListView.SelectedItems[0].SubItems[GetLVSubitemIndexByName(targetListView, "ClaimDiscription")].Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + "Возможно, отсутствуют необходимые для заполнения формы данные");
                }

                //Отображение формы на экране
                //Если была нажата кнопка OK
                if (ccf.ShowDialog() == DialogResult.OK)
                {
                    //Подготовка данных для отправки на сервер
                    DataTable dataToSend = new DataTable();
                    dataToSend.Columns.Add("ClaimID");
                    dataToSend.Columns.Add("ClaimSenderRoom");
                    dataToSend.Columns.Add("ClaimSenderName");
                    dataToSend.Columns.Add("ClaimSenderPhone");
                    dataToSend.Columns.Add("ClaimSenderUserName");
                    dataToSend.Columns.Add("ClaimSenderHostName");
                    dataToSend.Columns.Add("ClaimSenderHostIP");
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
                    newRow["ClaimSenderUserName"] = ccf.tbSenderUserName.Text;
                    newRow["ClaimSenderHostName"] = ccf.tbHostName.Text;
                    newRow["ClaimSenderHostIP"] = ccf.tbHostIP.Text;
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
            }
            else
            {
                MessageBox.Show("Необходимо выбрать заявку для редактирования");
            }
        }

        private void tsbSelectUser_Click(object sender, EventArgs e)
        {
            selectUserButtonClicked = true;

            //запрос на сервер, формирование переменной usersData, заполнение cbUsersList значениями
            try
            {
                NetMessage msg = new NetMessage(NetMessage.commandType.GetInfo, "GetUsers");
                client.SendMessage(msg, false);

                usersData = receivedData;

                DataView usersDataView = new DataView();
                usersDataView = usersData.DefaultView;
                usersDataView.Sort = "PersLastName asc";
                usersData = usersDataView.ToTable();

                loginForm.cbUsersList.Items.Clear();
                for (int rowCounter = 0; rowCounter < usersData.Rows.Count; rowCounter++)
                {
                    if (usersData.Rows[rowCounter]["PersID"].ToString() != (24).ToString())
                    {
                        loginForm.cbUsersList.Items.Add(usersData.Rows[rowCounter]["PersLastName"] + " " + usersData.Rows[rowCounter]["PersFirstName"] + " " + usersData.Rows[rowCounter]["PersPatronymic"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            loginForm.mtbUserPassword.Text = "";
            loginForm.cbUsersList.SelectedIndex = -1;
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                //Проверка пароля и формирование переменной currentUserData 
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

                //переменная хранит введенный пароль
                string enteredPswd = loginForm.mtbUserPassword.Text;

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

                //переменная-флаг. False, если данные с введенным пользователем паролем
                //не удалось отправить на сервер
                bool dataSendingError = false;

                //отправка данных на сервер
                NetMessage msg = new NetMessage(NetMessage.commandType.PswdCheck);
                msg.dataToSend = loginUserData;
                try
                {
                    client.SendMessage(msg, false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка отправки данных на сервер при проверке пароля\n\n" + ex.Message);
                    dataSendingError = true;
                    //чтобы получаемые данные загружались в ListView, значение пременной selectUserButtonClicked устанавливается в false
                    selectUserButtonClicked = false;
                }

                //если при отправке сообщения PswdCheck не возникло ошибок
                if (!dataSendingError)
                {

                    //чтобы получаемые данные загружались в ListView, значение пременной selectUserButtonClicked устанавливается в false
                    selectUserButtonClicked = false;

                    //если количество возвращенных с сервера строк в переменной receivedData равно 1,
                    //значит, в результате сравнения паролей в таблице Persons нашлась запись с совпадающими
                    //паролями. Следовательно, пароль был введен правильно.
                    if (receivedData.Rows.Count == 1)
                    {

                        //чтобы избежать появления сообщения на экране о том, что была получена новая заявка
                        //при получении информации после осуществления входа новым пользователем,
                        //значение переменной firstTimeDataReceivedAfterLogin устанавливается на true
                        firstTimeDataReceivedAfterLogin = true;

                        lUserName.Text = loginForm.cbUsersList.SelectedItem.ToString();

                        //очищается execSelForm.cbExecutorsList
                        execSelForm.cbExecutorsList.Items.Clear();
                        execSelForm.cbExecutorsList.SelectedItem = "";
                        execSelForm.cbExecutorsList.SelectedIndex = -1;

                        //Отключение от сервера (в случае, если клиент был к нему подключен) для последующего переподключения.
                        //Процедура необходима для того, чтобы в списке connectedClients на сервере
                        //оказался данный ExecutorModule с корректным значением свойства dbUserID
                        if (client.connectedToServer)
                        {
                            try
                            {
                                client.DisconectFromServer();
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
                            client.ConnectToServer(client.serverIP, client.serverPort);
                        }
                        catch
                        {
                            MessageBox.Show("connection failed");
                        }

                        msg = new NetMessage(NetMessage.commandType.GetInfo);
                        msg.dataToSend = currentUserData;
                        try
                        {
                            client.SendMessage(msg, false);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Отправить данные на сервер не удалось\n\n" + ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверный пароль");
                    }
                }
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client != null)
            {
                if (client.connectedToServer)
                {
                    try
                    {
                        client.DisconectFromServer();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Отключиться от сервера не удалось\n\n" + ex.Message);
                    }
                }
            }
        }

        private void tsbSelectExecutor_Click(object sender, EventArgs e)
        {
            //если была выбрана хотя бы одна активная заявка группы
            if (lvGroupActiveClaims.SelectedItems.Count > 0)
            {
                //данные о сотрудниках группы загружаются из lvGroupMembers (или из groupMembers) в cbExecutorsList,
                //если он пуст
                if (execSelForm.cbExecutorsList.Items.Count == 0)
                {
                    for (int row = 0; row < groupMembers.Rows.Count; row++)
                    {
                        string rowString = groupMembers.Rows[row]["PersLastName"] + " " + groupMembers.Rows[row]["PersFirstName"] + " " + groupMembers.Rows[row]["PersPatronymic"];
                        execSelForm.cbExecutorsList.Items.Add(rowString);
                    }
                }

                if (execSelForm.ShowDialog() == DialogResult.OK)
                {
                    //создание сообщения для отправки на сервер
                    NetMessage updateClaims = new NetMessage(NetMessage.commandType.UpdateClaim);

                    //формирование DataTable
                    updateClaims.dataToSend.Columns.Add("ClaimID");
                    updateClaims.dataToSend.Columns.Add("ExecID");
                    updateClaims.dataToSend.Columns.Add("ClaimStatus");
                    updateClaims.dataToSend.Columns.Add("ExecOrdererID");
                    string status = "Ожидает выполнения";

                    //Вычисление номера столбца lvActiveClaims, в котором хранятся ID заявок
                    int idIndex = -1;
                    for (int i = 0; i < lvGroupActiveClaims.Columns.Count; i++)
                    {
                        if (lvGroupActiveClaims.Columns[i].Name == "ClaimID")
                        {
                            idIndex = i;
                        }
                    }
                    //переменная хранит индекс выбранного сотрудника
                    int selectedExecutorIndex = execSelForm.cbExecutorsList.SelectedIndex;

                    //переменная хранит ID выбранного сотрудника
                    int selectedExecutorID = Convert.ToInt32(groupMembers.Rows[selectedExecutorIndex]["PersID"]);

                    //Заполнение DataTable
                    foreach (ListViewItem curlvi in lvGroupActiveClaims.SelectedItems)
                    {
                        DataRow dr = updateClaims.dataToSend.NewRow();
                        dr["ClaimID"] = curlvi.SubItems[idIndex].Text;
                        dr["ExecID"] = selectedExecutorID;
                        dr["ClaimStatus"] = status;
                        dr["ExecOrdererID"] = currentUserData.Rows[0]["PersID"];
                        updateClaims.dataToSend.Rows.Add(dr);
                    }

                    //отправка данных на сервер
                    try
                    {
                        client.SendMessage(updateClaims, false);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    //обнуление comboBox
                    execSelForm.cbExecutorsList.SelectedIndex = -1;
                    execSelForm.cbExecutorsList.SelectedItem = "";
                }
            }
        }

        private void tsbCloseTheClaim_Click(object sender, EventArgs e)
        {
            if (lvUserAcitveClaims.SelectedItems.Count == 1)
            {
                //вычисление индекса столбца, в котором отображается статус заявки
                int statusColumnIndex = -1;
                for (int colCounter = 0; colCounter < lvUserAcitveClaims.Columns.Count; colCounter++)
                {
                    if (lvUserAcitveClaims.Columns[colCounter].Name == "ClaimStatus")
                    {
                        statusColumnIndex = colCounter;
                    }
                }

                if (statusColumnIndex != -1)
                {
                    if (lvUserAcitveClaims.SelectedItems[0].SubItems[statusColumnIndex].Text == "Выполняется")
                    {
                        //вычисление индекса столбца, в котором отображается ID заявки
                        int columnIndex = -1;
                        for (int curCol = 0; curCol < lvUserAcitveClaims.Columns.Count; curCol++)
                        {
                            if (lvUserAcitveClaims.Columns[curCol].Name == "ClaimID")
                            {
                                columnIndex = curCol;
                            }
                        }
                        if (columnIndex != -1)
                        {
                            closingClaimID = Convert.ToInt32(lvUserAcitveClaims.SelectedItems[0].SubItems[columnIndex].Text);

                            //----------------------Сброс формы

                            claimClosingForm.cbCommonCategories.SelectedIndex = -1;
                            claimClosingForm.cbInternalCategories.SelectedIndex = -1;
                            claimClosingForm.tbExecutionInformation.Text = "";
                            claimClosingForm.mtbAffectedObjectsCount.Text = "";

                            //-------------------Конец сброса формы

                            //------------Запрос общепринятых категорий----------------
                            
                            claimClosingForm.cbCommonCategories.Items.Clear();
                            claimClosingForm.cbCommonCategories.Enabled = false;
                            //подготовка и отправка запроса на получение списка общепринятых категорий заявок
                            MainWindow.selectCategoryButtonsClicked = true;
                            NetMessage msg = new NetMessage(NetMessage.commandType.GetInfo, "GetCommonCategories");
                            try
                            {
                                MainWindow.client.SendMessage(msg, false);

                                commonCategories = receivedData;

                                //заполнение cbCommonCategories полученными данными
                                for (int row = 0; row < MainWindow.receivedData.Rows.Count; row++)
                                {
                                    claimClosingForm.cbCommonCategories.Items.Add(MainWindow.receivedData.Rows[row]["CommonCatName"] + " - " + MainWindow.receivedData.Rows[row]["CommonCatDiscription"]);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }

                            MainWindow.selectCategoryButtonsClicked = false;
                            claimClosingForm.cbCommonCategories.Enabled = true;

                            //------------Конец запроса общепринятых категорий----------------

                            //------------Запрос внутренних категорий----------------

                            claimClosingForm.cbInternalCategories.Items.Clear();
                            //подготовка и отправка запроса на получение списка внутренних категорий заявок
                            MainWindow.selectCategoryButtonsClicked = true;
                            msg = new NetMessage(NetMessage.commandType.GetInfo, "GetInternalCategories");
                            try
                            {
                                MainWindow.client.SendMessage(msg, false);

                                internalCategories = receivedData;

                                //заполнение cbInternalCategories полученными данными
                                for (int row = 0; row < MainWindow.receivedData.Rows.Count; row++)
                                {
                                    claimClosingForm.cbInternalCategories.Items.Add(MainWindow.receivedData.Rows[row]["IntCatName"]);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            MainWindow.selectCategoryButtonsClicked = false;

                            //------------Конец запроса внутренних категорий----------------

                            for (int row = 0; row < groupActiveClaims.Rows.Count; row++)
                            {
                                if (Convert.ToInt32(groupActiveClaims.Rows[row]["ClaimID"]) == closingClaimID)
                                {
                                    claimClosingForm.tbClaimTypeOfIssue.Text = groupActiveClaims.Rows[row]["TypeOfIssue"].ToString();
                                    claimClosingForm.tbClaimSenderInfo.Text = "Учетная запись: " + groupActiveClaims.Rows[row]["ClaimSenderUserName"].ToString() + ", ПК: " + groupActiveClaims.Rows[row]["ClaimSenderHostName"].ToString() +
                                        " (" + groupActiveClaims.Rows[row]["ClaimSenderHostIP"].ToString() + ")";
                                    claimClosingForm.tbRoom.Text = groupActiveClaims.Rows[row]["ClaimSenderRoom"].ToString();
                                    claimClosingForm.tbClaimAddInfo.Text = groupActiveClaims.Rows[row]["ClaimDiscription"].ToString();
                                    claimClosingForm.tbClaimCreationDate.Text = groupActiveClaims.Rows[row]["ClaimReceivedDate"].ToString() + " " + groupActiveClaims.Rows[row]["ClaimReceivedTime"].ToString();
                                }
                            }

                            if (claimClosingForm.ShowDialog() == DialogResult.OK)
                            {
                                //создание сообщения для отправки обновленной информации о закрываемой заявке на сервер
                                //и формирование столбцов DataTable
                                NetMessage closingClaimData = new NetMessage(NetMessage.commandType.UpdateClaim);
                                closingClaimData.dataToSend.Columns.Add("ClaimID");
                                closingClaimData.dataToSend.Columns.Add("ClaimCommonCategoryID");
                                closingClaimData.dataToSend.Columns.Add("ClaimInternalCategoryID");
                                closingClaimData.dataToSend.Columns.Add("AffectedObjectsCount");
                                closingClaimData.dataToSend.Columns.Add("ClaimExecutionDescription");
                                closingClaimData.dataToSend.Columns.Add("ClaimStatus");

                                //формирование строки
                                DataRow dr = closingClaimData.dataToSend.NewRow();
                                dr["ClaimID"] = closingClaimID;
                                dr["ClaimCommonCategoryID"] = selectedCommonCategoryID;
                                dr["ClaimInternalCategoryID"] = selectedInternalCategoryID;
                                dr["AffectedObjectsCount"] = Convert.ToInt32(claimClosingForm.mtbAffectedObjectsCount.Text);
                                if (claimClosingForm.tbExecutionInformation.Text.Length > 0)
                                {
                                    dr["ClaimExecutionDescription"] = claimClosingForm.tbExecutionInformation.Text;
                                }
                                else
                                {
                                    dr["ClaimExecutionDescription"] = "";
                                }
                                dr["ClaimStatus"] = "Выполнена";
                                closingClaimData.dataToSend.Rows.Add(dr);

                                client.SendMessage(closingClaimData, false);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Не удалось определить ID выбранной заявки. Целевой столбец отсутствует в ListView");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Невозможно завершить выполнение заявки, которая не выполнялась");
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось определить индекс столбца статуса заявки");
                }
            }
            else
            {
                if (lvUserAcitveClaims.SelectedItems.Count > 1)
                {
                    MessageBox.Show("Нельзя закрыть несколько заявок одновременно");
                }
                else
                {
                    MessageBox.Show("Выберите заявку, которую Вы хотите закрыть");
                }
            }
        }

        private void tsbStartExecuteTheClaim_Click(object sender, EventArgs e)
        {
            if (lvUserAcitveClaims.SelectedItems.Count > 0)
            {
                //вычисление индекса столбца, в котором отображается ID заявки
                int columnIndex = -1;
                for (int curCol = 0; curCol < lvUserAcitveClaims.Columns.Count; curCol++)
                {
                    if (lvUserAcitveClaims.Columns[curCol].Name == "ClaimID")
                    {
                        columnIndex = curCol;
                    }
                }
                if (columnIndex != -1)
                {
                    //проверка, что заявка уже не выполняется.
                    //Для этого необходимо определить номер SubItem, где хранятся статусы заявок
                    int statusColumnIndex = -1;
                    for (int col = 0; col < lvUserAcitveClaims.Columns.Count; col++)
                    {
                        if (lvUserAcitveClaims.Columns[col].Name == "ClaimStatus")
                        {
                            statusColumnIndex = col;
                        }
                    }

                    if (statusColumnIndex != -1)
                    {
                        bool claimIsAlreadyExecuting = false;
                        foreach (ListViewItem curLVI in lvUserAcitveClaims.Items)
                        {
                            if (curLVI.SubItems[statusColumnIndex].Text == "Выполняется")
                            {
                                claimIsAlreadyExecuting = true;
                            }
                        }

                        if (claimIsAlreadyExecuting == true)
                        {
                            MessageBox.Show("Операция неприменима к уже выполняющимся заявкам");
                        }
                        else
                        {

                            if (MessageBox.Show("Вы уверены, что хотите приступить к выполнению выбранной заявки?\n(Данное действие необратимо)", "Внимание", MessageBoxButtons.OKCancel) == DialogResult.OK)
                            {
                                //создание сообщения для отправки на сервер
                                NetMessage updateClaims = new NetMessage(NetMessage.commandType.UpdateClaim);

                                //формирование DataTable
                                updateClaims.dataToSend.Columns.Add("ClaimID");
                                updateClaims.dataToSend.Columns.Add("ClaimStatus");
                                string status = "Выполняется";

                                //Заполнение DataTable
                                foreach (ListViewItem curlvi in lvUserAcitveClaims.SelectedItems)
                                {
                                    DataRow dr = updateClaims.dataToSend.NewRow();
                                    dr["ClaimID"] = curlvi.SubItems[columnIndex].Text;
                                    dr["ClaimStatus"] = status;
                                    updateClaims.dataToSend.Rows.Add(dr);
                                }

                                //отправка данных на сервер
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
                        MessageBox.Show("Не удалось определить статус выбранной заявки. Целевой столбец не обнаружен");
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось определить ID выбранной заявки. Целевой столбец отсутствует в ListView");
                }
            }
            else
            {
                MessageBox.Show("Выберите заявку, к выполнению которой Вы хотите приступить");
            }
        }

        private void tsbShowReport_Click(object sender, EventArgs e)
        {
            if (currentUserData.Rows.Count == 1)
            {
                try
                {
                    ReportForm rf = new ReportForm();
                    rf.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Выполните вход в систему");
            }
        }

        private void lvGroupActiveClaims_Click(object sender, EventArgs e)
        {
            CloseGroupNotification();
        }

        private void lvUserAcitveClaims_Click(object sender, EventArgs e)
        {
            CloseUserNotification();
        }

        private void lvGroupActiveClaims_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                cmsGroupActiveClaimsMenu.Show(MousePosition);
            }
        }

        private void setExecutor_Click(object sender, EventArgs e)
        {
            tsbSelectExecutor_Click(sender, e);
        }

        private void startToExecute_Click(object sender, EventArgs e)
        {
            tsbStartExecuteTheClaim_Click(sender, e);
        }

        private void tsbChangePswd_Click(object sender, EventArgs e)
        {
            if (logined)
            {
                pswdChangeButtonClicked = true;

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

                pswdChangeButtonClicked = false;
            }
            else
            {
                MessageBox.Show("Для того, чтобы воспользоваться данной функцией, необходимо выполнить вход в программу");
            }
        }

        private void tsbEditClaim_Click(object sender, EventArgs e)
        {
            //Так как кнопка, находящаяся на панели инструментов, предназначена как для работы со списком заявок группы, так и со списком заявок исполнителя,
            //может возникнуть ситуация, когда и в том, и в другом списке будут присутствовать выделенные элементы. Это приведет к тому, что станет неясным,
            //какую именно выделенную (выбранную) заявку необходимо обрабатывать. Поэтому осуществляется нижеследующая проверка.
            if (lvGroupActiveClaims.SelectedItems.Count > 0 && lvUserAcitveClaims.SelectedItems.Count > 0)
            {
                MessageBox.Show("Снимите выделение с одного из списков активных заявок");
            }
            else
            {
                //Если выбрана заявка из списка активных заявок группы,
                if (lvGroupActiveClaims.SelectedItems.Count == 1)
                {
                    //осуществляется дополнительная проверка на предмет того, что эта заявка была назначена на конкретного исполнителя. Если исполнитель назначен,
                    //тогда столбец ExecPersName элемента lvGroupActiveClaims будет содержать некую строку, и, следовательно, ее длинна будет больше 0.
                    if (lvGroupActiveClaims.SelectedItems[0].SubItems[GetLVSubitemIndexByName(lvGroupActiveClaims, "ExecPersName")].Text.Length > 0)
                    {
                        //Если длинна строки, содержащей фамилию исполнителя, больше 0, значит, исполнитель был назначен. Поэтому выполняется проверка на предмет
                        //совпадения имени исполнителя заявки и имени пользователя, выполнившего вход в программу.  Если имена совпадают, пользователю разрешается
                        //внести изменения в заявку.
                       if (lvGroupActiveClaims.SelectedItems[0].SubItems[GetLVSubitemIndexByName(lvGroupActiveClaims, "ExecPersName")].Text == currentUserData.Rows[0]["PersLastName"].ToString())
                        {
                            EditClaim(lvGroupActiveClaims);
                        }
                        else
                        {
                            MessageBox.Show("Заявка была назначена на исполнителя. Теперь изменить заявку может только исполнитель");
                        }
                    }
                }
                else
                {
                    if (lvGroupActiveClaims.SelectedItems.Count > 1)
                    {
                        MessageBox.Show("Необходимо выбрать только одну заявку");
                    }

                    if (lvUserAcitveClaims.SelectedItems.Count == 1)
                    {
                        EditClaim(lvUserAcitveClaims);
                    }
                }
            }
        }

        private void editGroupClaim_Click(object sender, EventArgs e)
        {
            //Если выбрана заявка из списка активных заявок группы,
            if (lvGroupActiveClaims.SelectedItems.Count == 1)
            {
                //осуществляется дополнительная проверка на предмет того, что эта заявка была назначена на конкретного исполнителя. Если исполнитель назначен,
                //тогда столбец ExecPersName элемента lvGroupActiveClaims будет содержать некую строку, и, следовательно, ее длинна будет больше 0.
                if (lvGroupActiveClaims.SelectedItems[0].SubItems[GetLVSubitemIndexByName(lvGroupActiveClaims, "ExecPersName")].Text.Length > 0)
                {
                    //Если длинна строки, содержащей фамилию исполнителя, больше 0, значит, исполнитель был назначен. Поэтому выполняется проверка на предмет
                    //совпадения имени исполнителя заявки и имени пользователя, выполнившего вход в программу.  Если имена совпадают, пользователю разрешается
                    //внести изменения в заявку.
                    if (lvGroupActiveClaims.SelectedItems[0].SubItems[GetLVSubitemIndexByName(lvGroupActiveClaims, "ExecPersName")].Text == currentUserData.Rows[0]["PersLastName"].ToString())
                    {
                        EditClaim(lvGroupActiveClaims);
                    }
                    else
                    {
                        MessageBox.Show("Заявка была назначена на исполнителя. Теперь изменить заявку может только исполнитель");
                    }
                }
                else
                {
                    EditClaim(lvGroupActiveClaims);
                }
            }
            else
            {
                MessageBox.Show("Необходимо выбрать заявку для редактирования");
            }
        }

        private void editUserClaim_Click(object sender, EventArgs e)
        {
            if (lvUserAcitveClaims.SelectedItems.Count == 1)
            {
                EditClaim(lvUserAcitveClaims);
            }
        }

        private void lvUserAcitveClaims_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                cmsUserActiveClaimsMenu.Show(MousePosition);
            }
        }

        private void lvGroupActiveClaims_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            editGroupClaim_Click(sender, e);
        }

        private void lvUserAcitveClaims_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            editUserClaim_Click(sender, e);
        }

        private void MainWindow_Resize(object sender, EventArgs e)
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

        private void closeTheClaim_Click(object sender, EventArgs e)
        {
            tsbCloseTheClaim_Click(sender, e);
        }
    }
}
