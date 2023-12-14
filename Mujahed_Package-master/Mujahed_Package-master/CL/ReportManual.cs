using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Mujahed_Package.CL
{

    

    class ReportManual
    {
        CL.Layer_Data Layer = new Layer_Data();

        //ادخال وثيقة جديدة
        public void InsertNewReports(string DateReport,string DateCustom)
        {
           
            if (GetReport(DateCustom).Rows.Count<=0)
            {
                System.Data.DataTable DTUsername = new CL.SalesCash().GetSalesmanName();
                for (int i = 0; i < DTUsername.Rows.Count; i++)
                {
                    string Columns = " DateReport,Username,State";
                    string SQLStatement = $"insert into CashReportManual({Columns}) values('{DateReport}','{DTUsername.Rows[i][0]}','Null')";
                    Layer.Execute_Mannual(SQLStatement);
                }
               
            }
          
            
        }


        public DataTable GetReport(string Date)
        {
            string SQLStatement = $"select ID, FORMAT(DateReport, 'Short Date') as DateReport,Username,State"
               + $" from CashReportManual where DateReport between #{Date}# and #{Date}#";
            return Layer.Select_Mannual(SQLStatement);
           
        }
        public void DeleteReportData(string Date)
        {

            string SQLStatement = $"delete from CashReportManual where DateReport between #{Date}# and #{Date}#";

            Layer.Execute_Mannual(SQLStatement);
           
        }

        public void UpdateReportState(string UserID, string State)
        {

            string SQLStatement = $"update CashReportManual set State='{State}' where ID={UserID} ";

            Layer.Execute_Mannual(SQLStatement);

        }
    }
}
