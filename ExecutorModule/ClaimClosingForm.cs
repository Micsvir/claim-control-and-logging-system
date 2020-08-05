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
    public partial class ClaimClosingForm : Form
    {
        public ClaimClosingForm()
        {
            InitializeComponent();            
        }

        bool CommonCategorySelected = false;
        bool InternalCategorySelected = false;
        bool SolvingDescriptionAdded = false;
        bool AffectedObjectsCountAdded = false;
        Regex regex = new Regex("^[0-9]+$");

        //выбор общепринятой категории
        private void cbCommonCategories_Click(object sender, EventArgs e)
        {

        }

        //выбор внутренней категории системы
        private void cbInternalCategories_Click(object sender, EventArgs e)
        {

        }

        //присваивание значения переменной selectedCommonCategoryID
        private void cbCommonCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCommonCategories.SelectedIndex != -1)
            {
                MainWindow.selectedCommonCategoryID = Convert.ToInt32(MainWindow.commonCategories.Rows[cbCommonCategories.SelectedIndex]["CommonCatID"]);
                CommonCategorySelected = true;
            }
            else
            {
                CommonCategorySelected = false;
            }
            if (CommonCategorySelected == true
                && InternalCategorySelected == true
                && SolvingDescriptionAdded == true
                && AffectedObjectsCountAdded == true)
            {
                bOK.Enabled = true;
            }
            else
            {
                bOK.Enabled = false;
            }
        }

        //присваивание значения переменной selectedInternalCategoryID
        private void cbInternalCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbInternalCategories.SelectedIndex != -1)
            {
                MainWindow.selectedInternalCategoryID = Convert.ToInt32(MainWindow.internalCategories.Rows[cbInternalCategories.SelectedIndex]["IntCatID"]);
                InternalCategorySelected = true;
            }
            else
            {
                InternalCategorySelected = false;
            }
            if (CommonCategorySelected == true
                && InternalCategorySelected == true
                && SolvingDescriptionAdded == true
                && AffectedObjectsCountAdded == true)
            {
                bOK.Enabled = true;
            }
            else
            {
                bOK.Enabled = false;
            }
        }

        private void mtbAffectedObjectsCount_TextChanged(object sender, EventArgs e)
        {
            if (!regex.IsMatch(mtbAffectedObjectsCount.Text))
            {
                mtbAffectedObjectsCount.Text = "";
            }
            if (mtbAffectedObjectsCount.Text.Length > 0)
            {
                AffectedObjectsCountAdded = true;
            }
            else
            {
                AffectedObjectsCountAdded = false;
            }
            if (CommonCategorySelected == true
                && InternalCategorySelected == true
                && SolvingDescriptionAdded == true
                && AffectedObjectsCountAdded == true)
            {
                bOK.Enabled = true;
            }
            else
            {
                bOK.Enabled = false;
            }
        }

        private void tbExecutionInformation_TextChanged(object sender, EventArgs e)
        {
            if (tbExecutionInformation.Text.Length > 0)
            {
                SolvingDescriptionAdded = true;
            }
            else
            {
                SolvingDescriptionAdded = false;
            }
            if (CommonCategorySelected == true
                && InternalCategorySelected == true
                && SolvingDescriptionAdded == true
                && AffectedObjectsCountAdded == true)
            {
                bOK.Enabled = true;
            }
            else
            {
                bOK.Enabled = false;
            }
        }
    }
}
