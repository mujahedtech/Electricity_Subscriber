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

            Loaded += SelectAccount_Loaded;
        }

        private void SelectAccount_Loaded(object sender, RoutedEventArgs e)
        {
            ListAccount.ItemsSource = App.AccountList.ToList();

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
            var SearchList = App.AccountList.ToList();

            SearchList = SearchList.Where(i => i.AccountName.Contains(txtSearch.Text)).ToList();


            ListAccount.ItemsSource = SearchList;
        }
    }
}
