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
    public partial class AddEditUserForm : Form
    {
        public AddEditUserForm()
        {
            InitializeComponent();

            //подписка на событие
            TextChanged += new TextChangedDelegate(CheckEmptyFields);
            
            if ((tbUserFirstName.Text.Length > 0 || tbUserLastName.Text.Length > 0) &&
                cbUserRole.SelectedIndex != -1 &&
                cbUserGroup.SelectedIndex != -1)
            {
                bOK.Enabled = true;
            }
            else
            {
                bOK.Enabled = false;
            }
        }

        //делегат для события
        public delegate void TextChangedDelegate();

        //событие, возникающее каждый раз, когда изменяется текст какого-либо элемента формы 
        public event TextChangedDelegate TextChanged;

        //в зависимости от того, заполнены ли все необходимые поля,
        //процедура изменяет значение свойства bOK.Enabled
        public void CheckEmptyFields()
        {
            //переменная count служит маркером, определяющим количество совпадений
            //текста, введенного пользователем в поле ввода элемента cbUserRole с
            //имеющимся списком допустимых значений.

            //Далее каждое допустимое значение cbUserRole сравнивается с введенным пользователем текстом.
            //Если они совпадают, значение переменной count увеличивается на 1.
            //Соответственно, если count = 0, то введенный пользователем текст не совпал
            //ни с одним элементом списка cbUserRole, а значит, свойство bOK.Enabled должно быть false.
            int countRole = 0;
            for (int i = 0; i < cbUserRole.Items.Count; i++)
            {
                if (cbUserRole.Text == cbUserRole.Items[i].ToString())
                {
                    countRole++;
                }
            }

            //логика аналогична cbUserRole
            int countGroup = 0;
            for (int i = 0; i < cbUserGroup.Items.Count; i++)
            {
                if (cbUserGroup.Text == cbUserGroup.Items[i].ToString())
                {
                    countGroup++;
                }
            }

            if ((tbUserFirstName.Text.Length > 0 || tbUserLastName.Text.Length > 0) &&
                cbUserRole.SelectedIndex != -1 &&
                cbUserGroup.SelectedIndex != -1 &&
                countRole == 1 &&
                countGroup == 1)
            {
                bOK.Enabled = true;
            }
            else
            {
                bOK.Enabled = false;
            }
        }

        private void tbUserLastName_TextChanged(object sender, EventArgs e)
        {
            TextChanged();
        }

        private void tbUserFirstName_TextChanged(object sender, EventArgs e)
        {
            TextChanged();
        }

        private void tbUserPatronymic_TextChanged(object sender, EventArgs e)
        {
            TextChanged();
        }

        private void cbUserRole_TextChanged(object sender, EventArgs e)
        {
            TextChanged();
        }

        private void cbUserGroup_TextChanged(object sender, EventArgs e)
        {
            TextChanged();
        }
    }
}
