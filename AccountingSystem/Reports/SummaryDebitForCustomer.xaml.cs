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

namespace AccountingSystem.Reports
{
    public class MonthlyReportsVM
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public string ClassName { get; set; }
        public int ClassID { get; set; }
        public double Value { get; set; }
        public string TransTypeStr { get; set; }
        public double TransType { get; set; }





    }
    /// <summary>
    /// Interaction logic for SummaryDebitForCustomer.xaml
    /// </summary>
    public partial class SummaryDebitForCustomer : UserControl
    {
        List<AccountsTable> AccountList = new List<AccountsTable>();

        List<TransactionAccounting> TransList = new List<TransactionAccounting>();

        List<AccountClass> AccountClassList = new List<AccountClass>();

        public SummaryDebitForCustomer()
        {
            InitializeComponent();
            Loaded += MonthlyReports_Loaded;
        }

        private async void MonthlyReports_Loaded(object sender, RoutedEventArgs e)
        {
            AccountList = App.AccountList.ToList();

            TransList = await new Models.Repositories.TransactionAccountingRepository().List();

            AccountClassList = await new Models.Repositories.AccountClassRepository().List();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var results = from Account in AccountList.AsEnumerable()
                          join AccountClass in AccountClassList.AsEnumerable() on Account.idclassAccount equals AccountClass.Id
                          join Trasn in TransList.AsEnumerable() on Account.Id equals Trasn.AccountId
                          select new MonthlyReportsVM
                          {
                              AccountName = Account.AccountName,
                              ClassName = AccountClass.ClassName,
                              ClassID = AccountClass.Id,
                              Value = Trasn.Amount,
                              TransTypeStr = GetAccSide(Trasn.TransType),
                              TransType = GetAccSideId(Trasn.TransType)



                          };

            results = results.Where(i => i.ClassID == (int)App.AccountType.Customer).ToList();


            //var Data2= results.GroupBy(i => i.AccountName)
            //                                .Select(g => new 
            //                                {

            //                                    AccountName = g.Key,
            //                                    Debit = g.Where(i=>i.TransType==0).Sum(i=>i.Value),
            //                                    Credit = g.Where(i=>i.TransType==1).Sum(i=>i.Value),
            //                                    Balance=(g.Where(i => i.TransType == 0).Sum(i => i.Value)- g.Where(i => i.TransType == 1).Sum(i => i.Value)),
            //                                    balanceStr= (g.Where(i => i.TransType == 0).Sum(i => i.Value) - g.Where(i => i.TransType == 1).Sum(i => i.Value))>0?"مدين":"دائن",
            //                                    ClassName = g.FirstOrDefault().ClassName,

            //                                });


            var Data2 = results.GroupBy(i => i.AccountName)
                                          .Select(g => new
                                          {

                                              AccountName = g.Key,
                                              Balance = (g.Where(i => i.TransType == 0).Sum(i => i.Value) - g.Where(i => i.TransType == 1).Sum(i => i.Value)),
                                              balanceStr = (g.Where(i => i.TransType == 0).Sum(i => i.Value) - g.Where(i => i.TransType == 1).Sum(i => i.Value)) > 0 ? "مدين" : "دائن",
                                              ClassName = g.FirstOrDefault().ClassName,



                                          });



            DataGrid1.ItemsSource = Data2.Where(i => i.Balance > 0);
        }

        string GetAccSide(int Id)
        {
            string Value = "";
            if (Id == 2 || Id == 4|| Id == 5)
                Value = "Debit";
            else if (Id == 1 || Id == 3)
                Value = "Credit";

            return Value;
        }
        int GetAccSideId(int Id)
        {
            int Value = 0;
            if (Id == 2 || Id == 4 || Id == 5)
                Value = 0;
            else if (Id == 1 || Id == 3)
                Value = 1;

            return Value;
        }
    }
}
