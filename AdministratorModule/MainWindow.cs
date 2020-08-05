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
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;

namespace AdministratorModule
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            Configuration.GetSettings();
            Server.MessageRecieved += new Server.MessageWatcher(getDataFromServer);
            Client.AnswerRecievedFromServer += new Client.AnswerReciever(getDataFromServer);
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
                            catch {  }
                        }
                    }
                }

                server = new Server(Convert.ToInt32(Configuration.GetValuesBySettingsName("ThisServerPort")[0]));
                try
                {
                    server.Start(true);
                }
                catch
                {
                    MessageBox.Show("Не удалось запустить сервер");
                }
                client = new Client(Client.ClientType.AdministratorModule);
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
            spliterCont1Panel1Height = splitContainer1.SplitterDistance;
        }

        //сервер модуля администратора
        public static Server server = null;

        //клиент модуля администратора
        public static Client client = null;

        //в этой переменной хранится получаемая по сети информация
        public static DataTable receivedData = new DataTable();

        //переменная формы для логина (LoginForm)
        LoginForm loginForm = new LoginForm();

        //в эту переменную загружаются данные о количестве перечисленных в представленном отчете заявок
        DataTable reportClaimsCount = new DataTable();

        //переменная, в которой хранится информация о всех пользователях, полученных с сервера во время
        //процедуры входа в программу 
        public static DataTable usersDataForLogin = new DataTable();

        //переменная хранит сведения о текущем пользователе
        DataTable currentUserData = new DataTable();

        //true, если пользователь залогинился, иначе false
        bool logined = false;

        //индикатор, что была нажата кнопка получения данных
        bool getDataButtonClicked = false;

        //была нажата кнопка выбора пользователя
        bool getUsersButtonClicked = false;

        //была нажата кнопка добавления нового пользователя
        bool addUserButtonClicked = false;

        //была нажата кнопка добавления нового группы
        bool addGroupButtonClicked = false;

        //индикатор выполнения запроса на получение столбцов той или инной таблицы
        bool getTableColumnsRequest = false;

        //Информация о высоте Panel1 элемента SpliterContainer1
        int spliterCont1Panel1Height = 0;

        //Данные о пользователях, полученные из базы данных из представления UsersView.
        //Значение переменной присваивается в процессе выполнения ф-и getDataFromServer.
        DataTable usersData = new DataTable();

        //Данные о группах, полученные из базы данных из представления GroupsView.
        //Значение переменной присваивается в процессе выполнения ф-и getDataFromServer.
        DataTable groupsData = new DataTable();

        //Данные об "официальных" категориях, полученные из базы данных из таблицы CommonCategories.
        //Значение переменной присваивается в процессе выполнения ф-и getDataFromServer.
        DataTable commonCatData = new DataTable();

        //Данные о "внутренних" категориях, полученные из базы данных из таблицы InternalCategories.
        //Значение переменной присваивается в процессе выполнения ф-и getDataFromServer.
        DataTable internalCatData = new DataTable();

        //true, когда выполняется запрос из ф-и GetUsers()
        bool getUsersData = false;

        //true, когда выполняется запрос из ф-и GetGroups()
        bool getGroupsData = false;

        //true, когда выполняется запрос из ф-и GetCommonCategories()
        bool getCommonCatData = false;

        //true, когда выполняется запрос из ф-и GetInternalCategories()
        bool getInternalCatData = false;

        //Удаление заявок (как формальное, т.е. изменение статуса заявки на "Удалена", так и фактическое, т.е. удаление записи из базы данных)
        public void DeleteClaim(bool deleteFromDB)
        {
            //если выбрана одна или более заявок
            if (dgvData.SelectedRows.Count > 0)
            {
                //Если выбрана опция фактического удаления заявки
                if (deleteFromDB)
                {
                    //Создание сообщения
                    NetMessage deleteClaimsMsg = new NetMessage(NetMessage.commandType.DeleteClaim);
                    deleteClaimsMsg.text = "deleteFromDB";

                    //формирование списка ID
                    deleteClaimsMsg.dataToSend.Columns.Add();
                    for (int i = 0; i < dgvData.SelectedRows.Count; i++)
                    {
                        DataRow newRow = deleteClaimsMsg.dataToSend.NewRow();
                        newRow[0] = Convert.ToInt32(dgvData.SelectedRows[i].Cells["ID"].Value);
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
                //Если выбрана опция формального удаления заявки
                else
                {
                    //Создание сообщения
                    NetMessage deleteClaimsMsg = new NetMessage(NetMessage.commandType.DeleteClaim);
                    deleteClaimsMsg.text = "Удалена администратором";

                    deleteClaimsMsg.dataToSend.Columns.Add("ClaimID");
                    deleteClaimsMsg.dataToSend.Columns.Add("ClaimDeletionReceivedDate");
                    deleteClaimsMsg.dataToSend.Columns.Add("ClaimDeletionReceivedTime");
                    deleteClaimsMsg.dataToSend.Columns.Add("ClaimDeletedByID");
                    deleteClaimsMsg.dataToSend.Columns.Add("ClaimStatus");

                    //формирование списка ID
                    for (int i = 0; i < dgvData.SelectedRows.Count; i++)
                    {
                        DataRow newRow = deleteClaimsMsg.dataToSend.NewRow();
                        newRow["ClaimID"] = Convert.ToInt32(dgvData.SelectedRows[i].Cells["ID"].Value);
                        newRow["ClaimDeletionReceivedDate"] = "CONVERT(VARCHAR(50), GETDATE(), 102)";
                        newRow["ClaimDeletionReceivedTime"] = "CONVERT(VARCHAR(50), GETDATE(), 8)";
                        newRow["ClaimDeletedByID"] = currentUserData.Rows[0]["PersID"];
                        newRow["ClaimStatus"] = "Удалена администратором";

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
            }
            else
            {
                MessageBox.Show("Необходимо выбрать по крайней мере одну заявку");
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
                reportClaimsCount = data.dataToSend;
                getDataButtonClicked = false;
            }
            //в противном случае, входящее сообщение обрабатывается стандартным способом
            else
            {
                receivedData = data.dataToSend;

                if (getTableColumnsRequest)
                {
                    //очистить dgvData (содержимое, структуру)
                    dgvData.Rows.Clear();
                    dgvData.Columns.Clear();

                    if (tbSelector.SelectedTab.Name == "tpUsers")
                    {
                        //очистить dgvParams (содержимое, структуру)
                        dgvUsersParams.Columns.Clear();
                        
                        //сформировать колонки dgvData
                        CreateDGVColumns(dgvData, receivedData);

                        //сформировать dgvParams
                        SetDGVParams(dgvUsersParams);

                        //скрыть столбец с ID
                        try
                        {
                            dgvData.Columns["ID"].Visible = false;
                        }
                        catch { }
                    }

                    //флаг запроса столбцов таблицы снимается
                    getTableColumnsRequest = false;
                }

                if (getDataButtonClicked)
                {
                    //загрузить данные в dgvData (структура dgvData к этому моменту уже должна быть сформирована,
                    //т.к. перед этим была получена информация о столбцах)
                    

                    //сбросить флаг нажатия кнопки getData (users, claims или groups)
                    getDataButtonClicked = false;
                }

                //получение списка сотрудников из представления UsersView и сохранение списка в переменной usersData
                if (getUsersData == true)
                {
                    usersData = receivedData;
                    getUsersData = false;
                }

                //получение списка групп из представления GroupsView и сохранение списка в переменной groupsData
                if (getGroupsData == true)
                {
                    groupsData = receivedData;
                    getGroupsData = false;
                }

                //получение списка "официальных" категорий заявок и сохранение его в переменной commonCatData
                if (getCommonCatData == true)
                {
                    commonCatData = receivedData;
                    getCommonCatData = false;
                }

                //получение списка "внутренних" категорий заявок и сохранение его в переменной internalCatData
                if (getInternalCatData == true)
                {
                    internalCatData = receivedData;
                    getInternalCatData = false;
                }
            }
        }

        //настройка dgvParams
        public void SetDGVParams(DataGridView targetDGV)
        {
            targetDGV.Font = new Font(targetDGV.Font.FontFamily, 10);
            DataGridViewComboBoxColumn newCbColumn = new DataGridViewComboBoxColumn();
            newCbColumn.Name = "Parameter";
            newCbColumn.HeaderText = "Параметр";
            for (int col = 0; col < dgvData.Columns.Count; col++)
            {
                //чтобы избежать ситуации, когда дата и время выбирается и в dtPeriodBegining, dtPeriodEnding,
                //и в dgvParametersSelection
                //if (dgvReport.Columns[col].HeaderText != "Дата" && dgvReport.Columns[col].HeaderText != "Время")
                {
                    newCbColumn.Items.Add(dgvData.Columns[col].HeaderText);
                }
            }
            newCbColumn.DropDownWidth = 300;
            newCbColumn.Width = 300;
            targetDGV.Columns.Add(newCbColumn);

            newCbColumn = new DataGridViewComboBoxColumn();
            newCbColumn.Name = "Relation";
            newCbColumn.HeaderText = "Отношение";
            string[] relations = { "<", ">", "=", "LIKE", "NOT LIKE" };
            foreach (string curRelation in relations)
            {
                newCbColumn.Items.Add(curRelation);
            }
            targetDGV.Columns.Add(newCbColumn);

            targetDGV.Columns.Add("Value", "Значение");
            targetDGV.Columns.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None).Width = 300;

            newCbColumn = new DataGridViewComboBoxColumn();
            newCbColumn.Name = "LogicOperator";
            newCbColumn.HeaderText = "Логич. оператор";
            newCbColumn.Items.Add("AND");
            newCbColumn.Items.Add("OR");
            targetDGV.Columns.Add(newCbColumn);
        }

        //создание колонок v2
        public void CreateDGVColumns(DataGridView targetDGV, DataTable columnsData)
        {
            //создание колонок для отчета
            targetDGV.Font = new Font(targetDGV.Font.FontFamily, 10);
            for (int row = 0; row < columnsData.Rows.Count; row++)
            {
                targetDGV.Columns.Add(columnsData.Rows[row][0].ToString(), columnsData.Rows[row][0].ToString());
            }

            //Сохранение полученных данных в переменной reportColumns для дальнейшего использования формой ExportForm
            //reportColumns = columnsData;
        }

        //заполнение целевого DataGridView данными из указанного в параметрах источника
        public void FillDGVWithDBData(DataGridView targetDGV, DataTable dataSource)
        {
            //для каждой строки полученных данных
            for (int dbRow = 0; dbRow < dataSource.Rows.Count; dbRow++)
            {
                //создается новая строка в targetDGV
                targetDGV.Rows.Add();

                //для каждой колонки полученных данных
                for (int dbCol = 0; dbCol < dataSource.Columns.Count; dbCol++)
                {
                    //выполняется сравнение с каждой колонкой targetDGV
                    for (int dgvCol = 0; dgvCol < targetDGV.Columns.Count; dgvCol++)
                    {
                        //если имена колонок совпадают
                        if (targetDGV.Columns[dgvCol].Name == dataSource.Columns[dbCol].ColumnName)
                        {
                            //создается новая ячейка в targetDGV
                            targetDGV.Rows[targetDGV.Rows.Count - 1].Cells[dgvCol].Value = dataSource.Rows[dbRow][dbCol];

                        }
                    }
                }
                //Если были загружены данные из таблицы (представления) ExecutedClaimsReport,
                //выполняется процедура конвертации временной задержки, выраженной в секундах, в текстовый формат
                if (dataSource.TableName.ToString() == "ClaimsView")
                {
                    //формирование строки, содержащей информацию о временнОй задержке между наступлением очередных событий
                    //(в блоке формирования строки, содержащей инф-ю о временной задержке, заменены все dgvReport на targetDGV)
                    if (dataSource.Rows[dbRow]["Прошло времени с поступления заявки"] != DBNull.Value)
                    {
                        targetDGV.Rows[dbRow].Cells["Прошло времени с поступления заявки"].Value = Configuration.GetTimeStringFromSeconds(Convert.ToInt32(dataSource.Rows[dbRow]["Прошло времени с поступления заявки"]));
                    }

                    if (dataSource.Rows[dbRow]["Прошло времени с назначения группы"] != DBNull.Value)
                    {
                        targetDGV.Rows[dbRow].Cells["Прошло времени с назначения группы"].Value = Configuration.GetTimeStringFromSeconds(Convert.ToInt32(dataSource.Rows[dbRow]["Прошло времени с назначения группы"]));
                    }
                    if (dataSource.Rows[dbRow]["Прошло времени с назначения исполнителя"] != DBNull.Value)
                    {
                        targetDGV.Rows[dbRow].Cells["Прошло времени с назначения исполнителя"].Value = Configuration.GetTimeStringFromSeconds(Convert.ToInt32(dataSource.Rows[dbRow]["Прошло времени с назначения исполнителя"]));
                    }
                    if (dataSource.Rows[dbRow]["Продолжительность выполнения заявки"] != DBNull.Value)
                    {
                        targetDGV.Rows[dbRow].Cells["Продолжительность выполнения заявки"].Value = Configuration.GetTimeStringFromSeconds(Convert.ToInt32(dataSource.Rows[dbRow]["Продолжительность выполнения заявки"]));
                    }
                }
            }
        }

        //запрос на получение данных БД
        public void GetDataRequest(DataGridView RequestParametersSource, string DBSourceObjectName, Client client)
        {
            getDataButtonClicked = true;

            //Формирование строки-условия для отправки на сервер в качестве условия для SQL запроса к представлению ClaimsView

            string condition = "";
            string stringsWithErrors = "";
            int errorsCounter = 0;
            for (int row = 0; row < RequestParametersSource.Rows.Count - 1; row++)
            {
                string newCondition = "";
                //если строка непоследняя, к новому состоянию нужно прибавать логический оператор,
                //если же строка последняя, то логический оператор не требуется
                if (row != RequestParametersSource.Rows.Count - 2)
                {
                    for (int col = 0; col < RequestParametersSource.Columns.Count; col++)
                    {
                        //проверка на непустые значения
                        if (RequestParametersSource.Rows[row].Cells[col].Value != null)
                        {
                            //расстановка скобок
                            if (col == 0)
                            {
                                newCondition += "([" + RequestParametersSource.Rows[row].Cells[col].Value.ToString() + "] ";
                            }
                            else
                            {
                                if (col == RequestParametersSource.Columns.Count - 2)
                                {
                                    //Выполняется проверка на наличие параметров, содержащих условия для столбцов, в которых описаны временные задержки между наступлениями событий.
                                    //Если таковые имеются, необходимо выразить строковое значение временной задержки ("00:00:00") в секундах (целочисленный тип данных)
                                    if (RequestParametersSource.Rows[row].Cells["Parameter"].Value.ToString() == "Прошло времени с поступления заявки" ||
                                        RequestParametersSource.Rows[row].Cells["Parameter"].Value.ToString() == "Прошло времени с назначения группы" ||
                                        RequestParametersSource.Rows[row].Cells["Parameter"].Value.ToString() == "Прошло времени с назначения исполнителя" ||
                                        RequestParametersSource.Rows[row].Cells["Parameter"].Value.ToString() == "Продолжительность выполнения заявки")
                                    {
                                        if (Configuration.IsTimeStringGood(RequestParametersSource.Rows[row].Cells[col].Value.ToString()))
                                        {
                                            newCondition += "'" + Configuration.GetSecondsFromTimeString(RequestParametersSource.Rows[row].Cells[col].Value.ToString()).ToString() + "') ";
                                        }
                                        else
                                        {
                                            stringsWithErrors += " строке " + (row + 1).ToString() + ", поле " + RequestParametersSource.Columns[col].HeaderText + " (неверный формат времени (HH:mm:ss))\n"; //Columns["Value"]
                                            errorsCounter++;
                                        }
                                    }
                                    else
                                    {
                                        if (RequestParametersSource.Rows[row].Cells["Relation"].Value.ToString().IndexOf("LIKE") == -1)
                                        {
                                            newCondition += "'" + RequestParametersSource.Rows[row].Cells[col].Value.ToString() + "') ";
                                        }
                                        else
                                        {
                                            newCondition += "'%" + RequestParametersSource.Rows[row].Cells[col].Value.ToString() + "%') ";
                                        }
                                    }
                                }
                                else
                                {
                                    newCondition += RequestParametersSource.Rows[row].Cells[col].Value.ToString() + " ";
                                }
                            }
                        }
                        //формирование сообщения об ошибках, если пустые значения все же есть
                        else
                        {
                            stringsWithErrors += " строке " + row.ToString() + ", поле " + RequestParametersSource.Columns[col].HeaderText + " (значение не указано)\n";
                            errorsCounter++;
                        }
                    }
                }
                else
                {
                    for (int col = 0; col < RequestParametersSource.Columns.Count - 1; col++)
                    {
                        //проверка на непустые значения
                        if (RequestParametersSource.Rows[row].Cells[col].Value != null)
                        {
                            //расстановка скобок
                            if (col == 0)
                            {
                                newCondition += "([" + RequestParametersSource.Rows[row].Cells[col].Value.ToString() + "] ";
                            }
                            else
                            {
                                if (col == RequestParametersSource.Columns.Count - 2)
                                {
                                    //Выполняется проверка на наличие параметров, содержащих условия для столбцов, в которых описаны временные задержки между наступлениями событий.
                                    //Если таковые имеются, необходимо выразить строковое значение временной задержки ("00:00:00") в секундах (целочисленный тип данных)
                                    if (RequestParametersSource.Rows[row].Cells["Parameter"].Value.ToString() == "Прошло времени с поступления заявки" ||
                                        RequestParametersSource.Rows[row].Cells["Parameter"].Value.ToString() == "Прошло времени с назначения группы" ||
                                        RequestParametersSource.Rows[row].Cells["Parameter"].Value.ToString() == "Прошло времени с назначения исполнителя" ||
                                        RequestParametersSource.Rows[row].Cells["Parameter"].Value.ToString() == "Продолжительность выполнения заявки")
                                    {
                                        if (Configuration.IsTimeStringGood(RequestParametersSource.Rows[row].Cells[col].Value.ToString()))
                                        {
                                            newCondition += "'" + Configuration.GetSecondsFromTimeString(RequestParametersSource.Rows[row].Cells[col].Value.ToString()).ToString() + "') ";
                                        }
                                        else
                                        {
                                            stringsWithErrors += " строке " + (row + 1).ToString() + ", поле " + RequestParametersSource.Columns[col].HeaderText + " (неверный формат времени (HH:mm:ss))\n";
                                            errorsCounter++;
                                        }
                                    }
                                    else
                                    {
                                        if (RequestParametersSource.Rows[row].Cells["Relation"].Value.ToString().IndexOf("LIKE") == -1)
                                        {
                                            newCondition += "'" + RequestParametersSource.Rows[row].Cells[col].Value.ToString() + "') ";
                                        }
                                        else
                                        {
                                            newCondition += "'%" + RequestParametersSource.Rows[row].Cells[col].Value.ToString() + "%') ";
                                        }
                                    }
                                }
                                else
                                {
                                    newCondition += RequestParametersSource.Rows[row].Cells[col].Value.ToString() + " ";
                                }
                            }
                        }
                        //формирование сообщения об ошибках, если пустые значения все же есть
                        else
                        {
                            stringsWithErrors += " строке " + (row + 1).ToString() + ", поле " + RequestParametersSource.Columns[col].HeaderText + " (значение не указано)\n";
                            errorsCounter++;
                        }
                    }
                }
                //проверка ввода даты и времени, соответствующих формату
                if (RequestParametersSource.Rows[row].Cells["Parameter"].Value.ToString() == "Дата")
                {
                    string datePattern = "[0-9]{4}.{1}[0-9]{2}.{1}[0-9]{2}";
                    Regex regex = new Regex(datePattern);
                    if (!regex.IsMatch(RequestParametersSource.Rows[row].Cells["Value"].Value.ToString()))
                    {
                        stringsWithErrors += " строке " + (row + 1).ToString() + ", поле " + RequestParametersSource.Columns["Value"].HeaderText + " (неверный формат даты (yyyy.MM.dd))\n";
                        errorsCounter++;
                    }
                }

                //проверка ввода даты и времени, соответствующих формату
                if (RequestParametersSource.Rows[row].Cells["Parameter"].Value.ToString() == "Время")
                {
                    string datePattern = "[0-9]{2}:{1}[0-9]{2}:{1}[0-9]{2}";
                    Regex regex = new Regex(datePattern);
                    if (!regex.IsMatch(RequestParametersSource.Rows[row].Cells["Value"].Value.ToString()))
                    {
                        stringsWithErrors += " строке " + (row + 1).ToString() + ", поле " + RequestParametersSource.Columns["Value"].HeaderText + " (неверный формат времени (HH:mm:ss))\n";
                        errorsCounter++;
                    }
                }

                //Добавление только что сформированного нового условия к общему условию запроса
                condition += newCondition;
            }
            if (errorsCounter > 0)
            {
                stringsWithErrors = "Обнаружены ошибки в:\n" + stringsWithErrors + "\n\nЗапрос отправлен не был";
                MessageBox.Show(stringsWithErrors);
            }
            //если ошибок обнаружено не было,
            //можно приступать к следующему этапу формирования переменной condition
            //и отправки запроса на сервер
            else
            {
                //на случай, если все параметры из RequestParametersSource были удалены
                if (condition.Length > 0)
                {
                    condition = condition.Substring(0, condition.Length - 1);
                }

                //Дополнительные скобки для объединения двух одинаковых параметров.
                //Два вложенных цикла по всем строкам столбца Parameter. Если обнаруживается, что значения строк в этом столбце совпали при том, что значения счетчиков не совпали
                //(т.к. само собой разумеется, что значения строки в столбце совпадает со значением этой же самой строки в этом же столбце),
                //выполняется расстановка скобок, объединяющих эти два или более (?) совпадающих параметра(ов)
                int curParameterIndex = 0;
                while (curParameterIndex < RequestParametersSource.Rows.Count - 2)
                {
                    if (RequestParametersSource.Rows[curParameterIndex].Cells["Parameter"].Value.ToString() == RequestParametersSource.Rows[curParameterIndex + 1].Cells["Parameter"].Value.ToString())
                    {
                        int sameParametersCount = 2; //потому что уже найдено совпадение имен 2-х параметров
                        string sameParametersName = RequestParametersSource.Rows[curParameterIndex].Cells["Parameter"].Value.ToString();
                        int i = curParameterIndex + 2;
                        while ((i < (RequestParametersSource.Rows.Count - 1)) && (RequestParametersSource.Rows[i].Cells["Parameter"].Value.ToString() == sameParametersName))
                        {
                            i++;
                            sameParametersCount++;
                        }

                        //на данный момент было вычислено количество повторяющихся элементов (sameParametersCount) и определен индекс следующего неповторяющегося элемента (i).
                        //Теперь нужно определить позицию в строке первого и последнего повторяющихся параметров и заключить их в скобки.
                        //Т.к. каждая пара "параметр значение" заключена в скобки, можно вычислить индекс первого повторяющегося параметра, отсчитав необходимое кол-во пар скобок.
                        //Например, если первый повторяющийся параметр в списке параметров находится на 3 месте (индекс = 2), значит, нужно отсчитать 2 пары скобок.
                        int passedBracketsCount = 0;
                        int curChar = 0;
                        while (passedBracketsCount <= curParameterIndex && curChar < condition.Length)
                        {
                            //condition[curChar + 1] != '(' нужно, чтобы счетчик не считал двойные скобки
                            if (condition[curChar] == '(' && condition[curChar + 1] != '(')
                            {
                                passedBracketsCount++;
                            }
                            curChar++;
                        }
                        condition = condition.Insert(curChar, "(");

                        //открывающая скобка, объединяющая повторяющиеся параметры, добавлена. Теперь необходимо вычислить позицию для закрывающей скобки
                        //т.к. индекс следующего элемента списка, отличающегося от найденых повторяющихся элементов, известен (i), известен и индекс последнего повторяющегося
                        //элемента - i-1
                        passedBracketsCount = 0;
                        curChar = 0;
                        while (passedBracketsCount <= i - 1 && curChar < condition.Length - 1)
                        {
                            //условие condition[curChar-1] != ')' добавлено для того, чтобы счетчик не считал двойные скобки
                            if (condition[curChar] == ')' && condition[curChar - 1] != ')')
                            {
                                passedBracketsCount++;
                            }
                            curChar++;
                        }
                        condition = condition.Insert(curChar, ")");

                        //i - 1 потому что далее переменная curParameterIndex еще раз увеличивается на 1
                        curParameterIndex = i - 1;
                    }
                    curParameterIndex++;
                }

                //Еще одно объединение скобками:
                //когда в списке параметров 2 раза встречаются 2 чередующихся параметра "Дата" и "Время", следующих друг за другом (дата, время, дата, время)
                //они попарно объединяются скобками, а затем заключаются в скобки целиком ((дата, время) (дата, время)).
                //Это позволит создавать суточные запросы, указывая время начала и конца периода
                curParameterIndex = 0;
                while (curParameterIndex < RequestParametersSource.Rows.Count - 4)
                {
                    if (RequestParametersSource.Rows[curParameterIndex].Cells["Parameter"].Value.ToString() == "Дата"
                        && RequestParametersSource.Rows[curParameterIndex + 1].Cells["Parameter"].Value.ToString() == "Время"
                        && RequestParametersSource.Rows[curParameterIndex + 2].Cells["Parameter"].Value.ToString() == "Дата"
                        && RequestParametersSource.Rows[curParameterIndex + 3].Cells["Parameter"].Value.ToString() == "Время")
                    {
                        //план действий такой же, как и при расстановке объединяющих скобок для одинаковых параметров
                        int firstIndex = curParameterIndex;
                        int lastIndex = firstIndex + 3;

                        //расстановка скобок в начале двух пар
                        int passedBracketsCount = 0;
                        int curChar = 0;
                        while (passedBracketsCount <= firstIndex && curChar < condition.Length)
                        {
                            //condition[curChar + 1] != '(' нужно, чтобы счетчик не считал двойные скобки
                            if (condition[curChar] == '(' && condition[curChar + 1] != '(')
                            {
                                passedBracketsCount++;
                            }
                            curChar++;
                        }
                        condition = condition.Insert(curChar, "((");

                        //расстановка скобок между парами
                        passedBracketsCount = 0;
                        curChar = 0;
                        while (passedBracketsCount <= firstIndex + 1 && curChar < condition.Length - 1)
                        {
                            //условие condition[curChar-1] != ')' добавлено для того, чтобы счетчик не считал двойные скобки
                            if (condition[curChar] == ')' && condition[curChar - 1] != ')')
                            {
                                passedBracketsCount++;
                            }
                            curChar++;
                        }
                        condition = condition.Insert(curChar, ")");

                        passedBracketsCount = 0;
                        curChar = 0;
                        while (passedBracketsCount <= firstIndex + 2 && curChar < condition.Length)
                        {
                            //condition[curChar + 1] != '(' нужно, чтобы счетчик не считал двойные скобки
                            if (condition[curChar] == '(' && condition[curChar + 1] != '(')
                            {
                                passedBracketsCount++;
                            }
                            curChar++;
                        }
                        condition = condition.Insert(curChar, "(");

                        //расстановка скобок в конце двух пар
                        passedBracketsCount = 0;
                        curChar = 0;
                        while (passedBracketsCount <= lastIndex && curChar < condition.Length - 1)
                        {
                            //условие condition[curChar-1] != ')' добавлено для того, чтобы счетчик не считал двойные скобки
                            if (condition[curChar] == ')' && condition[curChar - 1] != ')')
                            {
                                passedBracketsCount++;
                            }
                            curChar++;
                        }
                        condition = condition.Insert(curChar, "))");

                    }
                    curParameterIndex++;
                }

                //переменная-флаг. Принимает true, если запрос был успешно отправлен на сервер
                bool isRequestSent = false;
                try
                {
                    //создание сообщения для отправки запроса
                    NetMessage msg = new NetMessage(NetMessage.commandType.GetInfo, "GetData");

                    //в поле dataToSend создается 2 столбца и 1 строка - там будет хранится условие, которое необходимо
                    //передать на сервер, и имя таблицы, из которой необходимо будет получить данные
                    msg.dataToSend.Columns.Add("Condition");
                    msg.dataToSend.Columns.Add("TableName");
                    DataRow newDR = msg.dataToSend.NewRow();
                    newDR[0] = condition;
                    newDR[1] = DBSourceObjectName;
                    msg.dataToSend.Rows.Add(newDR);

                    //отправка запроса на сервер
                    client.SendMessage(msg, false);

                    //установка значения флага
                    isRequestSent = true;
                }
                catch (Exception ex)
                {
                    //отправка сообщения об ошибке
                    MessageBox.Show("Запрос на получение данных для формирования отчета отправить не удалось\n\n" + ex.Message);
                    isRequestSent = false;
                    getDataButtonClicked = false;
                }

                //если запрос на получение данных был выполнен успешно,
                //полученные данные загружаются в DataGridView
                if (isRequestSent)
                {
                    dgvData.Rows.Clear();
                    FillDGVWithDBData(dgvData, receivedData);
                }
            }
            getDataButtonClicked = false;
        }

        //нажатие на кнопку выбора пользователя
        private void tsbSelectUser_Click(object sender, EventArgs e)
        {
            getUsersButtonClicked = true;

            //запрос на сервер, формирование переменной usersData, заполнение cbUsersList значениями
            try
            {
                NetMessage msg = new NetMessage(NetMessage.commandType.GetInfo, "GetUsers");
                client.SendMessage(msg, false);

                usersDataForLogin = receivedData;

                DataView usersDataView = new DataView();
                usersDataView = usersDataForLogin.DefaultView;
                usersDataView.Sort = "PersLastName asc";
                usersDataForLogin = usersDataView.ToTable();

                loginForm.cbUsersList.Items.Clear();
                for (int rowCounter = 0; rowCounter < usersDataForLogin.Rows.Count; rowCounter++)
                {
                    if (usersDataForLogin.Rows[rowCounter]["PersID"].ToString() != (24).ToString())
                    {
                        loginForm.cbUsersList.Items.Add(usersDataForLogin.Rows[rowCounter]["PersLastName"] + " " + usersDataForLogin.Rows[rowCounter]["PersFirstName"] + " " + usersDataForLogin.Rows[rowCounter]["PersPatronymic"]);
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

                string enteredPswd = loginForm.mtbUserPassword.Text;
                //индекс выбранного из списка сотрудника
                int selectedIndex = -1; //loginForm.cbUsersList.SelectedIndex;

                //поиск строки таблицы usersData, содержащий информацию о выбранном пользователе
                for (int i = 0; i < usersDataForLogin.Rows.Count; i++)
                {
                    if (loginForm.cbUsersList.SelectedItem.ToString().IndexOf(usersDataForLogin.Rows[i]["PersFirstName"].ToString()) != -1 &&
                        loginForm.cbUsersList.SelectedItem.ToString().IndexOf(usersDataForLogin.Rows[i]["PersLastName"].ToString()) != -1 &&
                        loginForm.cbUsersList.SelectedItem.ToString().IndexOf(usersDataForLogin.Rows[i]["PersPatronymic"].ToString()) != -1)
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
                for (int colCounter = 0; colCounter < usersDataForLogin.Columns.Count; colCounter++)
                {
                    loginUserData.Columns.Add(usersDataForLogin.Columns[colCounter].ColumnName);
                    newRow[colCounter] = usersDataForLogin.Rows[selectedIndex][colCounter];
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
                    if (loginUserData.Rows[0]["PersRole"].ToString() == "Администратор")
                    {
                        //Отключение от сервера (в случае, если клиент был к нему подключен) для последующего переподключения.
                        //Процедура необходима для того, чтобы в списке connectedClients на сервере
                        //оказался данный ExecutorModule с корректным значением свойства dbUserID
                        if (client.connectedToServer)
                        {
                            try
                            {
                                client.DisconectFromServer();
                                lStatus.Text = "Вход не выполнен";
                                lStatus.ForeColor = Color.Red;
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

                        //отправка информации на сервер с запросом получения информации о заявках
                        try
                        {
                            //и выполнение процедуры "подключения" к серверу
                            client.ConnectToServer(client.serverIP, client.serverPort);

                            //обновление информации о состоянии подключения
                            lStatus.Text = "Вход выполнен";
                            lStatus.ForeColor = Color.Green;

                            //обновление информации о активном пользователе
                            lActiveUser.Text = currentUserData.Rows[0]["PersLastName"] + "  " + currentUserData.Rows[0]["PersFirstName"] + " " + currentUserData.Rows[0]["PersPatronymic"];

                            //переменная-флаг, указывающая, что вход выполнен, принимает значение true
                            logined = true;

                            //снятие флага, что была нажата кнопка выбора сотрудника
                            getUsersButtonClicked = false;

                            //Так как в момент логина (или релогина) одна из вкладок будет активна в любом случае,
                            //необходимо получить список столбцов таблицы в зависимости от выбранной вкладки, чтобы
                            //у пользователя сразу же была возможность указать условия запроса
                            getTableColumnsRequest = true;

                            //создание сообщения для отправки на сервер необходимой информации для выполнения запроса
                            msg = new NetMessage(NetMessage.commandType.GetInfo, "GetColumns");

                            //возможны 3 варианта: пользователи, группы и заявки.
                            //Для каждого необходимо выполнить свой запрос

                            if (tbSelector.SelectedTab.Name == "tpUsers")
                            {
                                msg.dataToSend.Columns.Add("TableName");
                                DataRow nr = msg.dataToSend.NewRow();
                                nr["TableName"] = "UsersView";
                                msg.dataToSend.Rows.Add(nr);
                                client.SendMessage(msg, false);

                            }
                            if (tbSelector.SelectedTab.Name == "tpGroups")
                            {
                                msg.dataToSend.Columns.Add("TableName");
                                DataRow nr = msg.dataToSend.NewRow();
                                nr["TableName"] = "GroupsView";
                                msg.dataToSend.Rows.Add(nr);
                                client.SendMessage(msg, false);
                            }
                            if (tbSelector.SelectedTab.Name == "tpClaims")
                            {
                                msg.dataToSend.Columns.Add("TableName");
                                DataRow nr = msg.dataToSend.NewRow();
                                nr["TableName"] = "ClaimsView";
                                msg.dataToSend.Rows.Add(nr);
                                client.SendMessage(msg, false);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Отправить данные на сервер не удалось\n\n" + ex.Message);
                            getUsersButtonClicked = false;
                            getTableColumnsRequest = false;
                            logined = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Недостаточно прав на использование модуля администратора");
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
            getUsersButtonClicked = false;
        }

        //Приведение даты и времени из двух ячеек dgvData к типу DateTime
        //Используется при редактировании заявок
        public DateTime StringToDateTime(string date, string time)
        {
            try
            {
                DateTime result = new DateTime(Convert.ToInt32(date.Split('.')[0]), Convert.ToInt32(date.Split('.')[1]), Convert.ToInt32(date.Split('.')[2]), Convert.ToInt32(time.Split(':')[0]), Convert.ToInt32(time.Split(':')[1]), Convert.ToInt32(time.Split(':')[2]));
                return result;
            }
            catch
            {
                return DateTime.Now;
            }
        }

        //Обратная операция. Приводит выбранное значение контрола DateTimePicker
        //к нужному формату строки
        public string DateTimeToDateString(DateTimePicker sourceDTP)
        {
            string result = "";
            if (sourceDTP.Value != null)
            {
                result += sourceDTP.Value.Year.ToString();

                if (sourceDTP.Value.Month < 10)
                {
                    result += ".0" + sourceDTP.Value.Month.ToString();
                }
                else
                {
                    result += "." + sourceDTP.Value.Month.ToString();
                }

                if (sourceDTP.Value.Day < 10)
                {
                    result += ".0" + sourceDTP.Value.Day.ToString();
                }
                else
                {
                    result += "." + sourceDTP.Value.Day.ToString();
                }
                return result;
            }
            else
            {
                return result;
            }
        }

        //Получение списка пользователей из базы данных (из представления UsersView).
        //Используется при редактировании заявок
        public void GetUsers()
        {
            try
            {
                getUsersData = true;

                NetMessage msg = new NetMessage(NetMessage.commandType.GetInfo, "GetData");
                msg.dataToSend.Columns.Add("Condition");
                msg.dataToSend.Columns.Add("TableName");
                DataRow newRow = msg.dataToSend.NewRow();
                newRow[0] = "Статус = 'Работает'";
                newRow[1] = "UsersView";
                msg.dataToSend.Rows.Add(newRow);
                MainWindow.client.SendMessage(msg, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Получение списка групп из базы данных
        //Используется при редактировании заявок
        public void GetGroups()
        {
            try
            {
                getGroupsData = true;

                NetMessage msg = new NetMessage(NetMessage.commandType.GetInfo, "GetData");
                msg.dataToSend.Columns.Add("Condition");
                msg.dataToSend.Columns.Add("TableName");
                DataRow newRow = msg.dataToSend.NewRow();
                newRow[0] = "[Отображение группы] = 'True'";
                newRow[1] = "GroupsView";
                msg.dataToSend.Rows.Add(newRow);
                MainWindow.client.SendMessage(msg, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Получение списка "официальных" категорий заявок из базы данных
        //Используется при редактировании заявок
        public void GetCommonCategories()
        {
            try
            {
                getCommonCatData = true;

                NetMessage msg = new NetMessage(NetMessage.commandType.GetInfo, "GetData");
                msg.dataToSend.Columns.Add("Condition");
                msg.dataToSend.Columns.Add("TableName");
                DataRow newRow = msg.dataToSend.NewRow();
                newRow[0] = "";
                newRow[1] = "CommonCategories";
                msg.dataToSend.Rows.Add(newRow);
                MainWindow.client.SendMessage(msg, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Получение списка внутренних категорий заявок из базы данных
        //Используется при редактировании заявок
        public void GetInternalCategories()
        {
            try
            {
                getInternalCatData = true;

                NetMessage msg = new NetMessage(NetMessage.commandType.GetInfo, "GetData");
                msg.dataToSend.Columns.Add("Condition");
                msg.dataToSend.Columns.Add("TableName");
                DataRow newRow = msg.dataToSend.NewRow();
                newRow[0] = "";
                newRow[1] = "InternalCategories";
                msg.dataToSend.Rows.Add(newRow);
                MainWindow.client.SendMessage(msg, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        public void FillComboBoxWithUsersData(ComboBox targetComboBox, DataTable source, bool useCondition, string conditionValue, string conditionColumn)
        {
            targetComboBox.Items.Clear();
            if (!useCondition)
            {
                for (int rowCounter = 0; rowCounter < source.Rows.Count; rowCounter++)
                {
                    targetComboBox.Items.Add(source.Rows[rowCounter]["Фамилия"] + " " + source.Rows[rowCounter]["Имя"] + " " + source.Rows[rowCounter]["Отчество"]);
                }
            }
            else
            {
                for (int rowCounter = 0; rowCounter < source.Rows.Count; rowCounter++)
                {
                    if (source.Rows[rowCounter][conditionColumn].ToString() == conditionValue)
                    {
                        targetComboBox.Items.Add(source.Rows[rowCounter]["Фамилия"] + " " + source.Rows[rowCounter]["Имя"] + " " + source.Rows[rowCounter]["Отчество"]);
                    }
                }
            }
        }

        public void FillComboBoxWithGroupsData(ComboBox targetComboBox, DataTable source)
        {
            targetComboBox.Items.Clear();
            for (int rowCounter = 0; rowCounter < source.Rows.Count; rowCounter++)
            {
                targetComboBox.Items.Add(source.Rows[rowCounter]["Группа"]);
            }
        }

        public void FillComboBoxWithCommonCatData(ComboBox targetComboBox, DataTable source)
        {
            targetComboBox.Items.Clear();
            for (int rowCounter = 0; rowCounter < source.Rows.Count; rowCounter++)
            {
                targetComboBox.Items.Add(source.Rows[rowCounter]["CommonCatName"] + " - " + source.Rows[rowCounter]["CommonCatDiscription"]);
            }
        }

        public void FillComboBoxWithInternalCatData(ComboBox targetComboBox, DataTable source)
        {
            targetComboBox.Items.Clear();
            for (int rowCounter = 0; rowCounter < source.Rows.Count; rowCounter++)
            {
                targetComboBox.Items.Add(source.Rows[rowCounter]["IntCatName"]);
            }
        }

        //Выбирает среди существующих элементов ComboBox тот, значение которого совпадает со значением указанного столбца 
        //в выделенной для редактирования строке dgvData
        public void PickUpComboBoxItem(ComboBox targetComboBox, string dgvDataColumnName)
        {
            try
            {
                for (int i = 0; i < targetComboBox.Items.Count; i++)
                {
                    if (dgvDataColumnName != "Категория заявки")
                    {
                        if (targetComboBox.Items[i].ToString() == dgvData.SelectedRows[0].Cells[dgvDataColumnName].Value.ToString())
                        {
                            targetComboBox.SelectedIndex = i;
                        }
                    }
                    else
                    {
                        if (targetComboBox.Items[i].ToString().IndexOf(dgvData.SelectedRows[0].Cells[dgvDataColumnName].Value.ToString()) != -1)
                        {
                            targetComboBox.SelectedIndex = i;
                        }
                    }
                }
            }
            catch
            {

            }
        }

        //Находит в указанной в параметрах DataTable строку, совпадающую с выбранной в указанном в параметрах ComboBox, и возвращает значение столбца "ID" для 
        //найденного элемента. Возвращает -1, если элемент не был найден
        public int GetUserIDFromSelectedItem(ComboBox ComboBoxWithItems)
        {
            if (ComboBoxWithItems.SelectedIndex != -1)
            {
                string selectedUser = ComboBoxWithItems.Items[ComboBoxWithItems.SelectedIndex].ToString();
                for (int i = 0; i < usersData.Rows.Count; i++)
                {
                    string dbUser = usersData.Rows[i]["Фамилия"].ToString() + " " + usersData.Rows[i]["Имя"].ToString() + " " + usersData.Rows[i]["Отчество"].ToString();
                    if (selectedUser == dbUser)
                    {
                        return Convert.ToInt32(usersData.Rows[i]["ID"]);
                    }
                }
            }
            return -1;
        }

        public int GetGroupIDFromSelectedItem(ComboBox ComboBoxWithItems)
        {
            if (ComboBoxWithItems.SelectedIndex != -1)
            {
                string selectedGroup = ComboBoxWithItems.Items[ComboBoxWithItems.SelectedIndex].ToString();
                for (int i = 0; i < groupsData.Rows.Count; i++)
                {
                    string dbGroup = groupsData.Rows[i]["Группа"].ToString();
                    if (selectedGroup == dbGroup)
                    {
                        return Convert.ToInt32(groupsData.Rows[i]["ID"]);
                    }
                }
            }
            return -1;
        }

        //редактирование выбранной заявки
        public void EditClaim()
        {
            if (dgvData.SelectedRows.Count == 1)
            {
                //Создание объекта формы
                ClaimChangingForm ccf = new ClaimChangingForm();


                //-------------Передача в форму данных о выбранной заявке--------------------//


                //Передаются значения всех полей таблицы из БД, в том числе и еще незаполненных на момент редактирования заявки,
                //поэтому часть из них передается в блоке try


                ccf.lClaimID.Text = dgvData.SelectedRows[0].Cells["ID"].Value.ToString();
                ccf.dtpClaimReceivedDate.Value = StringToDateTime(dgvData.SelectedRows[0].Cells["Дата"].Value.ToString(), dgvData.SelectedRows[0].Cells["Время"].Value.ToString());
                ccf.tbClaimSenderName.Text = dgvData.SelectedRows[0].Cells["Имя заявителя"].Value.ToString();
                ccf.tbClaimSenderPhone.Text = dgvData.SelectedRows[0].Cells["Телефон заявителя"].Value.ToString();
                ccf.tbRoom.Text = dgvData.SelectedRows[0].Cells["Комната"].Value.ToString();
                ccf.tbClaimUserName.Text = dgvData.SelectedRows[0].Cells["Имя пользователя"].Value.ToString();
                ccf.tbHostName.Text = dgvData.SelectedRows[0].Cells["ПК"].Value.ToString();
                ccf.tbHostIP.Text = dgvData.SelectedRows[0].Cells["IP"].Value.ToString();
                ccf.tbAffectedObjectsCount.Text = dgvData.SelectedRows[0].Cells["Кол-во затронутых объектов"].Value.ToString();
                ccf.tbClaim.Text = dgvData.SelectedRows[0].Cells["Заявка"].Value.ToString();
                ccf.tbAddInfo.Text = dgvData.SelectedRows[0].Cells["Доп. инфо"].Value.ToString();
                ccf.tbClaimExecutionDescription.Text = dgvData.SelectedRows[0].Cells["Предпринятые действия"].Value.ToString();


                GetUsers();
                try //на случай, если список получить не удалось
                {
                    FillComboBoxWithUsersData(ccf.cbGroupOrdererName, usersData, true, "Контролер", "Роль");
                    FillComboBoxWithUsersData(ccf.cbExecName, usersData, true, "Исполнитель", "Роль");
                    FillComboBoxWithUsersData(ccf.cbExecOrdererName, usersData, true, "Исполнитель", "Роль");
                }
                catch { }


                GetGroups();
                try //на случай, если список получить не удалось
                {
                    FillComboBoxWithGroupsData(ccf.cbGroupName, groupsData);
                }
                catch { }


                GetCommonCategories();
                try //на случай, если список получить не удалось
                {
                    FillComboBoxWithCommonCatData(ccf.cbClaimCategory, commonCatData);
                }
                catch { }


                GetInternalCategories();
                try
                {
                    FillComboBoxWithInternalCatData(ccf.cbClaimSpecCategory, internalCatData);
                }
                catch { }


                try //потому что значения дат и времен для разных этапов жизненного цикла заявки могут отсутствовать на момент ее редактирования
                {
                    ccf.dtpGroupOrderDate.Value = StringToDateTime(dgvData.SelectedRows[0].Cells["Дата назначения группы"].Value.ToString(), dgvData.SelectedRows[0].Cells["Время назначения группы"].Value.ToString());
                    ccf.dtpExecOrderDate.Value = StringToDateTime(dgvData.SelectedRows[0].Cells["Дата назначения исполнителя"].Value.ToString(), dgvData.SelectedRows[0].Cells["Время назначения исполнителя"].Value.ToString());
                    ccf.dtpClaimStartExecDate.Value = StringToDateTime(dgvData.SelectedRows[0].Cells["Дата начала выполнения заявки"].Value.ToString(), dgvData.SelectedRows[0].Cells["Время начала выполнения заявки"].Value.ToString());
                    ccf.dtpClaimExecEndDate.Value = StringToDateTime(dgvData.SelectedRows[0].Cells["Дата завершения выполнения заявки"].Value.ToString(), dgvData.SelectedRows[0].Cells["Время завершения выполнения заявки"].Value.ToString());
                }
                catch { }
                
                PickUpComboBoxItem(ccf.cbClaimCategory, "Категория заявки");
                PickUpComboBoxItem(ccf.cbClaimSpecCategory, "Внутренняя категория заявки");
                PickUpComboBoxItem(ccf.cbExecName, "Исполнитель");
                PickUpComboBoxItem(ccf.cbExecOrdererName, "Назначивший исполнителя");
                PickUpComboBoxItem(ccf.cbGroupName, "Ответственная группа");
                PickUpComboBoxItem(ccf.cbGroupOrdererName, "Контролер");
                PickUpComboBoxItem(ccf.cbClaimStatus, "Статус");
                                
                
                //-------------Конец передачи в форму данных о выбранной заявке--------------------//


                //Отображение формы на экране
                //Если была нажата кнопка OK
                if (ccf.ShowDialog() == DialogResult.OK)
                {
                    //Подготовка данных для отправки на сервер
                    DataTable dataToSend = new DataTable();
                    dataToSend.Columns.Add("ClaimID");
                    dataToSend.Columns.Add("ClaimSenderName");
                    dataToSend.Columns.Add("ClaimSenderPhone");
                    dataToSend.Columns.Add("ClaimSenderRoom");
                    dataToSend.Columns.Add("ClaimSenderUserName");
                    dataToSend.Columns.Add("ClaimSenderHostName");
                    dataToSend.Columns.Add("ClaimSenderHostIP");
                    dataToSend.Columns.Add("TypeOfIssue");
                    dataToSend.Columns.Add("ClaimDiscription");
                    dataToSend.Columns.Add("ClaimReceivedDate");
                    dataToSend.Columns.Add("ClaimReceivedTime");
                    if (ccf.cbGroupOrdererName.SelectedIndex != -1)
                    {
                        dataToSend.Columns.Add("ExecGroupOrdererID");
                    }
                    if (ccf.cbGroupName.SelectedIndex != -1)
                    {
                        dataToSend.Columns.Add("ExecGroupID");
                    }
                    if (ccf.chbGroupOrderDate.Checked)
                    {
                        dataToSend.Columns.Add("ExecGroupOrderReceivedDate");
                        dataToSend.Columns.Add("ExecGroupOrderReceivedTime");
                    }
                    if (ccf.cbExecName.SelectedIndex != -1)
                    {
                        dataToSend.Columns.Add("ExecID");
                    }
                    if (ccf.cbExecOrdererName.SelectedIndex != -1)
                    {
                        dataToSend.Columns.Add("ExecOrdererID");
                    }
                    if (ccf.chbExecOrderDate.Checked)
                    {
                        dataToSend.Columns.Add("ExecOrderReceivedDate");
                        dataToSend.Columns.Add("ExecOrderReceivedTime");
                    }
                    if (ccf.chbExecStartDate.Checked)
                    {
                        dataToSend.Columns.Add("ClaimExecStartReceivedDate");
                        dataToSend.Columns.Add("ClaimExecStartReceivedTime");
                    }
                    dataToSend.Columns.Add("ClaimExecutionDescription");
                    if (ccf.chbExecEndDate.Checked)
                    {
                        dataToSend.Columns.Add("ClaimExecCompleteReceivedDate");
                        dataToSend.Columns.Add("ClaimExecCompleteReceivedTime");
                    }
                    if (ccf.cbClaimCategory.SelectedIndex != -1)
                    {
                        dataToSend.Columns.Add("ClaimCommonCategoryID");
                    }
                    if (ccf.cbClaimSpecCategory.SelectedIndex != -1)
                    {
                        dataToSend.Columns.Add("ClaimInternalCategoryID");
                    }
                    dataToSend.Columns.Add("AffectedObjectsCount");
                    if (ccf.cbClaimStatus.SelectedIndex != -1)
                    {
                        dataToSend.Columns.Add("ClaimStatus");
                    }
                    dataToSend.Columns.Add("ClaimChangesReceivedDate");
                    dataToSend.Columns.Add("ClaimChangesReceivedTime");
                    dataToSend.Columns.Add("ClaimChangerID");

                    DataRow newRow = dataToSend.NewRow();
                    newRow["ClaimID"] = Convert.ToInt32(ccf.lClaimID.Text);
                    newRow["ClaimSenderName"] = ccf.tbClaimSenderName.Text;
                    newRow["ClaimSenderPhone"] = ccf.tbClaimSenderPhone.Text;
                    newRow["ClaimSenderRoom"] = ccf.tbRoom.Text;
                    newRow["ClaimSenderUserName"] = ccf.tbClaimUserName.Text;
                    newRow["ClaimSenderHostName"] = ccf.tbHostName.Text;
                    newRow["ClaimSenderHostIP"] = ccf.tbHostIP.Text;
                    newRow["TypeOfIssue"] = ccf.tbClaim.Text;
                    newRow["ClaimDiscription"] = ccf.tbAddInfo.Text;
                    newRow["ClaimReceivedDate"] = DateTimeToDateString(ccf.dtpClaimReceivedDate);
                    newRow["ClaimReceivedTime"] = ccf.dtpClaimReceivedDate.Value.ToLongTimeString();
                    if (ccf.cbGroupOrdererName.SelectedIndex != -1)
                    {
                        newRow["ExecGroupOrdererID"] = GetUserIDFromSelectedItem(ccf.cbGroupOrdererName);
                    }
                    if (ccf.cbGroupName.SelectedIndex != -1)
                    {
                        newRow["ExecGroupID"] = GetGroupIDFromSelectedItem(ccf.cbGroupName);
                    }
                    if (ccf.chbGroupOrderDate.Checked)
                    {
                        newRow["ExecGroupOrderReceivedDate"] = DateTimeToDateString(ccf.dtpGroupOrderDate);
                        newRow["ExecGroupOrderReceivedTime"] = ccf.dtpGroupOrderDate.Value.ToLongTimeString();
                    }
                    if (ccf.cbExecName.SelectedIndex != -1)
                    {
                        newRow["ExecID"] = GetUserIDFromSelectedItem(ccf.cbExecName);
                    }
                    if (ccf.cbExecOrdererName.SelectedIndex != -1)
                    {
                        newRow["ExecOrdererID"] = GetUserIDFromSelectedItem(ccf.cbExecOrdererName);
                    }
                    if (ccf.chbExecOrderDate.Checked)
                    {
                        newRow["ExecOrderReceivedDate"] = DateTimeToDateString(ccf.dtpExecOrderDate);
                        newRow["ExecOrderReceivedTime"] = ccf.dtpExecOrderDate.Value.ToLongTimeString();
                    }
                    if (ccf.chbExecStartDate.Checked)
                    {
                        newRow["ClaimExecStartReceivedDate"] = DateTimeToDateString(ccf.dtpClaimStartExecDate);
                        newRow["ClaimExecStartReceivedTime"] = ccf.dtpClaimStartExecDate.Value.ToLongTimeString();
                    }
                    newRow["ClaimExecutionDescription"] = ccf.tbClaimExecutionDescription.Text;
                    if (ccf.chbExecEndDate.Checked)
                    {
                        newRow["ClaimExecCompleteReceivedDate"] = DateTimeToDateString(ccf.dtpClaimExecEndDate);
                        newRow["ClaimExecCompleteReceivedTime"] = ccf.dtpClaimExecEndDate.Value.ToLongTimeString();
                    }
                    newRow["AffectedObjectsCount"] = ccf.tbAffectedObjectsCount.Text;
                    if (ccf.cbClaimCategory.SelectedIndex != -1)
                    {
                        newRow["ClaimCommonCategoryID"] = Convert.ToInt32(commonCatData.Rows[ccf.cbClaimCategory.SelectedIndex]["CommonCatID"]);
                    }
                    if (ccf.cbClaimSpecCategory.SelectedIndex != -1)
                    {
                        newRow["ClaimInternalCategoryID"] = Convert.ToInt32(internalCatData.Rows[ccf.cbClaimSpecCategory.SelectedIndex]["IntCatID"]);
                    }
                    if (ccf.cbClaimStatus.SelectedIndex != -1)
                    {
                        newRow["ClaimStatus"] = ccf.cbClaimStatus.Items[ccf.cbClaimStatus.SelectedIndex].ToString();
                    }
                    newRow["ClaimChangesReceivedDate"] = "CONVERT(VARCHAR(50), GETDATE(), 102)";
                    newRow["ClaimChangesReceivedTime"] = "CONVERT(VARCHAR(50), GETDATE(), 8)";
                    newRow["ClaimChangerID"] = currentUserData.Rows[0]["PersID"];

                    dataToSend.Rows.Add(newRow);

                    NetMessage messageToSend = new NetMessage(NetMessage.commandType.UpdateClaim);
                    messageToSend.dataToSend = dataToSend;
                    messageToSend.text = ccf.lClaimID.Text;

                    //отправка данных на сервер
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
                if (dgvData.SelectedRows.Count > 1)
                {
                    MessageBox.Show("Одновременное редактирование нескольких заявок невозможно");
                }
                else
                {
                    MessageBox.Show("Необходимо выбрать заявку для редактирования");
                }
            }
        }

        public void DeleteDGVRow(DataGridView targetDGV)
        {
            //Список, в котором будут хранится индексы строк, подлежащих удалению
            List<int> selectedIndexes = new List<int>();

            //заполнение списка индексов
            for (int i = 0; i < targetDGV.SelectedRows.Count; i++)
            {
                selectedIndexes.Add(targetDGV.SelectedRows[i].Index);
            }

            //необязательное действие
            targetDGV.ClearSelection();

            //сортировка списка индексов, гарантирующая в последующем, что строки будут удаляться от последней к первой
            selectedIndexes.Sort();

            //цикл удаления строк. Цикл от последнего элемента списка selectedIndexes к первому. Соответственно, от максимального индекса удаляемой строки к минимальному
            for (int i = selectedIndexes.Count - 1; i >= 0; i--)
            {
                //Если индекс удаляемой строки больше или равен dgvParametersSelection.Rows.Count - 1, строка пропускается, т.к. в DataGridView всегда есть последняя
                //пустая строка (типа новая)
                if (selectedIndexes[i] < targetDGV.Rows.Count - 1)
                {
                    targetDGV.Rows.RemoveAt(selectedIndexes[i]);
                }
            }

            //необязательное действие
            targetDGV.ClearSelection();
        }

        //отключение от сервера при закрытии приложения
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

        private void tbSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (logined)
            {
                if (tbSelector.SelectedTab.Name.ToString() == "tpUsers")
                {
                    NetMessage msg = new NetMessage(NetMessage.commandType.GetInfo, "GetColumns");
                    msg.dataToSend.Columns.Add("TableName");
                    DataRow nr = msg.dataToSend.NewRow();
                    nr["TableName"] = "UsersView";
                    msg.dataToSend.Rows.Add(nr);
                    try
                    {
                        dgvData.Rows.Clear();
                        dgvData.Columns.Clear();
                        dgvUsersParams.Rows.Clear();
                        dgvUsersParams.Columns.Clear();
                        client.SendMessage(msg, false);
                        dgvUsersParams.Rows.Clear();
                        CreateDGVColumns(dgvData, receivedData);
                        SetDGVParams(dgvUsersParams);
                        //скрыть столбец с ID
                        try
                        {
                            dgvData.Columns["ID"].Visible = false;
                        }
                        catch { }
                    }
                    catch
                    {
                        MessageBox.Show("Error");
                    }
                }

                if (tbSelector.SelectedTab.Name.ToString() == "tpGroups")
                {
                    NetMessage msg = new NetMessage(NetMessage.commandType.GetInfo, "GetColumns");
                    msg.dataToSend.Columns.Add("TableName");
                    DataRow nr = msg.dataToSend.NewRow();
                    nr["TableName"] = "GroupsView";
                    msg.dataToSend.Rows.Add(nr);
                    try
                    {
                        dgvData.Rows.Clear();
                        dgvData.Columns.Clear();
                        dgvGroupsParams.Rows.Clear();
                        dgvGroupsParams.Columns.Clear();
                        client.SendMessage(msg, false);
                        dgvUsersParams.Rows.Clear();
                        CreateDGVColumns(dgvData, receivedData);
                        SetDGVParams(dgvGroupsParams);
                        //скрыть столбец с ID
                        try
                        {
                            dgvData.Columns["ID"].Visible = false;
                        }
                        catch { }
                    }
                    catch
                    {
                        MessageBox.Show("Error");
                    }
                }

                if (tbSelector.SelectedTab.Name.ToString() == "tpClaims")
                {
                    NetMessage msg = new NetMessage(NetMessage.commandType.GetInfo, "GetColumns");
                    msg.dataToSend.Columns.Add("TableName");
                    DataRow nr = msg.dataToSend.NewRow();
                    nr["TableName"] = "ClaimsView";
                    msg.dataToSend.Rows.Add(nr);
                    try
                    {
                        dgvData.Rows.Clear();
                        dgvData.Columns.Clear();
                        dgvClaimsParams.Rows.Clear();
                        dgvClaimsParams.Columns.Clear();
                        client.SendMessage(msg, false);
                        dgvClaimsParams.Rows.Clear();
                        CreateDGVColumns(dgvData, receivedData);
                        SetDGVParams(dgvClaimsParams);
                        //скрыть столбец с ID
                        try
                        {
                            dgvData.Columns["ID"].Visible = false;
                        }
                        catch { }
                    }
                    catch
                    {
                        MessageBox.Show("Error");
                    }
                }
            }
        }

        private void bGetUsers_Click(object sender, EventArgs e)
        {
            if (logined)
            {
                GetDataRequest(dgvUsersParams, "UsersView", client);
            }
        }

        private void bGetClaims_Click(object sender, EventArgs e)
        {
            if (logined)
            {
                GetDataRequest(dgvClaimsParams, "ClaimsView", client);
            }
        }

        private void bGetGroups_Click(object sender, EventArgs e)
        {
            if (logined)
            {
                GetDataRequest(dgvGroupsParams, "GroupsView", client);
            }
        }

        private void bAddUser_Click(object sender, EventArgs e)
        {
            //если не залогинен, ничего нельзя
            if (logined)
            {
                addUserButtonClicked = true;

                //Запрос на получение списка групп
                bool isRequestSuccessful = false;
                NetMessage groupsListRequest = new NetMessage(NetMessage.commandType.GetInfo, "GetGroups");
                try
                {
                    client.SendMessage(groupsListRequest, false);
                    isRequestSuccessful = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось выполнить запрос на получение списка групп\n\n" + ex.Message);
                    isRequestSuccessful = false;
                }

                //Если запрос на получение списка групп был выполнен успешно, можно продолжать
                if (isRequestSuccessful)
                {
                    //Создание экземпляра класса windows формы
                    AddEditUserForm AEUF = new AddEditUserForm();

                    //заполнение cbGroups полученным списком групп
                    for (int row = 0; row < receivedData.Rows.Count; row++)
                    {
                        AEUF.cbUserGroup.Items.Add(receivedData.Rows[row][1].ToString());
                    }

                    //для повторного использования
                    isRequestSuccessful = false;

                    //В случае, если была нажата кнопка ОК, формируются данные и запрос отправляется на сервер
                    if (AEUF.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            //Формирование и отправка запроса на добавление пользователя
                            NetMessage msg = new NetMessage(NetMessage.commandType.AddUser);
                            msg.dataToSend.Columns.Add("UserFirstName");
                            msg.dataToSend.Columns.Add("UserLastName");
                            msg.dataToSend.Columns.Add("UserPatronymic");
                            msg.dataToSend.Columns.Add("UserRole");
                            msg.dataToSend.Columns.Add("UserGroup");
                            msg.dataToSend.Columns.Add("UserStatus");

                            DataRow newRow = msg.dataToSend.NewRow();

                            newRow["UserFirstName"] = AEUF.tbUserFirstName.Text;
                            newRow["UserLastName"] = AEUF.tbUserLastName.Text;
                            newRow["UserPatronymic"] = AEUF.tbUserPatronymic.Text;
                            newRow["UserRole"] = AEUF.cbUserRole.Items[AEUF.cbUserRole.SelectedIndex].ToString();
                            newRow["UserGroup"] = AEUF.cbUserGroup.Items[AEUF.cbUserGroup.SelectedIndex].ToString();
                            newRow["UserStatus"] = AEUF.cbUserStatus.Items[AEUF.cbUserStatus.SelectedIndex].ToString();

                            msg.dataToSend.Rows.Add(newRow);

                            client.SendMessage(msg, false);

                            isRequestSuccessful = true;
                        }
                        catch (Exception ex)
                        {
                            isRequestSuccessful = false;
                            MessageBox.Show("Ошибка при отправке запроса на добавление нового пользователя\n\n" + ex.Message);
                        }

                        if (isRequestSuccessful)
                        {
                            MessageBox.Show("Пользователь успешно добавлен в базу данных");
                            try
                            {
                                GetDataRequest(dgvUsersParams, "UsersView", client);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Не удалось обновить информацию о пользователях/n/n" + ex.Message);
                            }
                        }
                    }
                }
            }
        }

        private void bAddGroup_Click(object sender, EventArgs e)
        {
            if (logined)
            {
                addGroupButtonClicked = true;

                GroupManagementForm GMF = new GroupManagementForm();
                GMF.Text = "Добавление новой группы";

                if (GMF.ShowDialog() == DialogResult.OK)
                {
                    NetMessage newGroupInfo = new NetMessage(NetMessage.commandType.AddGroup);
                    newGroupInfo.dataToSend.Columns.Add("GroupName");
                    newGroupInfo.dataToSend.Columns.Add("GroupComment");
                    newGroupInfo.dataToSend.Columns.Add("GroupVisibility");

                    DataRow newRow = newGroupInfo.dataToSend.NewRow();

                    newRow["GroupName"] = GMF.tbGroupName.Text;
                    newRow["GroupComment"] = GMF.tbGroupComment.Text;
                    if (GMF.chbGroupVisibility.Checked == true)
                    {
                        newRow["GroupVisibility"] = "True";
                    }
                    else
                    {
                        newRow["GroupVisibility"] = "False";
                    }

                    newGroupInfo.dataToSend.Rows.Add(newRow);

                    bool isRequestSuccessful = false;
                    try
                    {
                        client.SendMessage(newGroupInfo, false);
                        isRequestSuccessful = true;
                    }
                    catch (Exception ex)
                    {
                        isRequestSuccessful = false;
                        addGroupButtonClicked = false;
                        MessageBox.Show("Ошибка при отправке запроса на добавление новой группы\n\n" + ex.Message);
                    }

                    if (isRequestSuccessful)
                    {
                        MessageBox.Show("Группа успешно добавлена в базу данных");
                        try
                        {
                            GetDataRequest(dgvUsersParams, "GroupsView", client);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Не удалось обновить информацию о группах/n/n" + ex.Message);
                        }
                    }
                }
                addGroupButtonClicked = false;
            }
        }

        private void bChangeUserData_Click(object sender, EventArgs e)
        {
            if (logined)
            {
                if (dgvData.SelectedRows.Count == 1)
                {
                    //Запрос на получение списка групп
                    bool isRequestSuccessful = false;
                    NetMessage groupsListRequest = new NetMessage(NetMessage.commandType.GetInfo, "GetGroups");
                    try
                    {
                        client.SendMessage(groupsListRequest, false);
                        isRequestSuccessful = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Не удалось выполнить запрос на получение списка групп\n\n" + ex.Message);
                        isRequestSuccessful = false;
                    }

                    if (isRequestSuccessful)
                    {
                        //Отобразить окно добавления нового пользователя (потому что оно служит и для изменения пользовательских данных)
                        //и заполнить его поля данными выбранного пользователя
                        AddEditUserForm editUserForm = new AddEditUserForm();
                        editUserForm.Text = "Изменение пользовательских данных";

                        //заполнение cbGroups полученным списком групп
                        for (int row = 0; row < receivedData.Rows.Count; row++)
                        {
                            editUserForm.cbUserGroup.Items.Add(receivedData.Rows[row][1].ToString());
                        }

                        //Заполнение полей формы данными из dgvData
                        editUserForm.tbUserLastName.Text = dgvData.SelectedRows[0].Cells["Фамилия"].Value.ToString();
                        editUserForm.tbUserFirstName.Text = dgvData.SelectedRows[0].Cells["Имя"].Value.ToString();
                        editUserForm.tbUserPatronymic.Text = dgvData.SelectedRows[0].Cells["Отчество"].Value.ToString();
                        editUserForm.cbUserRole.Text = dgvData.SelectedRows[0].Cells["Роль"].Value.ToString();
                        editUserForm.cbUserGroup.Text = dgvData.SelectedRows[0].Cells["Группа"].Value.ToString();
                        editUserForm.cbUserStatus.Text = dgvData.SelectedRows[0].Cells["Статус"].Value.ToString();

                        editUserForm.CheckEmptyFields();

                        //подготовка сообщения с необходимыми данными для формирования запроса и отправки его на сервер.
                        //На данном этапе добавляются исходные данные пользователя. Это будет первая строка таблицы (индекс = 0)
                        NetMessage updateUserDataMsg = new NetMessage(NetMessage.commandType.UpdateUser);
                        updateUserDataMsg.dataToSend.Columns.Add("ID");
                        updateUserDataMsg.dataToSend.Columns.Add("PersFirstName");
                        updateUserDataMsg.dataToSend.Columns.Add("PersLastName");
                        updateUserDataMsg.dataToSend.Columns.Add("PersPatronymic");
                        updateUserDataMsg.dataToSend.Columns.Add("PersRole");
                        updateUserDataMsg.dataToSend.Columns.Add("PersStatus");

                        DataRow newRow = updateUserDataMsg.dataToSend.NewRow();

                        newRow["ID"] = dgvData.SelectedRows[0].Cells["ID"].Value.ToString();
                        newRow["PersFirstName"] = dgvData.SelectedRows[0].Cells["Имя"].Value.ToString();
                        newRow["PersLastName"] = dgvData.SelectedRows[0].Cells["Фамилия"].Value.ToString();
                        newRow["PersPatronymic"] = dgvData.SelectedRows[0].Cells["Отчество"].Value.ToString();
                        newRow["PersRole"] = dgvData.SelectedRows[0].Cells["Роль"].Value.ToString();
                        newRow["PersStatus"] = dgvData.SelectedRows[0].Cells["Статус"].Value.ToString();

                        updateUserDataMsg.dataToSend.Rows.Add(newRow);

                        //Если была нажата кнопка OK
                        if (editUserForm.ShowDialog() == DialogResult.OK)
                        {
                            //формируется вторая строка в таблице.
                            //Она содержит обновленные данные пользователя, которые заменят старую информацию в бд
                            newRow = updateUserDataMsg.dataToSend.NewRow();

                            newRow["PersFirstName"] = editUserForm.tbUserFirstName.Text;
                            newRow["PersLastName"] = editUserForm.tbUserLastName.Text;
                            newRow["PersPatronymic"] = editUserForm.tbUserPatronymic.Text;
                            newRow["PersRole"] = editUserForm.cbUserRole.Items[editUserForm.cbUserRole.SelectedIndex].ToString();
                            newRow["PersStatus"] = editUserForm.cbUserStatus.Items[editUserForm.cbUserStatus.SelectedIndex].ToString();

                            updateUserDataMsg.dataToSend.Rows.Add(newRow);

                            //В качестве текста сообщения будет отправлена информация о новой группе для пользователя
                            //Ее нельзя было указать в dataToSend, т.к. процесс разбора пар "Столбец - значение" автоматизирован
                            //на сервере, а для таблицы Personal, в которой нужно будет обновлять запись, не существует столбца PersGroup
                            updateUserDataMsg.text = editUserForm.cbUserGroup.Items[editUserForm.cbUserGroup.SelectedIndex].ToString();

                            isRequestSuccessful = false;
                            try
                            {
                                client.SendMessage(updateUserDataMsg, false);
                                isRequestSuccessful = true;
                            }
                            catch (Exception ex)
                            {
                                isRequestSuccessful = false;
                                MessageBox.Show("Не удалось отправить данные на сервер\n\n" + ex.Message);
                            }

                            if (isRequestSuccessful)
                            {
                                MessageBox.Show("Информация о пользователе успешно обновлена");

                                try
                                {
                                    GetDataRequest(dgvUsersParams, "UsersView", client);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Не удалось загрузить обновленную информацию о пользователях\n\n" + ex.Message);
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Выберите одну строку для редактирования");
                }

            }
        }

        private void bChangeUserPswd_Click(object sender, EventArgs e)
        {
            if (logined)
            {
                if (dgvData.SelectedRows.Count == 1)
                {
                    //подготовка сообщения с необходимыми данными для формирования запроса и отправки его на сервер.
                    NetMessage resetUserPswdMsg = new NetMessage(NetMessage.commandType.PswdChange);
                    resetUserPswdMsg.text = "PswdReset";
                    resetUserPswdMsg.dataToSend.Columns.Add("PersFirstName");
                    resetUserPswdMsg.dataToSend.Columns.Add("PersLastName");
                    resetUserPswdMsg.dataToSend.Columns.Add("PersPatronymic");
                    resetUserPswdMsg.dataToSend.Columns.Add("PersRole");
                    resetUserPswdMsg.dataToSend.Columns.Add("PersPswd");

                    DataRow newRow = resetUserPswdMsg.dataToSend.NewRow();

                    newRow["PersFirstName"] = dgvData.SelectedRows[0].Cells["Имя"].Value.ToString();
                    newRow["PersLastName"] = dgvData.SelectedRows[0].Cells["Фамилия"].Value.ToString();
                    newRow["PersPatronymic"] = dgvData.SelectedRows[0].Cells["Отчество"].Value.ToString();
                    newRow["PersRole"] = dgvData.SelectedRows[0].Cells["Роль"].Value.ToString();
                    newRow["PersPswd"] = Configuration.GetValuesBySettingsName("DefaultPswd")[0];

                    resetUserPswdMsg.dataToSend.Rows.Add(newRow);

                    bool isRequestSuccessful = false;
                    try
                    {
                        client.SendMessage(resetUserPswdMsg, false);
                        isRequestSuccessful = true;
                    }
                    catch (Exception ex)
                    {
                        isRequestSuccessful = false;
                        MessageBox.Show("Не удалось отправить данные на сервер\n\n" + ex.Message);
                    }

                    if (isRequestSuccessful)
                    {
                        MessageBox.Show("Пароль был успешно сброшен");
                    }

                }
                else
                {
                    MessageBox.Show("Выберите одну строку c пользователем, которому необходимо сбросить пароль");
                }
            }
        }

        private void tsbPswdChange_Click(object sender, EventArgs e)
        {
            if (logined)
            {
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
            }
        }

        private void bChangeGroupData_Click(object sender, EventArgs e)
        {
            if (logined)
            {
                if (dgvData.SelectedRows.Count == 1)
                {

                    GroupManagementForm GMF = new GroupManagementForm();
                    GMF.Text = "Редактирование группы";

                    GMF.tbGroupName.Text = dgvData.SelectedRows[0].Cells["Группа"].Value.ToString();
                    GMF.tbGroupComment.Text = dgvData.SelectedRows[0].Cells["Описание группы"].Value.ToString();
                    if (dgvData.SelectedRows[0].Cells["Отображение группы"].Value.ToString() == "True")
                    {
                        GMF.chbGroupVisibility.Checked = true;
                    }
                    else
                    {
                        GMF.chbGroupVisibility.Checked = false;
                    }

                    if (GMF.ShowDialog() == DialogResult.OK)
                    {
                        NetMessage updateGroupInfo = new NetMessage(NetMessage.commandType.UpdateGroup);
                        updateGroupInfo.dataToSend.Columns.Add("ID");
                        updateGroupInfo.dataToSend.Columns.Add("GroupName");
                        updateGroupInfo.dataToSend.Columns.Add("GroupComment");
                        updateGroupInfo.dataToSend.Columns.Add("GroupVisibility");

                        DataRow newRow = updateGroupInfo.dataToSend.NewRow();

                        newRow["ID"] = dgvData.SelectedRows[0].Cells["ID"].Value.ToString();
                        newRow["GroupName"] = GMF.tbGroupName.Text;
                        newRow["GroupComment"] = GMF.tbGroupComment.Text;
                        if (GMF.chbGroupVisibility.Checked == true)
                        {
                            newRow["GroupVisibility"] = "True";
                        }
                        else
                        {
                            newRow["GroupVisibility"] = "False";
                        }

                        updateGroupInfo.dataToSend.Rows.Add(newRow);

                        bool isRequestSuccessful = false;
                        try
                        {
                            client.SendMessage(updateGroupInfo, false);
                            isRequestSuccessful = true;
                        }
                        catch (Exception ex)
                        {
                            isRequestSuccessful = false;
                            addGroupButtonClicked = false;
                            MessageBox.Show("Ошибка при отправке запроса на изменение данных группы\n\n" + ex.Message);
                        }

                        if (isRequestSuccessful)
                        {
                            MessageBox.Show("Данные группы успешно обновлены");
                            try
                            {
                                GetDataRequest(dgvUsersParams, "GroupsView", client);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Не удалось обновить информацию о группах/n/n" + ex.Message);
                            }
                        }

                    }
                }
                else
                {
                    if (dgvData.SelectedRows.Count > 1)
                    {
                        MessageBox.Show("Редактирование сразу нескольких записей невозможно");
                    }
                    else
                    {
                        MessageBox.Show("Выберите запись для редактирования");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выполните вход в систему");
            }
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                splitContainer1.SplitterDistance = spliterCont1Panel1Height;
            }
        }

        private void bDeleteClaim_Click(object sender, EventArgs e)
        {
            if (dgvData.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Вы хотите удалить заявку из базы данных?", "Удалить заявку фактически или формально?", MessageBoxButtons.YesNoCancel) == DialogResult.No)
                {
                    DeleteClaim(false);

                    try
                    {
                        GetDataRequest(dgvClaimsParams, "ClaimsView", client);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Не удалось обновить список заявок.\n" + ex.Message);
                    }
                }
                else
                {
                    DeleteClaim(true);

                    try
                    {
                        GetDataRequest(dgvClaimsParams, "ClaimsView", client);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Не удалось обновить список заявок.\n" + ex.Message);
                    }
                }
            }
        }

        private void bChangeClaimData_Click(object sender, EventArgs e)
        {
            EditClaim();
        }

        private void tsmEditRow_Click(object sender, EventArgs e)
        {
            //Если выбрана вкладка пользователей
            if (tbSelector.SelectedTab == tbSelector.TabPages[0])
            {
                bChangeUserData_Click(sender, e);
            }

            //Если выбрана вкладка групп
            if (tbSelector.SelectedTab == tbSelector.TabPages[1])
            {
                bChangeGroupData_Click(sender, e);
            }

            //Если выбрана вкладка заявок
            if (tbSelector.SelectedTab == tbSelector.TabPages[2])
            {
                EditClaim();
            }
        }

        private void dgvData_MouseClick(object sender, MouseEventArgs e)
        {
            //Если выбрана вкладка пользователей
            if (tbSelector.SelectedTab == tbSelector.TabPages[0])
            {
                dgvDataMenuStrip.Items[1].Enabled = false;
                if (e.Button == MouseButtons.Right)
                {
                    dgvDataMenuStrip.Show(MousePosition);
                }
            }

            //Если выбрана вкладка групп
            if (tbSelector.SelectedTab == tbSelector.TabPages[1])
            {
                dgvDataMenuStrip.Items[1].Enabled = false;
                if (e.Button == MouseButtons.Right)
                {
                    dgvDataMenuStrip.Show(MousePosition);
                }
            }

            //Если выбрана вкладка заявок
            if (tbSelector.SelectedTab == tbSelector.TabPages[2])
            {
                dgvDataMenuStrip.Items[1].Enabled = true;
                if (e.Button == MouseButtons.Right)
                {
                    dgvDataMenuStrip.Show(MousePosition);
                }
            }
        }

        private void tsmDeleteRow_Click(object sender, EventArgs e)
        {
             //Если выбрана вкладка заявок
            if (tbSelector.SelectedTab == tbSelector.TabPages[2])
            {
                bDeleteClaim_Click(sender, e);
            }
        }

        private void dgvData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Y > dgvData.ColumnHeadersHeight + dgvData.Top - 30)
            {
                //Если выбрана вкладка пользователей
                if (tbSelector.SelectedTab == tbSelector.TabPages[0])
                {
                    bChangeUserData_Click(sender, e);
                }

                //Если выбрана вкладка групп
                if (tbSelector.SelectedTab == tbSelector.TabPages[1])
                {
                    bChangeGroupData_Click(sender, e);
                }

                //Если выбрана вкладка заявок
                if (tbSelector.SelectedTab == tbSelector.TabPages[2])
                {
                    EditClaim();
                }
            }
        }

        private void DeleteRow_Click(object sender, EventArgs e)
        {
            DeleteDGVRow(dgvUsersParams);
        }

        private void dgvUsersParams_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Y > dgvUsersParams.Top + dgvUsersParams.ColumnHeadersHeight - 30)
            {
                if (e.Button == MouseButtons.Right)
                {
                    cmsUsersParams.Show(MousePosition);
                }
            }
        }

        private void dgvGroupsParams_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Y > dgvGroupsParams.Top + dgvGroupsParams.ColumnHeadersHeight - 30)
            {
                if (e.Button == MouseButtons.Right)
                {
                    cmsGroupsParams.Show(MousePosition);
                }
            }
        }

        private void deleteRowFromGroupsParams_Click(object sender, EventArgs e)
        {
            DeleteDGVRow(dgvGroupsParams);
        }

        private void DeleteRowFromClaimsParams_Click(object sender, EventArgs e)
        {
            DeleteDGVRow(dgvClaimsParams);
        }

        private void dgvClaimsParams_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Y > dgvClaimsParams.Top + dgvClaimsParams.ColumnHeadersHeight - 30)
            {
                if (e.Button == MouseButtons.Right)
                {
                    cmsClaimsParams.Show(MousePosition);
                }
            }
        }
    }
}
