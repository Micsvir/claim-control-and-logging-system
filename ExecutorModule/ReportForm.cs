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
using System.Text.RegularExpressions;

namespace ExecutorModule
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
            MainWindow.getReportColumnsRequest = true;
            try
            {
                //отправка запроса на сервер
                NetMessage msg = new NetMessage(NetMessage.commandType.GetInfo, "GetColumns");
                msg.dataToSend.Columns.Add("TableName");
                DataRow newRow = msg.dataToSend.NewRow();
                newRow["TableName"] = "ExecutedClaimsReport";
                msg.dataToSend.Rows.Add(newRow);
                MainWindow.client.SendMessage(msg, false);

                //создание колонок dgvReport
                CreateDGVColumns(dgvReport, MainWindow.receivedData);

                //создание колонок dgvParametersSelection
                //!!!
                //метод должен выполняться только после CreateDGVColumns(), т.к. основывается на информации о наборе колонок элемента dgvReport
                SetdgvParametersSelection();

                SetDefaultParameters();

                object myObject = new object();
                EventArgs e = new EventArgs();
                tsbGetReport_Click(myObject, e);
            }
            catch (Exception ex)
            {
                //отправка сообщения об ошибке
                MessageBox.Show("не удалось загрузить схему таблицы отчета\n\n" + ex.Message);
                this.Close();
            }
            MainWindow.getReportColumnsRequest = false;
        }

        //переменная нужна для работы метода GetColumns(DataTable) класса ExportForm
        public static DataTable reportColumns = new DataTable();

        //переменная нужна для работы класса ExportForm.
        //заполняется значениями в методе tsbGetReport_Click
        //при получении данных от сервера
        public static DataTable report = new DataTable();

        //переменная хранит информацию полученную от серера (которую он отправлял от роли "клиента", т.к. это было второе сообщение,
        //после отправки информации о запрашиваемом отчете), которая представляет собой таблицу со сведениями о количестве 
        //заявок всех трех типов, содержащихся в полученном отчете
        public static DataTable reportClaimsCount = new DataTable();

        //условие, по которому будет производится выборка данных из таблицы ExecutedClaimsReport из БД
        public static string condition = "";

        //настройка dgvParametersSelection
        public void SetdgvParametersSelection()
        {
            dgvParametersSelection.Font = new Font(dgvParametersSelection.Font.FontFamily, 10);
            DataGridViewComboBoxColumn newCbColumn = new DataGridViewComboBoxColumn();
            newCbColumn.Name = "Parameter";
            newCbColumn.HeaderText = "Параметр";
            for (int col = 0; col < dgvReport.Columns.Count; col++)
            {
                //чтобы избежать ситуации, когда дата и время выбирается и в dtPeriodBegining, dtPeriodEnding,
                //и в dgvParametersSelection
                //if (dgvReport.Columns[col].HeaderText != "Дата" && dgvReport.Columns[col].HeaderText != "Время")
                {
                    newCbColumn.Items.Add(dgvReport.Columns[col].HeaderText);
                }
            }
            newCbColumn.DropDownWidth = 400;
            newCbColumn.Width = 150;
            dgvParametersSelection.Columns.Add(newCbColumn);
            
            newCbColumn = new DataGridViewComboBoxColumn();
            newCbColumn.Name = "Relation";
            newCbColumn.HeaderText = "Отношение";
            string[] relations = { "<", ">", "=", "LIKE", "NOT LIKE" };
            foreach (string curRelation in relations)
            {
                newCbColumn.Items.Add(curRelation);
            }
            dgvParametersSelection.Columns.Add(newCbColumn);
            
            dgvParametersSelection.Columns.Add("Value", "Значение");
            dgvParametersSelection.Columns.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None).Width = 300;

            newCbColumn = new DataGridViewComboBoxColumn();
            newCbColumn.Name = "LogicOperator";
            newCbColumn.HeaderText = "Логич. оператор";
            newCbColumn.Items.Add("AND");
            newCbColumn.Items.Add("OR");
            dgvParametersSelection.Columns.Add(newCbColumn);
        }

        //создание колонок DataGridView элемента
        public void CreateDGVColumns(DataGridView targetDGV, List<string> columns)
        {
            //создание колонок для отчета
            targetDGV.Font = new Font(targetDGV.Font.FontFamily, 10);
            foreach (string curColumn in columns)
            {
                targetDGV.Columns.Add(curColumn.Split(':')[0], curColumn.Split(':')[1]);
            }
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
            reportColumns = columnsData;
        }

        //добавление фамилии текущего контролера в список параметров в dgvParametersSelection
        public void SetDefaultParameters()
        {
            dgvParametersSelection.Rows.Add();
            dgvParametersSelection.Rows[0].Cells[0].Value = "Исполнитель";
            dgvParametersSelection.Rows[0].Cells[1].Value = "LIKE";
            dgvParametersSelection.Rows[0].Cells[2].Value = MainWindow.currentUserData.Rows[0]["PersLastName"].ToString();
            
            //получение вчерашней даты
            DateTime dt = DateTime.Now;
            dt = dt.AddDays(-1);
            string year = dt.Year.ToString();
            if (year.Length == 2)
            {
                year = "20" + year;
            }
            string month = dt.Month.ToString();
            if (month.Length < 2)
            {
                month = "0" + month;
            }
            string day = dt.Day.ToString();
            if (day.Length < 2)
            {
                day = "0" + day;
            }
            /*
            dgvParametersSelection.Rows.Add();
            dgvParametersSelection.Rows[1].Cells[0].Value = "Дата";
            dgvParametersSelection.Rows[1].Cells[1].Value = "=";
            dgvParametersSelection.Rows[1].Cells[2].Value = year + "." + month + "." + day;

            dgvParametersSelection.Rows.Add();
            dgvParametersSelection.Rows[2].Cells[0].Value = "Время";
            dgvParametersSelection.Rows[2].Cells[1].Value = ">";
            dgvParametersSelection.Rows[2].Cells[2].Value = "10:30:00";
            dgvParametersSelection.Rows[2].Cells[3].Value = "OR";
            */
            //получение сегодняшней даты
            dt = DateTime.Now;
            year = dt.Year.ToString();
            if (year.Length == 2)
            {
                year = "20" + year;
            }
            month = dt.Month.ToString();
            if (month.Length < 2)
            {
                month = "0" + month;
            }
            day = dt.Day.ToString();
            if (day.Length < 2)
            {
                day = "0" + day;
            }

            dgvParametersSelection.Rows.Add();
            dgvParametersSelection.Rows[1].Cells[0].Value = "Дата";
            dgvParametersSelection.Rows[1].Cells[1].Value = "=";
            dgvParametersSelection.Rows[1].Cells[2].Value = year + "." + month + "." + day;
            /*
            dgvParametersSelection.Rows.Add();
            dgvParametersSelection.Rows[4].Cells[0].Value = "Время";
            dgvParametersSelection.Rows[4].Cells[1].Value = "<";
            dgvParametersSelection.Rows[4].Cells[2].Value = "10:30:00";*/
        }

        //заполнение целевого DataGridView данными из указанного в параметрах источника
        public void FillDGVWithDBData(DataGridView targetDGV, DataTable dataSource)
        {
            //для каждой строки полученных данных
            for (int dbRow = 0; dbRow < dataSource.Rows.Count; dbRow++)
            {
                //создается новая строка в targetDGV
                //DataGridViewRow newDGVRow = new DataGridViewRow();
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

        //запрос отчета
        private void tsbGetReport_Click(object sender, EventArgs e)
        {
            MainWindow.viewReportButtonClicked = true;

            //Формирование строки-условия для отправки на сервер в качестве условия для SQL запроса к представлению ClaimsView
            
            condition = "";
            string stringsWithErrors = "";
            int errorsCounter = 0;
            for (int row = 0; row < dgvParametersSelection.Rows.Count - 1; row++)
            {
                string newCondition = "";
                //если строка непоследняя, к новому состоянию нужно прибавать логический оператор,
                //если же строка последняя, то логический оператор не требуется
                if (row != dgvParametersSelection.Rows.Count - 2)
                {
                    for (int col = 0; col < dgvParametersSelection.Columns.Count; col++)
                    {
                        //проверка на непустые значения
                        if (dgvParametersSelection.Rows[row].Cells[col].Value != null)
                        {
                            //расстановка скобок
                            if (col == 0)
                            {
                                newCondition += "([" + dgvParametersSelection.Rows[row].Cells[col].Value.ToString() + "] ";
                            }
                            else
                            {
                                if (col == dgvParametersSelection.Columns.Count - 2)
                                {
                                    if (dgvParametersSelection.Rows[row].Cells["Parameter"].Value != null)
                                    {
                                        //Выполняется проверка на наличие параметров, содержащих условия для столбцов, в которых описаны временные задержки между наступлениями событий.
                                        //Если таковые имеются, необходимо выразить строковое значение временной задержки ("00:00:00") в секундах (целочисленный тип данных)
                                        if (dgvParametersSelection.Rows[row].Cells["Parameter"].Value.ToString() == "Прошло времени с поступления заявки" ||
                                            dgvParametersSelection.Rows[row].Cells["Parameter"].Value.ToString() == "Прошло времени с назначения группы" ||
                                            dgvParametersSelection.Rows[row].Cells["Parameter"].Value.ToString() == "Прошло времени с назначения исполнителя" ||
                                            dgvParametersSelection.Rows[row].Cells["Parameter"].Value.ToString() == "Продолжительность выполнения заявки")
                                        {
                                            if (Configuration.IsTimeStringGood(dgvParametersSelection.Rows[row].Cells[col].Value.ToString()))
                                            {
                                                newCondition += "'" + Configuration.GetSecondsFromTimeString(dgvParametersSelection.Rows[row].Cells[col].Value.ToString()).ToString() + "') ";
                                            }
                                            else
                                            {
                                                stringsWithErrors += " строке " + (row + 1).ToString() + ", поле " + dgvParametersSelection.Columns[col].HeaderText + " (неверный формат времени (HH:mm:ss))\n"; //Columns["Value"]
                                                errorsCounter++;
                                            }
                                        }
                                        else
                                        {
                                            if (dgvParametersSelection.Rows[row].Cells["Relation"].Value != null)
                                            {
                                                if (dgvParametersSelection.Rows[row].Cells["Relation"].Value.ToString().IndexOf("LIKE") == -1)
                                                {
                                                    newCondition += "'" + dgvParametersSelection.Rows[row].Cells[col].Value.ToString() + "') ";
                                                }
                                                else
                                                {
                                                    newCondition += "'%" + dgvParametersSelection.Rows[row].Cells[col].Value.ToString() + "%') ";
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    newCondition += dgvParametersSelection.Rows[row].Cells[col].Value.ToString() + " ";
                                }
                            }
                        }
                        //формирование сообщения об ошибках, если пустые значения все же есть
                        else
                        {
                            stringsWithErrors += " строке " + row.ToString() + ", поле " + dgvParametersSelection.Columns[col].HeaderText + " (значение не указано)\n";
                            errorsCounter++;
                        }
                    }
                }
                else
                {
                    for (int col = 0; col < dgvParametersSelection.Columns.Count - 1; col++)
                    {
                        //проверка на непустые значения
                        if (dgvParametersSelection.Rows[row].Cells[col].Value != null)
                        {
                            //расстановка скобок
                            if (col == 0)
                            {
                                newCondition += "([" + dgvParametersSelection.Rows[row].Cells[col].Value.ToString() + "] ";
                            }
                            else
                            {
                                if (col == dgvParametersSelection.Columns.Count - 2)
                                {
                                    if (dgvParametersSelection.Rows[row].Cells["Parameter"].Value != null)
                                    {
                                        //Выполняется проверка на наличие параметров, содержащих условия для столбцов, в которых описаны временные задержки между наступлениями событий.
                                        //Если таковые имеются, необходимо выразить строковое значение временной задержки ("00:00:00") в секундах (целочисленный тип данных)
                                        if (dgvParametersSelection.Rows[row].Cells["Parameter"].Value.ToString() == "Прошло времени с поступления заявки" ||
                                            dgvParametersSelection.Rows[row].Cells["Parameter"].Value.ToString() == "Прошло времени с назначения группы" ||
                                            dgvParametersSelection.Rows[row].Cells["Parameter"].Value.ToString() == "Прошло времени с назначения исполнителя" ||
                                            dgvParametersSelection.Rows[row].Cells["Parameter"].Value.ToString() == "Продолжительность выполнения заявки")
                                        {
                                            if (Configuration.IsTimeStringGood(dgvParametersSelection.Rows[row].Cells[col].Value.ToString()))
                                            {
                                                newCondition += "'" + Configuration.GetSecondsFromTimeString(dgvParametersSelection.Rows[row].Cells[col].Value.ToString()).ToString() + "') ";
                                            }
                                            else
                                            {
                                                stringsWithErrors += " строке " + (row + 1).ToString() + ", поле " + dgvParametersSelection.Columns[col].HeaderText + " (неверный формат времени (HH:mm:ss))\n";
                                                errorsCounter++;
                                            }
                                        }
                                        else
                                        {
                                            if (dgvParametersSelection.Rows[row].Cells["Relation"].Value != null)
                                            {
                                                if (dgvParametersSelection.Rows[row].Cells["Relation"].Value.ToString().IndexOf("LIKE") == -1)
                                                {
                                                    newCondition += "'" + dgvParametersSelection.Rows[row].Cells[col].Value.ToString() + "') ";
                                                }
                                                else
                                                {
                                                    newCondition += "'%" + dgvParametersSelection.Rows[row].Cells[col].Value.ToString() + "%') ";
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    newCondition += dgvParametersSelection.Rows[row].Cells[col].Value.ToString() + " ";
                                }
                            }
                        }
                        //формирование сообщения об ошибках, если пустые значения все же есть
                        else
                        {
                            stringsWithErrors += " строке " + (row + 1).ToString() + ", поле " + dgvParametersSelection.Columns[col].HeaderText + " (значение не указано)\n";
                            errorsCounter++;
                        }
                    }
                }

                if (dgvParametersSelection.Rows[row].Cells["Parameter"].Value != null)
                {
                    //проверка ввода даты и времени, соответствующих формату
                    if (dgvParametersSelection.Rows[row].Cells["Parameter"].Value.ToString() == "Дата")
                    {
                        string datePattern = "[0-9]{4}.{1}[0-9]{2}.{1}[0-9]{2}";
                        Regex regex = new Regex(datePattern);
                        if (!regex.IsMatch(dgvParametersSelection.Rows[row].Cells["Value"].Value.ToString()))
                        {
                            stringsWithErrors += " строке " + (row + 1).ToString() + ", поле " + dgvParametersSelection.Columns["Value"].HeaderText + " (неверный формат даты (yyyy.MM.dd))\n";
                            errorsCounter++;
                        }
                    }

                    //проверка ввода даты и времени, соответствующих формату
                    if (dgvParametersSelection.Rows[row].Cells["Parameter"].Value.ToString() == "Время")
                    {
                        string datePattern = "[0-9]{2}:{1}[0-9]{2}:{1}[0-9]{2}";
                        Regex regex = new Regex(datePattern);
                        if (!regex.IsMatch(dgvParametersSelection.Rows[row].Cells["Value"].Value.ToString()))
                        {
                            stringsWithErrors += " строке " + (row + 1).ToString() + ", поле " + dgvParametersSelection.Columns["Value"].HeaderText + " (неверный формат времени (HH:mm:ss))\n";
                            errorsCounter++;
                        }
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
                //на случай, если все параметры из dgvParametersSelection были удалены
                if (condition.Length > 0)
                {
                    condition = condition.Substring(0, condition.Length - 1);
                }

                //Дополнительные скобки для объединения двух одинаковых параметров.
                //Два вложенных цикла по всем строкам столбца Parameter. Если обнаруживается, что значения строк в этом столбце совпали при том, что значения счетчиков не совпали
                //(т.к. само собой разумеется, что значения строки в столбце совпадает со значением этой же самой строки в этом же столбце),
                //выполняется расстановка скобок, объединяющих эти два или более (?) совпадающих параметра(ов)
                int curParameterIndex = 0;
                while (curParameterIndex < dgvParametersSelection.Rows.Count - 2) 
                {
                    if (dgvParametersSelection.Rows[curParameterIndex].Cells["Parameter"].Value.ToString() == dgvParametersSelection.Rows[curParameterIndex + 1].Cells["Parameter"].Value.ToString())
                    {
                        int sameParametersCount = 2; //потому что уже найдено совпадение имен 2-х параметров
                        string sameParametersName = dgvParametersSelection.Rows[curParameterIndex].Cells["Parameter"].Value.ToString();
                        int i = curParameterIndex + 2;
                        while ((i < (dgvParametersSelection.Rows.Count - 1)) && (dgvParametersSelection.Rows[i].Cells["Parameter"].Value.ToString() == sameParametersName))
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
                        while (passedBracketsCount <= i-1 && curChar < condition.Length-1)
                        {
                            //условие condition[curChar-1] != ')' добавлено для того, чтобы счетчик не считал двойные скобки
                            if (condition[curChar] == ')' && condition[curChar-1] != ')')
                            {
                                passedBracketsCount++;
                            }
                            curChar++;
                        }
                        condition = condition.Insert(curChar, ")");

                        //i - 1 потому что далее переменная curParameterIndex еще раз увеличивается на 1
                        curParameterIndex = i-1;
                    }
                    curParameterIndex++;
                }

                //Еще одно объединение скобками:
                //когда в списке параметров 2 раза встречаются 2 чередующихся параметра "Дата" и "Время", следующих друг за другом (дата, время, дата, время)
                //они попарно объединяются скобками, а затем заключаются в скобки целиком ((дата, время) (дата, время)).
                //Это позволит создавать суточные запросы, указывая время начала и конца периода
                curParameterIndex = 0;
                while (curParameterIndex < dgvParametersSelection.Rows.Count - 4) 
                {
                    if (dgvParametersSelection.Rows[curParameterIndex].Cells["Parameter"].Value.ToString() == "Дата"
                        && dgvParametersSelection.Rows[curParameterIndex + 1].Cells["Parameter"].Value.ToString() == "Время"
                        && dgvParametersSelection.Rows[curParameterIndex + 2].Cells["Parameter"].Value.ToString() == "Дата"
                        && dgvParametersSelection.Rows[curParameterIndex + 3].Cells["Parameter"].Value.ToString() == "Время")
                    {
                        //MessageBox.Show("Да, в списке параметров есть две пары параметров Дата-Время, расположенных друг за другом");
                        
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

                //для наглядности
                tbRequestResult.Text = condition;

                //переменная-флаг. Принимает true, если запрос был успешно отправлен на сервер
                bool isRequestSent = false;
                try
                {
                    //создание сообщения для отправки запроса
                    NetMessage msg = new NetMessage(NetMessage.commandType.GetInfo, "GetReportInfo");

                    //в поле dataToSend создается 1 столбец и 1 строка - там будет хранится условие, которое необходимо
                    //передать на сервер
                    msg.dataToSend.Columns.Add("Condition");
                    DataRow newDR = msg.dataToSend.NewRow();
                    newDR[0] = condition;
                    msg.dataToSend.Rows.Add(newDR);

                    //отправка запроса на сервер
                    MainWindow.client.SendMessage(msg, false);

                    //установка значения флага
                    isRequestSent = true;
                }
                catch (Exception ex)
                {
                    //отправка сообщения об ошибке
                    MessageBox.Show("Запрос на получение данных для формирования отчета отправить не удалось\nМетод: FillDGVWithDBData()\n\n" + ex.Message);

                    //установка флага
                    isRequestSent = false;
                }

                //если запрос на получение данных был выполнен успешно,
                //полученные данные загружаются в DataGridView
                if (isRequestSent)
                {
                    dgvReport.Rows.Clear();
                    FillDGVWithDBData(dgvReport, MainWindow.receivedData);

                    //полученная информация помещается в переменную report для дальнейшей работы с формой ExportForm
                    report = MainWindow.receivedData;
                }
            }
            //переменная сбрасывает значение в false после получения второго сообщения от сервера, содержащего сведения о количестве заявок разных типов
            //в запрашиваемом отчете. Поэтому значение false присваивается переменной в методе получения сообщений от сервера
            //MainWindow.viewReportButtonClicked = false;
        }

        //Просмотр выбранной заявки в форме
        public void ViewClaim()
        {
            ClaimViewingForm cvf = new ClaimViewingForm();
            cvf.AddControls(dgvReport);
            cvf.ReplaceControlsHorizontal();
            cvf.Show();
        }

        private void dgvParametersSelection_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if ((dgvParametersSelection.Rows.Count - 2) != -1)
            {
                dgvParametersSelection.Rows[dgvParametersSelection.Rows.Count - 2].Cells[3].Value = "AND";
            }
        }

        private void dgvParametersSelection_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                cmsParametersSelectionMenu.Show(MousePosition);
            }
        }

        private void parametersRowDeleting_Click(object sender, EventArgs e)
        {
            //Список, в котором будут хранится индексы строк, подлежащих удалению
            List<int> selectedIndexes = new List<int>();

            //заполнение списка индексов
            for (int i = 0; i < dgvParametersSelection.SelectedRows.Count; i++)
            {
                selectedIndexes.Add(dgvParametersSelection.SelectedRows[i].Index);
            }

            //необязательное действие
            dgvParametersSelection.ClearSelection();

            //сортировка списка индексов, гарантирующая в последующем, что строки будут удаляться от последней к первой
            selectedIndexes.Sort();

            //цикл удаления строк. Цикл от последнего элемента списка selectedIndexes к первому. Соответственно, от максимального индекса удаляемой строки к минимальному
            for (int i = selectedIndexes.Count - 1; i >= 0; i--)
            {
                //Если индекс удаляемой строки больше или равен dgvParametersSelection.Rows.Count - 1, строка пропускается, т.к. в DataGridView всегда есть последняя
                //пустая строка (типа новая)
                if (selectedIndexes[i] < dgvParametersSelection.Rows.Count - 1)
                {
                    dgvParametersSelection.Rows.RemoveAt(selectedIndexes[i]);
                }
            }

            //необязательное действие
            dgvParametersSelection.ClearSelection();
        }

        private void tsbTransferToWord_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Функция находится в разработке");
        }

        private void tsbExport_Click(object sender, EventArgs e)
        {
            ExportForm exportForm = new ExportForm();
            exportForm.ShowDialog();
        }

        private void tsbTest_Click(object sender, EventArgs e)
        {
            
            string result = "";
            for (int row = 0; row < reportClaimsCount.Rows.Count; row++)
            {
                for (int col = 0; col < reportClaimsCount.Columns.Count; col++)
                {
                    result += reportClaimsCount.Rows[row][col].ToString() + "            ";
                }
                result += "\n";
            }
            MessageBox.Show("reportClaimsCount rows count = " + reportClaimsCount.Rows.Count.ToString() + "\nReportClaimsCount cols count = " + reportClaimsCount.Columns.Count.ToString());
            MessageBox.Show("result:\n" + result);
        }

        private void dgvReport_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Y > dgvReport.Top + dgvReport.ColumnHeadersHeight - 30)
            {
                if (dgvReport.SelectedRows.Count == 1)
                {
                    ViewClaim();
                }
            }
        }

    }
}
