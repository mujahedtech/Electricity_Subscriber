using Electricity_Subscriber.CL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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

namespace Electricity_Subscriber.Layouts
{
    public class PrintViewModel : ViewModelClass 
    {

        public List<SubscribeInfo> Subscribes { get; set; }

        public List<string> PrinterList { get => System.Drawing.Printing.PrinterSettings.InstalledPrinters.Cast<string>().ToList(); }


        private string _getselectprinter { get; set; } = System.IO.File.ReadAllText("Printer.dll");
        public string GetSelectPrinter
        {
            get
            {
                string str = "";
                if (System.IO.File.Exists("Printer.dll") == false)
                {
                    System.IO.File.WriteAllText("Printer.dll", "");
                }

                else if (System.IO.File.Exists("Printer.dll"))
                {
                    str = System.IO.File.ReadAllText("Printer.dll");
                }

                return str;
            }
            set
            {
                _getselectprinter = value;

                OnPropertyChanged(nameof(GetSelectPrinter));
            }
        }


        public ICommand PrintCommand
        {
            get
            {
                return new RelayCommand(x =>
                {

                    System.IO.File.WriteAllText("Printer.dll", _getselectprinter);

                    PrintSingle.Instance.Print(PrintSingle.Instance.PrintContent);

                });
            }
        }

        public string NameSubscribe { get; set; }
        public string IdSubscribe { get; set; }

        public PrintViewModel(List<SubscribeInfo> subscribes,string namesub,string idsub)
        {
            Subscribes = subscribes;

            NameSubscribe=namesub; IdSubscribe=idsub;
        }
    }

    /// <summary>
    /// Interaction logic for PrintSingle.xaml
    /// </summary>
    public partial class PrintSingle : Window
    {

        public static PrintSingle Instance;

        int Count = 0;
        public PrintSingle(List<SubscribeInfo> subscribes,string name,string id)
        {
            InitializeComponent();

            Instance = this;

            Count= subscribes.Count;

            DataContext = new PrintViewModel(subscribes,name,id);
        }

        public void Print(Visual v)
        {
           

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


            if (Count <= 25)
            {
                //Transform the Visual to scale
                e.LayoutTransform = new ScaleTransform(.9, 1);
            }
            else
            {
                double SizeCustom = 0;
                if (PrintSize.Text.Length > 0)
                {

                    //SizeCustom = double.Parse(PrintSize.Text);
                    SizeCustom = 60;

                    SizeCustom -= (Count/1.2) - 25;



                    SizeCustom = SizeCustom / 100;
                    e.LayoutTransform = new ScaleTransform(SizeCustom, SizeCustom);
                }
                else
                { //Transform the Visual to scale
                    e.LayoutTransform = new ScaleTransform(1, 1);

                }


            }







            //get the size of the printer page
            System.Windows.Size sz = new System.Windows.Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);

            //update the layout of the visual to the printer page size.
            e.Measure(sz);
            e.Arrange(new System.Windows.Rect(new System.Windows.Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight), sz));


            PrintQueue queue = new LocalPrintServer().GetPrintQueue(System.IO.File.ReadAllText("Printer.dll"));

            pd.PrintQueue = queue;


            //now print the visual to printer to fit on the one page.
            pd.PrintVisual(v, "My Print");

            //apply the original transform.
            e.LayoutTransform = originalScale;

            this.Close();

        }

    }
}
