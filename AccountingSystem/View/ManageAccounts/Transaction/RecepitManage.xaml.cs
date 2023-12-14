using AccountingSystem.Models;
using AccountingSystem.View.PurchaseManage;
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

namespace AccountingSystem.View.ManageAccounts.Transaction
{
    /// <summary>
    /// Interaction logic for RecepitManage.xaml
    /// </summary>
    public partial class RecepitManage : UserControl
    {
        public RecepitManage()
        {
            InitializeComponent();
            Loaded += ExpenseManage_Loaded;
        }
        int ValidCounter = 0;
        void DefaultMode()
        {
            txtCashHouse.BorderBrush = Brushes.Gray;
            txtCashHouse.ToolTip = null;

            CobAccount.BorderBrush = Brushes.Gray;
            CobAccount.ToolTip = null;

            txtDate.BorderBrush = Brushes.Gray;
            txtDate.ToolTip = null;

            txtAmount.FontFamily = new FontFamily(nameof(Validtion.Ok));
            txtAmount.ToolTip = null;

        }


        public List<TransactionAccountingVM> GetTrans()
        {

            var temp = from em in TransList.ToList()

                       select new TransactionAccountingVM
                       {
                           DateEntered = em.DateEntered,
                           AccountId = em.AccountId,
                           Amount = em.Amount,
                           Note = em.Note,
                           TransId = em.TransId,
                           TransType = em.TransType,
                           Id = em.Id,
                           InventoryId = em.InventoryId,
                           VendorId = em.VendorId,

                       };

            return temp.ToList();
        }

        List<TransactionAccounting> TransList;
        private async void ExpenseManage_Loaded(object sender, RoutedEventArgs e)
        {
            txtCashHouse.ItemsSource = App.InventoryList.ToList();
            CobAccount.ItemsSource = App.AccountList.ToList();

            LoadData();
        }

        async void LoadData()
        {
            TransList = await new Models.Repositories.TransactionAccountingRepository().ListPerTransType((int)App.AccountTrans.Revenue);

            DataGridListTrans.ItemsSource = GetTrans();


        }



        TransactionAccounting InsertData = new TransactionAccounting();
        private async void btnADDClick(object sender, RoutedEventArgs e)
        {

            ValidCounter = 0;
            DefaultMode();


            if (txtAmount.Text.Length == 0 || Isdouble(txtAmount.Text) == false)
            {
                var TextBox = txtAmount;

                TextBox.FontFamily = new FontFamily(nameof(Validtion.Error));
                TextBox.ToolTip = GetErrorMessage(TextBox);

                ValidCounter += 1;

            }

            if (IsDate(txtDate.Text) == false)
            {
                var TextBox = txtDate;

                TextBox.BorderBrush = ErrorColor;
                TextBox.ToolTip = GetErrorMessage(TextBox);

                ValidCounter += 1;

            }

            if (txtCashHouse.SelectedIndex == -1)
            {
                var TextBox = txtCashHouse;

                TextBox.BorderBrush = ErrorColor;
                TextBox.ToolTip = GetErrorMessage(TextBox);

                ValidCounter += 1;

            }
            if (CobAccount.SelectedIndex == -1)
            {
                var TextBox = CobAccount;

                TextBox.BorderBrush = ErrorColor;
                TextBox.ToolTip = GetErrorMessage(TextBox);

                ValidCounter += 1;

            }


            if (ValidCounter != 0) return;

            InsertData.AccountId = CobAccount.SelectedValue.GetHashCode();
            InsertData.InventoryId = txtCashHouse.SelectedValue.GetHashCode();
            InsertData.Amount = double.Parse(txtAmount.Text);
            InsertData.Note = txtNote.Text;
            InsertData.DateEntered = txtDate.SelectedDate.Value;
            InsertData.TransId = Guid.NewGuid();
            InsertData.TransType = (int)App.AccountTrans.Revenue;


            if (InsertData.Id == 0)
            {
                await new Models.Repositories.TransactionAccountingRepository().Add(InsertData);
            }
            else
            {
                await new Models.Repositories.TransactionAccountingRepository().Update(InsertData);
            }



            LoadData();

            CobAccount.SelectedIndex = txtCashHouse.SelectedIndex = -1;

            txtAmount.Text = txtNote.Text = "";


        }

        private void DataGridListTrans_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGridListTrans.SelectedItem == null) return;

            var item = (TransactionAccountingVM)DataGridListTrans.SelectedItem;

            InsertData = new Models.TransactionAccounting
            {
                Id = item.Id,
                AccountId = item.AccountId,
                Amount = item.Amount,
                DateEntered = item.DateEntered,
                Note = item.Note,
                TransId = item.TransId,
                TransType = item.TransType,
                InventoryId = item.InventoryId,
                VendorId = item.VendorId,
            };


            CobAccount.SelectedValue = InsertData.AccountId;
            txtCashHouse.SelectedValue = InsertData.AccountId;
            txtDate.SelectedDate = InsertData.DateEntered;
            txtAmount.Text = InsertData.Amount.ToString();
            txtNote.Text = InsertData.Note;




        }
    }

}
