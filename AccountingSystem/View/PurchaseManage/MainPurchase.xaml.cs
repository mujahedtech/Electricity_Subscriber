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

namespace AccountingSystem.View.PurchaseManage
{
    public class TransactionAccountingVM : TransactionAccounting
    {

        public string AccountName { get => App.AccountList.Where(i => i.Id == AccountId).FirstOrDefault().AccountName; }

        public string TransactionType { get => App.GetAccountTransStr(TransType); }

        public string InvName { get => App.InventoryList.Where(i => i.Id == InventoryId).FirstOrDefault().InvName; }



    }

    /// <summary>
    /// Interaction logic for MainPurchase.xaml
    /// </summary>
    public partial class MainPurchase : UserControl
    {
        public static MainPurchase Instance;
        public MainPurchase()
        {
            InitializeComponent();

            Instance = this;

            Loaded += MainPurchase_Loaded;
        }
        List<TransactionAccounting> TransList;
        private async void MainPurchase_Loaded(object sender, RoutedEventArgs e)
        {

            TransList = await new Models.Repositories.TransactionAccountingRepository().ListPerTransType((int)App.AccountTrans.Purchase);

            DataGridListTrans.ItemsSource = GetTrans();
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

        private void btnADD_Click(object sender, RoutedEventArgs e)
        {
            AddContent.Visibility = Visibility.Visible;
            AddContent.Content = new PurchaseManage.AddPurchase();

        }

        private void DataGridListTrans_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGridListTrans.SelectedItem == null) return;

            var item = (TransactionAccountingVM)DataGridListTrans.SelectedItem;

            Models.TransactionAccounting transactionAccounting = new Models.TransactionAccounting
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

            AddContent.Visibility = Visibility.Visible;
            AddContent.Content = new PurchaseManage.AddPurchase(transactionAccounting);
        }
    }
}
