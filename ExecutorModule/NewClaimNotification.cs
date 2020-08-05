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
    public partial class NewClaimNotification : Form
    {
        public NewClaimNotification()
        {
            InitializeComponent();

            //Настройка таймера
            notificationTimer.Interval = 1000;
            notificationTimer.Tick += new EventHandler(notificationTimer_Tick);

            //настройка размеров и расположения формы и lNotification, задание текста для lNotification
            this.TransparencyKey = this.BackColor;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.Size = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
            this.ShowInTaskbar = false;
            TopMost = true;
            lNotification.ForeColor = Color.DarkRed;
            lNotification.Text = MainWindow.notificationString;
            lNotification.TextAlign = ContentAlignment.MiddleCenter;
            if (lNotification.Height < lNotification.Width)
            {
                while ((lNotification.Width < System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - 100))
                {
                    lNotification.Font = new Font(lNotification.Font.FontFamily, lNotification.Font.Size + 5);
                }
            }
            else
            {
                while ((lNotification.Height < System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - 100))
                {
                    lNotification.Font = new Font(lNotification.Font.FontFamily, lNotification.Font.Size + 5);
                }
            }
            lNotification.Left = (this.Width / 2) - (lNotification.Width / 2);
            lNotification.Top = (this.Height / 2) - (lNotification.Height / 2);

            //Запуск таймера
            notificationTimer.Start();
        }

        void notificationTimer_Tick(object sender, EventArgs e)
        {
            lNotification.Visible = !lNotification.Visible;
        }

        //переменная типа Timer. Нужна для того, чтобы lNotification мигала
        Timer notificationTimer = new Timer();
    }
}
