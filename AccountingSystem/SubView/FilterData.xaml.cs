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
    /// Interaction logic for FilterData.xaml
    /// </summary>
    public partial class FilterData : Window
    {
        public FilterData()
        {
            InitializeComponent();
        }



        public List<TransactionAccounting> transList;
        public FilterData(List<Models.TransactionAccounting> _transList)
        {
            InitializeComponent();


            transList = _transList;

        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {

            transList = transList.Where(i => i.DateEntered >= txtDatefrom.SelectedDate.Value && i.DateEntered <= txtDateto.SelectedDate.Value).OrderBy(i => i.DateEntered).ToList();
            Close();
        }
    }
}
