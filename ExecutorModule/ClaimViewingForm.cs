using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExecutorModule
{
    public partial class ClaimViewingForm : Form
    {
        public ClaimViewingForm()
        {
            InitializeComponent();
            panel1.Focus();
        }

        public int labelTop = 10;
        public int labelLeft = 10;
        public int maxIndent = 0;
        public int columnIndex = 0;
        public struct TableCellPresentation
        {
            public Label tableCellCaption;
            public TextBox tableCellValue;
            public int controlsColumnIndex;
        }
        public List<TableCellPresentation> controlsList = new List<TableCellPresentation>();

        //добавляет на форму в panel1 контролы Label и TextBox по кол-ву столбцов указанного в параметрах DataGridView
        public void AddControls(DataGridView sourceDGV)
        {
            for (int i = 0; i < sourceDGV.Columns.Count; i++)
            {
                Label newLabel = new Label();
                newLabel.Name = sourceDGV.Columns[i].Name + "Label";
                newLabel.Text = sourceDGV.Columns[i].Name;
                //newLabel.ForeColor = Color.Gray;
                //newLabel.Font = new Font(newLabel.Font, FontStyle.Italic);
                newLabel.AutoSize = true;

                TextBox newTextBox = new TextBox();
                newTextBox.Width = 250;
                newTextBox.ReadOnly = true;
                //newTextBox.BorderStyle = BorderStyle.None;
                newTextBox.ScrollBars = ScrollBars.Vertical;
                newTextBox.Font = new Font(newTextBox.Font.FontFamily, 10);
                newTextBox.Name = sourceDGV.Columns[i].Name + "Value";
                newTextBox.Text = sourceDGV.SelectedRows[0].Cells[i].Value.ToString();

                if (newTextBox.Name.IndexOf("инфо") != -1 || 
                    newTextBox.Name.IndexOf("аявка") != -1 ||
                    newTextBox.Name.IndexOf("действия") != -1)
                {
                    newTextBox.Multiline = true;
                    newTextBox.Height = 80;
                }

                newLabel.Parent = panel1;

                newTextBox.Parent = panel1;

                TableCellPresentation newControlSet = new TableCellPresentation();
                newControlSet.tableCellCaption = newLabel;
                newControlSet.tableCellValue = newTextBox;
                newControlSet.controlsColumnIndex = columnIndex;
                controlsList.Add(newControlSet);
            }
        }

        //размещает контролы на форме таким образом, чтобы они умещались горизонтально
        public void ReplaceControlsHorizontal()
        {
            int horizontalIndent = 0; //общий отступ по горизонтали для всех "столбцов" (что-то типа <tab>)
            int verticalIndent = 0; //общий отступ по вертикали для всех "строк"
            int curRowVerticalIndent = 0; //высчитывающийся для каждой "строки" отступ
            int prevTopValue = 10;
            int prevLeftValue = 10;
            int curReplacingElementNumber = 1;

            //сброс размеров panel1
            panel1.Width = this.Width;
            panel1.Height = this.Height;

            //нахождение максимального значения width у контролов Label и TextBox, чтобы расчитать отступы
            foreach (TableCellPresentation curElement in controlsList)
            {
                if (curElement.tableCellCaption.Width > horizontalIndent || curElement.tableCellValue.Width > horizontalIndent)
                {
                    if (curElement.tableCellCaption.Width > curElement.tableCellValue.Width)
                    {
                        horizontalIndent = curElement.tableCellCaption.Width;
                    }
                    else
                    {
                        horizontalIndent = curElement.tableCellValue.Width;
                    }
                }
            }

            //установка значения horizontalIndent
            horizontalIndent = horizontalIndent + 50;

            //нахождение максимального значения height у контролов Label и TextBox, чтобы расчитать отступы
            foreach (TableCellPresentation curElement in controlsList)
            {
                if (curElement.tableCellValue.Height > verticalIndent)
                {
                    verticalIndent = curElement.tableCellValue.Height;
                }
            }

            //установка значения verticalIndent
            verticalIndent = verticalIndent + 20;

            //каждый элемент размещается на форме таким образом, чтобы обеспечить наполнение формы по горизонтали (с вертикальным скролом)
            foreach (TableCellPresentation curElement in controlsList)
            {
                //если это первый перемещаемый контрол, он добавляется безусловно
                if (curReplacingElementNumber == 1)
                {
                    curElement.tableCellCaption.Top = prevTopValue;
                    curElement.tableCellCaption.Left = prevLeftValue;
                    curElement.tableCellValue.Top = curElement.tableCellCaption.Top + curElement.tableCellCaption.Height;
                    curElement.tableCellValue.Left = curElement.tableCellCaption.Left;

                    prevLeftValue = curElement.tableCellValue.Left + horizontalIndent;

                    if (curElement.tableCellValue.Height > curRowVerticalIndent)
                    {
                        curRowVerticalIndent = curElement.tableCellValue.Height + 20;
                    }

                    curReplacingElementNumber++;
                }
                //если это не первый контрол
                else
                {
                    //если добавляемый контрол не умещается на форме по горизонтали, он помещается на следующей "строке"
                    if (prevLeftValue + horizontalIndent - 50 > this.Width)
                    {
                        prevLeftValue = 10;
                        curElement.tableCellCaption.Top = prevTopValue + curRowVerticalIndent + 10;
                        curElement.tableCellCaption.Left = prevLeftValue;
                        curElement.tableCellValue.Top = curElement.tableCellCaption.Top + curElement.tableCellCaption.Height;
                        curElement.tableCellValue.Left = curElement.tableCellCaption.Left;

                        prevLeftValue = curElement.tableCellValue.Left + horizontalIndent;
                        prevTopValue = curElement.tableCellCaption.Top;

                        curRowVerticalIndent = 0;
                        if (curElement.tableCellValue.Height > curRowVerticalIndent)
                        {
                            curRowVerticalIndent = curElement.tableCellValue.Height + 20;
                        }
                    }
                    //иначе он добавляется в ту же "строку" правее
                    else
                    {
                        curElement.tableCellCaption.Top = prevTopValue;
                        curElement.tableCellCaption.Left = prevLeftValue;
                        curElement.tableCellValue.Top = curElement.tableCellCaption.Top + curElement.tableCellCaption.Height;
                        curElement.tableCellValue.Left = curElement.tableCellCaption.Left;

                        prevLeftValue = curElement.tableCellValue.Left + horizontalIndent;

                        if (curElement.tableCellValue.Height > curRowVerticalIndent)
                        {
                            curRowVerticalIndent = curElement.tableCellValue.Height + 20;
                        }
                    }
                }
            }
        }

        private void ClaimViewingForm_Resize(object sender, EventArgs e)
        {
            ReplaceControlsHorizontal();
        }
    }
}
