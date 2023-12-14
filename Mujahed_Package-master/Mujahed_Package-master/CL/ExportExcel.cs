using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mujahed_Package.CL
{
    class ExportExcel
    {
        public string ExportToExcel(System.Data.DataTable DTForExcel)
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
                Header = WS.Range["A1", "D1"];
                Header.Font.Bold = true;
                Header.Rows.RowHeight = 30;
                Header.Interior.Color = XlRgbColor.rgbLightGray;




                //تنسيق الجدول
                Range RA;

                Count_Rows += 1;
                RA = WS.Range["A1", "D" + Count_Rows];
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

                XLS.Visible = true;
                XLS.WindowState = XlWindowState.xlMaximized;
                XLS.Columns.AutoFit();

                Message = "تم التحويل بنجاح";
            }
            else if (DTForExcel.Rows.Count == 0)
            {
                Message = "لا يوجد اي بيانات ليتم تحويلها الى اكسل";
            }

            return Message;

        }
    }
}
