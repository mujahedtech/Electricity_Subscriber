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
using System.Windows.Shapes;

namespace AccountingSystem.SubView
{
    /// <summary>
    /// Interaction logic for SelectAccount.xaml
    /// </summary>
    public partial class SelectAccount : Window
    {
        public SelectAccount()
        {
            InitializeComponent();

            Close();
        }

        List<AccountsTable> Accounts;
        public SelectAccount(List<AccountsTable> accounts)
        {
            InitializeComponent();

            Accounts = accounts;

            ListAccount.ItemsSource = Accounts;
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.Key == Key.Escape) Close();
            base.OnKeyUp(e);
        }


      

        public AccountsTable AccountsSelected()
        {
            var item = (AccountsTable)ListAccount.SelectedItem;


            return item;




        }

        private void ListAccount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Close();

        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var SearchList = Accounts;

            SearchList = SearchList.Where(i => i.AccountName.Contains(txtSearch.Text)).ToList();


            ListAccount.ItemsSource = SearchList;
        }
    }
}
