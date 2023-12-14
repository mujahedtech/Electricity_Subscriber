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

namespace AccountingSystem.View.NajahEpic
{
    public class DailyTransactionVM : Models.DailyTransaction
    {

        public double SlaughterAmount { get; set; }



    }


    /// <summary>
    /// Interaction logic for DailyTransaction.xaml
    /// </summary>
    public partial class DailyTransaction : UserControl
    {


        public static DailyTransaction Instance;
        public DailyTransaction()
        {
            InitializeComponent();

            Instance = this;

            Loaded += DailyTransaction_Loaded;
        }





        List<Models.DailyTransaction> Trans;
        List<Models.DailySlaughter> Slaughter_;
        private void DailyTransaction_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }


        public async void LoadData()
        {
            Trans = await new Models.Repositories.DailyTransactionRepository().List();
            Slaughter_ = await new Models.Repositories.DailySlaughterRepository().List();


            DataGridListTrans.ItemsSource = GetTrans(); ;
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


        private void btnADD_Click(object sender, RoutedEventArgs e)
        {
            AddContent.Visibility = Visibility.Visible;
            AddContent.Content = new NajahEpic.AddTransactionDaily();
        }

        private async void DataGridListTrans_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGridListTrans.SelectedItem == null) return;

            var item = (DailyTransactionVM)DataGridListTrans.SelectedItem;




            Models.DailyTransaction dailyTransaction = new Models.DailyTransaction
            {
                Date = item.Date,
                IdFromSlaughter = item.IdFromSlaughter,
                EndCash = item.EndCash,
                Id = item.Id
            };

            AddContent.Visibility = Visibility.Visible;
            AddContent.Content = new NajahEpic.AddTransactionDaily(dailyTransaction);

        }
    }
}
