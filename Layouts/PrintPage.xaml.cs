using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Documents.Serialization;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace Electricity_Subscriber.Layouts
{


    class PrintData
    {

        public string ID_SYS { get; set; }
        public string NameSubscriber { get; set; }
        public string NumberSubscriber { get; set; }
        public string PreviousAmount { get; set; }
        public string BillAmount { get; set; }
        public string PaidAmount { get; set; }
        public string TotalAmount { get; set; }
        public string SubscriberPaid { get; set; }
        public string PaidMethod { get; set; }
        public string NoteSubscriber { get; set; }
        public string SysCostID { get; set; }
    }
    /// <summary>
    /// Interaction logic for PrintPage.xaml
    /// </summary>
    public partial class PrintPage : Window
    {
        void UpdateCheckList()
        {






            List<PrintData> Data = new List<PrintData>();
            if (DTChecks.Rows.Count > 0 && DTChecks.Columns.Count > 0)
            {
                for (int i = 0; i < DTChecks.Rows.Count; i++)
                {
                    PrintData data = new PrintData();

                    data.ID_SYS = DTChecks.Rows[i]["ID_SYS"].ToString();
                    data.NameSubscriber = DTChecks.Rows[i]["NameSubscriber"].ToString();
                    data.NumberSubscriber = DTChecks.Rows[i]["NumberSubscriber"].ToString();
                    data.BillAmount = DTChecks.Rows[i]["BillAmount"].ToString();
                    data.PaidAmount = DTChecks.Rows[i]["PaidAmount"].ToString();
                    data.TotalAmount = DTChecks.Rows[i]["TotalAmount"].ToString();
                    data.PaidMethod = DTChecks.Rows[i]["PaidMethod"].ToString();
                    data.SubscriberPaid = DTChecks.Rows[i]["SubscriberPaid"].ToString(); 
                    data.SysCostID = DTChecks.Rows[i]["SysCostID"].ToString();
                    data.PreviousAmount = DTChecks.Rows[i]["PreviousAmount"].ToString();


                    Data.Add(data);
                }

                listprint.ItemsSource = null;
                listprint.ItemsSource = Data;

               
            }
            if (DTChecks.Rows.Count == 0)
            {
                listprint.ItemsSource = null;
               
            }


        }

        System.Data.DataTable DTChecks = new System.Data.DataTable();
        public PrintPage(string TypeAccount,string MonthSelected,System.Data.DataTable DtReport)
        {
            InitializeComponent();

            lblTypeAccount.Text = TypeAccount;
            lblMonthSelected.Text = MonthSelected;

            DTChecks = DtReport;
            Loaded += PrintPage_Loaded;

        }

        private void PrintPage_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                txtPrinter.Items.Add(printer);
            }


            if (System.IO.File.Exists("Printer.dll") == false)
            {
                System.IO.File.WriteAllText("Printer.dll", "");
            }

            else if (System.IO.File.Exists("Printer.dll"))
            {
                txtPrinter.Text = System.IO.File.ReadAllText("Printer.dll");
            }

            ////System.Data.DataView Dv = new System.Data.DataView(DTChecks);
            ////Dv.RowFilter = "Serial>0 and Serial<=50";



            //DTChecks = Dv.ToTable();



            UpdateCheckList();

        }

        public void Print(Visual v)
        {
            System.IO.File.WriteAllText("Printer.dll", txtPrinter.Text);

            System.Windows.FrameworkElement e = v as System.Windows.FrameworkElement;
            if (e == null)
                return;

            PrintDialog pd = new PrintDialog();
          //store original scale
                Transform originalScale = e.LayoutTransform;
                //get selected printer capabilities
                System.Printing.PrintCapabilities capabilities = pd.PrintQueue.GetPrintCapabilities(pd.PrintTicket);

                //get scale of the print wrt to screen of WPF visual
                double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / e.ActualWidth, capabilities.PageImageableArea.ExtentHeight /
                               e.ActualHeight);

                //Transform the Visual to scale
                e.LayoutTransform = new ScaleTransform(scale, scale);

                //get the size of the printer page
                System.Windows.Size sz = new System.Windows.Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);

                //update the layout of the visual to the printer page size.
                e.Measure(sz);
                e.Arrange(new System.Windows.Rect(new System.Windows.Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight), sz));


                PrintQueue queue = new LocalPrintServer().GetPrintQueue(txtPrinter.Text);

                pd.PrintQueue = queue;


                //now print the visual to printer to fit on the one page.
                pd.PrintVisual(v, "My Print");

                //apply the original transform.
                e.LayoutTransform = originalScale;

            this.Close();

        }










        private void PrintBtn_Click(object sender, RoutedEventArgs e)
        {

          
            Print(MainGrid);

            return;
            try
            {

                System.IO.File.WriteAllText("Printer.dll", txtPrinter.Text);

                PrintDialog printDialog = new PrintDialog();
                PrintQueue queue = new LocalPrintServer().GetPrintQueue(txtPrinter.Text);

                printDialog.PrintQueue = queue;

                printDialog.PageRangeSelection = PageRangeSelection.AllPages;

                printDialog.PrintVisual(MainGrid, "");


                return;

                ////this.IsEnabled = false;
                //PrintDialog printDialog = new PrintDialog();
                //printDialog.PageRangeSelection = PageRangeSelection.AllPages;
                //if (printDialog.ShowDialog() == true)
                //{

                //    printDialog.PrintVisual(MainGrid, "invoice");


                //}



            }
            finally
            {
                this.Close();
            }
        }



        private XpsDocument _xpsDocument;

        private XpsDocumentWriter GetSaveXpsDocumentWriter(string containerName)
        {
            File.Delete(containerName);

            _xpsDocument = new XpsDocument(containerName, FileAccess.ReadWrite);

            XpsDocumentWriter xpsdw = XpsDocument.CreateXpsDocumentWriter(_xpsDocument);

            return xpsdw;
        }



        public void SaveSingleVisual(Visual willSavedVisual, string containerPath)
        {
            XpsDocumentWriter xdwSave = GetSaveXpsDocumentWriter(containerPath);

            SaveVisual(xdwSave, willSavedVisual);
            _xpsDocument.Close();
        }

        private void SaveVisual(XpsDocumentWriter xpsdw, Visual v)
        {
            var element = v as System.Windows.FrameworkElement;
            element.Width = element.ActualWidth;
            element.Height = element.ActualHeight;
            var adjustedVisual = element as Visual;
            xpsdw.Write(adjustedVisual);
        }

       
    }
}
