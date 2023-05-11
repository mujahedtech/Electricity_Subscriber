using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop;

namespace Electricity_Subscriber.CL
{
    class ExportExcel
    {
        private string GetExcelColumnName(int columnNumber)
        {
            string columnName = "";

            while (columnNumber > 0)
            {
                int modulo = (columnNumber - 1) % 26;
                columnName = Convert.ToChar('A' + modulo) + columnName;
                columnNumber = (columnNumber - modulo) / 26;
            }

            return columnName;
        }

        public string ExportToExcel(System.Data.DataTable DTForExcel,bool SaveAs=false,string SaveAsPath="")
        {
            string Message = "";

            if (DTForExcel.Rows.Count > 0)
            {

                int Count_Rows = 0;
                int Count_Columns = 0;
                Count_Rows = DTForExcel.Rows.Count;

                Count_Columns = DTForExcel.Columns.Count;

                Microsoft.Office.Interop.Excel.Application XLS = new Microsoft.Office.Interop.Excel.Application();
                Workbook WB = XLS.Workbooks.Add(XlSheetType.xlWorksheet);
                Worksheet WS = (Worksheet)XLS.ActiveSheet;

                WS.DisplayRightToLeft = true;
                //عنوان الجدول
                Microsoft.Office.Interop.Excel.Range Header;
                Header = WS.Range["A1", $"{GetExcelColumnName(DTForExcel.Columns.Count)}1"];
                Header.Font.Bold = true;
                Header.Rows.RowHeight = 30;
                Header.Interior.Color = XlRgbColor.rgbLightGray;

                Header.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                Header.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                Header.Columns.Borders.Color = System.Drawing.Color.Black;




                //تنسيق الجدول
                Range RA;

                Count_Rows += 1;
                RA = WS.Range["A2", $"{GetExcelColumnName(DTForExcel.Columns.Count)}" + Count_Rows];
                WS.Columns.Font.Size = 15;
                RA.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                RA.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                RA.Columns.Borders.Color = System.Drawing.Color.Black;

                RA.RowHeight = 25;

                for (int i = 0; i < DTForExcel.Columns.Count; i++)
                {
                    XLS.Cells[1, i + 1] = DTForExcel.Columns[i].Caption;
                }
                for (int i = 0; i < DTForExcel.Rows.Count; i++)
                {
                    for (int j = 0; j < DTForExcel.Columns.Count; j++)
                    {

                        XLS.Cells[i + 2, j + 1] = DTForExcel.Rows[i][j].ToString();
                    }

                }

                string FolderName = AppDomain.CurrentDomain.BaseDirectory+"ExcelFiles";

                string filname = FolderName+ @"\filename.xlsx";

           

                XLS.WindowState = XlWindowState.xlMaximized;
                XLS.Columns.AutoFit();

                if (SaveAs)
                {
                    if (System.IO.Directory.Exists(FolderName) ==false)
                    {
                        System.IO.Directory.CreateDirectory(FolderName);
                    }
                    if (System.IO.File.Exists($"{FolderName}\\{SaveAsPath}.xlsx"))
                    {
                        System.IO.File.Delete($"{FolderName}\\{SaveAsPath}.xlsx");
                    }

                    //WB.SaveAs($"f:\\{SaveAsPath}.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);

                    System.Windows.MessageBox.Show(SaveAsPath);

                    WB.SaveAs($"{FolderName}\\{SaveAsPath}.xls");
                    WB.Close();

                    XLS.Visible = false;

                }
                else
                {
                    XLS.Visible = true;


                    Message = "تم التحويل بنجاح";
                }
               

               
            }
            else if (DTForExcel.Rows.Count == 0)
            {
                Message = "لا يوجد اي بيانات ليتم تحويلها الى اكسل";
            }

            return Message;

        }




    }
}
