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

namespace AccountingSystem.View.ManageAccounts
{
    /// <summary>
    /// Interaction logic for AccountStatement.xaml
    /// </summary>
    public partial class AccountStatement : UserControl
    {

        public class AccountStatementVM
        {

            public DateTime Date { get; set; }
            public string TransType { get; set; }
            public string Note { get; set; }
            public double? Debit { get; set; }
            public double? Credit { get; set; }
            public double Balance { get; set; }


        }

        public AccountStatement()
        {
            InitializeComponent();

            Loaded += AccountStatement_Loaded;
        }

        private void AccountStatement_Loaded(object sender, RoutedEventArgs e)
        {
            CobAccount.ItemsSource = App.AccountList.ToList();
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            lblTypeAccount.Text = CobAccount.Text;

            ListAccount.ItemsSource = null;

            LoadData();
        }

        List<TransactionAccounting> TransList;
        async void LoadData()
        {
            TransList = await new Models.Repositories.TransactionAccountingRepository().ListIdAccount(CobAccount.SelectedValue.GetHashCode());


            var List = GetTrans2();

            if (TransList.Count == 0) return;

            for (int i = 0; i < List.Count; i++)
            {
                if (List[i].Note == "المجموع") List[i].Balance = List[i - 1].Balance;

                else if (i == 0)
                    List[i].Balance = List[i].Debit.Value - List[i].Credit.Value;
                else List[i].Balance = List[i - 1].Balance + List[i].Debit.Value - List[i].Credit.Value;
            }




            ListAccount.ItemsSource = List;
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


        public List<AccountStatementVM> GetTrans2()
        {
            var temp = from em in GetTrans().ToList()

                       select new AccountStatementVM
                       {
                           Date = em.DateEntered,
                           TransType = em.TransactionType,
                           Note = em.Note,
                           Debit = em.TransType == 2 || em.TransType == 4 ? em.Amount : 0,
                           Credit = em.TransType == 3 || em.TransType == 1 ? em.Amount : 0,


                       };


            List<AccountStatementVM> Local = temp.ToList();

            Local.Add(new AccountStatementVM { Note = "المجموع", Debit = Local.Sum(i => i.Debit), Credit = Local.Sum(i => i.Credit) });



            return Local.ToList();
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
    }

}
