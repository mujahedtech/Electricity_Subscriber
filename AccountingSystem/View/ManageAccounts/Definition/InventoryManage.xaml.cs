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
    /// Interaction logic for InventoryManage.xaml
    /// </summary>
    public partial class InventoryManage : UserControl
    {
        public InventoryManage()
        {
            InitializeComponent();
        }

        int ValidCounter = 0;
        void DefaultMode()
        {
            txtInvName.FontFamily = new FontFamily(nameof(Validtion.Ok));
            txtInvName.ToolTip = null;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ValidCounter = 0;
            DefaultMode();
            //اسم الصنف
            if (txtInvName.Text.Length == 0)
            {
                var TextBox = txtInvName;

                TextBox.FontFamily = new FontFamily(nameof(Validtion.Error));
                TextBox.ToolTip = GetErrorMessage(TextBox);
                txtMessage.Text = TextBox.ToolTip.ToString();

                ValidCounter += 1;

            }



            if (ValidCounter != 0) return;

            var Data = new Inventory();

            Data.InvName = txtInvName.Text;
            Data.IsGasInv = ChkIsGasInv.IsChecked.Value;


            new Models.Repositories.InventoryRepository().Add(Data);

            txtInvName.Text = "";
            txtMessage.Text = "تم الاضافة بنجاح";

            //App.UpdateList();

        }
    }

}
