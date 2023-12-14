using AccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static AccountingSystem.ClassMujahed.DataValidation;

namespace AccountingSystem.View.ManageAccounts.Definition
{
    /// <summary>
    /// Interaction logic for CatManage.xaml
    /// </summary>
    public partial class CatManage : UserControl
    {
        public CatManage()
        {
            InitializeComponent();
        }

        int ValidCounter = 0;
        void DefaultMode()
        {
            txtCatName.FontFamily = new FontFamily(nameof(Validtion.Ok));
            txtCatName.ToolTip = null;

        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {


            ValidCounter = 0;
            DefaultMode();
            //اسم الصنف
            if (txtCatName.Text.Length == 0)
            {
                var TextBox = txtCatName;

                TextBox.FontFamily = new FontFamily(nameof(Validtion.Error));
                TextBox.ToolTip = GetErrorMessage(TextBox);
                txtMessage.Text = TextBox.ToolTip.ToString();

                ValidCounter += 1;

            }



            if (ValidCounter != 0) return;

            var Data = new Categories();

            Data.CatName = txtCatName.Text;




            new Models.Repositories.CategoriesRepository().Add(Data);

            txtCatName.Text = "";
            txtMessage.Text = "تم الاضافة بنجاح";




        }
    }

}
