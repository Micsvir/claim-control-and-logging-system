using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using UtilSubSys;
using Microsoft.Office.Interop.Word;

namespace ControllerModule
{
    public partial class ExportForm : Form
    {
        public ExportForm()
        {
            InitializeComponent();
            GetColumns(ReportForm.reportColumns);

            try
            {
                LoadSettings();
            }
            catch
            {

            }
        }

        //Если процедура экспорта была выполнена успешно, переменная принимает значение true,
        //иначе false
        bool exportDoneWell = false;

        //В переменную загружаются строки HTML-кода заготовки отчета
        List<string> HTMLPattern = new List<string>();

        //Заполнение ReportColumnsSet именами столбцов dgvReport формы ReportForm.
        //т.к. из этой формы нельзя обратиться к конкретному экземпляру класса ReportForm, нельзя обратиться
        //и к объекту dgvReport напрямую. Поэтому в классе ReportForm предусмотрена переменная типа DataTable
        //с модификаторами public static, через которую данные могут быть получены в этой форме
        private void GetColumns(DataTable columnsSet)
        {
            for (int row = 0; row < columnsSet.Rows.Count; row++)
            {
                ReportColumnSet.Items.Add(columnsSet.Rows[row][0].ToString());
            }
        }

        private void ReadHTMLPattern(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            HTMLPattern.Clear();
            while (!sr.EndOfStream)
            {
                HTMLPattern.Add(sr.ReadLine());
            }
            sr.Close();
            fs.Close();
        }

        //метод генерирует строку, содержащую информацию о фамилии имени и отчестве текущего пользователя
        //в формате Фамилия И.О. с учетом того, что Имя и(или) Отчество пользователя могут быть не заданы (отсутствовать)
        private string GenerateUserName(DataTable userData)
        {
            string firstName = "";
            string lastName = "";
            string fathersName = "";
            if (userData.Rows[0]["PersFirstName"].ToString().Length > 0)
            {
                firstName = userData.Rows[0]["PersFirstName"].ToString().Substring(0, 1) + ".";
            }
            if (userData.Rows[0]["PersLastName"].ToString().Length > 0)
            {
                lastName = userData.Rows[0]["PersLastName"].ToString();
            }
            if (userData.Rows[0]["PersPatronymic"].ToString().Length > 0)
            {
                fathersName = userData.Rows[0]["PersPatronymic"].ToString().Substring(0, 1) + ".";
            }

            if (firstName.Length > 0 && lastName.Length > 0 && fathersName.Length > 0)
            {
                return lastName + " " + firstName + fathersName;
            }
            else
            {
                if (firstName.Length > 0 && lastName.Length > 0)
                {
                    return lastName + " " + firstName;
                }
                else
                {
                    if (lastName.Length > 0)
                    {
                        return lastName;
                    }
                    else
                    {
                        return "";
                    }
                }
            }
        }

        //метод генерирует строку, содержащую информацию о том, за какой период времени был запрошен отчет.
        //в качестве входного параметра используется текст запроса, по которому был получен отчет.
        private string GenerateDateRangeString(string conditionString)
        {
            //осуществляется попытка найти в ней слово "Дата" и в зависимости от их количества и операций отношений определить
            //интересующий пользователя период
            List<int> datesPositions = new List<int>();
            int index = 0;
            while (index < ReportForm.condition.Length - 4)
            {
                if (ReportForm.condition.Substring(index, 4) == "Дата")
                {
                    datesPositions.Add(index);
                }
                index++;
            }

            //если количество элементов в datesPositions не равно 0, следовательно, в строке condition были обнаружены даты
            if (datesPositions.Count > 0)
            {
                //переменные для сравнения и других манипуляций с датами
                int year = 0, month = 0, day = 0;

                //формирование строки дат со знаками равенства (Дата = 2018.01.01)
                string datesString = "";
                List<int> moreLessDatesPositions = new List<int>();
                foreach (int curDatePosition in datesPositions)
                {
                    if (conditionString[curDatePosition + 6] == '=')
                    {
                        datesString += ChangeDateFormat(conditionString.Substring(curDatePosition + 9, 10)) + ", ";
                    }

                    //поиск дат со знаками больше и меньше и сохранение их позиций
                    if ((conditionString[curDatePosition + 6] == '>') || (conditionString[curDatePosition + 6] == '<'))
                    {
                        moreLessDatesPositions.Add(curDatePosition);
                    }
                }

                //если дата со знаком больше/меньше одна
                if (moreLessDatesPositions.Count == 1)
                {
                    year = Convert.ToInt32(conditionString.Substring(moreLessDatesPositions[0] + 9, 4));
                    month = Convert.ToInt32(conditionString.Substring(moreLessDatesPositions[0] + 14, 2));
                    day = Convert.ToInt32(conditionString.Substring(moreLessDatesPositions[0] + 17, 2));

                    DateTime curDate = new DateTime(year, month, day);

                    if (conditionString[moreLessDatesPositions[0] + 6].ToString() == ">")
                    {
                        curDate = curDate.AddDays(1);
                        datesString += Configuration.DateToString(curDate, "DD.MM.YYYY") + " - ∞, ";
                    }
                    else
                    {
                        curDate = curDate.AddDays(-1);
                        datesString += "∞ - " + Configuration.DateToString(curDate, "DD.MM.YYYY") + ", ";
                    }
                }
                else
                {
                    //если дат со знаком больше/меньше две
                    if (moreLessDatesPositions.Count == 2)
                    {
                        //если меньшая дата со знаком > и большая дата со знаком <, тогда запрашиваемый диапазон находится между этими датами.
                        //Если меньшая со знаком < и большая со знаком >, тогда диапазон выглядит следующим образом: ∞ - 2018.06.10, 2018.06.20 - ∞.
                        //Для формирования соответствующей строки datesString, необходимо выполнить процедуру сравнения дат

                        year = Convert.ToInt32(conditionString.Substring(moreLessDatesPositions[0] + 9, 4));
                        month = Convert.ToInt32(conditionString.Substring(moreLessDatesPositions[0] + 14, 2));
                        day = Convert.ToInt32(conditionString.Substring(moreLessDatesPositions[0] + 17, 2));

                        DateTime firstDate = new DateTime(year, month, day);

                        year = Convert.ToInt32(conditionString.Substring(moreLessDatesPositions[1] + 9, 4));
                        month = Convert.ToInt32(conditionString.Substring(moreLessDatesPositions[1] + 14, 2));
                        day = Convert.ToInt32(conditionString.Substring(moreLessDatesPositions[1] + 17, 2));

                        DateTime secondDate = new DateTime(year, month, day);

                        if ((firstDate < secondDate) && (conditionString.Substring(moreLessDatesPositions[0] + 6, 1).ToString() == ">") && (conditionString.Substring(moreLessDatesPositions[1] + 6, 1).ToString() == "<"))
                        {
                            firstDate = firstDate.AddDays(1);
                            secondDate = secondDate.AddDays(-1);
                            datesString += Configuration.DateToString(firstDate, "DD.MM.YYYY") + " - " + Configuration.DateToString(secondDate, "DD.MM.YYYY") + ", ";
                        }
                        else
                        {
                            if ((firstDate > secondDate) && (conditionString.Substring(moreLessDatesPositions[0] + 6, 1).ToString() == "<") && (conditionString.Substring(moreLessDatesPositions[1] + 6, 1).ToString() == ">"))
                            {
                                firstDate = firstDate.AddDays(-1);
                                secondDate = secondDate.AddDays(1);
                                datesString += Configuration.DateToString(secondDate, "DD.MM.YYYY") + " - " + Configuration.DateToString(firstDate, "DD.MM.YYYY") + ", ";
                            }
                            else
                            {
                                if ((firstDate > secondDate) && (conditionString.Substring(moreLessDatesPositions[0] + 6, 1).ToString() == ">") && (conditionString.Substring(moreLessDatesPositions[1] + 6, 1).ToString() == "<"))
                                {
                                    firstDate = firstDate.AddDays(1);
                                    secondDate = secondDate.AddDays(-1);
                                    datesString += "∞ - " + Configuration.DateToString(secondDate, "DD.MM.YYYY") + ", " + Configuration.DateToString(firstDate, "DD.MM.YYYY") + " - ∞, ";
                                }
                                else
                                {
                                    if ((firstDate < secondDate) && (conditionString.Substring(moreLessDatesPositions[0] + 6, 1).ToString() == "<") && (conditionString.Substring(moreLessDatesPositions[1] + 6, 1).ToString() == ">"))
                                    {
                                        firstDate = firstDate.AddDays(-1);
                                        secondDate = secondDate.AddDays(1);
                                        datesString += "∞ - " + Configuration.DateToString(firstDate, "DD.MM.YYYY") + ", " + Configuration.DateToString(secondDate, "DD.MM.YYYY") + " - ∞, ";
                                    }
                                    else
                                    {
                                        datesString += Configuration.DateToString(firstDate, "DD.MM.YYYY") + ", " + Configuration.DateToString(secondDate, "DD.MM.YYYY") + ", ";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (int curMoreLessPos in moreLessDatesPositions)
                        {
                            datesString += ChangeDateFormat(conditionString.Substring(curMoreLessPos + 9, 10)) + ", ";
                        }
                    }
                }
                datesString = datesString.Substring(0, datesString.Length - 2);
                return datesString;
            }
            return "";
        }

        //метод проверяет, чтобы имя файла не было непустым и расширение было ".html" или ".doc"
        private string CheckedFileName(string fileName, string fileExtention)
        {
            if (fileName.Length == 0)
            {
                fileName = "report." + fileExtention;
            }

            if (fileName[fileName.Length - 1] == '\\')
            {
                fileName += "report." + fileExtention;
            }

            if (fileName.IndexOf('.') != -1)
            {
                if (fileName.Substring(fileName.LastIndexOf('.')) != ("." + fileExtention))
                {
                    fileName += "." + fileExtention;
                }
            }

            if (fileName.Length > 0 && fileName.IndexOf('.') == -1)
            {
                fileName += "." + fileExtention;
            }

            if (fileName.IndexOf("." + fileExtention) == 0)
            {
                fileName = "report." + fileExtention;
            }
            else
            {
                if (fileName.IndexOf(".") != -1)
                {
                    if (fileName[fileName.LastIndexOf(".") - 1].ToString() == "\\")
                    {
                        fileName = fileName.Substring(0, fileName.LastIndexOf(".") - 1) + "report." + fileExtention;
                    }
                }
            }
            return fileName;
        }

        //изменение формата времени
        private string ChangeDateFormat(string dateToChange)
        {
            string year = dateToChange.Substring(0, 4);
            string month = dateToChange.Substring(5, 2);
            string day = dateToChange.Substring(8, 2);
            return day + "." + month + "." + year;
        }

        //метод генерирует HTML файл, содержащий информацию о запрошенном пользователем отчете
        private void ExportToHTML(string outputFileName, DataTable reportData, DataTable reportClaimsCountData)
        {
            int errorLevel = 0;
            FileStream fs = null;
            StreamWriter sw = null;
            exportDoneWell = false;

            //выполняется проверка, что выбран хотя бы один вариант отображения содержимого отчета
            if (chbDetails.Checked || chbMainInfo.Checked)
            {
                //проверка расширения файла
                outputFileName = CheckedFileName(outputFileName, "html");

                try
                {
                    fs = new FileStream(outputFileName, FileMode.Create);
                    sw = new StreamWriter(fs, Encoding.UTF8);
                    errorLevel = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    errorLevel = 1;
                    exportDoneWell = false;
                }

                //если предыдущие процедуры были выполнены без ошибок,
                //можно начинать записывать необходимую информацию в файл
                if (errorLevel == 0)
                {
                    try
                    {
                        //Запись в файл служебной информации об HTML файле
                        sw.WriteLine("<!DOCTYPE html>");
                        sw.WriteLine("<HTML>");
                        sw.WriteLine("<head>");
                        sw.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
                        sw.WriteLine("<title>Отчет</title>");
                        sw.WriteLine("<style type=\"text/css\">");
                        sw.WriteLine(" TH {text-align: center; border: 1px solid;}");
                        sw.WriteLine(" TD {text-align: left; border: 1px solid; font-size: 10pt}");
                        sw.WriteLine("TABLE {width: 100%; border: 1px solid;}");
                        sw.WriteLine("</style>");
                        sw.WriteLine("</head>");
                        sw.WriteLine("<BODY>");

                        //формирование заголовка
                        sw.WriteLine("<H2 align=\"Center\">ОТЧЕТ</H2>");


                        sw.WriteLine("<H2 align=\"left\">Контролер: " + GenerateUserName(MainForm.currentUserData) + "</H2>");

                        //если строка условия непуста
                        if (ReportForm.condition.Length > 0)
                        {
                            //генерируется строка, описывающая диапазон выбранных пользователем дат
                            string datesString = GenerateDateRangeString(ReportForm.condition);
                            //если она непуста
                            if (datesString != "")
                            {
                                //в HTML файл добавляется соответствующая информация
                                sw.WriteLine("<H2> Дата: " + GenerateDateRangeString(ReportForm.condition) + "</H2>");
                            }
                        }

                        //если выбран детализированный вариант отображения отчета, формируется соответствующая таблица
                        if (chbDetails.Checked && ReportColumnSet.CheckedItems.Count > 0)
                        {
                            //формирование таблицы
                            sw.WriteLine("<TABLE cellspacing=\"0\">");

                            //формирование заголовков стобцов
                            sw.WriteLine("<TR>");

                            //для каждого выбранного элемента ReportColumnsSet
                            for (int rcsIndex = 0; rcsIndex < ReportColumnSet.CheckedItems.Count; rcsIndex++)
                            {
                                //формируется столбец в таблице HTML файла
                                sw.WriteLine("<TH>" + ReportColumnSet.CheckedItems[rcsIndex].ToString() + "</TH>");
                            }
                            //конец формирования заголовков столбцов
                            sw.WriteLine("</TR>");

                            //наполнение таблицы данными отчета
                            //для каждой строки отчета
                            for (int reportRow = 0; reportRow < reportData.Rows.Count; reportRow++)
                            {
                                //формируется новый элемент TR
                                sw.WriteLine("<TR>");

                                //и каждый столбец отчета
                                for (int reportCol = 0; reportCol < reportData.Columns.Count; reportCol++)
                                {
                                    //сравнивается с каждым выбранным элементом ReportColumnsSet
                                    for (int chckdIndex = 0; chckdIndex < ReportColumnSet.CheckedItems.Count; chckdIndex++)
                                    {
                                        //и если названия столбца отчета и названия выбранного элемента ReportColumnsSet совпадают
                                        if (reportData.Columns[reportCol].ColumnName.ToString() == ReportColumnSet.CheckedItems[chckdIndex].ToString())
                                        {
                                            //проверка, является ли данный столбец столбцом, который содержит информацию о временной задержке.
                                            //Если да, необходимо выполнить процедуру Configuration.GetTimeStringFromSeconds
                                            if (ReportColumnSet.CheckedItems[chckdIndex].ToString() == "Прошло времени с поступления заявки" ||
                                                ReportColumnSet.CheckedItems[chckdIndex].ToString() == "Прошло времени с назначения группы" ||
                                                ReportColumnSet.CheckedItems[chckdIndex].ToString() == "Прошло времени с назначения исполнителя" ||
                                                ReportColumnSet.CheckedItems[chckdIndex].ToString() == "Продолжительность выполнения заявки")
                                            {
                                                if (reportData.Rows[reportRow][reportCol] != DBNull.Value)
                                                {
                                                    sw.WriteLine("<TD>" + Configuration.GetTimeStringFromSeconds(Convert.ToInt32(reportData.Rows[reportRow][reportCol])) + "</TD>");
                                                }
                                                else
                                                {
                                                    sw.WriteLine("<TD>&nbsp;</TD>");
                                                }
                                            }
                                            else
                                            {
                                                //иначе выполняется проверка, является ли данный столбец столбцом, который содержит информацию о дате.
                                                //Если да, выполняется процедура изменения формата дата с гггг.мм.дд на дд.мм.гггг
                                                if (ReportColumnSet.CheckedItems[chckdIndex].ToString().IndexOf("Дата") != -1)
                                                {
                                                    if (reportData.Rows[reportRow][reportCol].ToString().Length == 10)
                                                    {
                                                        sw.WriteLine("<TD>" + ChangeDateFormat(reportData.Rows[reportRow][reportCol].ToString()) + "</TD>");
                                                    }
                                                    else
                                                    {
                                                        if (reportData.Rows[reportRow][reportCol] != DBNull.Value)
                                                        {
                                                            sw.WriteLine("<TD>" + reportData.Rows[reportRow][reportCol].ToString() + "</TD>");
                                                        }
                                                        else
                                                        {
                                                            sw.WriteLine("<TD>&nbsp;</TD>");
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (reportData.Rows[reportRow][reportCol] != DBNull.Value)
                                                    {
                                                        sw.WriteLine("<TD>" + reportData.Rows[reportRow][reportCol].ToString() + "</TD>");
                                                    }
                                                    else
                                                    {
                                                        sw.WriteLine("<TD>&nbsp;</TD>");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                sw.WriteLine("</TR>");
                            }
                            sw.WriteLine("</TABLE>");
                            //конец формирования таблицы
                        }

                        //это вместо else. Использование else приводит к тому, что при выборе только информации о количестве заявок
                        //постоянно появляется сообщение о том, что не выбран ни один столбец.
                        if (chbDetails.Checked && ReportColumnSet.CheckedItems.Count == 0)
                        {
                            MessageBox.Show("Не выбрано ни одного столбца для экспорта");
                            exportDoneWell = false;
                        }

                        //если выбран вариант отчета с информацией о кол-ве заявок, формируется соответствующая таблица
                        if (chbMainInfo.Checked)
                        {
                            sw.WriteLine("&nbsp");
                            //формирование таблицы
                            sw.WriteLine("<TABLE cellspacing=\"0\">");

                            //формирование заголовков стобцов
                            sw.WriteLine("<TR>");

                            sw.WriteLine("<TH>Категория заявок</TH>");
                            sw.WriteLine("<TH>Описание категории</TH>");
                            sw.WriteLine("<TH>Количество заявок</TH>");

                            //конец формирования заголовков столбцов
                            sw.WriteLine("</TR>");

                            //По какой-то причине, в случае отсутствия в запросе на получение этой информации (т.е. таблицы reportClaimsCount)
                            //ключевого слова WHERE с последующим указанием некоторого условия, запрос возвращает не 3, а 6 столбцов, нужными 
                            //из которых являются последние три (индексы, соответственно, 3, 4, 5).
                            //поэтому в случае, если столбцов 6, нужно взять последние 3.
                            if (reportClaimsCountData.Columns.Count > 3)
                            {

                                //наполнение таблицы данными отчета
                                //для каждой строки отчета
                                for (int rowIndex = 0; rowIndex < reportClaimsCountData.Rows.Count; rowIndex++)
                                {
                                    //формируется новый элемент TR
                                    sw.WriteLine("<TR>");

                                    //для каждого столбца создается ячейка
                                    for (int colIndex = 3; colIndex < reportClaimsCountData.Columns.Count; colIndex++)
                                    {
                                        sw.WriteLine("<TD>" + reportClaimsCountData.Rows[rowIndex][colIndex] + "</TD>");
                                    }

                                    //Закрывается элемент TR
                                    sw.WriteLine("</TR>");
                                }
                                sw.WriteLine("</TABLE>");
                            }
                            else
                            {
                                //наполнение таблицы данными отчета
                                //для каждой строки отчета
                                for (int rowIndex = 0; rowIndex < reportClaimsCountData.Rows.Count; rowIndex++)
                                {
                                    //формируется новый элемент TR
                                    sw.WriteLine("<TR>");

                                    //для каждого столбца создается ячейка
                                    for (int colIndex = 0; colIndex < reportClaimsCountData.Columns.Count; colIndex++)
                                    {
                                        sw.WriteLine("<TD>" + reportClaimsCountData.Rows[rowIndex][colIndex] + "</TD>");
                                    }

                                    //Закрывается элемент TR
                                    sw.WriteLine("</TR>");
                                }
                                sw.WriteLine("</TABLE>");
                            }
                        }

                        //число, подпись
                        sw.WriteLine("&nbsp");
                        sw.WriteLine("<TABLE border=\"0\" cellspacing=\"0\">");
                        sw.WriteLine("<TR>");
                        sw.WriteLine("<TD align=\"Left\" border=\"0\">" + "<H3>Число:</H3>" + "</TD>");
                        sw.WriteLine("<TD align=\"Right\" border=\"0\">" + "<H3>Подпись:</H3>" + "</TD>");
                        sw.WriteLine("</TR>");
                        sw.WriteLine("</TABLE>");
                        sw.WriteLine("</BODY>");
                        sw.WriteLine("</HTML>");

                        sw.Close();
                        fs.Close();

                        exportDoneWell = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        sw.Close();
                        fs.Close();

                        exportDoneWell = false;
                    }
                }
            }
            else
            {
                MessageBox.Show("Не выбрано ни одного варианта отображения отчета");
            }
        }

        //метод генерирует DOC файл, содержащий информацию о запрошенном пользователем отчете
        private void ExportToDOC(string outputFileName, DataTable reportData, DataTable reportClaimsCountData)
        {
            exportDoneWell = false;

            //выполняется проверка, что выбран хотя бы один вариант отображения содержимого
            if (chbMainInfo.Checked || chbDetails.Checked)
            {
                //проверка расширения файла
                outputFileName = CheckedFileName(outputFileName, "doc");

                try
                {
                    //Создаём новый Word.Application
                    Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();

                    //Загружаем документ
                    Microsoft.Office.Interop.Word.Document wordDoc = null;

                    object fileName = outputFileName;
                    object falseValue = false;
                    object trueValue = true;
                    object missing = Type.Missing;
                    object rangeStartPosition = 0;
                    object rangeEndPosition = 0;
                    object lineBreak = Microsoft.Office.Interop.Word.WdBreakType.wdLineBreak;

                    wordDoc = wordApp.Documents.Add(ref missing, ref missing, ref missing, ref falseValue);

                    Microsoft.Office.Interop.Word.Paragraph newParagraph = wordDoc.Paragraphs.Add(ref missing);
                    newParagraph.Range.Text = "ОТЧЕТ";
                    newParagraph.Range.Font.Name = "Times New Roman";
                    newParagraph.Range.Font.Size = 20;
                    newParagraph.Range.Font.Bold = 1;
                    newParagraph.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    newParagraph.Range.InsertParagraphAfter();

                    newParagraph = wordDoc.Paragraphs.Add(ref missing);
                    newParagraph.Range.Text = "Контролер: " + GenerateUserName(MainForm.currentUserData);
                    newParagraph.Range.Font.Size = 16;
                    newParagraph.Range.Font.Bold = 0;
                    newParagraph.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    newParagraph.Range.InsertParagraphAfter();

                    //если строка условия непуста
                    if (ReportForm.condition.Length > 0)
                    {
                        //генерируется строка, описывающая диапазон выбранных пользователем дат
                        string datesString = GenerateDateRangeString(ReportForm.condition);
                        //если она непуста
                        if (datesString != "")
                        {
                            //в HTML файл добавляется соответствующая информация
                            newParagraph = wordDoc.Paragraphs.Add(ref missing);
                            newParagraph.Range.Text = "Дата: " + GenerateDateRangeString(ReportForm.condition);
                            newParagraph.Range.InsertParagraphAfter();
                        }
                    }

                    //если выбран детализированный вариант отчета, формируется соответствующая таблица
                    //выполняется проверка, что есть хотя бы 1 выбранный столбец для экспорта при условии, что выбран вариант детализированного представления отчета
                    if (ReportColumnSet.CheckedItems.Count > 0 && chbDetails.Checked)
                    {
                        //Добавление таблицы с отчетом
                        newParagraph = wordDoc.Paragraphs.Add(ref missing);
                        newParagraph.Range.Font.Size = 8;
                        Table reportTable = newParagraph.Range.Tables.Add(newParagraph.Range, reportData.Rows.Count + 1, ReportColumnSet.CheckedItems.Count, ref missing, ref missing);
                        reportTable.AllowAutoFit = true;
                        reportTable.Range.Font.Size = 8;
                        reportTable.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
                        reportTable.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
                        reportTable.Borders.OutsideLineWidth = WdLineWidth.wdLineWidth100pt;
                        reportTable.Borders.InsideLineWidth = WdLineWidth.wdLineWidth100pt;

                        //формирование заголовков таблицы
                        for (int column = 0; column < ReportColumnSet.CheckedItems.Count; column++)
                        {
                            reportTable.Rows[1].Cells[column + 1].Range.Text = ReportColumnSet.CheckedItems[column].ToString();
                        }

                        //для каждой строки отчета
                        for (int reportRow = 0; reportRow < reportData.Rows.Count; reportRow++)
                        {
                            //и каждый столбец отчета
                            for (int reportCol = 0; reportCol < reportData.Columns.Count; reportCol++)
                            {
                                //сравнивается с каждым выбранным элементом ReportColumnsSet
                                for (int chckdIndex = 0; chckdIndex < ReportColumnSet.CheckedItems.Count; chckdIndex++)
                                {
                                    //и если названия столбца отчета и названия выбранного элемента ReportColumnsSet совпадают
                                    if (reportData.Columns[reportCol].ColumnName.ToString() == ReportColumnSet.CheckedItems[chckdIndex].ToString())
                                    {
                                        if (reportData.Rows[reportRow][reportCol] == DBNull.Value)
                                        {
                                            reportTable.Rows[reportRow + 2].Cells[chckdIndex + 1].Range.Text = "";
                                        }
                                        else
                                        {
                                            //проверка, является ли данный столбец столбцом, который содержит информацию о временной задержке.
                                            //Если да, необходимо выполнить процедуру Configuration.GetTimeStringFromSeconds
                                            if (ReportColumnSet.CheckedItems[chckdIndex].ToString() == "Прошло времени с поступления заявки" ||
                                                ReportColumnSet.CheckedItems[chckdIndex].ToString() == "Прошло времени с назначения группы" ||
                                                ReportColumnSet.CheckedItems[chckdIndex].ToString() == "Прошло времени с назначения исполнителя" ||
                                                ReportColumnSet.CheckedItems[chckdIndex].ToString() == "Продолжительность выполнения заявки")
                                            {
                                                reportTable.Rows[reportRow + 2].Cells[chckdIndex + 1].Range.Text = Configuration.GetTimeStringFromSeconds(Convert.ToInt32(reportData.Rows[reportRow][reportCol]));
                                            }
                                            else
                                            {
                                                if (ReportColumnSet.CheckedItems[chckdIndex].ToString().IndexOf("Дата") != -1)
                                                {
                                                    if (reportData.Rows[reportRow][reportCol].ToString().Length == 10)
                                                    {
                                                        reportTable.Rows[reportRow + 2].Cells[chckdIndex + 1].Range.Text = ChangeDateFormat(reportData.Rows[reportRow][reportCol].ToString());
                                                    }
                                                    else
                                                    {
                                                        reportTable.Rows[reportRow + 2].Cells[chckdIndex + 1].Range.Text = reportData.Rows[reportRow][reportCol].ToString();
                                                    }
                                                }
                                                else
                                                {
                                                    reportTable.Rows[reportRow + 2].Cells[chckdIndex + 1].Range.Text = reportData.Rows[reportRow][reportCol].ToString();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        newParagraph.Range.InsertParagraphAfter();
                    }

                    //это вместо else. Использование else приводит к тому, что при выборе только информации о количестве заявок
                    //постоянно появляется сообщение о том, что не выбран ни один столбец.
                    if (chbDetails.Checked && ReportColumnSet.CheckedItems.Count == 0)
                    {
                        MessageBox.Show("Не выбрано ни одного столбца для экспорта");
                        exportDoneWell = false;
                    } 

                    //если выбран вариант отчета, содержащий только основную информацию о количестве выполенных заявок, формируется соответствующая таблица
                    if (chbMainInfo.Checked)
                    {
                        //Добавление таблицы с отчетом
                        newParagraph = wordDoc.Paragraphs.Add(ref missing);
                        newParagraph.Range.Font.Size = 8;
                        Table reportTable = newParagraph.Range.Tables.Add(newParagraph.Range, reportClaimsCountData.Rows.Count + 1, 3, ref missing, ref missing);
                        reportTable.AllowAutoFit = true;
                        reportTable.Range.Font.Size = 8;
                        reportTable.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
                        reportTable.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
                        reportTable.Borders.OutsideLineWidth = WdLineWidth.wdLineWidth100pt;
                        reportTable.Borders.InsideLineWidth = WdLineWidth.wdLineWidth100pt;

                        //Формирование заголовков таблицы
                        reportTable.Rows[1].Cells[1].Range.Text = "Категория заявок";
                        reportTable.Rows[1].Cells[2].Range.Text = "Описание категории";
                        reportTable.Rows[1].Cells[3].Range.Text = "Количество заявок";

                        //По какой-то причине, в случае отсутствия в запросе на получение этой информации (т.е. таблицы reportClaimsCount)
                        //ключевого слова WHERE с последующим указанием некоторого условия, запрос возвращает не 3, а 6 столбцов, нужными 
                        //из которых являются последние три (индексы, соответственно, 3, 4, 5).
                        //поэтому в случае, если столбцов 6, нужно взять последние 3.
                        if (reportClaimsCountData.Columns.Count > 3)
                        {
                            //Наполнение таблицы                   
                            for (int reportRow = 0; reportRow < reportClaimsCountData.Rows.Count; reportRow++)
                            {
                                //и каждый столбец отчета
                                for (int reportCol = 3; reportCol < reportClaimsCountData.Columns.Count; reportCol++)
                                {
                                    reportTable.Rows[reportRow + 2].Cells[reportCol - 2].Range.Text = reportClaimsCountData.Rows[reportRow][reportCol].ToString();
                                }
                            }
                        }
                        else
                        {
                            //Наполнение таблицы                   
                            for (int reportRow = 0; reportRow < reportClaimsCountData.Rows.Count; reportRow++)
                            {
                                //и каждый столбец отчета
                                for (int reportCol = 0; reportCol < reportClaimsCountData.Columns.Count; reportCol++)
                                {
                                    reportTable.Rows[reportRow + 2].Cells[reportCol + 1].Range.Text = reportClaimsCountData.Rows[reportRow][reportCol].ToString();
                                }
                            }
                        }
                    }

                    //сохранение и закрытие документа
                    wordDoc.SaveAs(ref fileName, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing);
                    ((Microsoft.Office.Interop.Word._Document)wordDoc).Close(ref trueValue, ref missing, ref missing);
                    ((Microsoft.Office.Interop.Word._Application)wordApp).Quit(ref missing, ref missing, ref missing);

                    exportDoneWell = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.ToString());
                    exportDoneWell = false;
                }
            }
            else
            {
                MessageBox.Show("Не выбрано ни одного варианта отображения отчета");
                exportDoneWell = false;
            }
        }

        //метод сохраняет выставленные галочки, указанные пути и Т.д.
        private void SaveSettings()
        {
            FileStream fs = new FileStream("exportSettings", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            if (chbDetails.Checked)
            {
                sw.WriteLine("Details=true");
            }
            else
            {
                sw.WriteLine("Details=false");
            }

            if (chbMainInfo.Checked)
            {
                sw.WriteLine("MainInfo=true");
            }
            else
            {
                sw.WriteLine("MainInfo=false");
            }

            if (rbDOC.Checked)
            {
                sw.WriteLine("Doc=true");
            }
            else
            {
                sw.WriteLine("Doc=false");
            }

            sw.WriteLine("FileName=" + tbFileName.Text);

            for (int i = 0; i < ReportColumnSet.CheckedItems.Count; i++)
            {
                sw.WriteLine(ReportColumnSet.CheckedItems[i]);
            }
            sw.Close();
            fs.Close();
        }

        //вспомогательная структура для выставления галочек у списка столбцов, перечисленных в ReportColumnSet
        struct reportColumn
        {
            public string name;
            public bool isChecked;
        }

        //метод загружает настройки из файла и соответствующим образом выставляет галочки и прочее на форме
        private void LoadSettings()
        {
            FileStream fs = new FileStream("exportSettings", FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            //хранит список столбцов, перечисленных в ReportColumnsList
            List<reportColumn> columns = new List<reportColumn>();

            //Заполнение этого списка
            for (int i = 0; i < ReportColumnSet.Items.Count; i++)
            {
                reportColumn newReportColumn = new reportColumn();
                newReportColumn.name = ReportColumnSet.Items[i].ToString();
                newReportColumn.isChecked = false;
                columns.Add(newReportColumn);
            }

            //обнуление списка столбцов 
            ReportColumnSet.Items.Clear();

            string curSetting = "";
            int readingsCount = 0;
            while (!sr.EndOfStream)
            {
                curSetting = sr.ReadLine();
                if (readingsCount < 4)
                {
                    if (curSetting.IndexOf("Details") != -1)
                    {
                        if (curSetting.IndexOf("true") != -1)
                        {
                            chbDetails.Checked = true;
                        }
                        else
                        {
                            chbDetails.Checked = false;
                        }
                    }

                    if (curSetting.IndexOf("MainInfo") != -1)
                    {
                        if (curSetting.IndexOf("true") != -1)
                        {
                            chbMainInfo.Checked = true;
                        }
                        else
                        {
                            chbMainInfo.Checked = false;
                        }
                    }

                    if (curSetting.IndexOf("Doc") != -1)
                    {
                        if (curSetting.IndexOf("true") != -1)
                        {
                            rbDOC.Checked = true;
                        }
                        else
                        {
                            rbHTML.Checked = true;
                        }
                    }

                    if (curSetting.IndexOf("FileName") != -1)
                    {
                        tbFileName.Text = curSetting.Split('=')[1];
                    }
                }
                else
                {
                    for (int i = 0; i < columns.Count; i++)
                    {
                        if (curSetting == columns[i].name)
                        {
                            reportColumn newReportColumn = new reportColumn();
                            newReportColumn.name = curSetting;
                            newReportColumn.isChecked = true;

                            columns[i] = newReportColumn;
                        }
                    }
                }
                readingsCount++;
            }

            //Заполнение ReportColumnSet столбцами из columns
            foreach (reportColumn curReportColumn in columns)
            {
                ReportColumnSet.Items.Add(curReportColumn.name, curReportColumn.isChecked);
            }

            sr.Close();
            fs.Close();
        }

        private void rbDOC_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDOC.Checked)
            {
                chbUsePattern.Enabled = true;
            }
            else
            {
                chbUsePattern.Checked = false;
                chbUsePattern.Enabled = false;
            }
        }

        private void chbUsePattern_CheckedChanged(object sender, EventArgs e)
        {
            if (chbUsePattern.Checked)
            {
                tbPatternFileName.Enabled = true;
                bPatternSelect.Visible = true;
            }
            else
            {
                tbPatternFileName.Text = "";
                tbPatternFileName.Enabled = false;
                bPatternSelect.Visible = false;
            }
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            if (rbHTML.Checked)
            {
                ExportToHTML(tbFileName.Text, ReportForm.report, ReportForm.reportClaimsCount);
            }
            if (rbDOC.Checked)
            {
                ExportToDOC(tbFileName.Text, ReportForm.report, ReportForm.reportClaimsCount);
            }

            if (exportDoneWell)
            {
                MessageBox.Show("Файл отчета успешно создан");

                try
                {
                    SaveSettings();
                }
                catch
                {
                    MessageBox.Show("Сохранить настройки не удалось");
                }

                this.Close();
            }
        }

        private void bPatternSelect_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                tbPatternFileName.Text = "";
                tbPatternFileName.Text = openFileDialog1.FileName;
            }
        }

        private void bFileNameSelect_Click(object sender, EventArgs e)
        {
            if (rbHTML.Checked)
            {
                saveFileDialog1.DefaultExt = "html";
            }

            if (rbDOC.Checked)
            {
                saveFileDialog1.DefaultExt = "doc";
            }

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                tbFileName.Text = "";
                tbFileName.Text = saveFileDialog1.FileName;
            }
        }

    }
}
