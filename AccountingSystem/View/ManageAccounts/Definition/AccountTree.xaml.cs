using AccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    public class AccountsTableVM : AccountsTable
    {
        public string ClassName { get; set; }

    }

    /// <summary>
    /// Interaction logic for AccountTree.xaml
    /// </summary>


    public partial class AccountTree : UserControl
    {
        public AccountTree()
        {
            InitializeComponent();

            Loaded += MainAccounts_Loaded;
        }
        int ValidCounter = 0;
        void DefaultMode()
        {
            txtAccountName.FontFamily = new FontFamily(nameof(Validtion.Ok));
            txtAccountName.ToolTip = null;

            txtMobile.FontFamily = new FontFamily(nameof(Validtion.Ok));
            txtMobile.ToolTip = null;

            cobAccountClass.BorderBrush = Brushes.Gray;
            cobAccountClass.ToolTip = null;

            txtAccountTypeCreate.BorderBrush = Brushes.Gray;
            txtAccountTypeCreate.ToolTip = null;



        }

        ObservableCollection<AccountsTableVM> accountsVM;


        List<AccountClass> Cat;
        List<AccountsTable> Accounts;
        private void MainAccounts_Loaded(object sender, RoutedEventArgs e)
        {
            GetData(1);

        }

        async void GetData(int Code = 0)
        {

            Cat = await new Models.Repositories.AccountClassRepository().List();

            cobAccountClass.ItemsSource = Cat;



            Accounts = await new Models.Repositories.AccountsTableRepository().List();

            DataGridAccounts.ItemsSource = GetAccount();
        }




        private void lblAccountType_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                PopAdd.Visibility = Visibility.Visible;
            }
        }


        int ValidCounterClassCreate = 0;

        AccountsTable InsertData = new AccountsTable();

        private void btnSaveClassCreate_Click(object sender, RoutedEventArgs e)
        {

            ValidCounterClassCreate = 0;
            DefaultMode();



            if (txtAccountTypeCreate.Text.Length == 0)
            {
                var TextBox = txtAccountTypeCreate;

                TextBox.FontFamily = new FontFamily(nameof(Validtion.Error));
                TextBox.ToolTip = GetErrorMessage(TextBox);
                txtMessage.Text = TextBox.ToolTip.ToString();

                ValidCounterClassCreate += 1;

            }

            if (ValidCounterClassCreate != 0) return;

            var Data = new AccountClass();

            Data.ClassName = txtAccountTypeCreate.Text;


            new Models.Repositories.AccountClassRepository().Add(Data);
            txtAccountTypeCreate.Text = "";

            PopAdd.Visibility = Visibility.Collapsed;


        }

        private void btnADD_Click(object sender, RoutedEventArgs e)
        {


            ValidCounter = 0;
            DefaultMode();


            if (cobAccountClass.SelectedIndex == -1)
            {
                var TextBox = cobAccountClass;

                TextBox.BorderBrush = ErrorColor;
                TextBox.ToolTip = GetErrorMessage(TextBox);


                ValidCounter += 1;

            }
            if (txtMobile.Text.Length == 0 || Isint(txtMobile.Text) == false)
            {
                var TextBox = txtMobile;

                TextBox.FontFamily = new FontFamily(nameof(Validtion.Error));
                TextBox.ToolTip = GetErrorMessage(TextBox);
                ValidCounter += 1;

            }

            if (txtAccountName.Text.Length == 0)
            {
                var TextBox = txtAccountName;

                TextBox.FontFamily = new FontFamily(nameof(Validtion.Error));
                TextBox.ToolTip = GetErrorMessage(TextBox);


                ValidCounter += 1;

            }

            if (ValidCounter != 0) return;




            InsertData.idclassAccount = cobAccountClass.SelectedValue.GetHashCode();
            InsertData.AccountName = txtAccountName.Text;
            InsertData.AccountMobile = txtMobile.Text;
            InsertData.AccountNote = txtNote.Text;


            if (InsertData.Id == 0) new Models.Repositories.AccountsTableRepository().Add(InsertData);
            else new Models.Repositories.AccountsTableRepository().Update(InsertData);





            Thread.Sleep(100);
            cobAccountClass.SelectedIndex = -1;
            txtAccountName.Text = txtMobile.Text = txtNote.Text = "";

            cobAccountClass.IsEnabled = true;

            InsertData = new AccountsTable();

            GetData();

            App.UpdateList();


        }

        public List<AccountsTableVM> GetAccount()
        {
            var temp = (from cat in Cat
                        join acc in Accounts on cat.Id equals acc.idclassAccount
                        select new AccountsTableVM
                        {
                            AccountMobile = acc.AccountMobile,
                            DateEntered = acc.DateEntered,
                            ClassName = cat.ClassName,
                            AccountName = acc.AccountName,
                            AccountNote = acc.AccountNote,
                            Id = acc.Id,
                            idclassAccount = acc.idclassAccount

                        }).ToList();
            return temp;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var SearchList = GetAccount();

            SearchList = SearchList.Where(i => i.AccountName.Contains(txtSearch.Text)).ToList();


            DataGridAccounts.ItemsSource = SearchList;

        }

        private void DataGridAccounts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGridAccounts.SelectedItem == null) return;

            var item = (AccountsTableVM)DataGridAccounts.SelectedItem;




            InsertData = new AccountsTable
            {
                AccountMobile = item.AccountMobile,
                DateEntered = item.DateEntered,
                AccountName = item.AccountName,
                AccountNote = item.AccountNote,
                Id = item.Id,
                idclassAccount = item.idclassAccount
            };


            cobAccountClass.SelectedValue = InsertData.idclassAccount;
            txtAccountName.Text = InsertData.AccountName;
            txtMobile.Text = InsertData.AccountMobile;
            txtNote.Text = InsertData.AccountNote;

            cobAccountClass.IsEnabled = false;
        }
    }

}
