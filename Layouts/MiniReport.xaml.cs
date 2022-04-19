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

namespace Electricity_Subscriber.Layouts
{


    public class MiniClassReport 
    {
    
        public string TypeName { get; set; }

        public string TypeAmount { get; set; }

        public string RowColor { get; set; }

        public string TextColor { get; set; }

        

    }

    /// <summary>
    /// Interaction logic for MiniReport.xaml
    /// </summary>
    public partial class MiniReport : UserControl
    {
        void UpdateCheckList()
        {
            List<MiniClassReport> checks = new List<MiniClassReport>();
            if (DTChecks.Rows.Count > 0 && DTChecks.Columns.Count > 0)
            {
                for (int i = 0; i < DTChecks.Rows.Count; i++)
                {
                    MiniClassReport check = new MiniClassReport();

                    check.TypeName = DTChecks.Rows[i]["type"].ToString();
                    check.TypeAmount = DTChecks.Rows[i]["amount"].ToString();

                    check.RowColor = "#FF90CAF9";
                    check.TextColor = "Black";
                   

                    checks.Add(check);
                }

                listReport.ItemsSource = null;
                listReport.ItemsSource = checks;

              
            }
            if (DTChecks.Rows.Count == 0)
            {
                listReport.ItemsSource = null;
               
            }


        }


        System.Data.DataTable DTChecks;
        public MiniReport()
        {
            InitializeComponent();

            this.Loaded += MiniReport_Loaded;  
        }

        private void MiniReport_Loaded(object sender, RoutedEventArgs e)
        {
            DTChecks = CL.Pass.DataTable;

           
            UpdateCheckList();
        }
    }
}
