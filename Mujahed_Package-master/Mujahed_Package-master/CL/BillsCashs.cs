using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Mujahed_Package.CL
{
    class BillsCashs
    {
        string TableName = "CashsBillsMaster";
        CL.Layer_Data Layer = new Layer_Data();
        public int GetLastSerialID()
        {
            string SQLStatement = $"select Max(IDNumber) from CashsBillsDetails ";
            DataTable DT = Layer.Select_Mannual(SQLStatement);

            int NewSerialID = new int();
            if (DT.Rows[0][0].ToString() == "")
            {
                NewSerialID = 1;
            }
            if (DT.Rows[0][0].ToString() != "")
            {
                NewSerialID = int.Parse(DT.Rows[0][0].ToString()) + 1;
            }

            return NewSerialID;
        }

        //                   0         1       2              3     4         5
        string Columns = "IDNumber,DateSales,SalesManName,NetCashs,NetBills,NoteBills";
        public void InsertMaster(string IDNumber, string DateSales, string SalesManName,string NetCashs,string NetBills ,string Note)
        {
            

            if (NetCashs == "")
            {
                NetCashs = "0";
            }
            if (NetBills == "")
            {
                NetBills = "0";
            }
           

            string SQLStatement = $"insert into {TableName}({Columns}) values({IDNumber}," +
                                                                            $"'{DateSales}'," +
                                                                            $"'{SalesManName}'," +
                                                                            $"{NetCashs}," +
                                                                            $"{NetBills}," +
                                                                            $"'{Note}')";
            Layer.Execute_Mannual(SQLStatement);
        }

        public void UpdateMaster(string IDNumber,string DateSales, string SalesManName, string NetCashs, string NetBills,string Note)
        {
           

            if (NetCashs == "")
            {
                NetCashs = "0";
            }
            if (NetBills == "")
            {
                NetBills = "0";
            }


            string SQLStatement = $"insert into {TableName}({Columns}) values({IDNumber}," +
                                                                            $"'{DateSales}'," +
                                                                            $"'{SalesManName}'," +
                                                                            $"{NetCashs}," +
                                                                            $"{NetBills}," +
                                                                            $"'{Note}')";
            Layer.Execute_Mannual(SQLStatement);
            
        }

        public void InsertDetails(string IDNumber, string DateSales, string SalesManName, string TypeTrans,string Amount,string StateBill)
        {
            string Columns = "  IDNumber,  DateSales,  SalesManName,  TypeTrans, Amount, StateBill";

            if (Amount=="")
            {
                Amount = "0";
            }

            string SQLStatement = $"insert into CashsBillsDetails({Columns}) values({IDNumber}," +
                                                                            $"'{DateSales}'," +
                                                                            $"'{SalesManName}'," +
                                                                            $"'{TypeTrans}'," +
                                                                            $"{Amount}," +
                                                                            $"'{StateBill}')";
            Layer.Execute_Mannual(SQLStatement);
        }


        public void DeleteMaster(string IDNumber)
        {

            string SQLStatement = $"delete from {TableName} where IDNumber={IDNumber} ";

            Layer.Execute_Mannual(SQLStatement);
           
        }

        public void DeleteDetails(string IDNumber)
        {


            string SQLStatement = $"delete from CashsBillsDetails where IDNumber={IDNumber}";

            Layer.Execute_Mannual(SQLStatement);
            DeleteMaster(IDNumber);
        }



        public DataTable GetCashByIDNumberDetails(string IDNumber,string TypeTrans)
        {
            string Colmuns = "IDNumber," +
                             "FORMAT(DateSales, 'Short Date') as DateSales," +
                             "SalesManName," +
                             "TypeTrans," +
                             "Amount," +
                             "StateBill";
            //FORMAT(DateSales, 'Short Date') as DateSales
            string SQLStatement = $"select {Colmuns} from CashsBillsDetails where IDNumber={IDNumber} and TypeTrans='{TypeTrans}' ";

            return Layer.Select_Mannual(SQLStatement);
        }

        
        public DataTable GetCashByIDNumberMaster(string IDNumber)
        {
          

            string SQLStatement = $"select {Columns} from {TableName} where IDNumber={IDNumber} ";


            return Layer.Select_Mannual(SQLStatement);
        }

        public DataTable GetNoteByIDNumberMaster(string IDNumber)
        {


            string SQLStatement = $"select NoteBills from {TableName} where IDNumber={IDNumber} ";


            return Layer.Select_Mannual(SQLStatement);
        }
        public DataTable GetCashMaster(string IDNumber)
        {
            string Columns = " IDNumber,FORMAT(DateSales, 'Short Date') as DateSales,SalesManName,NetCashs,NetBills, (NetCashs-NetBills) as NetAll";

            string SQLStatement = $"select {Columns} from {TableName} where IDNumber={IDNumber} ";

            if (IDNumber == "-1")
            {
                SQLStatement = $"select {Columns} from {TableName} ";

            }

            return Layer.Select_Mannual(SQLStatement);
        }
    }
}
