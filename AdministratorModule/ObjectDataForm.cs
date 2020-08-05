using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AdministratorModule
{
    public partial class ObjectDataForm : Form
    {
        public ObjectDataForm()
        {
            InitializeComponent();

            //Обнуление информации о созданных контролах формы
            winCtrlInfoList.Clear();
            WinCtrlsInfoStruct.winCtrlCount = 0;
            WinCtrlsInfoStruct.xPos = 10;
            WinCtrlsInfoStruct.yPos = 10;
        }

        //структура хранит информацию о создаваемых контролах формы
        public struct WinCtrlsInfoStruct
        {
            public string winCtrlName;
            public string dbTableColumnName;
            public string winCtrlType;
            public static int xPos = 10;
            public static int yPos = 10;
            public static int winCtrlCount = 0;
        }

        //список созданных контролов на форме
        public List<WinCtrlsInfoStruct> winCtrlInfoList = new List<WinCtrlsInfoStruct>();

        public void CreateNewTextBox(string dbTableColumnName, Control parentContol)
        {
            //создание заголовка для TextBox
            Label newLabel = new Label();
            newLabel.Text = dbTableColumnName;
            newLabel.Top = WinCtrlsInfoStruct.yPos;
            newLabel.Left = WinCtrlsInfoStruct.xPos;
            newLabel.Parent = parentContol;

            //создание TextBox
            TextBox newTB = new TextBox();
            newTB.Name = "textBox" + WinCtrlsInfoStruct.winCtrlCount;
            newTB.Width = 200;
            newTB.Left = WinCtrlsInfoStruct.xPos;
            newTB.Top = WinCtrlsInfoStruct.yPos + 15;
            newTB.Parent = parentContol;
            newTB.BringToFront();

            //Добавление информации о созданном TextBox в winCtrlInfoList
            WinCtrlsInfoStruct winCtrlsInfo = new WinCtrlsInfoStruct();
            winCtrlsInfo.dbTableColumnName = dbTableColumnName;
            winCtrlsInfo.winCtrlName = newTB.Name;
            winCtrlsInfo.winCtrlType = newTB.GetType().ToString();
            winCtrlInfoList.Add(winCtrlsInfo);

            //Количество созданных элементов +1, задание следующей позиции
            WinCtrlsInfoStruct.winCtrlCount++;
            WinCtrlsInfoStruct.yPos += 50;
        }
    }
}
