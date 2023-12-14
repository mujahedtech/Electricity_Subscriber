using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
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

namespace Mujahed_Package
{
    /// <summary>
    /// Interaction logic for ReportsWindow.xaml
    /// </summary>
    public partial class ReportsWindow : Window
    {
        public ReportsWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);


            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = e.ExceptionObject as Exception;
                MessageBox.Show(ex.Message, "Uncaught Thread Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show("Application Will ShutDown !!!", "ShutDown", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            catch (Exception)
            {


            }

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            reportViewer1.Reset();

            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Mujahed_Package.Report1.rdlc";
            StartReport("DS1");
        }

        void StartReport(string DataSet)
        {
            DataSet1 ds = GetData2();
            ReportDataSource datasource = new ReportDataSource(DataSet, ds.Tables[0]);

            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(datasource);
            this.reportViewer1.RefreshReport();
        }
        private DataSet1 GetData()
        {
            string Local = $"Provider=Microsoft.ACE.OLEDB.12.0;" +
                             $"Data Source=Khirat_DB.accdb;" +
                             $"Persist Security Info=True";
            string Colmuns = "IDNumber," +
                           "FORMAT(DateSales, 'Short Date') as DateSales," +
                           "SalesManName," +
                           "TotalZakah," +
                           "TotalRequiredCash," +
                           "TotalCashOnHand," +
                           "TotalEndCash," +
                           "Cash50,Cash20,Cash10,Cash5,Cash1,CashNon," +
                           "SalesNote,ImageCash";


            OleDbConnection con = new OleDbConnection(Local);
            OleDbCommand cmd = new OleDbCommand($"SELECT {Colmuns} FROM SalesMan", con);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet1 ds = new DataSet1();
            da.Fill(ds, ds.Tables[0].TableName);
            return ds;
        }

        private DataSet1 GetData2()
        {
            DataSet1 ds = new DataSet1();
            if (CL.PassParameters.DTReport.Rows.Count > 0 && CL.PassParameters.DTReport.Columns.Count > 0)
            {

                ds.Tables.Clear();
                ds.Tables.Add(CL.PassParameters.DTReport.Copy());
            }

           

         
          
            return ds;
        }
    }
}
