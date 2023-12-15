using AccountingSystem.ClassMujahed;
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
    /// Interaction logic for ExpenseManage.xaml
    /// </summary>
    public partial class ExpenseManage : UserControl
    {
        public ExpenseManage()
        {
            InitializeComponent();

            Loaded += ExpenseManage_Loaded;
        }
        int ValidCounter = 0;

        void ValidControlData(Control Control, bool NumberValid = false)
        {
            if (Control is TextBox)
            {
                Control.FontFamily = new FontFamily(nameof(DataValidation.Validtion.Ok));
                Control.ToolTip = null;

                var Text = (TextBox)Control;

                if (NumberValid)
                {
                    if (Text.Text.Length == 0 || DataValidation.Isint(Text.Text) == false)
                    {

                        Text.FontFamily = new FontFamily(nameof(DataValidation.Validtion.Error));
                        Text.ToolTip = DataValidation.GetErrorMessage(Text);
                        ValidCounter += 1;

                    }
                }
                else
                {
                    if (Text.Text.Length == 0)
                    {
                        Text.FontFamily = new FontFamily(nameof(DataValidation.Validtion.Error));
                        Text.ToolTip = DataValidation.GetErrorMessage(Text);
                        ValidCounter += 1;

                    }
                }
            }
            else if (Control is ComboBox)
            {
                var Combo = (ComboBox)Control;

                Combo.BorderBrush = Brushes.Gray;
                Combo.ToolTip = null;

                if (Combo.SelectedIndex == -1)
                {
                    Combo.BorderBrush = DataValidation.ErrorColor;
                    Combo.ToolTip = DataValidation.GetErrorMessage(Combo);
                    ValidCounter += 1;
                }
            }
            else if (Control is DatePicker)
            {
                var datePicker = (DatePicker)Control;

                datePicker.BorderBrush = Brushes.Gray;
                datePicker.ToolTip = null;

                if (DataValidation.IsDate(datePicker.Text) == false)
                {
                    datePicker.BorderBrush = DataValidation.ErrorColor;
                    datePicker.ToolTip = DataValidation.GetErrorMessage(datePicker);
                    ValidCounter += 1;

                }



            }


        }

        void DefaultMode()
        {
            CobCenterCost.BorderBrush = Brushes.Gray;
            CobCenterCost.ToolTip = null;

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
            CobCenterCost.ItemsSource = App.InventoryList.ToList();
            CobAccount.ItemsSource = App.AccountList.ToList();

            LoadData();
        }

        async void LoadData()
        {
            TransList = await new Models.Repositories.TransactionAccountingRepository().ListPerTransType((int)App.AccountTrans.Expense);

            DataGridListTrans.ItemsSource = GetTrans();


        }



        TransactionAccounting InsertData = new TransactionAccounting();
        private async void btnADDClick(object sender, RoutedEventArgs e)
        {
            ValidCounter = 0;
            DefaultMode();


            ValidControlData(txtAmount,true);
            ValidControlData(txtDate);
           
            ValidControlData(CobAccount);



          

            if (ValidCounter != 0) return;

            InsertData.AccountId = CobAccount.SelectedValue.GetHashCode();
            InsertData.InventoryId = 0;
            InsertData.Amount = double.Parse(txtAmount.Text);
            InsertData.Note = txtNote.Text;
            InsertData.DateEntered = txtDate.SelectedDate.Value;
            InsertData.TransId = Guid.NewGuid();
            InsertData.TransType = (int)App.AccountTrans.Expense;


            if (InsertData.Id == 0)
            {
                await new Models.Repositories.TransactionAccountingRepository().Add(InsertData);
            }
            else
            {
                await new Models.Repositories.TransactionAccountingRepository().Update(InsertData);
            }



            LoadData();

            CobAccount.SelectedIndex = CobCenterCost.SelectedIndex = -1;

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
            CobCenterCost.SelectedValue = InsertData.AccountId;
            txtDate.SelectedDate = InsertData.DateEntered;
            txtAmount.Text = InsertData.Amount.ToString();
            txtNote.Text = InsertData.Note;




        }

        private void btnSelectAccount_Click(object sender, RoutedEventArgs e)
        {
            var Result = new SubView.SelectAccount(App.AccountList.ToList());




            Result.ShowInTaskbar = false;
            Result.Owner = Application.Current.MainWindow;
            Result.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            Result.Width = Application.Current.MainWindow.ActualWidth;
            Result.Height = Application.Current.MainWindow.ActualHeight;

            Result.ShowDialog();



            CobAccount.SelectedItem = Result.AccountsSelected();
        }
    }

}
