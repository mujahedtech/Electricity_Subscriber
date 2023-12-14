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

namespace Mujahed_Package.Layouts
{
    /// <summary>
    /// Interaction logic for CashReportManual.xaml
    /// </summary>
    /// 
    public class ClManualReport 
    {
        public string ID { get; set; }
        public string DateReport { get; set; }
        public string Username { get; set; }
        public string State { get; set; }
        public string RowColor { get; set; }
        public string TextColor { get; set; }
    }

    public partial class CashReportManual : UserControl
    {
        public CashReportManual()
        {
            InitializeComponent();
        }

        private void btnplusday(object sender, RoutedEventArgs e)
        {
            DateTime dt = new DateTime();
            dt = Convert.ToDateTime(txtdate.Text);
            dt = dt.AddDays(1);
            txtdate.SelectedDate = dt;

            UpdateReport();

        }

        private void btnlessday_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt = new DateTime();
            dt = Convert.ToDateTime(txtdate.Text);
            dt = dt.AddDays(-1);
            txtdate.SelectedDate = dt;

            UpdateReport();

        }

        void UpdateReport()
        {
            DateTime dateTime = Convert.ToDateTime(txtdate.Text);

            new CL.ReportManual().InsertNewReports(dateTime.ToShortDateString(), dateTime.ToString(CL.PassParameters.DateFormat));

            UpdateCheckList(true);
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            UpdateReport();
        }

        System.Data.DataTable DTReport;
        void UpdateCheckList(bool Code)
        {
            DateTime dateTime = Convert.ToDateTime(txtdate.Text);


            DTReport = new CL.ReportManual().GetReport(dateTime.ToString(CL.PassParameters.DateFormat));
            if (Code==false)
            {
                System.Data.DataView DV = new System.Data.DataView(DTReport);
                DV.RowFilter = "State='Null'";
                DTReport = DV.ToTable();
            }
            List<ClManualReport> Reports = new List<ClManualReport>();
            if (DTReport.Rows.Count > 0 && DTReport.Columns.Count > 0)
            {
                for (int i = 0; i < DTReport.Rows.Count; i++)
                {
                    ClManualReport Report = new ClManualReport();
                   
                    Report.ID = DTReport.Rows[i][0].ToString();
                    Report.DateReport = DTReport.Rows[i][1].ToString();
                    Report.Username = DTReport.Rows[i][2].ToString();
                    Report.State = DTReport.Rows[i][3].ToString();
                    if (DTReport.Rows[i][3].ToString()=="Done")
                    {
                        Report.RowColor = "#FF90CAF9";
                        Report.TextColor = "Black";
                    }
                    else if (DTReport.Rows[i][3].ToString() == "Null")
                    {
                        Report.RowColor = "#FF2C6CAC";
                        Report.TextColor = "White";
                    }

                    Reports.Add(Report);
                }
                lbEurInsuredType.ItemsSource = null;
                lbEurInsuredType.ItemsSource = Reports;
            }
            if (DTReport.Rows.Count == 0)
            {
                lbEurInsuredType.ItemsSource = null;
            }


        }

        private void btnchangestate_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var Report = button.CommandParameter as ClManualReport;
            string StrState = Report.State;

            if (StrState == "Null")
            {
                new CL.ReportManual().UpdateReportState(Report.ID, "Done");
            }
            else if (StrState == "Done")
            {
                new CL.ReportManual().UpdateReportState(Report.ID, "Null");
            }
            else
            {
                new CL.ReportManual().UpdateReportState(Report.ID, "Null");
            }
            UpdateCheckList(true);
        }

        private void btnCashLess_Click(object sender, RoutedEventArgs e)
        {
            UpdateCheckList(false);
        }
    }
}
