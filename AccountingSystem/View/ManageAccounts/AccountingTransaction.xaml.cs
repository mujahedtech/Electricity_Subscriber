using AccountingSystem.View.ManageAccounts.Transaction;
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
    /// Interaction logic for AccountingTransaction.xaml
    /// </summary>
    public partial class AccountingTransaction : UserControl
    {
        public AccountingTransaction()
        {
            InitializeComponent();
        }





     
              
        private void btnMainButtons(object sender, RoutedEventArgs e)
        {

            int index = int.Parse(((Button)e.Source).Uid);


            //GridCursor.Margin = new Thickness(5 + (140 * index), 0, 0, 0);



            switch (index)
            {
                case 0:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new PurchaseManage.MainPurchase(accountTrans:App.AccountTrans.Purchase));



                    break;
                case 1:
                    GridMain.Children.Clear();

                   
                    GridMain.Children.Add(new ManageAccounts.Transaction.RecepitManage());

                    break;
                case 2:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new ManageAccounts.Transaction.ExpenseManage());

                    break;
                case 3:
                    GridMain.Children.Clear();
                  
                    break;
                case 4:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new ManageAccounts.AccountStatement());
                    break;
                case 5:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new Reports.MainReports());

                    break;
                case 6:
                    GridMain.Children.Clear();

                    break;
            }

        }

    }
}
