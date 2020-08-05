using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Word;
using System.Diagnostics;

namespace UtilSubSys
{
    //для функционирования класса необходимо, чтобы в каталоге приложения находился файл текстовый "path",
    //содержащий в себе путь к файлу конфигурации.
    //Файл конфигурации должен иметь имя, совпадающее с исполняемым файлом приложения, включая расширение,
    //и оканчивающееся строкой ".conf". Например, если исполняемый файл приложения называется app.exe, следовательно
    //файл конфигурации должен называться app.exe.conf.
    abstract public class Configuration
    {
        //классы

        //переменные, константы

        //configurationFilePath содержит информацию о том, где находится файл конфигурации.
        //Это позволяет использовать один общий файл конфигурации несколькими приложениями.
        static public string configurationFilePath
        {
            get
            {
                return _configurationFilePath;
            }
        }
        static private string _configurationFilePath = "empty";
        static private string _pathFile = "path";
        public struct ConfigString
        {
            public string confName;
            public List<string> confValues;
        }
        static public List<ConfigString> Settings = new List<ConfigString>();
        public enum GetSettingsResult
        {
            OK, PathFileNotFound, ConfigFileNotFound, ConfigFileIsEmpty, GetSettingsCallRequeried
        }
        static public GetSettingsResult status = GetSettingsResult.GetSettingsCallRequeried; 

        //процедуры, функции

        static public void GetSettings()
        {
            try
            {
                StreamReader sr = new StreamReader(_pathFile);
                _configurationFilePath = sr.ReadLine();
                sr.Close();
                if (_configurationFilePath[_configurationFilePath.Length - 1] == '\\')
                {
                    _configurationFilePath += System.Windows.Forms.Application.ExecutablePath.Substring(System.Windows.Forms.Application.ExecutablePath.LastIndexOf('\\') + 1, System.Windows.Forms.Application.ExecutablePath.Length - (System.Windows.Forms.Application.ExecutablePath.LastIndexOf('\\') + 1)) + ".conf";
                }
                else
                {
                    _configurationFilePath += "\\" + System.Windows.Forms.Application.ExecutablePath.Substring(System.Windows.Forms.Application.ExecutablePath.LastIndexOf('\\') + 1, System.Windows.Forms.Application.ExecutablePath.Length - (System.Windows.Forms.Application.ExecutablePath.LastIndexOf('\\') + 1)) + ".conf";
                }
            }
            catch 
            {
                status = GetSettingsResult.PathFileNotFound;
            }
            if (_configurationFilePath != "empty")
            {
                try
                {
                    Settings = new List<ConfigString>();
                    StreamReader confFileReader = new StreamReader(_configurationFilePath,Encoding.Default);
                    string readedLine = "";
                    string nameLine = "";
                    string valuesLine = "";
                    while (!confFileReader.EndOfStream)
                    {
                        readedLine = confFileReader.ReadLine();
                        ConfigString cfgStr = new ConfigString();
                        cfgStr.confValues = new List<string>();
                        /*nameLine = readedLine.Split('=')[0].Trim();
                        valuesLine = readedLine.Split('=')[1].Trim();*/

                        //Предыдущие две операции некорректны, если в строке valuesLine есть символы '=', ';'. 
                        //Такие символы есть, например, в строке подключение к SQL серверу MSSQLEXPRESS.
                        //Поэтому логику выборки имени свойства и его значений необходимо было переработать.
                        
                        //если строка не начинается с '=' (т.к. если она начинается с этого символа, значит,
                        //для последующих значений параметра не будет имени. Такая строка является неверной.
                        if (readedLine[0] != '=')
                        {
                            nameLine = readedLine.Substring(0, readedLine.IndexOf('='));
                            cfgStr.confName = nameLine.Trim();

                            valuesLine = readedLine.Substring(readedLine.IndexOf('=') + 1).Trim();

                            //Если в строке значений свойства есть строка-разделитель "||",
                            //эту строку нужно преобразовать в List<string>
                            if (valuesLine.IndexOf("||") != -1)
                            {
                                while (valuesLine.IndexOf("||") != -1)
                                {
                                    cfgStr.confValues.Add(valuesLine.Substring(0, valuesLine.IndexOf("||")));
                                    valuesLine = valuesLine.Substring(valuesLine.IndexOf("||") + 2);
                                }
                                cfgStr.confValues.Add(valuesLine); //оставшаяся строка, несодержащая || тем не менее является значением свойства
                            }
                            else
                            {
                                cfgStr.confValues.Add(valuesLine);
                            }
                            Settings.Add(cfgStr);
                        }
                    }
                    confFileReader.Close();
                    if (Settings.Count == 0)
                    {
                        status = GetSettingsResult.ConfigFileIsEmpty;
                    }
                    else
                    {
                        status = GetSettingsResult.OK;
                    }
                }
                catch
                {
                    status = GetSettingsResult.ConfigFileNotFound;
                }
            }
        }

        static public List<string> GetValuesBySettingsName(string settingsName)
        {
            if (status == GetSettingsResult.OK)
            {
                if (Settings.Count > 0)
                {
                    int i = 0;
                    while ((Settings[i].confName != settingsName) && (i < Settings.Count - 1))
                    {
                        i++;
                    }
                    if (((Settings[i].confName == settingsName) && (i == Settings.Count - 1)) || (i < Settings.Count - 1))
                    {
                        return Settings[i].confValues;
                    }
                    else
                    {
                        List<string> answer = new List<string> { "SettingsNameNotFound" };
                        return answer;
                    }
                }
                else
                {
                    List<string> answer2 = new List<string> { "NoSettingsFound" };
                    status = GetSettingsResult.ConfigFileIsEmpty;
                    return answer2;
                }
            }
            else
            {
                List<string> answer3 = new List<string> { "SettingsArentLoadedYet" };
                return answer3;
            }
        }

        //единый шаблон для ListView с заявками
        static public void CreateListView(ListView targetListView, List<string> columnsNames, int fontSize)
        {
            targetListView.Columns.Clear();
            targetListView.View = System.Windows.Forms.View.Details;
            targetListView.GridLines = true;
            targetListView.Font = new System.Drawing.Font(targetListView.Font.FontFamily, fontSize);
            targetListView.FullRowSelect = true;
            targetListView.MultiSelect = true;
            targetListView.BorderStyle = BorderStyle.None;

            ColumnHeader ch = new ColumnHeader();
            foreach (string curCol in columnsNames)
            {
                ch = new ColumnHeader();
                string colName = curCol.Split(':')[0];
                string colCaption = curCol.Split(':')[1];
                ch.Name = colName;
                ch.Text = colCaption;
                ch.Width = 100;
                targetListView.Columns.Add(ch);
            }
        }

        //добавление всех заявок, полученных в сообщении, в указанный ListView
        static public void AddDataToListView(ListView targetListView, DataTable dbData)
        {
            //для каждой строки DataTabel из data.dataToSend создается ListViewItem
            for (int rowCounter = 0; rowCounter < dbData.Rows.Count; rowCounter++)
            {

                //Обновление метода:


                //только если такой строки еще нет в ListView.
                //Проверка наличия такой строки осуществляется по столбцам, содержащим ID.
                //Во всех таблицах БД столбец с ID всегда является первым
                int lvIDColumnIndex = -1;
                string idName = dbData.Columns[0].ColumnName;
                for (int col = 0; col < targetListView.Columns.Count; col++)
                {
                    if (targetListView.Columns[col].Name == idName)
                    {
                        //теперь переменная хранит индекс столбца ListView c ID
                        lvIDColumnIndex = col;
                    }
                }

                //переменная принимает значение true, если среди элементов targetListView
                //обнаруживается элемент с ID, равным ID из dbData
                bool itemFound = false;
                int existLVItemIndex = 0;
                int counter = 0;
                foreach (ListViewItem curLVI in targetListView.Items)
                {
                    if (curLVI.SubItems[lvIDColumnIndex].Text == dbData.Rows[rowCounter][0].ToString())
                    {
                        itemFound = true;
                        existLVItemIndex = counter;
                    }
                    counter++;
                }

                //если itemFound = true, нужно обновить данные в существующем элементе targetListView
                if (itemFound)
                {
                    for (int lvCol = 0; lvCol < targetListView.Columns.Count; lvCol++)
                    {
                        for (int dbCol = 0; dbCol < dbData.Columns.Count; dbCol++)
                        {
                            if (targetListView.Columns[lvCol].Name == dbData.Columns[dbCol].ColumnName)
                            {
                                targetListView.Items[existLVItemIndex].SubItems[lvCol].Text = dbData.Rows[rowCounter][dbCol].ToString();
                            }


                            if ((targetListView.Items[existLVItemIndex].SubItems[lvCol].Text == "Ожидает назначения ответственной группы") && (targetListView.Columns[lvCol].Name == "ClaimStatus"))
                            {
                                targetListView.Items[existLVItemIndex].BackColor = Color.Red;
                            }
                            if ((targetListView.Items[existLVItemIndex].SubItems[lvCol].Text == "Ожидает назначения ответственного исполнителя") && (targetListView.Columns[lvCol].Name == "ClaimStatus"))
                            {
                                targetListView.Items[existLVItemIndex].BackColor = Color.Orange;
                            }
                            if ((targetListView.Items[existLVItemIndex].SubItems[lvCol].Text == "Ожидает выполнения") && (targetListView.Columns[lvCol].Name == "ClaimStatus"))
                            {
                                targetListView.Items[existLVItemIndex].BackColor = Color.Yellow;
                            }
                            if ((targetListView.Items[existLVItemIndex].SubItems[lvCol].Text == "Выполняется") && (targetListView.Columns[lvCol].Name == "ClaimStatus"))
                            {
                                targetListView.Items[existLVItemIndex].BackColor = Color.LightBlue;
                            }
                            if ((targetListView.Items[existLVItemIndex].SubItems[lvCol].Text == "Выполнена") && (targetListView.Columns[lvCol].Name == "ClaimStatus"))
                            {
                                targetListView.Items[existLVItemIndex].BackColor = Color.LightGreen;
                            }
                            if ((targetListView.Items[existLVItemIndex].SubItems[lvCol].Text.IndexOf("Удалена") != -1) || (targetListView.Items[existLVItemIndex].SubItems[lvCol].Text.IndexOf("Отменена") != -1) && (targetListView.Columns[lvCol].Name == "ClaimStatus"))
                            {
                                targetListView.Items[existLVItemIndex].BackColor = Color.MediumPurple;
                            }
                        }
                    }
                }

                else
                {

                    //То, что было изначально:

                    ListViewItem newLVI = new ListViewItem();
                    //для каждого столбца текущего ListView создается SubItem в newLVI
                    for (int lvColCount = 0; lvColCount < targetListView.Columns.Count; lvColCount++)
                    {
                        newLVI.SubItems.Add("");
                    }
                    //каждая колонка DataTable из data.dataToSend сравнивается с колонкой ListView (их названия).
                    //Если названия совпадают, в SubItem c индексом, равным индексу колонки ListView, помещается значение 
                    //из DataTable
                    for (int lvColCount = 0; lvColCount < targetListView.Columns.Count; lvColCount++)
                    {
                        for (int colCounter = 0; colCounter < dbData.Columns.Count; colCounter++)
                        {
                            if (dbData.Columns[colCounter].ColumnName == targetListView.Columns[lvColCount].Name)
                            {
                                newLVI.SubItems[lvColCount].Text = dbData.Rows[rowCounter][colCounter].ToString();

                                //подгонка размера колонки
                                /*
                                if (targetListView.Columns[lvColCount].Width < newLVI.SubItems[lvColCount].Text.Length * 15)
                                {
                                    targetListView.Columns[lvColCount].Width = newLVI.SubItems[lvColCount].Text.Length * 15;
                                }
                                */
                                //назначение цвета
                                if ((newLVI.SubItems[lvColCount].Text == "Ожидает назначения ответственной группы") && (targetListView.Columns[lvColCount].Name == "ClaimStatus"))
                                {
                                    newLVI.BackColor = Color.Red;
                                }
                                if ((newLVI.SubItems[lvColCount].Text == "Ожидает назначения ответственного исполнителя") && (targetListView.Columns[lvColCount].Name == "ClaimStatus"))
                                {
                                    newLVI.BackColor = Color.Orange;
                                }
                                if ((newLVI.SubItems[lvColCount].Text == "Ожидает выполнения") && (targetListView.Columns[lvColCount].Name == "ClaimStatus"))
                                {
                                    newLVI.BackColor = Color.Yellow;
                                }
                                if ((newLVI.SubItems[lvColCount].Text == "Выполняется") && (targetListView.Columns[lvColCount].Name == "ClaimStatus"))
                                {
                                    newLVI.BackColor = Color.LightBlue;
                                }
                                if ((newLVI.SubItems[lvColCount].Text == "Выполнена") && (targetListView.Columns[lvColCount].Name == "ClaimStatus"))
                                {
                                    newLVI.BackColor = Color.LightGreen;
                                }
                            }
                        }
                    }
                    targetListView.Items.Add(newLVI);
                }
            }
            //подгонка средствами C#.
            for (int curCol = 0; curCol < targetListView.Columns.Count; curCol++)
            {
                if (targetListView.Columns[curCol].Name.IndexOf("ID") == -1)
                {
                    targetListView.Columns[curCol].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                }
                else
                {
                    targetListView.Columns[curCol].Width = 5;
                }
            }
        }

        //Создание столбцов контрола DataGridView в соответствии с настройками из файла конфигурации
        //Элементы списка columnsSet должны представлять строку формата "<имя столбца БД>:<отображаемое имя>"
        static public void CreateDGVColumns(DataGridView targetDGV, List<string> columnsSet)
        {
            //для каждого элемента списка columnsSet
            foreach (string currentColumn in columnsSet)
            {
                //выполняется операция его разделения на 2 строки по символу ':'
                string dbColumnName = currentColumn.Split(':')[0];
                string headerText = currentColumn.Split(':')[1];

                //и создание столбца targetDGV с помощью полученных значений
                targetDGV.Columns.Add(dbColumnName, headerText);
            }
        }

        //Заполнение контрола DataGridView значениями, полученными из экземпляра класса DataTable (именно он используется
        //для получения и передачи информации между модулями программы и БД). Значения распределяются по столбцам по признаку
        //соответствия названий столбцов DataGridView и DataTable
        static public void AddDataToDGV(DataGridView targetDGV, DataTable sourceDataTable, bool updateDGVData, string dtColumnToCompare, string dgvColumnToCompare)
        {
            //индикатор необходимости добавить новую строку к targetDGV
            bool rowWasAdded = false;

            //если выбрана опция обновления существующих записей в targetDGV (updateDGVData = true), переменная
            //выполняет роль индикатора существования в targetDGV текущего элемента sourceDataTable
            bool dgvItemExists = false;

            //если необходимо не просто заполнить targetDGV, но и обновить данные уже существующих элементов,
            if (updateDGVData)
            {
                int foundItemIndex = -1;

                //то для каждой строки sourceDataTable
                for (int dtRow = 0; dtRow < sourceDataTable.Rows.Count; dtRow++)
                {
                    rowWasAdded = false;
                    dgvItemExists = false;
                    foundItemIndex = -1;

                    //выполняется проверка, существует ли в targetDGV элемент с таким же значением в столбце сравнения (чаще всего это банальный ID), что и в sourceDataTable.
                    for (int dgvRow = 0; dgvRow < targetDGV.Rows.Count; dgvRow++)
                    {
                        //Если такой элемент находится,
                        if (targetDGV.Rows[dgvRow].Cells[dgvColumnToCompare].Value.ToString() == sourceDataTable.Rows[dtRow][dtColumnToCompare].ToString())
                        {
                            //переменная-маркер принимает занчение true,
                            dgvItemExists = true;

                            //индекс этого элемента запоминается
                            foundItemIndex = dgvRow;
                        }
                    }

                    //если элемент, значение которого в столбце сравнения совпало со значением в столбце сравнения sourceDataTable, новые данные не добавляются в targetDGV, а
                    //изменяются существующие
                    if (dgvItemExists)
                    {
                        for (int dtCol = 0; dtCol < sourceDataTable.Columns.Count; dtCol++)
                        {
                            for (int dgvCol = 0; dgvCol < targetDGV.Columns.Count; dgvCol++)
                            {
                                if (targetDGV.Columns[dgvCol].Name == sourceDataTable.Columns[dtCol].ColumnName)
                                {
                                    targetDGV.Rows[foundItemIndex].Cells[dgvCol].Value = sourceDataTable.Rows[dtRow][dtCol];
                                }
                            }
                        }
                    }
                    //иначе элемент добавляется к targetDGV точно так же, как это происходит в случае, когда обновлять элементы targetDGV не нужно (updateDGVData = false)
                    else
                    {
                        for (int dtCol = 0; dtCol < sourceDataTable.Columns.Count; dtCol++)
                        {
                            for (int dgvCol = 0; dgvCol < targetDGV.Columns.Count; dgvCol++)
                            {
                                if (targetDGV.Columns[dgvCol].Name == sourceDataTable.Columns[dtCol].ColumnName)
                                {
                                    if (!rowWasAdded)
                                    {
                                        targetDGV.Rows.Add();
                                        rowWasAdded = true;
                                    }
                                    targetDGV.Rows[targetDGV.Rows.Count - 1].Cells[dgvCol].Value = sourceDataTable.Rows[dtRow][dtCol];
                                }
                            }
                        }
                    }
                }
            }
            
            //простое заполнение targetDGV значениями из sourceDataTable
            else
            {
                for (int dtRow = 0; dtRow < sourceDataTable.Rows.Count; dtRow++)
                {
                    rowWasAdded = false;

                    for (int dtCol = 0; dtCol < sourceDataTable.Columns.Count; dtCol++)
                    {
                        for (int dgvCol = 0; dgvCol < targetDGV.Columns.Count; dgvCol++)
                        {
                            if (targetDGV.Columns[dgvCol].Name == sourceDataTable.Columns[dtCol].ColumnName)
                            {
                                if (!rowWasAdded)
                                {
                                    targetDGV.Rows.Add();
                                    rowWasAdded = true;
                                }
                                targetDGV.Rows[targetDGV.Rows.Count - 1].Cells[dgvCol].Value = sourceDataTable.Rows[dtRow][dtCol];
                            }
                        }
                    }
                }
            }
        }

        //Изменение цвета фона ячеек в зависимости от значений в столбце ClaimStatus
        static public void PaintDGVCells(DataGridView targetDGV, int dgvDefColumnIndex, List<StringAndColorPair> conditions)
        {
            for (int dgvRow = 0; dgvRow < targetDGV.Rows.Count; dgvRow++)
            {
                foreach (StringAndColorPair condition in conditions)
                {
                    if (targetDGV.Rows[dgvRow].Cells[dgvDefColumnIndex].Value.ToString().IndexOf(condition.stringToFind) != -1)
                    {
                        DataGridViewCellStyle newCellStyle = new DataGridViewCellStyle();
                        newCellStyle = targetDGV.Rows[dgvRow].Cells[dgvDefColumnIndex].Style;
                        newCellStyle.BackColor = condition.colorToSet;
                        for (int dgvCol = 0; dgvCol < targetDGV.Columns.Count; dgvCol++)
                        {
                            targetDGV.Rows[dgvRow].Cells[dgvCol].Style = newCellStyle;
                        }
                    }
                }
            }
        }

        //возвращает строку формата "d дней hh:mm:ss" или "hh:mm:ss". Принимает в качестве параметров результаты
        //выполнения функции MS SQL сервера DATEDIFF(second, date1, date2)
        static public string GetTimeStringFromSeconds(int seconds)
        {
            string hoursString = "", minutesString = "", secondsString = "";

            int resultSec = 0;
            int resultMin = 0;
            int resultHour = 0;
            int resultDay = 0;

            if (seconds < 60)
            {
                if (seconds < 10)
                {
                    return "00:00:0" + seconds.ToString();
                }
                else
                {
                    return "00:00:" + seconds.ToString();
                }
            }
            else
            {
                if ((seconds / 60) < 60)
                {
                    resultSec = seconds - (seconds / 60) * 60;
                    resultMin = seconds / 60;
                    if (resultSec < 10)
                    {
                        secondsString = "0" + resultSec.ToString();
                    }
                    else
                    {
                        secondsString = resultSec.ToString();
                    }
                    if (resultMin < 10)
                    {
                        minutesString = "0" + resultMin.ToString();
                    }
                    else
                    {
                        minutesString = resultMin.ToString();
                    }
                    return "00:" + minutesString + ":" + minutesString;
                }
                else
                {
                    if (((seconds / 60) / 60) < 24)
                    {
                        resultSec = seconds - (seconds / 60) * 60;
                        resultMin = (seconds / 60) - (((seconds / 60) / 60) * 60);
                        resultHour = (seconds / 60) / 60;
                        if (resultSec < 10)
                        {
                            secondsString = "0" + resultSec.ToString();
                        }
                        else
                        {
                            secondsString = resultSec.ToString();
                        }
                        if (resultMin < 10)
                        {
                            minutesString = "0" + resultMin.ToString();
                        }
                        else
                        {
                            minutesString = resultMin.ToString();
                        }
                        if (resultHour < 10)
                        {
                            hoursString = "0" + resultHour.ToString();
                        }
                        else
                        {
                            hoursString = resultHour.ToString();
                        }
                        return hoursString + ":" + minutesString + ":" + secondsString;
                    }
                    else
                    {
                        resultSec = seconds - (seconds / 60) * 60;
                        resultMin = (seconds / 60) - (((seconds / 60) / 60) * 60);
                        resultHour = ((seconds / 60) / 60) - (((seconds / 60) / 60) / 24) * 24;
                        resultDay = (((seconds / 60) / 60) / 24);
                        if (resultSec < 10)
                        {
                            secondsString = "0" + resultSec.ToString();
                        }
                        else
                        {
                            secondsString = resultSec.ToString();
                        }
                        if (resultMin < 10)
                        {
                            minutesString = "0" + resultMin.ToString();
                        }
                        else
                        {
                            minutesString = resultMin.ToString();
                        }
                        if (resultHour < 10)
                        {
                            hoursString = "0" + resultHour.ToString();
                        }
                        else
                        {
                            hoursString = resultHour.ToString();
                        }
                        return resultDay + " дн " + hoursString + ":" + minutesString + ":" + secondsString;
                    }
                }
            }
        }

        //из строки формата "d дней hh:mm:ss" или "hh:mm:ss" возвращает число секунд
        static public int GetSecondFromTimeString(string timeString)
        {
            //массив строк, который хранит информацию о часах, минутах и секундах в строковом формате
            string[] splitedDateString = timeString.Split(':');

            int result = (Convert.ToInt32(splitedDateString[0]) * 60 * 60) + (Convert.ToInt32(splitedDateString[1]) * 60) + (Convert.ToInt32(splitedDateString[2]));

            //если есть слово "дней"
            if (timeString.IndexOf("дней") != -1)
            {
                //формирование подстроки, содержащей количество дней
                string daysSubstring = "";
                int i = 0;
                while (timeString[i] != ' ')
                {
                    daysSubstring += timeString[i];
                    i++;
                }

                result += Convert.ToInt32(daysSubstring) * 24 * 60 * 60;
            }

            return result;
        }

        //проверка строки на соответствие формату "d дней hh:mm:ss" или "hh:mm:ss"
        static public bool IsTimeStringGood(string stringToCheck)
        {

            MessageBox.Show("string to check = " + stringToCheck);

            //если в строке не встречается символ ':', значит, эта строка имеет неверный формат
            if (stringToCheck.IndexOf(":") == -1)
            {

                MessageBox.Show("No ':' symbol found.");

                return false;
            }
            else
            {
                string[] splitedStringToCheck = stringToCheck.Split(':');

                //если количество подстрок, полученных в результате разделения строки по символу ':',
                //не равно 3, значит, строка имеет не верный формат
                if (splitedStringToCheck.Length != 3)
                {

                    MessageBox.Show("':' symbol count isn't equal 3");

                    return false;
                }
                else
                {
                    //если попытка приведения трех полученных подстрок к типу int приведет к возникновению
                    //исключения, значит, строка имеет неверный формат
                    try
                    {
                        MessageBox.Show("try to convert splitedStringToCheck[0].Substring(splitedStringToCheck[0].Length - 2) " + "(" + splitedStringToCheck[0].Substring(splitedStringToCheck[0].Length - 2) + ") to Int32");
                        int hours = Convert.ToInt32(splitedStringToCheck[0].Substring(splitedStringToCheck[0].Length - 2));

                        MessageBox.Show("try to convert splitedStringToCheck[1] " + "(" + splitedStringToCheck[1] + ") to Int32");
                        int minutes = Convert.ToInt32(splitedStringToCheck[1]);

                        MessageBox.Show("try to convert splitedStringToCheck[2] " + "(" + splitedStringToCheck[2] + ") to Int32");
                        int seconds = Convert.ToInt32(splitedStringToCheck[2]);

                        //проверка на значения
                        if (hours > 23 || minutes > 59 || seconds > 59)
                        {

                            MessageBox.Show("hours = " + hours.ToString() + ", minutes = " + minutes.ToString() + ", seconds = " + seconds.ToString());

                            return false;
                        }
                    }
                    catch
                    {

                        MessageBox.Show("Can't convert substrings to Int32");

                        MessageBox.Show("splitedStringToCheck[0].Substring(splitedStringToCheck[0].Length - 2) = " + splitedStringToCheck[0].Substring(splitedStringToCheck[0].Length - 2));

                        return false;
                    }

                    //осуществляется попытка привести к типу данных int больше, чем 2 последних символов первой подстроки (индекс = 0)
                    //с целью выявить трехзначное (и более значное) число
                    try
                    {

                        MessageBox.Show("try to convert splitedStringToCheck[0].Substring(splitedStringToCheck[0].Length - 3) " + "(" + splitedStringToCheck[0].Substring(splitedStringToCheck[0].Length - 3) + ") to Int32");

                        int hours = Convert.ToInt32(splitedStringToCheck[0].Substring(splitedStringToCheck[0].Length - 3));
                        //если предыдущая операция будет выполнена без ошибок, следовательно, как минимум последние 3 символа являются цифрами,
                        //а значит, проверяемая строка имеет неверный формат
                        return false;
                    }
                    //если же в момент выполнения процедуры приведения последних трех символов 1-ой подстроки возникло исключение,
                    //значит, третий символ от конца подстроки не является цифрой и выполнение процедуры можно продолжать.
                    catch
                    {

                    }

                    //если исключения не возникло, осуществляется проверка на наличие в строке слова "дней"
                    if (stringToCheck.IndexOf("дней") != -1)
                    {

                        MessageBox.Show("Keyword 'дней' has been detected");

                        //если такое слово найдено, строка снова разбивается на подстроки по символу ' ' (пробел)
                        splitedStringToCheck = stringToCheck.Split(' ');

                        //если в результате разбиения строки на подстроки количество полученных подстрок не равно 3
                        //или значение второй подстроки (у второй подстроки индекс = 1) не равно слову "дней",
                        //значит, строка имеет неверный формат
                        if ((splitedStringToCheck.Length != 3) || (splitedStringToCheck[1] != "дней"))
                        {

                            MessageBox.Show("splited strings count is not equal 3 or splitedStringToCheck[1] is not equal 'дней'");

                            return false;
                        }
                        else
                        {
                            //в противном случае осуществляется попытка привести значение первой подстроки к типу int.
                            try
                            {

                                MessageBox.Show("try to convert splitedStringToCheck[0] (" + splitedStringToCheck[0] + ") to Int32");

                                Convert.ToInt32(splitedStringToCheck[0]);
                            }
                            //если попытка привела к возникновению исключения, значит, строка имеет неверный формат
                            catch
                            {

                                MessageBox.Show("the try to convert splitedStringToCheck[0] was failed");

                                return false;
                            }
                            //иначе строка удовлетворяет всем условиям
                            return true;
                        }
                    }
                    else
                    {
                        //иначе строка удовлетворяет всем условиям
                        return true;
                    }
                }
            }
        }

        //преобразует значение даты переменной типа DateTime в строку по указанному формату, например YYYY/MM/DD
        static public string DateToString(DateTime date, string pattern)
        {
            string yearString = "", monthString = "", dayString = "";

            int index = 0;
            int yearDigits = 0;
            while (pattern[index].ToString() != "Y" && index < pattern.Length)
            {
                index++;
            }
            if (pattern[index].ToString() != "Y")
            {
                return "";
            }
            else
            {
                if (index == pattern.Length)
                {
                    yearDigits = 1;
                }
                else
                {
                    while (index < pattern.Length && pattern[index].ToString() == "Y")
                    {
                        index++;
                        yearDigits++;
                    }
                }
                if (yearDigits == 2)
                {
                    yearString = date.Year.ToString().Substring(2, 2);
                }
                else
                {
                    if (yearDigits == 4)
                    {
                        yearString = date.Year.ToString();
                    }
                    else
                    {
                        return "";
                    }
                }
            }

            index = 0;
            int monthDigits = 0;
            while (pattern[index].ToString() != "M" && index < pattern.Length)
            {
                index++;
            }
            if (pattern[index].ToString() != "M")
            {
                return "";
            }
            else
            {
                if (index == pattern.Length)
                {
                    monthDigits = 1;
                }
                else
                {
                    while (index < pattern.Length && pattern[index].ToString() == "M")
                    {
                        index++;
                        monthDigits++;
                    }
                }
                if (monthDigits == 1)
                {
                    monthString = date.Month.ToString();
                }
                else
                {
                    if (monthDigits == 2)
                    {
                        if (date.Month < 10)
                        {
                            monthString = "0" + date.Month;
                        }
                        else
                        {
                            monthString = date.Month.ToString();
                        }
                    }
                    else
                    {
                        return "";
                    }
                }
            }

            index = 0;
            int dayDigits = 0;
            while (pattern[index].ToString() != "D" && index < pattern.Length)
            {
                index++;
            }
            if (pattern[index].ToString() != "D")
            {
                return "";
            }
            else
            {
                if (index == pattern.Length)
                {
                    dayDigits = 1;
                }
                else
                {
                    while (index < pattern.Length && pattern[index].ToString() == "D")
                    {
                        index++;
                        dayDigits++;
                    }
                }
                if (dayDigits == 1)
                {
                    dayString = date.Day.ToString();
                }
                else
                {
                    if (dayDigits == 2)
                    {
                        if (date.Day < 10)
                        {
                            dayString = "0" + date.Day;
                        }
                        else
                        {
                            dayString = date.Day.ToString();
                        }
                    }
                    else
                    {
                        return "";
                    }
                }
            }

            index = 0;
            char curChar = pattern[index];
            string separator = "";
            while (pattern[index] == curChar && index < pattern.Length - 1)
            {
                index++;
            }
            if (pattern[index].ToString() != "Y" && pattern[index].ToString() != "M" && pattern[index].ToString() != "D")
            {
                separator = pattern[index].ToString();
            }
            else
            {
                separator = ".";
            }

            string result = "";
            index = 0;
            while (index < pattern.Length)
            {
                if (index > 0)
                {
                    if (pattern[index] != pattern[index - 1])
                    {
                        if (pattern[index] == 'Y')
                        {
                            result += yearString + separator;
                        }

                        if (pattern[index] == 'M')
                        {
                            result += monthString + separator;
                        }

                        if (pattern[index] == 'D')
                        {
                            result += dayString + separator;
                        }
                    }
                }
                else
                {
                    if (pattern[index] == 'Y')
                    {
                        result += yearString + separator;
                    }

                    if (pattern[index] == 'M')
                    {
                        result += monthString + separator;
                    }

                    if (pattern[index] == 'D')
                    {
                        result += dayString + separator;
                    }
                }
                index++;
            }
            result = result.Substring(0, result.Length - 1);
            return result;
        }

        //вспомогательная процедура
        static private bool isNumber(char targetChar)
        {
            char[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            bool isNumber = false;
            foreach (char curChar in numbers)
            {
                if (targetChar == curChar)
                {
                    isNumber = true;
                }
            }
            return isNumber;
        }

        //обратный к GetTimeStringFromSeconds() метод. Используется для формирования запроса, в котором учавствуют
        //значения временных интервалов
        static public int GetSecondsFromTimeString(string timeString)
        {
            //проверка на соответствие введенной строки требуемому шаблону: "n дн hh:mm:ss"
            //проверка на пустую строку
            if (timeString.Length > 0)
            {
                int days = 0, hours = 0, minutes = 0, seconds = 0;

                //если нет слова дн, значит формат должен быть hh:mm:ss
                if (timeString.IndexOf("дн") == -1)
                {
                    if (isNumber(timeString[0]) && isNumber(timeString[1]) && timeString[2] == ':' && isNumber(timeString[3]) && isNumber(timeString[4]) && timeString[5] == ':' && isNumber(timeString[6]) && isNumber(timeString[7]))
                    {
                        hours = Convert.ToInt32(timeString.Substring(0, 2));
                        minutes = Convert.ToInt32(timeString.Substring(3, 2));
                        seconds = Convert.ToInt32(timeString.Substring(6, 2));
                        return (hours * 60 * 60) + (minutes * 60) + seconds;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    int DayIndex = 0;
                    while (isNumber(timeString[DayIndex]) && DayIndex < timeString.Length)
                    {
                        DayIndex++;
                    }
                    if (timeString[DayIndex].ToString() == " " && timeString[DayIndex + 1].ToString() == "д" && timeString[DayIndex + 2].ToString() == "н" && timeString[DayIndex + 3].ToString() == " ")
                    {
                        int TimeIndex = DayIndex + 4;
                        if (isNumber(timeString[TimeIndex]) && isNumber(timeString[TimeIndex + 1]) && timeString[TimeIndex + 2] == ':' && isNumber(timeString[TimeIndex + 3]) && isNumber(timeString[TimeIndex + 4]) && timeString[TimeIndex + 5] == ':' && isNumber(timeString[TimeIndex + 6]) && isNumber(timeString[TimeIndex + 7]))
                        {
                            days = Convert.ToInt32(timeString.Substring(0, DayIndex));
                            hours = Convert.ToInt32(timeString.Substring(TimeIndex, 2));
                            minutes = Convert.ToInt32(timeString.Substring(TimeIndex + 3, 2));
                            seconds = Convert.ToInt32(timeString.Substring(TimeIndex + 6, 2));

                            return (days * 24 * 60 * 60) + (hours * 60 * 60) + (minutes * 60) + seconds;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            else
            {
                return -1;
            }
        }

        //конструкторы

    }

    public struct StringAndColorPair
    {
        public string stringToFind;
        public Color colorToSet;
    }

    abstract public class StringChecker
    {

    }

    public class FilesSearcher
    {
        //подклассы

        //переменные, константы

        //Структура и переменная для получения информации об основных свойствах директории
        public struct DirInfo
        {
            public int totalFilesCount;
            public int totalDirsCount;
            public double totalDirSize;
            internal void Clear()
            {
                totalFilesCount = 0;
                totalDirsCount = 0;
                totalDirSize = 0;
            }
        }
        private DirInfo dirInfo;

        //структуры, которые служат контейнерами для передачи информации
        //о результатах поиска
        public enum FoundObjectType
        {
            Directory,
            File
        }
        public struct FoundObjectInfo
        {
            public string Name;
            public FoundObjectType Type;
            public long Size;
        }
        public struct SearchResults
        {
            public int browsedFilesCount;
            public int browsedDirsCount;
            public int foundFilesCount;
            public int foundDirsCount;
            public List<FoundObjectInfo> foundObjectsList;
            public List<string> unreachableObjectsList;
            internal void Clear()
            {
                this.browsedFilesCount = 0;
                this.browsedDirsCount = 0;
                this.foundFilesCount = 0;
                this.foundDirsCount = 0;
                this.foundObjectsList.Clear();
                this.unreachableObjectsList.Clear();
            }
        }
        private SearchResults result;
        public delegate void FoundFilesHandler(FoundObjectInfo foundObjInf);
        public static event FoundFilesHandler FileHasBeenFound;

        //делегат и событие для него, возникающее при просмотре очередного файла для поиска
        public delegate void BrowsingFileHandler(string browsingFileName);
        public static event BrowsingFileHandler FileIsBrowsing;
        
        //public delegate void BrowsedFilesHandler(int browsedFilesAmount);
        //public static event BrowsedFilesHandler FilesHaveBeenBrowsed;
        public bool searchAtFilesBegining = false;

        //процедуры, функции

        private string GetPatternFromUserString(string userString)
        {
            string result = userString;
            //если вначале строки нет сиволов * и ?, необходимо добавить в начало строки
            //символ ^ для того, чтобы при последующем сопоставлении строк учитывался тот
            //факт, что искомая строка ДОЛЖНА начинаться с указанной последовательности символов
            if (result[0] != '*' /*&& result[0] != '?'*/)
            {
                result = "^" + result;
            }
            //тот же принцип с символами в конце строки. Если это не * и не ?, значит искомая строка
            //должна закончится именно указанными в маске символами
            if (result[result.Length - 1] != '*' /*&& result[result.Length - 1] != '?'*/)
            {
                result = result + "$";
            }
            //экранируем точки, т.к. с точки зрения регулярных выражений точка является спецсимволом
            result = result.Replace(".", "\\.");

            //дальше делим строку на подстроки сперва по символу *, затем по символу ?, чтобы заменить их
            //на, соответственно, \w* и \w?
            result = result.Replace("*", "\\w*");
            result = result.Replace("?", "\\w?");
            //возвращаем полученную измененную строку
            return result;
        }

        private void FindFilesWOEvents(string directoryPath)
        {
            try
            {
                string[] dirsList = Directory.GetDirectories(directoryPath);
                if (dirsList.Length == 0)
                {
                    string[] filesList = Directory.GetFiles(directoryPath);
                    foreach (string curFile in filesList)
                    {
                        dirInfo.totalFilesCount++;
                        FileInfo curFileInfo = new FileInfo(curFile);
                        dirInfo.totalDirSize += curFileInfo.Length;
                    }
                }
                else
                {
                    dirInfo.totalDirsCount += dirsList.Length;
                    string[] filesList = Directory.GetFiles(directoryPath);
                    foreach (string curFile in filesList)
                    {
                        dirInfo.totalFilesCount++;
                        FileInfo curFileInfo = new FileInfo(curFile);
                        dirInfo.totalDirSize += curFileInfo.Length;
                    }
                    foreach (string curDir in dirsList)
                    {
                        FindFilesWOEvents(curDir);
                    }
                }
            }
            catch
            {
            }
        }

        private void FindFiles(string searchingPath, bool searchInSubFolders)
        {
            if (!searchInSubFolders)
            {
                try
                {
                    string[] dirsList = Directory.GetDirectories(searchingPath);
                    result.browsedDirsCount += dirsList.Length;
                    string[] filesList = Directory.GetFiles(searchingPath);
                    result.browsedFilesCount += filesList.Length;
                    foreach (string curDirName in dirsList)
                    {
                        FoundObjectInfo newFoundObjectInfo = new FoundObjectInfo();
                        newFoundObjectInfo.Name = curDirName;
                        newFoundObjectInfo.Size = 0;
                        newFoundObjectInfo.Type = FoundObjectType.Directory;
                        result.foundObjectsList.Add(newFoundObjectInfo);
                        if (FileHasBeenFound != null)
                        {
                            FileHasBeenFound(newFoundObjectInfo);
                        }
                    }
                    foreach (string curFileName in filesList)
                    {
                        FoundObjectInfo newFoundObjectInfo = new FoundObjectInfo();
                        newFoundObjectInfo.Name = curFileName;
                        newFoundObjectInfo.Size = new FileInfo(curFileName).Length;
                        newFoundObjectInfo.Type = FoundObjectType.File;
                        result.foundObjectsList.Add(newFoundObjectInfo);
                        if (FileHasBeenFound != null)
                        {
                            FileHasBeenFound(newFoundObjectInfo);
                        }
                    }
                }
                catch
                {
                    FoundObjectInfo newFoundObjectInfo = new FoundObjectInfo();
                    newFoundObjectInfo.Name = "Root element is unreachable";
                    newFoundObjectInfo.Size = 0;
                    newFoundObjectInfo.Type = FoundObjectType.Directory;
                    result.foundObjectsList.Add(newFoundObjectInfo);
                }
            }
            else
            {
                try
                {
                    string[] dirsList = Directory.GetDirectories(searchingPath);
                    result.browsedDirsCount += dirsList.Length;
                    string[] filesList = Directory.GetFiles(searchingPath);
                    result.browsedFilesCount += filesList.Length;
                    if (dirsList.Length == 0)
                    {
                        foreach (string curFileName in filesList)
                        {
                            FoundObjectInfo newFoundObjectInfo = new FoundObjectInfo();
                            newFoundObjectInfo.Name = curFileName;
                            newFoundObjectInfo.Size = new FileInfo(curFileName).Length;
                            newFoundObjectInfo.Type = FoundObjectType.File;
                            result.foundObjectsList.Add(newFoundObjectInfo);
                            if (FileHasBeenFound != null)
                            {
                                FileHasBeenFound(newFoundObjectInfo);
                            }
                        }
                    }
                    else
                    {
                        foreach (string curFileName in filesList)
                        {
                            FoundObjectInfo newFoundObjectInfo = new FoundObjectInfo();
                            newFoundObjectInfo.Name = curFileName;
                            newFoundObjectInfo.Size = new FileInfo(curFileName).Length;
                            newFoundObjectInfo.Type = FoundObjectType.File;
                            result.foundObjectsList.Add(newFoundObjectInfo);
                            if (FileHasBeenFound != null)
                            {
                                FileHasBeenFound(newFoundObjectInfo);
                            }
                        }
                        foreach (string curDirName in dirsList)
                        {
                            FoundObjectInfo newFoundObjectInfo = new FoundObjectInfo();
                            newFoundObjectInfo.Name = curDirName;
                            newFoundObjectInfo.Size = 0;
                            newFoundObjectInfo.Type = FoundObjectType.Directory;
                            result.foundObjectsList.Add(newFoundObjectInfo);
                            if (FileHasBeenFound != null)
                            {
                                FileHasBeenFound(newFoundObjectInfo);
                            }
                            FindFiles(curDirName, true);
                        }
                    }
                }
                catch
                {
                    result.browsedDirsCount++;
                    result.unreachableObjectsList.Add(searchingPath);
                }
            }
        }

        private void FindFiles(string searchingPath, string[] masks, bool searchInSubFolders)
        {
            string[] patterns = new string[masks.Length];
            for (int i = 0; i < masks.Length; i++)
            {
                patterns[i] = GetPatternFromUserString(masks[i]);
            }
            if (!searchInSubFolders)
            {
                string fileNameWOPath = "";
                try
                {
                    string[] dirsList = Directory.GetDirectories(searchingPath);
                    result.browsedDirsCount += dirsList.Length;
                    string[] filesList = Directory.GetFiles(searchingPath);
                    result.browsedFilesCount += filesList.Length;
                    foreach (string curDirName in dirsList)
                    {
                        //событие, возникающее всякий раз при просмотре очередного файла (и, возможно, каталога)
                        if (FileIsBrowsing != null)
                        {
                            FileIsBrowsing(curDirName);
                        }

                        fileNameWOPath = curDirName.Substring(curDirName.LastIndexOf('\\') + 1);
                        foreach (string curPattern in patterns)
                        {
                            if (System.Text.RegularExpressions.Regex.IsMatch(fileNameWOPath.ToUpper(), curPattern.ToUpper()))
                            {
                                result.foundDirsCount++;
                                FoundObjectInfo newFoundObjectInfo = new FoundObjectInfo();
                                newFoundObjectInfo.Name = curDirName;
                                newFoundObjectInfo.Size = 0;
                                newFoundObjectInfo.Type = FoundObjectType.Directory;
                                result.foundObjectsList.Add(newFoundObjectInfo);
                                if (FileHasBeenFound != null)
                                {
                                    FileHasBeenFound(newFoundObjectInfo);
                                }
                            }
                        }
                    }
                    foreach (string curFileName in filesList)
                    {
                        //событие, возникающее всякий раз при просмотре очередного файла (и, возможно, каталога)
                        if (FileIsBrowsing != null)
                        {
                            FileIsBrowsing(curFileName);
                        }

                        fileNameWOPath = curFileName.Substring(curFileName.LastIndexOf('\\') + 1);
                        foreach (string curPattern in patterns)
                        {
                            if (System.Text.RegularExpressions.Regex.IsMatch(fileNameWOPath, curPattern))
                            {
                                result.foundFilesCount++;
                                FoundObjectInfo newFoundObjectInfo = new FoundObjectInfo();
                                newFoundObjectInfo.Name = curFileName;
                                newFoundObjectInfo.Size = new FileInfo(curFileName).Length;
                                newFoundObjectInfo.Type = FoundObjectType.File;
                                result.foundObjectsList.Add(newFoundObjectInfo);
                                if (FileHasBeenFound != null)
                                {
                                    FileHasBeenFound(newFoundObjectInfo);
                                }
                            }
                        }
                    }
                }
                catch
                {
                    FoundObjectInfo newFoundObjectInfo = new FoundObjectInfo();
                    newFoundObjectInfo.Name = "Root element is unreachable";
                    newFoundObjectInfo.Size = 0;
                    newFoundObjectInfo.Type = FoundObjectType.Directory;
                    result.foundObjectsList.Add(newFoundObjectInfo);
                }
            }
            else
            {
                string fileNameWOPath = "";
                try
                {
                    string[] dirsList = Directory.GetDirectories(searchingPath);
                    result.browsedDirsCount += dirsList.Length;
                    string[] filesList = Directory.GetFiles(searchingPath);
                    result.browsedFilesCount += filesList.Length;
                    if (dirsList.Length == 0)
                    {
                        foreach (string curFileName in filesList)
                        {
                            //событие, возникающее всякий раз при просмотре очередного файла (и, возможно, каталога)
                            if (FileIsBrowsing != null)
                            {
                                FileIsBrowsing(curFileName);
                            }

                            fileNameWOPath = curFileName.Substring(curFileName.LastIndexOf('\\') + 1);
                            foreach (string curPattern in patterns)
                            {
                                if (System.Text.RegularExpressions.Regex.IsMatch(fileNameWOPath.ToUpper(), curPattern.ToUpper()))
                                {
                                    result.foundFilesCount++;
                                    FoundObjectInfo newFoundObjectInfo = new FoundObjectInfo();
                                    newFoundObjectInfo.Name = curFileName;
                                    newFoundObjectInfo.Size = new FileInfo(curFileName).Length;
                                    newFoundObjectInfo.Type = FoundObjectType.File;
                                    result.foundObjectsList.Add(newFoundObjectInfo);
                                    if (FileHasBeenFound != null)
                                    {
                                        FileHasBeenFound(newFoundObjectInfo);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (string curFileName in filesList)
                        {
                            //событие, возникающее всякий раз при просмотре очередного файла (и, возможно, каталога)
                            if (FileIsBrowsing != null)
                            {
                                FileIsBrowsing(curFileName);
                            }

                            fileNameWOPath = curFileName.Substring(curFileName.LastIndexOf('\\') + 1);
                            foreach (string curPattern in patterns)
                            {
                                if (System.Text.RegularExpressions.Regex.IsMatch(fileNameWOPath.ToUpper(), curPattern.ToUpper()))
                                {
                                    result.foundFilesCount++;
                                    FoundObjectInfo newFoundObjectInfo = new FoundObjectInfo();
                                    newFoundObjectInfo.Name = curFileName;
                                    newFoundObjectInfo.Size = new FileInfo(curFileName).Length;
                                    newFoundObjectInfo.Type = FoundObjectType.File;
                                    result.foundObjectsList.Add(newFoundObjectInfo);
                                    if (FileHasBeenFound != null)
                                    {
                                        FileHasBeenFound(newFoundObjectInfo);
                                    }
                                }
                            }
                        }
                        foreach (string curDirName in dirsList)
                        {
                            //событие, возникающее всякий раз при просмотре очередного файла (и, возможно, каталога)
                            if (FileIsBrowsing != null)
                            {
                                FileIsBrowsing(curDirName);
                            }

                            fileNameWOPath = curDirName.Substring(curDirName.LastIndexOf('\\') + 1);
                            foreach (string curPattern in patterns)
                            {
                                if (System.Text.RegularExpressions.Regex.IsMatch(fileNameWOPath.ToUpper(), curPattern.ToUpper()))
                                {
                                    result.foundDirsCount++;
                                    FoundObjectInfo newFoundObjectInfo = new FoundObjectInfo();
                                    newFoundObjectInfo.Name = curDirName;
                                    newFoundObjectInfo.Size = 0;
                                    newFoundObjectInfo.Type = FoundObjectType.Directory;
                                    result.foundObjectsList.Add(newFoundObjectInfo);
                                    if (FileHasBeenFound != null)
                                    {
                                        FileHasBeenFound(newFoundObjectInfo);
                                    }
                                }
                            }
                            FindFiles(curDirName, masks, true);
                        }
                    }
                }
                catch
                {
                    result.browsedFilesCount++;
                    result.unreachableObjectsList.Add(searchingPath);
                }
            }
        }

        //останавливает незакрывшиеся в методе FindFiles(string searchingPath, string[] masks, string[] textToFind, bool searchInSubFolders)
        //процессы winword.exe
        private void KillWinwordProcs()
        {
            //информация о всех существующих процессах помещается в список allProc
            Process[] allProcs = Process.GetProcesses();

            foreach (Process curProc in allProcs)
            {
                if (curProc.ProcessName.IndexOf("winword") != -1)
                {
                    try
                    {
                        curProc.Kill();
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void FindFiles(string searchingPath, string[] masks, string[] textToFind, bool searchInSubFolders)
        {
            string[] maskPatterns = new string[masks.Length];
            string[] textPatterns = new string[textToFind.Length];
            for (int i = 0; i < masks.Length; i++)
            {
                maskPatterns[i] = GetPatternFromUserString(masks[i]);
            }
            for (int i = 0; i < textToFind.Length; i++)
            {
                textPatterns[i] = GetPatternFromUserString(textToFind[i]);
            }

            //переменная-индикатор. Принимает значение true, если искомый текст был найден.
            //С ее помощью после поиска в цикле по всем искомым словам будет выполнены действия по добавлению файла в список найденных.
            //В противном случае счетчик найденных файлов и итоговый размер найденных файлов будет увеличиваться всякий раз, 
            //когда в файле будет обнаруживаться следующее искомое слово, т.к. процедура будет выполняться в цикле
            bool textHasBeenFound = false;

            if (!searchInSubFolders)
            {
                string fileNameWOPath = "";
                try
                {
                    string[] dirsList = Directory.GetDirectories(searchingPath);
                    result.browsedDirsCount += dirsList.Length;
                    string[] filesList = Directory.GetFiles(searchingPath);
                    result.browsedFilesCount += filesList.Length;

                    foreach (string curFileName in filesList)
                    {
                        //событие, возникающее всякий раз при просмотре очередного файла (и, возможно, каталога)
                        if (FileIsBrowsing != null)
                        {
                            FileIsBrowsing(curFileName);
                        }

                        //обнуление переменной textHasBeenFound
                        textHasBeenFound = false;

                        fileNameWOPath = curFileName.Substring(curFileName.LastIndexOf('\\') + 1);
                        foreach (string curMaskPattern in maskPatterns)
                        {
                            if (System.Text.RegularExpressions.Regex.IsMatch(fileNameWOPath.ToUpper(), curMaskPattern.ToUpper()))
                            {
                                string fileExtention = fileNameWOPath.Substring(fileNameWOPath.LastIndexOf(".") + 1);
                                if (
                                    (fileExtention == "txt") ||
                                    (fileExtention == "ini") ||
                                    (fileExtention == "inf") ||
                                    (fileExtention == "cfg") ||
                                    (fileExtention == "conf") ||
                                    (fileExtention == "config") ||
                                    (fileExtention == "log") ||
                                    (fileExtention == "xml") ||
                                    (fileExtention == "rtf") ||
                                    (fileExtention == "url"))
                                {
                                    try
                                    {
                                        string textFromFile = File.ReadAllText(curFileName, Encoding.Default);

                                        foreach (string curTextPattern in textPatterns)
                                        {
                                            if (System.Text.RegularExpressions.Regex.IsMatch(textFromFile, curTextPattern))
                                            {
                                                textHasBeenFound = true;
                                            }
                                        }

                                        if (textHasBeenFound)
                                        {
                                            result.foundFilesCount++;
                                            FoundObjectInfo newFoundObjectInfo = new FoundObjectInfo();
                                            newFoundObjectInfo.Name = curFileName;
                                            newFoundObjectInfo.Size = new FileInfo(curFileName).Length;
                                            newFoundObjectInfo.Type = FoundObjectType.File;
                                            result.foundObjectsList.Add(newFoundObjectInfo);
                                            if (FileHasBeenFound != null)
                                            {
                                                FileHasBeenFound(newFoundObjectInfo);
                                            }
                                        }
                                    }
                                    catch
                                    {

                                    }
                                }

                                if ((fileExtention == "doc") ||
                                    (fileExtention == "docx"))
                                {
                                    //объявление необходимых переменных
                                    object nullObject = Type.Missing;
                                    object falseValue = false;
                                    object trueValue = true;
                                    object file = curFileName;
                                    Microsoft.Office.Interop.Word.Application app = null;
                                    Microsoft.Office.Interop.Word.Document doc = null;

                                    try
                                    {
                                        app = new Microsoft.Office.Interop.Word.Application();
                                        doc = app.Documents.Open(ref file, ref nullObject, ref nullObject,
                                                     ref nullObject, ref nullObject, ref nullObject, ref nullObject, ref nullObject, ref nullObject,
                                                     ref nullObject, ref nullObject, ref falseValue, ref nullObject, ref nullObject, ref nullObject, ref nullObject);

                                        //если опция поиска только в начале файлов не выбрана, поиск производится по всему содержимому, однако файлы, количество слов в которых
                                        //превышает 5000, пропускаются
                                        if (!searchAtFilesBegining)
                                        {
                                            //процесс поиска регулярных выражений в файлах большого объема занимает по-настоящему много времени.
                                            //Файл .doc размером 2,3 Мб (107000+ слов) обрабатывался с 11:18 до 13:35.
                                            if (doc.Words.Count < 5000)
                                            {
                                                if (doc.Paragraphs.Count > 1)
                                                {
                                                    for (int i = 1; i < doc.Paragraphs.Count - 1; i++)
                                                    {
                                                        foreach (string curTextPattern in textPatterns)
                                                        {
                                                            if (System.Text.RegularExpressions.Regex.IsMatch(doc.Paragraphs[i].Range.Text, curTextPattern))
                                                            {
                                                                textHasBeenFound = true;
                                                            }
                                                        }
                                                    }

                                                    if (textHasBeenFound)
                                                    {
                                                        result.foundFilesCount++;
                                                        FoundObjectInfo newFoundObjectInfo = new FoundObjectInfo();
                                                        newFoundObjectInfo.Name = curFileName;
                                                        newFoundObjectInfo.Size = new FileInfo(curFileName).Length;
                                                        newFoundObjectInfo.Type = FoundObjectType.File;
                                                        result.foundObjectsList.Add(newFoundObjectInfo);
                                                        if (FileHasBeenFound != null)
                                                        {
                                                            FileHasBeenFound(newFoundObjectInfo);
                                                        }
                                                    }
                                                }

                                                if (doc.Paragraphs.Count == 1)
                                                {
                                                    foreach (string curTextPattern in textPatterns)
                                                    {
                                                        if (System.Text.RegularExpressions.Regex.IsMatch(doc.Paragraphs[1].Range.Text, curTextPattern))
                                                        {
                                                            textHasBeenFound = true;
                                                        }
                                                    }

                                                    if (textHasBeenFound)
                                                    {
                                                        result.foundFilesCount++;
                                                        FoundObjectInfo newFoundObjectInfo = new FoundObjectInfo();
                                                        newFoundObjectInfo.Name = curFileName;
                                                        newFoundObjectInfo.Size = new FileInfo(curFileName).Length;
                                                        newFoundObjectInfo.Type = FoundObjectType.File;
                                                        result.foundObjectsList.Add(newFoundObjectInfo);
                                                        if (FileHasBeenFound != null)
                                                        {
                                                            FileHasBeenFound(newFoundObjectInfo);
                                                        }
                                                    }
                                                }

                                                //документ и приложение закрываются
                                                doc.Close(ref nullObject, ref nullObject, ref nullObject);
                                                app.Quit(ref nullObject, ref nullObject, ref nullObject);

                                                //на случай, если приложение winword так и не закрылось (а такое случается сплошь и рядом)
                                                //выполняется мой собственный метод, который закрывает вообще все процессы winword, которые были 
                                                //созданы данной программой
                                                //KillWinwordProcs();
                                            }
                                        }
                                        else
                                        {
                                            if (doc.Paragraphs.Count > 1)
                                            {
                                                for (int i = 1; i < 100; i++)
                                                {
                                                    foreach (string curTextPattern in textPatterns)
                                                    {
                                                        if (System.Text.RegularExpressions.Regex.IsMatch(doc.Paragraphs[i].Range.Text, curTextPattern))
                                                        {
                                                            textHasBeenFound = true;
                                                        }
                                                    }
                                                }

                                                if (textHasBeenFound)
                                                {
                                                    result.foundFilesCount++;
                                                    FoundObjectInfo newFoundObjectInfo = new FoundObjectInfo();
                                                    newFoundObjectInfo.Name = curFileName;
                                                    newFoundObjectInfo.Size = new FileInfo(curFileName).Length;
                                                    newFoundObjectInfo.Type = FoundObjectType.File;
                                                    result.foundObjectsList.Add(newFoundObjectInfo);
                                                    if (FileHasBeenFound != null)
                                                    {
                                                        FileHasBeenFound(newFoundObjectInfo);
                                                    }
                                                }
                                            }

                                            if (doc.Paragraphs.Count == 1)
                                            {
                                                foreach (string curTextPattern in textPatterns)
                                                {
                                                    if (System.Text.RegularExpressions.Regex.IsMatch(doc.Paragraphs[1].Range.Text, curTextPattern))
                                                    {
                                                        textHasBeenFound = true;
                                                    }
                                                }

                                                if (textHasBeenFound)
                                                {
                                                    result.foundFilesCount++;
                                                    FoundObjectInfo newFoundObjectInfo = new FoundObjectInfo();
                                                    newFoundObjectInfo.Name = curFileName;
                                                    newFoundObjectInfo.Size = new FileInfo(curFileName).Length;
                                                    newFoundObjectInfo.Type = FoundObjectType.File;
                                                    result.foundObjectsList.Add(newFoundObjectInfo);
                                                    if (FileHasBeenFound != null)
                                                    {
                                                        FileHasBeenFound(newFoundObjectInfo);
                                                    }
                                                }
                                            }

                                            //документ и приложение закрываются
                                            doc.Close(ref nullObject, ref nullObject, ref nullObject);
                                            app.Quit(ref nullObject, ref nullObject, ref nullObject);


                                        }
                                    }
                                    catch
                                    {

                                    }
                                    finally
                                    {
                                        //на случай, если приложение winword так и не закрылось (а такое случается сплошь и рядом)
                                        //выполняется мой собственный метод, который закрывает вообще все процессы winword, которые были 
                                        //созданы данной программой
                                        KillWinwordProcs();
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                    FoundObjectInfo newFoundObjectInfo = new FoundObjectInfo();
                    newFoundObjectInfo.Name = "Root element is unreachable";
                    newFoundObjectInfo.Size = 0;
                    newFoundObjectInfo.Type = FoundObjectType.Directory;
                    result.foundObjectsList.Add(newFoundObjectInfo);
                }
            }
            else
            {
                string fileNameWOPath = "";

                try
                {
                    string[] dirsList = Directory.GetDirectories(searchingPath);
                    result.browsedDirsCount += dirsList.Length;
                    string[] filesList = Directory.GetFiles(searchingPath);
                    result.browsedFilesCount += filesList.Length;
                    if (dirsList.Length == 0)
                    {
                        foreach (string curFileName in filesList)
                        {
                            //событие, возникающее всякий раз при просмотре очередного файла (и, возможно, каталога)
                            if (FileIsBrowsing != null)
                            {
                                FileIsBrowsing(curFileName);
                            }

                            //Обнуление переменной fileHasBeenFound
                            textHasBeenFound = false;

                            fileNameWOPath = curFileName.Substring(curFileName.LastIndexOf('\\') + 1);
                            foreach (string curMaskPattern in maskPatterns)
                            {
                                if (System.Text.RegularExpressions.Regex.IsMatch(fileNameWOPath.ToUpper(), curMaskPattern.ToUpper()))
                                {
                                    string fileExtention = fileNameWOPath.Substring(fileNameWOPath.LastIndexOf(".") + 1);
                                    if ((fileExtention == "txt") ||
                                        (fileExtention == "ini") ||
                                        (fileExtention == "inf") ||
                                        (fileExtention == "cfg") ||
                                        (fileExtention == "conf") ||
                                        (fileExtention == "config") ||
                                        (fileExtention == "log") ||
                                        (fileExtention == "xml") ||
                                        (fileExtention == "rtf") ||
                                        (fileExtention == "url"))
                                    {
                                        try
                                        {
                                            string textFromFile = File.ReadAllText(curFileName, Encoding.Default);

                                            foreach (string curTextPattern in textPatterns)
                                            {
                                                if (System.Text.RegularExpressions.Regex.IsMatch(textFromFile, curTextPattern))
                                                {
                                                    textHasBeenFound = true;
                                                }
                                            }

                                            //если хотя бы 1 искомая строка была обнаружена файл добавляется в список найденных
                                            if (textHasBeenFound)
                                            {
                                                result.foundFilesCount++;
                                                FoundObjectInfo newFoundObjectInfo = new FoundObjectInfo();
                                                newFoundObjectInfo.Name = curFileName;
                                                newFoundObjectInfo.Size = new FileInfo(curFileName).Length;
                                                newFoundObjectInfo.Type = FoundObjectType.File;
                                                result.foundObjectsList.Add(newFoundObjectInfo);
                                                if (FileHasBeenFound != null)
                                                {
                                                    FileHasBeenFound(newFoundObjectInfo);
                                                }
                                            }
                                        }
                                        catch
                                        {

                                        }
                                    }

                                    if ((fileExtention == "doc") ||
                                        (fileExtention == "docx"))
                                    {
                                        //объявление необходимых переменных
                                        object nullObject = Type.Missing;
                                        object falseValue = false;
                                        object trueValue = true;
                                        object file = curFileName;
                                        Microsoft.Office.Interop.Word.Application app = null;
                                        Microsoft.Office.Interop.Word.Document doc = null;

                                        try
                                        {
                                            app = new Microsoft.Office.Interop.Word.Application();
                                            doc = app.Documents.Open(ref file, ref nullObject, ref nullObject,
                                                         ref nullObject, ref nullObject, ref nullObject, ref nullObject, ref nullObject, ref nullObject,
                                                         ref nullObject, ref nullObject, ref falseValue, ref nullObject, ref nullObject, ref nullObject, ref nullObject);

                                            //если опция поиска только в начале файлов не выбрана, поиск производится по всему содержимому, однако файлы, количество слов в которых
                                            //превышает 5000, пропускаются
                                            if (!searchAtFilesBegining)
                                            {
                                                //процесс поиска регулярных выражений в файлах большого объема занимает по-настоящему много времени.
                                                //Файл .doc размером 2,3 Мб (107000+ слов) обрабатывался с 11:18 до 13:35.
                                                if (doc.Words.Count < 5000)
                                                {
                                                    if (doc.Paragraphs.Count > 1)
                                                    {
                                                        for (int i = 1; i < doc.Paragraphs.Count - 1; i++)
                                                        {
                                                            foreach (string curTextPattern in textPatterns)
                                                            {
                                                                if (System.Text.RegularExpressions.Regex.IsMatch(doc.Paragraphs[i].Range.Text, curTextPattern))
                                                                {
                                                                    textHasBeenFound = true;
                                                                }
                                                            }
                                                        }

                                                        if (textHasBeenFound)
                                                        {
                                                            result.foundFilesCount++;
                                                            FoundObjectInfo newFoundObjectInfo = new FoundObjectInfo();
                                                            newFoundObjectInfo.Name = curFileName;
                                                            newFoundObjectInfo.Size = new FileInfo(curFileName).Length;
                                                            newFoundObjectInfo.Type = FoundObjectType.File;
                                                            result.foundObjectsList.Add(newFoundObjectInfo);
                                                            if (FileHasBeenFound != null)
                                                            {
                                                                FileHasBeenFound(newFoundObjectInfo);
                                                            }
                                                        }
                                                    }

                                                    if (doc.Paragraphs.Count == 1)
                                                    {
                                                        foreach (string curTextPattern in textPatterns)
                                                        {
                                                            if (System.Text.RegularExpressions.Regex.IsMatch(doc.Paragraphs[1].Range.Text, curTextPattern))
                                                            {
                                                                textHasBeenFound = true;
                                                            }
                                                        }

                                                        if (textHasBeenFound)
                                                        {
                                                            result.foundFilesCount++;
                                                            FoundObjectInfo newFoundObjectInfo = new FoundObjectInfo();
                                                            newFoundObjectInfo.Name = curFileName;
                                                            newFoundObjectInfo.Size = new FileInfo(curFileName).Length;
                                                            newFoundObjectInfo.Type = FoundObjectType.File;
                                                            result.foundObjectsList.Add(newFoundObjectInfo);
                                                            if (FileHasBeenFound != null)
                                                            {
                                                                FileHasBeenFound(newFoundObjectInfo);
                                                            }
                                                        }
                                                    }

                                                    //документ и приложение закрываются
                                                    doc.Close(ref nullObject, ref nullObject, ref nullObject);
                                                    app.Quit(ref nullObject, ref nullObject, ref nullObject);

                                                    //на случай, если приложение winword так и не закрылось (а такое случается сплошь и рядом)
                                                    //выполняется мой собственный метод, который закрывает вообще все процессы winword, которые были 
                                                    //созданы данной программой
                                                    //KillWinwordProcs();
                                                }
                                            }
                                            else
                                            {
                                                if (doc.Paragraphs.Count > 1)
                                                {
                                                    for (int i = 1; i < 100; i++)
                                                    {
                                                        foreach (string curTextPattern in textPatterns)
                                                        {
                                                            if (System.Text.RegularExpressions.Regex.IsMatch(doc.Paragraphs[i].Range.Text, curTextPattern))
                                                            {
                                                                textHasBeenFound = true;
                                                            }
                                                        }
                                                    }

                                                    if (textHasBeenFound)
                                                    {
                                                        result.foundFilesCount++;
                                                        FoundObjectInfo newFoundObjectInfo = new FoundObjectInfo();
                                                        newFoundObjectInfo.Name = curFileName;
                                                        newFoundObjectInfo.Size = new FileInfo(curFileName).Length;
                                                        newFoundObjectInfo.Type = FoundObjectType.File;
                                                        result.foundObjectsList.Add(newFoundObjectInfo);
                                                        if (FileHasBeenFound != null)
                                                        {
                                                            FileHasBeenFound(newFoundObjectInfo);
                                                        }
                                                    }
                                                }

                                                if (doc.Paragraphs.Count == 1)
                                                {
                                                    foreach (string curTextPattern in textPatterns)
                                                    {
                                                        if (System.Text.RegularExpressions.Regex.IsMatch(doc.Paragraphs[1].Range.Text, curTextPattern))
                                                        {
                                                            textHasBeenFound = true;
                                                        }
                                                    }

                                                    if (textHasBeenFound)
                                                    {
                                                        result.foundFilesCount++;
                                                        FoundObjectInfo newFoundObjectInfo = new FoundObjectInfo();
                                                        newFoundObjectInfo.Name = curFileName;
                                                        newFoundObjectInfo.Size = new FileInfo(curFileName).Length;
                                                        newFoundObjectInfo.Type = FoundObjectType.File;
                                                        result.foundObjectsList.Add(newFoundObjectInfo);
                                                        if (FileHasBeenFound != null)
                                                        {
                                                            FileHasBeenFound(newFoundObjectInfo);
                                                        }
                                                    }
                                                }

                                                //документ и приложение закрываются
                                                doc.Close(ref nullObject, ref nullObject, ref nullObject);
                                                app.Quit(ref nullObject, ref nullObject, ref nullObject);


                                            }
                                        }
                                        catch
                                        {

                                        }
                                        finally
                                        {
                                            //на случай, если приложение winword так и не закрылось (а такое случается сплошь и рядом)
                                            //выполняется мой собственный метод, который закрывает вообще все процессы winword, которые были 
                                            //созданы данной программой
                                            KillWinwordProcs();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (string curFileName in filesList)
                        {
                            //событие, возникающее всякий раз при просмотре очередного файла (и, возможно, каталога)
                            if (FileIsBrowsing != null)
                            {
                                FileIsBrowsing(curFileName);
                            }

                            //обнуление переменной textHasBeenFound
                            textHasBeenFound = false; 

                            fileNameWOPath = curFileName.Substring(curFileName.LastIndexOf('\\') + 1);
                            foreach (string curMaskPattern in maskPatterns)
                            {
                                if (System.Text.RegularExpressions.Regex.IsMatch(fileNameWOPath.ToUpper(), curMaskPattern.ToUpper()))
                                {
                                    string fileExtention = fileNameWOPath.Substring(fileNameWOPath.LastIndexOf(".") + 1);
                                    if ((fileExtention == "txt") ||
                                        (fileExtention == "ini") ||
                                        (fileExtention == "inf") ||
                                        (fileExtention == "cfg") ||
                                        (fileExtention == "conf") ||
                                        (fileExtention == "config") ||
                                        (fileExtention == "url"))
                                    {
                                        try
                                        {
                                            string textFromFile = File.ReadAllText(curFileName, Encoding.Default);

                                            foreach (string curTextPattern in textPatterns)
                                            {
                                                if (System.Text.RegularExpressions.Regex.IsMatch(textFromFile, curTextPattern))
                                                {
                                                    textHasBeenFound = true;
                                                }
                                            }

                                            if (textHasBeenFound)
                                            {
                                                result.foundFilesCount++;
                                                FoundObjectInfo newFoundObjectInfo = new FoundObjectInfo();
                                                newFoundObjectInfo.Name = curFileName;
                                                newFoundObjectInfo.Size = new FileInfo(curFileName).Length;
                                                newFoundObjectInfo.Type = FoundObjectType.File;
                                                result.foundObjectsList.Add(newFoundObjectInfo);
                                                if (FileHasBeenFound != null)
                                                {
                                                    FileHasBeenFound(newFoundObjectInfo);
                                                }
                                            }
                                        }
                                        catch
                                        {

                                        }
                                    }

                                    if ((fileExtention == "doc") ||
                                        (fileExtention == "docx"))
                                    {
                                        //объявление необходимых переменных
                                        object nullObject = Type.Missing;
                                        object falseValue = false;
                                        object trueValue = true;
                                        object file = curFileName;
                                        Microsoft.Office.Interop.Word.Application app = null;
                                        Microsoft.Office.Interop.Word.Document doc = null;

                                        try
                                        {
                                            app = new Microsoft.Office.Interop.Word.Application();
                                            doc = app.Documents.Open(ref file, ref nullObject, ref nullObject,
                                                         ref nullObject, ref nullObject, ref nullObject, ref nullObject, ref nullObject, ref nullObject,
                                                         ref nullObject, ref nullObject, ref falseValue, ref nullObject, ref nullObject, ref nullObject, ref nullObject);

                                            //если опция поиска только в начале файлов не выбрана, поиск производится по всему содержимому, однако файлы, количество слов в которых
                                            //превышает 5000, пропускаются
                                            if (!searchAtFilesBegining)
                                            {
                                                //процесс поиска регулярных выражений в файлах большого объема занимает по-настоящему много времени.
                                                //Файл .doc размером 2,3 Мб (107000+ слов) обрабатывался с 11:18 до 13:35.
                                                if (doc.Words.Count < 5000)
                                                {
                                                    if (doc.Paragraphs.Count > 1)
                                                    {
                                                        for (int i = 1; i < doc.Paragraphs.Count - 1; i++)
                                                        {
                                                            foreach (string curTextPattern in textPatterns)
                                                            {
                                                                if (System.Text.RegularExpressions.Regex.IsMatch(doc.Paragraphs[i].Range.Text, curTextPattern))
                                                                {
                                                                    textHasBeenFound = true;
                                                                }
                                                            }
                                                        }

                                                        if (textHasBeenFound)
                                                        {
                                                            result.foundFilesCount++;
                                                            FoundObjectInfo newFoundObjectInfo = new FoundObjectInfo();
                                                            newFoundObjectInfo.Name = curFileName;
                                                            newFoundObjectInfo.Size = new FileInfo(curFileName).Length;
                                                            newFoundObjectInfo.Type = FoundObjectType.File;
                                                            result.foundObjectsList.Add(newFoundObjectInfo);
                                                            if (FileHasBeenFound != null)
                                                            {
                                                                FileHasBeenFound(newFoundObjectInfo);
                                                            }
                                                        }
                                                    }

                                                    if (doc.Paragraphs.Count == 1)
                                                    {
                                                        foreach (string curTextPattern in textPatterns)
                                                        {
                                                            if (System.Text.RegularExpressions.Regex.IsMatch(doc.Paragraphs[1].Range.Text, curTextPattern))
                                                            {
                                                                textHasBeenFound = true;
                                                            }
                                                        }

                                                        if (textHasBeenFound)
                                                        {
                                                            result.foundFilesCount++;
                                                            FoundObjectInfo newFoundObjectInfo = new FoundObjectInfo();
                                                            newFoundObjectInfo.Name = curFileName;
                                                            newFoundObjectInfo.Size = new FileInfo(curFileName).Length;
                                                            newFoundObjectInfo.Type = FoundObjectType.File;
                                                            result.foundObjectsList.Add(newFoundObjectInfo);
                                                            if (FileHasBeenFound != null)
                                                            {
                                                                FileHasBeenFound(newFoundObjectInfo);
                                                            }
                                                        }
                                                    }

                                                    //документ и приложение закрываются
                                                    doc.Close(ref nullObject, ref nullObject, ref nullObject);
                                                    app.Quit(ref nullObject, ref nullObject, ref nullObject);

                                                    //на случай, если приложение winword так и не закрылось (а такое случается сплошь и рядом)
                                                    //выполняется мой собственный метод, который закрывает вообще все процессы winword, которые были 
                                                    //созданы данной программой
                                                    //KillWinwordProcs();
                                                }
                                            }
                                            else
                                            {
                                                if (doc.Paragraphs.Count > 1)
                                                {
                                                    for (int i = 1; i < 100; i++)
                                                    {
                                                        foreach (string curTextPattern in textPatterns)
                                                        {
                                                            if (System.Text.RegularExpressions.Regex.IsMatch(doc.Paragraphs[i].Range.Text, curTextPattern))
                                                            {
                                                                textHasBeenFound = true;
                                                            }
                                                        }
                                                    }

                                                    if (textHasBeenFound)
                                                    {
                                                        result.foundFilesCount++;
                                                        FoundObjectInfo newFoundObjectInfo = new FoundObjectInfo();
                                                        newFoundObjectInfo.Name = curFileName;
                                                        newFoundObjectInfo.Size = new FileInfo(curFileName).Length;
                                                        newFoundObjectInfo.Type = FoundObjectType.File;
                                                        result.foundObjectsList.Add(newFoundObjectInfo);
                                                        if (FileHasBeenFound != null)
                                                        {
                                                            FileHasBeenFound(newFoundObjectInfo);
                                                        }
                                                    }
                                                }

                                                if (doc.Paragraphs.Count == 1)
                                                {
                                                    foreach (string curTextPattern in textPatterns)
                                                    {
                                                        if (System.Text.RegularExpressions.Regex.IsMatch(doc.Paragraphs[1].Range.Text, curTextPattern))
                                                        {
                                                            textHasBeenFound = true;
                                                        }
                                                    }

                                                    if (textHasBeenFound)
                                                    {
                                                        result.foundFilesCount++;
                                                        FoundObjectInfo newFoundObjectInfo = new FoundObjectInfo();
                                                        newFoundObjectInfo.Name = curFileName;
                                                        newFoundObjectInfo.Size = new FileInfo(curFileName).Length;
                                                        newFoundObjectInfo.Type = FoundObjectType.File;
                                                        result.foundObjectsList.Add(newFoundObjectInfo);
                                                        if (FileHasBeenFound != null)
                                                        {
                                                            FileHasBeenFound(newFoundObjectInfo);
                                                        }
                                                    }
                                                }

                                                //документ и приложение закрываются
                                                doc.Close(ref nullObject, ref nullObject, ref nullObject);
                                                app.Quit(ref nullObject, ref nullObject, ref nullObject);

                                                //на случай, если приложение winword так и не закрылось (а такое случается сплошь и рядом)
                                                //выполняется мой собственный метод, который закрывает вообще все процессы winword, которые были 
                                                //созданы данной программой
                                                KillWinwordProcs();
                                            }
                                        }
                                        catch
                                        {

                                        }
                                        finally
                                        {
                                            //на случай, если приложение winword так и не закрылось (а такое случается сплошь и рядом)
                                            //выполняется мой собственный метод, который закрывает вообще все процессы winword, которые были 
                                            //созданы данной программой
                                            KillWinwordProcs();
                                        }
                                    }
                                }
                            }
                        }
                        foreach (string curDirName in dirsList)
                        {
                            FindFiles(curDirName, masks, textToFind, true);
                        }
                    }
                }
                catch
                {
                    result.browsedFilesCount++;
                    result.unreachableObjectsList.Add(searchingPath);
                }
            }
        }

        //поиск осуществляют методы FindFiles. Данные методы необходимы лишь для обнуления значений переменной result.
        public DirInfo GetDirInfo(string dirPath)
        {
            dirInfo.Clear();
            FindFilesWOEvents(dirPath);
            return dirInfo;
        }

        public SearchResults GetFiles(string path, bool searchInSubDirs)
        {
            result.Clear();
            FindFiles(path, searchInSubDirs);
            return result;
        }

        public SearchResults GetFiles(string path, string[] filesMasks, bool searchInSubDirs)
        {
            result.Clear();
            FindFiles(path, filesMasks, searchInSubDirs);
            return result;
        }

        public SearchResults GetFiles(string path, string[] filesMasks, string[] searchingText, bool searchInSubDirs)
        {
            result.Clear();
            FindFiles(path, filesMasks, searchingText, searchInSubDirs);
            return result;
        }

        //конструкторы

        public FilesSearcher()
        {
            result = new SearchResults();
            result.foundObjectsList = new List<FoundObjectInfo>();
            result.unreachableObjectsList = new List<string>();
        }
    }
}
