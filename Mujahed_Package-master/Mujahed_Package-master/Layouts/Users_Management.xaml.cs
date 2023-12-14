using Mujahed_Package.CL;
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
    /// Interaction logic for Users_Management.xaml
    /// </summary>
    /// 


    class SalesManName
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string State { get; set; }
        public int SysID { get; set; }

        public bool StateBool { get; set; }




    }

    public partial class Users_Management : UserControl
    {

        System.Data.DataTable DTNameSalesMan = new System.Data.DataTable();
        
        public Users_Management()
        {
            InitializeComponent();
        }

        List<SalesManName> Sales = new List<SalesManName>();
        void UpdateCheckList()
        {
            DTNameSalesMan = new CL.SalesCash().GetSalesmanNameAll();

            Sales = new List<SalesManName>();

            if (DTNameSalesMan.Rows.Count > 0 && DTNameSalesMan.Columns.Count > 0)
            {
                for (int i = 0; i < DTNameSalesMan.Rows.Count; i++)
                {
                    SalesManName Sale = new SalesManName();

                    Sale.UserID = Validations.IsNumeric(DTNameSalesMan.Rows[i][0].ToString())?int.Parse( DTNameSalesMan.Rows[i][0].ToString()):0 ;
                    Sale.Username = DTNameSalesMan.Rows[i][1].ToString();
                    Sale.State = DTNameSalesMan.Rows[i][2].ToString();
                    Sale.SysID = Validations.IsNumeric(DTNameSalesMan.Rows[i][3].ToString()) ? int.Parse(DTNameSalesMan.Rows[i][3].ToString()) : 0;
                    Sale.StateBool = DTNameSalesMan.Rows[i][2].ToString()== "Active" ?true  : false;

                    Sales.Add(Sale);
                }
                ListUserNames.ItemsSource = null;
                ListUserNames.ItemsSource = Sales;
            }
            if (DTNameSalesMan.Rows.Count == 0)
            {
                ListUserNames.ItemsSource = null;
            }


        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            

            
            UpdateCheckList();
        }

        private void Btnchangestate_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var Check = button.CommandParameter as SalesManName;
            string StrState = Check.State;

            if (StrState == "Active")
            {
                new CL.SalesCash().UpdateSalesManState(Check.UserID.ToString(), "Deactive");
            }
            else if (StrState == "Deactive")
            {
                new CL.SalesCash().UpdateSalesManState(Check.UserID.ToString(), "Active");
            }
            else
            {
                new CL.SalesCash().UpdateSalesManState(Check.UserID.ToString(), "Active");
            }
            UpdateCheckList();
        }

        private void btnEditUsername_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var Check = button.CommandParameter as SalesManName;


            txtID.Text = Check.UserID.ToString();

            txtUserName.Text = Check.Username;

            txtState.Text = Check.State;

            GridEdit.Visibility = Visibility.Visible;
        }

        private void btnSaveEdit_Click(object sender, RoutedEventArgs e)
        {
            GridEdit.Visibility = Visibility.Collapsed;

            new CL.SalesCash().UpdateSalesManName(txtID.Text, txtUserName.Text);
            UpdateCheckList();
        }


        bool OrderAsc = true;
      
        private void UserID_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListUserNames.ItemsSource = null;

            if (OrderAsc) { ListUserNames.ItemsSource = Sales.OrderBy(i => i.UserID); OrderAsc = false; }

            else if (OrderAsc == false) { ListUserNames.ItemsSource = Sales.OrderByDescending(i => i.UserID); OrderAsc = true; }
        }

        private void Username_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListUserNames.ItemsSource = null;

            if (OrderAsc) { ListUserNames.ItemsSource = Sales.OrderBy(i => i.Username); OrderAsc = false; }

            else if (OrderAsc == false) { ListUserNames.ItemsSource = Sales.OrderByDescending(i => i.Username); OrderAsc = true; }
        }

        private void SysID_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListUserNames.ItemsSource = null;

            if (OrderAsc) { ListUserNames.ItemsSource = Sales.OrderBy(i => i.SysID); OrderAsc = false; }

            else if (OrderAsc == false) { ListUserNames.ItemsSource = Sales.OrderByDescending(i => i.SysID); OrderAsc = true; }
        }

        private void State_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListUserNames.ItemsSource = null;

            if (OrderAsc) { ListUserNames.ItemsSource = Sales.OrderBy(i => i.State); OrderAsc = false; }

            else if (OrderAsc == false) { ListUserNames.ItemsSource = Sales.OrderByDescending(i => i.State); OrderAsc = true; }
        }

        private void ChangeStyle_Click(object sender, RoutedEventArgs e)
        {
            if (listMujahedStyle.Visibility == Visibility.Visible) 
            {
                listMujahedStyle.Visibility = Visibility.Hidden;
                ListUserNames.Visibility = Visibility.Visible;
            }
            else
            {
                listMujahedStyle.Visibility = Visibility.Visible;
                ListUserNames.Visibility = Visibility.Hidden;
            }
        }

        private void chkActive_Checked(object sender, RoutedEventArgs e)
        {
            var button = sender as CheckBox;
            var Check = button.CommandParameter as SalesManName;
           
           new CL.SalesCash().UpdateSalesManState(Check.UserID.ToString(), "Active");
            
        }

        private void chkActive_Unchecked(object sender, RoutedEventArgs e)
        {
            var button = sender as CheckBox;
            var Check = button.CommandParameter as SalesManName;

            new CL.SalesCash().UpdateSalesManState(Check.UserID.ToString(), "Deactive");
        }

        private void unSelectAll_Click(object sender, RoutedEventArgs e)
        {
            new CL.SalesCash().ChangeStateForAll( "Deactive");
            UpdateCheckList();
        }

       
    }
}
