using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mujahed_Package.Layouts
{
    /// <summary>
    /// Interaction logic for MatchCashBill_Money.xaml
    /// </summary>

    public class ExcelManage
    {

        public static string BillPath = "BillPath.txt";


        public static string BillSheetName = "BillSheetName.txt";

        public static string CashPath = "CashPath.txt";

        public static string CashSheetName = "CashSheetName.txt";

        
        public static string FolderName = "ExcelSetting";

    }


    public partial class MatchCashBill_Money : UserControl
    {
        void getSheetBill(string path)
        {
            String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                                   "" + path + "" +
                                   ";Extended Properties='Excel 8.0;HDR=YES;';";

            OleDbConnection con = new OleDbConnection(constr);
            con.Open();
            System.Data.DataTable DT = new DataTable();
            DT = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            con.Close();



            txtSheetBill.Items.Clear();
            foreach (DataRow Row in DT.Rows)
            {
                txtSheetBill.Items.Add(Row["TABLE_NAME"].ToString().Replace("$", ""));


            }
            txtSheetBill.IsEnabled = false;
            if (txtSheetBill.Items.Count > 0)
            {
                txtSheetBill.IsEnabled = true;
                txtSheetBill.SelectedIndex = 0;

            }
        }

        void getSheetCash(string path)
        {
            String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                                   "" + path + "" +
                                   ";Extended Properties='Excel 8.0;HDR=YES;';";

            OleDbConnection con = new OleDbConnection(constr);
            con.Open();
            System.Data.DataTable DT = new DataTable();
            DT = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            con.Close();



            txtSheetCash.Items.Clear();
            foreach (DataRow Row in DT.Rows)
            {
                txtSheetCash.Items.Add(Row["TABLE_NAME"].ToString().Replace("$", ""));


            }
            txtSheetCash.IsEnabled = false;
            if (txtSheetCash.Items.Count > 0)
            {
                txtSheetCash.IsEnabled = true;
                txtSheetCash.SelectedIndex = 0;

            }
        }

        public DataTable Import_Excel(string Name_Path,string SheetName)
        {
            DataTable DataTable = new DataTable();

            String name = SheetName;
            String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            $"{Name_Path}" +
                            ";Extended Properties='Excel 8.0;HDR=YES;';";

            OleDbConnection con = new OleDbConnection(constr);
            OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", con);
            con.Open();

            OleDbDataAdapter sda = new OleDbDataAdapter(oconn);

            sda.Fill(DataTable);

            return DataTable;

        }

            //تغير مكان قاعدة البيانات
            System.Windows.Forms.OpenFileDialog FBD = new System.Windows.Forms.OpenFileDialog();
        public MatchCashBill_Money()
        {

            InitializeComponent();

            FBD.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm|All files (*.*)|*.*";


            CheckFolderPaths();
            GetPathData();
        }

        
        private void btnopenEXcel_Click(object sender, RoutedEventArgs e)
        {
           
            
            if (FBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtBillPath.Text = FBD.FileName;


                getSheetBill(txtBillPath.Text);
            }
        }

        private void btnopenExcelCash_Click(object sender, RoutedEventArgs e)
        {
            if (FBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtCashPath.Text = FBD.FileName;


                getSheetCash(txtCashPath.Text);
            }
        }

        private void CashSave_Click(object sender, RoutedEventArgs e)
        {
            UpdatePath(ExcelManage.CashPath, txtCashPath.Text);
            UpdatePath(ExcelManage.CashSheetName, txtSheetCash.Text);
        }

       
        private void BillSave_Click(object sender, RoutedEventArgs e)
        {
            

            UpdatePath(ExcelManage.BillPath,txtBillPath.Text);
            UpdatePath(ExcelManage.BillSheetName, txtSheetBill.Text);
        }


        //عرض تقرير نهائي بالقبوضات و الفواتير
        DataTable FinalData(DataTable Bills, ItemsControl Cash)
        {
            
           

            DataTable DTBills = Bills;
            DataTable DTCash = ((System.Data.DataView)Cash.ItemsSource).ToTable();

           

           

           

            var results = from Bill in DTBills.AsEnumerable()
                          join Cash_ in DTCash.AsEnumerable() on (string)Bill["AccountName"] equals (string)Cash_["AccountName"] into CashTable 
                          
                          from img in CashTable.DefaultIfEmpty()
                          select new
                          {
                              AccountNameStr = (string)Bill["AccountNameStr"],

                              AccountName = (string)Bill["AccountName"],
                              BillAmount = (double)Bill["BillAmount"],
                          

                              CashAmount = double.Parse((img != null ? img["CashAmount"] : "0").ToString()),
                              Total = ((double)Bill["BillAmount"] - double.Parse((img != null ? img["CashAmount"] : "0").ToString())).ToString("0.00"),

                              Color = ((double)Bill["BillAmount"] - double.Parse((img != null ? img["CashAmount"] : "0").ToString()))>0?"Red": "#FF2196F3"

                          };

            return ConvertToDataTable(results);
        }


        //تقوم بجلب البيانات من ملفات الخاصة بمكان الاكسل و اسم الصفحات
        void GetPathData()
        {

            try
            {
                txtBillPath.Text = "";
                txtSheetBill.Text = "";
                txtCashPath.Text = "";
                txtSheetCash.Text = "";
                bool CheckFinalData = false;
                if (System.IO.File.Exists(System.IO.Path.Combine(ExcelManage.FolderName, ExcelManage.BillPath)))
                {
                    txtBillPath.Text = System.IO.File.ReadAllText(System.IO.Path.Combine(ExcelManage.FolderName, ExcelManage.BillPath));
                    getSheetBill(txtBillPath.Text);
                    CheckFinalData = true;
                }
                CheckFinalData = false;
                if (System.IO.File.Exists(System.IO.Path.Combine(ExcelManage.FolderName, ExcelManage.BillSheetName)))
                {
                    txtSheetBill.Text = System.IO.File.ReadAllText(System.IO.Path.Combine(ExcelManage.FolderName, ExcelManage.BillSheetName));

                    var data= FinalDataBills(ClearDataBills(Import_Excel(txtBillPath.Text, txtSheetBill.Text)));

                    data.Columns.RemoveAt(3);

                    GridBill.ItemsSource = data.DefaultView;
                    CheckFinalData = true;
                }
                CheckFinalData = false;
                if (System.IO.File.Exists(System.IO.Path.Combine(ExcelManage.FolderName, ExcelManage.CashPath)))
                {
                    txtCashPath.Text = System.IO.File.ReadAllText(System.IO.Path.Combine(ExcelManage.FolderName, ExcelManage.CashPath));
                    getSheetCash(txtCashPath.Text);
                    CheckFinalData = true;
                }
                CheckFinalData = false;
                if (System.IO.File.Exists(System.IO.Path.Combine(ExcelManage.FolderName, ExcelManage.CashSheetName)))
                {
                    txtSheetCash.Text = System.IO.File.ReadAllText(System.IO.Path.Combine(ExcelManage.FolderName, ExcelManage.CashSheetName));

                    GridCash.ItemsSource = FinalDataCash(ClearDataCash(Import_Excel(txtCashPath.Text, txtSheetCash.Text))).DefaultView;
                    CheckFinalData = true;
                }
                if (CheckFinalData)
                {
                    GridFinal.ItemsSource = FinalData(FinalDataBills(ClearDataBills(Import_Excel(txtBillPath.Text, txtSheetBill.Text))), GridCash).DefaultView;


                    GridFinal.Columns.RemoveAt(5);
                }
            }
            catch (Exception m)
            {

                MessageBox.Show(m.Message);
            }

           
           


        }




        DataTable FinalDataBills(DataTable data)
        {

            var result = from r in data.AsEnumerable()
                         group r by new { SizeCode = r["AccountName"] , DateSelect = r["Date"],accountNamestr=r["AccountNameStr"] } into g
                         select new
                         {
                             AccountName = g.Key.SizeCode,
                             BillAmount = g.Sum(x => Convert.ToDouble(x["BillAmount"])),
                             Date=DateTime.Parse( g.Key.DateSelect.ToString()).ToString("dd/MM/yyyy"),
                             AccountNameStr= g.Key.accountNamestr.ToString()
                         };


            return ConvertToDataTable(result);
        }
        DataTable FinalDataCash(DataTable data)
        {

            var result = from r in data.AsEnumerable()
                         group r by new { SizeCode = r["AccountName"] , DateSelect = r["Date"] } into g
                         select new
                         {
                             AccountName = g.Key.SizeCode,
                             CashAmount = g.Sum(x => Convert.ToDouble(x["CashAmount"])),
                             Date = DateTime.Parse(g.Key.DateSelect.ToString()).ToString("dd/MM/yyyy")
                         };


            return ConvertToDataTable(result);
        }

        DataTable ClearDataBills(DataTable data)
        {
            if (data.Columns.Count > 0)
            {
                for (int i = 0; i < 16; i++)
                {
                    data.Columns.RemoveAt(0);
                }

                for (int i = 0; i < 5; i++)
                {
                    data.Columns.RemoveAt(1);
                }

                for (int i = 0; i < 3; i++)
                {
                    data.Columns.RemoveAt(2);
                }

                for (int i = 0; i < 4; i++)
                {
                    data.Columns.RemoveAt(4);
                }

                //for (int i = 0; i < data.Columns.Count+1; i++)
                //{
                //    data.Columns.RemoveAt(3);
                //}

                data.Columns[0].ColumnName = "BillAmount";
                data.Columns[1].ColumnName = "AccountNameStr";
                data.Columns[2].ColumnName = "AccountName";
                data.Columns[3].ColumnName = "Date";

                DataView dv = new DataView(data);
                dv.RowFilter = "AccountName <> '' and BillAmount >0";

                dv.Sort = "AccountName";

                data = dv.ToTable();

               

            }
            return data;
        }


        DataTable ClearDataCash(DataTable data)
        {
            if (data.Columns.Count > 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    data.Columns.RemoveAt(0);
                }

                for (int i = 0; i < 5; i++)
                {
                    data.Columns.RemoveAt(1);
                }
                for (int i = 0; i < data.Columns.Count + 2; i++)
                {
                    data.Columns.RemoveAt(3);
                }
                data.Columns[0].ColumnName = "CashAmount";
                data.Columns[1].ColumnName = "AccountName";
                data.Columns[2].ColumnName = "Date";

                DataView dv = new DataView(data);
                dv.RowFilter = "AccountName <> '' and CashAmount >0";

                dv.Sort = "AccountName";

                data = dv.ToTable();



            }
            return data;
        }


        DataTable ConvertToDataTable<TSource>(IEnumerable<TSource> source)
        {
            var props = typeof(TSource).GetProperties();

            var dt = new DataTable();
            dt.Columns.AddRange(
              props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray()
            );

            source.ToList().ForEach(
              i => dt.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray())
            );

            return dt;
        }



        //هنا يتم تحديث الملفات الخاصة بالاكسل
        void UpdatePath(string Path,string Value)
        {
            System.IO.File.WriteAllText(System.IO.Path.Combine(ExcelManage.FolderName, Path),"");
            System.IO.File.WriteAllText(System.IO.Path.Combine(ExcelManage.FolderName, Path), Value);
        }

        //هنا يتم تعريف الملف الخاص بتعريفات الاكسل
        void CheckFolderPaths()
        {
            if (System.IO.Directory.Exists(ExcelManage.FolderName)) return;
            System.IO.Directory.CreateDirectory(ExcelManage.FolderName);


        }

        private void checkTotal_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ExcelFile_Click(object sender, RoutedEventArgs e)
        {
            new CL.ExportExcel().ExportToExcel(((System.Data.DataView)GridFinal.ItemsSource).ToTable());
        }
    }
}
