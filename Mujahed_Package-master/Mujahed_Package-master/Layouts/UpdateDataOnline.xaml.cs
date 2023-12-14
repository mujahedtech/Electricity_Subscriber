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
    /// Interaction logic for UpdateDataOnline.xaml
    /// </summary>
    public partial class UpdateDataOnline : UserControl
    {
        public UpdateDataOnline()
        {
            InitializeComponent();
        }
        System.Data.DataTable DTData;
        FlowDocument myFlowDoc = new FlowDocument();
        void GetSalesData()
        {
            DTData = new CL.SalesCash().GetCash();
            if (DTData.Rows.Count > 0)
            {
                var myText = new TextRange(txtCode.Document.ContentStart, txtCode.Document.ContentEnd);
               

                if (myText.Text.Length > 0)
                {
                    // Add paragraphs to the FlowDocument.
                    myFlowDoc.Blocks.Add(new Paragraph(new Run("delete from SalesMan;")));
                    myFlowDoc.Blocks.Add(new Paragraph(new Run("INSERT INTO `SalesMan` (`IDNumber`, `DateSales`, `SalesManName`, `TotalZakah`, `TotalRequiredCash`, `TotalCashOnHand`, `TotalEndCash`, `Cash50`, `Cash20`, `Cash10`, `Cash5`, `Cash1`, `CashNon`, `SalesNote`, `ImageCash`) VALUES")));

                    txtCode.Document = myFlowDoc;
                }




                for (int i = 0; i < DTData.Rows.Count; i++)
                {
                    if (i + 1 == DTData.Rows.Count)
                    {
                        myFlowDoc.Blocks.Add(new Paragraph(new Run($"({DTData.Rows[i][0]}," +
                                                              $" '{DTData.Rows[i][1]}'," +
                                                              $" '{DTData.Rows[i][2]}'," +
                                                              $" '{DTData.Rows[i][3]}'," +
                                                              $" '{DTData.Rows[i][4]}'," +
                                                              $" '{DTData.Rows[i][5]}'," +
                                                              $" '{DTData.Rows[i][6]}', " +
                                                              $" '{DTData.Rows[i][7]}'," +
                                                              $" '{DTData.Rows[i][8]}'," +
                                                              $" '{DTData.Rows[i][9]}'," +
                                                              $" '{DTData.Rows[i][10]}'," +
                                                              $" '{DTData.Rows[i][11]}', " +
                                                              $" '{DTData.Rows[i][12]}', " +
                                                              $" '{DTData.Rows[i][13]}'," +
                                                              $" '{DTData.Rows[i][14]}');")));
                        txtCode.Document = myFlowDoc;
                    }
                    else if (i < DTData.Rows.Count)
                    {
                        myFlowDoc.Blocks.Add(new Paragraph(new Run($"({DTData.Rows[i][0]}," +
                                                              $" '{DTData.Rows[i][1]}'," +
                                                              $" '{DTData.Rows[i][2]}'," +
                                                              $" '{DTData.Rows[i][3]}'," +
                                                              $" '{DTData.Rows[i][4]}'," +
                                                              $" '{DTData.Rows[i][5]}'," +
                                                              $" '{DTData.Rows[i][6]}', " +
                                                              $" '{DTData.Rows[i][7]}'," +
                                                              $" '{DTData.Rows[i][8]}'," +
                                                              $" '{DTData.Rows[i][9]}'," +
                                                              $" '{DTData.Rows[i][10]}'," +
                                                              $" '{DTData.Rows[i][11]}', " +
                                                              $" '{DTData.Rows[i][12]}', " +
                                                              $" '{DTData.Rows[i][13]}'," +
                                                              $" '{DTData.Rows[i][14]}'),")));
                        txtCode.Document = myFlowDoc;
                    }

                }
          
            }
           
        }

        void GetCashOut()
        {
            DTData = new CL.SalesCash().GetZakah();
            if (DTData.Rows.Count > 0)
            {
                var myText = new TextRange(txtCode.Document.ContentStart, txtCode.Document.ContentEnd);
              

                if (myText.Text.Length > 0)
                {
                    // Add paragraphs to the FlowDocument.
                    myFlowDoc.Blocks.Add(new Paragraph(new Run("delete from ZakahCash;")));
                    myFlowDoc.Blocks.Add(new Paragraph(new Run("INSERT INTO `ZakahCash` (`IDNumber`, `ZakahAmount`,`ZakahNote`) VALUES")));

                    txtCode.Document = myFlowDoc;
                }




                for (int i = 0; i < DTData.Rows.Count; i++)
                {
                    if (i + 1 == DTData.Rows.Count)
                    {
                        myFlowDoc.Blocks.Add(new Paragraph(new Run($"({DTData.Rows[i][0]}," +
                                                              $" '{DTData.Rows[i][1]}'," +
                                                              $" '{DTData.Rows[i][2]}');")));
                        txtCode.Document = myFlowDoc;
                    }
                    else if (i < DTData.Rows.Count)
                    {
                        myFlowDoc.Blocks.Add(new Paragraph(new Run($"({DTData.Rows[i][0]}," +
                                                              $" '{DTData.Rows[i][1]}'," +
                                                              $" '{DTData.Rows[i][2]}'),")));
                        txtCode.Document = myFlowDoc;
                    }

                }

            }

        }


        void GetSalesMan()
        {
            DTData = new CL.SalesCash().GetSalesmanNameAll();
            if (DTData.Rows.Count > 0)
            {
                var myText = new TextRange(txtCode.Document.ContentStart, txtCode.Document.ContentEnd);
              

                if (myText.Text.Length > 0)
                {
                    // Add paragraphs to the FlowDocument.
                    
                    myFlowDoc.Blocks.Add(new Paragraph(new Run("delete from NameSalesMan;")));
                    myFlowDoc.Blocks.Add(new Paragraph(new Run("INSERT INTO `NameSalesMan` (`UserID`, `Username`, `State`,`SYSID`) VALUES")));

                    txtCode.Document = myFlowDoc;
                }




                for (int i = 0; i < DTData.Rows.Count; i++)
                {
                    if (i + 1 == DTData.Rows.Count)
                    {
                        myFlowDoc.Blocks.Add(new Paragraph(new Run($"({DTData.Rows[i][0]}," +
                                                              $" '{DTData.Rows[i][1]}'," +
                                                              $" '{DTData.Rows[i][2]}'," +
                                                              $"  {DTData.Rows[i][3]});")));
                        txtCode.Document = myFlowDoc;
                    }
                    else if (i < DTData.Rows.Count)
                    {
                        myFlowDoc.Blocks.Add(new Paragraph(new Run($"({DTData.Rows[i][0]}," +
                                                              $" '{DTData.Rows[i][1]}'," +
                                                              $" '{DTData.Rows[i][2]}'," +
                                                              $"  {DTData.Rows[i][3]}),")));
                        txtCode.Document = myFlowDoc;
                    }

                }

            }

        }

        void UpdateData()
        {
            var myText = new TextRange(txtCode.Document.ContentStart, txtCode.Document.ContentEnd);

            //new OnlineDataBase.QueryOnline().UpdateDataBase(myText.Text);


        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            int Index = int.Parse(((Button)e.Source).Uid);

            switch (Index)
            {
                case 0:
                    myFlowDoc = new FlowDocument();
                    GetSalesData();
                    UpdateData();
                    break;
                case 1:
                    myFlowDoc = new FlowDocument();
                    GetCashOut();
                    UpdateData();
                    break;
                case 2:
                    myFlowDoc = new FlowDocument();
                    GetSalesMan();
                    UpdateData();
                    break;
                case 3:
                    UpdateData();
                    break;
                case 4:
                    GetSalesData();
                    GetCashOut();
                    GetSalesMan();
                    UpdateData();
                    break;
            }


           
        }
    }
}
