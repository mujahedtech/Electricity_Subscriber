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
    /// Interaction logic for SalesManReport.xaml
    /// </summary>
    /// 
    public class SalesManClass
    {
        public string SalesMan { get; set; }

    }

    public partial class SalesManReport : UserControl
    {


        System.Data.DataTable DTChecks = new System.Data.DataTable();

        public SalesManReport()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DTChecks.Columns.Count == 0)
            {
                DTChecks.Columns.Add("SalesMan", typeof(string));
            }

            RoutedEventArgs newEventArgs = new RoutedEventArgs(System.Windows.Controls.Button.ClickEvent);
            btnSearch.RaiseEvent(newEventArgs);
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {


            StartReport();


        }


        System.Data.DataTable ListSaleManInSysID = new System.Data.DataTable();
        System.Data.DataTable ListSaleManOut = new System.Data.DataTable();
        void StartReport()
        {

            System.Data.DataTable DTSalesMan = new CL.SalesCash().GetSalesmanName();

            DateTime dateTime = Convert.ToDateTime(txtdate.Text);



            ListSaleManInSysID = new CL.SalesCash().GetCashForReport(dateTime.ToString(CL.PassParameters.DateFormat));

            string code = "";


            //int counter_2 = DTCashs.Rows.Count;
            //for (int i = 0; i < DTCashs.Rows.Count; i++)
            //{
            //    if (counter_2 != i + 1)
            //    {
            //        code += "'" + DTCashs.Rows[i][0] + "',";
            //    }
            //    else if (counter_2 == i + 1)
            //    {
            //        code += "'" + DTCashs.Rows[i][0] + "'";

            //    }

            //}
            int counter_2 = ListSaleManInSysID.Rows.Count;
            for (int i = 0; i < ListSaleManInSysID.Rows.Count; i++)
            {
                if (counter_2 != i + 1)
                {
                    code += ListSaleManInSysID.Rows[i][0] + ",";
                }
                else if (counter_2 == i + 1)
                {
                    code += ListSaleManInSysID.Rows[i][0];

                }

            }
            if (code == "")
            {
                code = "null";
            }

          

            ListSaleManOut = new CL.SalesCash().GetSalesmanNameReport(code);


            listsalesman.ItemsSource= GetSalesManOut();
            lbllistsalesman.Text = listsalesman.Items.Count.ToString();



            listsalesmanIn.ItemsSource= GetSalesManIn();
            lbllistsalesmanIn.Text = listsalesmanIn.Items.Count.ToString();
        }
        
       List<SalesManClass> GetSalesManOut()
        {


            List<SalesManClass> salesManClasses = new List<SalesManClass>();
            if (ListSaleManOut.Rows.Count > 0 && ListSaleManOut.Columns.Count > 0)
            {
                for (int i = 0; i < ListSaleManOut.Rows.Count; i++)
                {
                    SalesManClass salesMan = new SalesManClass();
                    salesMan.SalesMan = ListSaleManOut.Rows[i][0].ToString();
                    salesManClasses.Add(salesMan);
                }
                

            }
            if (ListSaleManOut.Rows.Count == 0) salesManClasses = null;

            return salesManClasses;
        }
        List<SalesManClass> GetSalesManIn()
        {
           

            List<SalesManClass> salesManClasses = new List<SalesManClass>();
            if (ListSaleManInSysID.Rows.Count > 0 && ListSaleManInSysID.Columns.Count > 0)
            {
                for (int i = 0; i < ListSaleManInSysID.Rows.Count; i++)
                {
                    SalesManClass salesMan = new SalesManClass();
                    salesMan.SalesMan = ListSaleManInSysID.Rows[i][1].ToString();
                    salesManClasses.Add(salesMan);
                }
               

            }
            if (ListSaleManInSysID.Rows.Count == 0) salesManClasses = null;

            return salesManClasses;


        }



        private void btnplusday(object sender, RoutedEventArgs e)
        {
            DateTime dt = new DateTime();
            dt = Convert.ToDateTime(txtdate.Text);
            dt = dt.AddDays(1);
            txtdate.SelectedDate = dt;


            try
            {
                StartReport();
            }
            catch (Exception m)
            {

                MessageBox.Show(m.Message);
            }
        }

        private void btnlessday_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt = new DateTime();
            dt = Convert.ToDateTime(txtdate.Text);
            dt = dt.AddDays(-1);
            txtdate.SelectedDate = dt;

            try
            {
                StartReport();
            }
            catch (Exception m)
            {

                MessageBox.Show(m.Message);
            }
        }
    }
}
