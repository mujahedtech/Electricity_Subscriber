using AccountingSystem.Models;
using AccountingSystem.View.NajahEpic;
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
    public class DebitSummaryVM
    {
        public int id { get; set; }
        public string AccountName { get; set; }
        public double Balance { get; set; }
        public string balanceStr { get; set; }
        public string ClassName { get; set; }
        public int ClassId { get; set; }

    }

    /// <summary>
    /// Interaction logic for MonthlyReports.xaml
    /// </summary>
    public partial class MonthlyReports : UserControl
    {




        List<AccountsTable> AccountList = new List<AccountsTable>();

        List<TransactionAccounting> TransList = new List<TransactionAccounting>();

        List<AccountClass> AccountClassList = new List<AccountClass>();
        public MonthlyReports()
        {
            InitializeComponent();

            Loaded += MonthlyReports_Loaded;
        }

        private void MonthlyReports_Loaded(object sender, RoutedEventArgs e)
        {
            int year = 2020;
            for (int i = 0; i < 15; i++)
            {
                cobYears.Items.Add(year.ToString());
                year += 1;
            }


            cobYears.Text = DateTime.Now.Year.ToString();
            cobMonth.Text = DateTime.Now.Month.ToString();



        }


        List<Models.DailyTransaction> Trans;
        List<Models.DailySlaughter> Slaughter_;


        public async void LoadData()
        {
            int Month = int.Parse(cobMonth.Text);
            int Year = int.Parse(cobYears.Text);

            Trans = await new Models.Repositories.DailyTransactionRepository().ListPerDate(Month, Year);
            Slaughter_ = await new Models.Repositories.DailySlaughterRepository().List();


            var Data = GetTrans();

            txtTotalCash.Text = Data.Sum(i => i.EndCash).ToString("0.00");
            txtTotalEpic.Text = Data.Sum(i => i.SlaughterAmount).ToString("0.00");
            txtGrossMargin.Text = (Data.Sum(i => i.EndCash) - Data.Sum(i => i.SlaughterAmount)).ToString("0.00");

            txtGrossMarginStr.Text = (Data.Sum(i => i.EndCash) - Data.Sum(i => i.SlaughterAmount)) > 0 ? "ربح" : "خسارة";
        }

        public List<DailyTransactionVM> GetTrans()
        {

            //var temp = from em in Trans.ToList()

            //           select new DailyTransactionVM
            //           {
            //               Date = em.Date,
            //               IdFromSlaughter = em.IdFromSlaughter,
            //               EndCash = em.EndCash,
            //               TotalSlaughter = Slaughter_.Where(i => i.IdForTransaction == em.IdFromSlaughter).Sum(i => i.Total),Id=em.Id
            //           };



            var results = from Trans in Trans.AsEnumerable()
                          join Slaughter in Slaughter_.AsEnumerable() on Trans.IdFromSlaughter equals Slaughter.IdForTransaction

                          select new DailyTransactionVM
                          {
                              Date = Trans.Date,
                              IdFromSlaughter = Slaughter.IdForTransaction,
                              EndCash = Trans.EndCash,
                              SlaughterAmount = Slaughter.Total,
                              Id = Trans.Id



                          };


            var Data2 = results.GroupBy(i => i.Id)
                                          .Select(g => new DailyTransactionVM
                                          {

                                              Id = g.Key,
                                              Date = g.First().Date,
                                              SlaughterAmount = g.Sum(i => i.SlaughterAmount),
                                              EndCash = g.First().EndCash,
                                              IdFromSlaughter = g.First().IdFromSlaughter

                                          });








            return Data2.ToList();
        }


        private void btnCalculateGrossProfit_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }






        private async void btnTotalDebit_Click(object sender, RoutedEventArgs e)
        {
            int Month = int.Parse(cobMonth.Text);
            int Year = int.Parse(cobYears.Text);



            var PastDebit = GetDebitRange(Month - 1, Year);
            var NowDebit = GetDebitRange(Month, Year);

            txtTotalPerviousDebit.Text = PastDebit.Sum(i => i.Balance).ToString("0.00");
            txtTotalDebit.Text = NowDebit.Sum(i => i.Balance).ToString("0.00");

            txtNetDebit.Text = (NowDebit.Sum(i => i.Balance) - PastDebit.Sum(i => i.Balance)).ToString("0.00");
            txtNetDebitStr.Text = (NowDebit.Sum(i => i.Balance) - PastDebit.Sum(i => i.Balance)) > 0 ? "زيادة" : "نقص";




        }


        IEnumerable<DebitSummaryVM> GetDebit(int month, int year)
        {
            AccountList = App.AccountList.ToList();

            TransList = new Models.Repositories.TransactionAccountingRepository().ListPerDate(month, year).Result.ToList();

            AccountClassList = new Models.Repositories.AccountClassRepository().List().Result.ToList();


            var results = from Account in AccountList.AsEnumerable()
                          join AccountClass in AccountClassList.AsEnumerable() on Account.idclassAccount equals AccountClass.Id
                          join Trasn in TransList.AsEnumerable() on Account.Id equals Trasn.AccountId
                          select new MonthlyReportsVM
                          {
                              Id = Trasn.Id,
                              AccountName = Account.AccountName,
                              ClassName = AccountClass.ClassName,
                              ClassID = AccountClass.Id,
                              Value = Trasn.Amount,
                              TransTypeStr = GetAccSide(Trasn.TransType),
                              TransType = GetAccSideId(Trasn.TransType)



                          };


            results = results.Where(i => i.ClassID == (int)App.AccountType.Customer).ToList();

            var Data2 = results.GroupBy(i => i.Id)
                                         .Select(g => new DebitSummaryVM
                                         {
                                             id = g.Key,
                                             AccountName = g.FirstOrDefault().AccountName,
                                             Balance = (g.Where(i => i.TransType == 0).Sum(i => i.Value) - g.Where(i => i.TransType == 1).Sum(i => i.Value)),
                                             balanceStr = (g.Where(i => i.TransType == 0).Sum(i => i.Value) - g.Where(i => i.TransType == 1).Sum(i => i.Value)) > 0 ? "مدين" : "دائن",
                                             ClassName = g.FirstOrDefault().ClassName,

                                         });



            return Data2;

        }


        IEnumerable<DebitSummaryVM> GetDebitRange(int month, int year)
        {
            AccountList = App.AccountList.ToList();

            TransList = new Models.Repositories.TransactionAccountingRepository().ListPerDateRange(month, year).Result.ToList();

            AccountClassList = new Models.Repositories.AccountClassRepository().List().Result.ToList();


            var results = from Account in AccountList.AsEnumerable()
                          join AccountClass in AccountClassList.AsEnumerable() on Account.idclassAccount equals AccountClass.Id
                          join Trasn in TransList.AsEnumerable() on Account.Id equals Trasn.AccountId
                          select new MonthlyReportsVM
                          {
                              Id = Trasn.Id,
                              AccountName = Account.AccountName,
                              ClassName = AccountClass.ClassName,
                              ClassID = AccountClass.Id,
                              Value = Trasn.Amount,
                              TransTypeStr = GetAccSide(Trasn.TransType),
                              TransType = GetAccSideId(Trasn.TransType)



                          };

            results = results.Where(i => i.ClassID == (int)App.AccountType.Customer).ToList();


            var Data2 = results.GroupBy(i => i.Id)
                                         .Select(g => new DebitSummaryVM
                                         {
                                             id = g.Key,
                                             AccountName = g.FirstOrDefault().AccountName,

                                             Balance = (g.Where(i => i.TransType == 0).Sum(i => i.Value) - g.Where(i => i.TransType == 1).Sum(i => i.Value)),
                                             balanceStr = (g.Where(i => i.TransType == 0).Sum(i => i.Value) - g.Where(i => i.TransType == 1).Sum(i => i.Value)) > 0 ? "مدين" : "دائن",
                                             ClassName = g.FirstOrDefault().ClassName,
                                             ClassId = g.First().ClassID

                                         });



            return Data2;

        }


        string GetAccSide(int Id)
        {
            string Value = "";
            if (Id == 2 || Id == 4)
                Value = "Debit";
            else if (Id == 1 || Id == 3)
                Value = "Credit";

            return Value;
        }
        int GetAccSideId(int Id)
        {
            int Value = 0;
            if (Id == 2 || Id == 4)
                Value = 0;
            else if (Id == 1 || Id == 3)
                Value = 1;

            return Value;
        }
    }
}
