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

namespace AccountingSystem.View.NajahEpic
{
    /// <summary>
    /// Interaction logic for DebitManage.xaml
    /// </summary>
    public partial class DebitManage : UserControl
    {
        public DebitManage()
        {
            InitializeComponent();



            Loaded += ExpenseManage_Loaded;
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
        private void ExpenseManage_Loaded(object sender, RoutedEventArgs e)
        {
            cobInv.ItemsSource = App.InventoryList.ToList();
            CobAccount.ItemsSource = App.AccountList.ToList().Where(i => i.idclassAccount == (int)App.AccountType.Customer);




            LoadData();






        }

        async void LoadData()
        {
            TransList = await new Models.Repositories.TransactionAccountingRepository().ListPerTransType((int)App.AccountTrans.A_R);

            DataGridListTrans.ItemsSource = GetTrans();





        }

        int ValidCounter = 0;
        void DefaultMode()
        {
            cobInv.BorderBrush = Brushes.Gray;
            cobInv.ToolTip = null;

            CobAccount.BorderBrush = Brushes.Gray;
            CobAccount.ToolTip = null;

            txtDate.BorderBrush = Brushes.Gray;
            txtDate.ToolTip = null;

            txtAmount.FontFamily = new FontFamily(nameof(Validtion.Ok));
            txtAmount.ToolTip = null;

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

            //if (cobInv.SelectedIndex == -1)
            //{
            //    var TextBox = cobInv;

            //    TextBox.BorderBrush = ErrorColor;
            //    TextBox.ToolTip = GetErrorMessage(TextBox);

            //    ValidCounter += 1;

            //}
            if (CobAccount.SelectedIndex == -1)
            {
                var TextBox = CobAccount;

                TextBox.BorderBrush = ErrorColor;
                TextBox.ToolTip = GetErrorMessage(TextBox);

                ValidCounter += 1;

            }


            if (ValidCounter != 0) return;


            InsertData.AccountId = CobAccount.SelectedValue.GetHashCode();
            InsertData.InventoryId = 1;
            InsertData.Amount = double.Parse(txtAmount.Text);
            InsertData.Note = txtNote.Text;
            InsertData.DateEntered = txtDate.SelectedDate.Value;
            InsertData.TransId = Guid.NewGuid();
            InsertData.TransType = (int)App.AccountTrans.A_R;


            if (InsertData.Id == 0)
            {
                await new Models.Repositories.TransactionAccountingRepository().Add(InsertData);
            }
            else
            {
                await new Models.Repositories.TransactionAccountingRepository().Update(InsertData);
            }



            LoadData();

            CobAccount.SelectedIndex = cobInv.SelectedIndex = -1;

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
            cobInv.SelectedValue = InsertData.AccountId;
            txtDate.SelectedDate = InsertData.DateEntered;
            txtAmount.Text = InsertData.Amount.ToString();
            txtNote.Text = InsertData.Note;




        }

        private void btnSelectAccount_Click(object sender, RoutedEventArgs e)
        {
            var Result = new SubView.SelectAccount();


            Result.ShowInTaskbar = false;
            Result.Owner = Application.Current.MainWindow;
            Result.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            Result.Width = Application.Current.MainWindow.ActualWidth;
            Result.Height = Application.Current.MainWindow.ActualHeight;

            Result.ShowDialog();

            CobAccount.SelectedItem = Result.AccountsSelected();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            var Result = new SubView.FilterData(TransList);


            Result.ShowInTaskbar = false;
            Result.Owner = Application.Current.MainWindow;
            Result.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            Result.Width = Application.Current.MainWindow.ActualWidth;
            Result.Height = Application.Current.MainWindow.ActualHeight;




            Result.ShowDialog();

            TransList = Result.transList;

            DataGridListTrans.ItemsSource = GetTrans();


        }
    }

}
