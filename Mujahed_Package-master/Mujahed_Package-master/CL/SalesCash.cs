using System;
using System.Data;
namespace Mujahed_Package.CL
{
    class SalesCash
    {
        Layer_Data Layer = new Layer_Data();

        //DataBase Variables
        string TableName = "SalesMan";


        //جلب الرقم التسلسلي الجديد
        public int GetLastSerialID()
        {
            string SQLStatement = $"select Max(IDNumber) from {TableName} ";
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


        public DataTable GetCashPerID(string IDNumber)
        {
            string SQLStatement = $"select * from {TableName} where IDNumber={IDNumber}";

            return Layer.Select_Mannual(SQLStatement);
        }
        //--

        public DataTable GetCash()
        {
            string Colmuns = "IDNumber," +
                             "FORMAT(DateSales, 'Short Date') as DateSales," +
                             "SalesManName," +
                             "TotalZakah," +
                             "TotalRequiredCash," +
                             "TotalCashOnHand," +
                             "TotalEndCash," +
                             "Cash50,Cash20,Cash10,Cash5,Cash1,CashNon," +
                             "SalesNote,ImageCash,SYSID";
            //FORMAT(DateSales, 'Short Date') as DateSales
            string SQLStatement = $"select {Colmuns} from {TableName} order by DateSales asc, IDNumber asc ";
                                 
            return Layer.Select_Mannual(SQLStatement);
        }

        public DataTable GetCashNote(string IDNumber)
        {
            string SQLStatement = $"select SalesNote from {TableName} where IDNumber={IDNumber} ";

            return Layer.Select_Mannual(SQLStatement);
        }
        public DataTable GetCashForReport(string date)
        {
            //DataTable DT = new DataTable();

            //string SQLStatement = $"select Distinct (SalesManName), SYSID from {TableName} where DateSales = #{date}# group by SalesManName,SYSID  order by SYSID asc ";

            string SQLStatement = $"select distinct( SalesMan.sysid),username " +
                $"from SalesMan inner join NameSalesMan on SalesMan.sysid=NameSalesMan.sysid " +
                $"where DateSales =#{date}#";




            //string SQLStatement = $"select {ColoumnsDriver} from tb2 inner join tbl1 on tb2.CarNo=tbl1.VehNo group by tb2.CarNo,tbl1.DriverName";


            //string SQLStatement = $"select Distinct SalesManName,DateSales from SalesMan group by SalesManName,DateSales ";

            //DT= Layer.Select_Mannual(SQLStatement);

            //DataView dv = new DataView(DT);
            //dv.RowFilter = "DateSales <= #8/7-2020# and DateSales >= #8/7-2020#";
            //DT = dv.ToTable();
            return Layer.Select_Mannual(SQLStatement); ;
        }
        //---------------------------------------------------------------------------------------------------------------------------------------

        public DataTable GetSalesmanNameReport(string Code)
        {
            string SQLStatement = $"select Username from NameSalesMan where SYSID not in({Code}) and state='Active'";

          

            return Layer.Select_Mannual(SQLStatement);
        }
        //---------------------------------------------------------------------------------------------------------------------------------------

        //ادخال وثيقة جديدة
        public void InsertNewCash(string IDNumber, string DateSales, string SalesManName, 
                                  string TotalZakah, string TotalRequiredCash,  string TotalCashOnHand,
                                  string TotalEndCash, string Cash50, string Cash20, string Cash10,
                                  string Cash5, string Cash1, string CashNon,string SalesNote, string Image,int SYSID)
        {
            string Columns = " IDNumber,DateSales,SalesManName,TotalZakah,TotalRequiredCash," +
                              "TotalCashOnHand,TotalEndCash,Cash50,Cash20,Cash10,Cash5,Cash1,CashNon,SalesNote,ImageCash,SYSID";

            if (Cash50=="")
            {
                Cash50 = "0";
            }
            if (Cash20 == "")
            {
                Cash20 = "0";
            }
            if (Cash10 == "")
            {
                Cash10 = "0";
            }
            if (Cash5 == "")
            {
                Cash5 = "0";
            }
            if (Cash1 == "")
            {
                Cash1 = "0";
            }
            if (CashNon == "")
            {
                CashNon = "0";
            }
            
            if (TotalZakah == "")
            {
                TotalZakah = "0";
            }
            if (SalesNote == "")
            {
                SalesNote = "Null";
            }
            if (Image != "")
            {
                Image = $"'Null'";
            }
           
            string SQLStatement = $"insert into {TableName}({Columns}) values({GetLastSerialID().ToString()}," +
                                                                            $"'{DateSales}'," +
                                                                            $"'{SalesManName}'," +
                                                                            $"{TotalZakah}," +
                                                                            $"{TotalRequiredCash}," +
                                                                            $"{TotalCashOnHand}," +
                                                                            $"{TotalEndCash}," +
                                                                            $"{Cash50}," +
                                                                            $"{Cash20}," +
                                                                            $"{Cash10}," +
                                                                            $"{Cash5}," +
                                                                            $"{Cash1}," +
                                                                            $"{CashNon}," +
                                                                            $"'{SalesNote}'," +
                                                                            $"{Image},'{SYSID}')";
            Layer.Execute_Mannual(SQLStatement);
        }


        //ادخال وثيقة جديدة
        public void UpdateCash(string IDNumber, string DateSales, string SalesManName,
                                  string TotalZakah, string TotalRequiredCash, string TotalCashOnHand,
                                  string TotalEndCash, string Cash50, string Cash20, string Cash10,
                                  string Cash5, string Cash1, string CashNon, string SalesNote,string Image, int SYSID)
        {
          

            if (Cash50 == "")
            {
                Cash50 = "0";
            }
            if (Cash20 == "")
            {
                Cash20 = "0";
            }
            if (Cash10 == "")
            {
                Cash10 = "0";
            }
            if (Cash5 == "")
            {
                Cash5 = "0";
            }
            if (Cash1 == "")
            {
                Cash1 = "0";
            }
            if (CashNon == "")
            {
                CashNon = "0";
            }

            if (TotalZakah == "")
            {
                TotalZakah = "0";
            }
            if (SalesNote == "")
            {
                SalesNote = "null";
            }
            if (Image != "")
            {
                Image = $"'{Image}'";
            }

            string SQLStatement = $"update {TableName} set DateSales='{DateSales}'," +
                                                         $"SalesManName='{SalesManName}'," +
                                                         $"TotalZakah={TotalZakah}," +
                                                         $"TotalRequiredCash={TotalRequiredCash}," +
                                                         $"TotalCashOnHand={TotalCashOnHand}," +
                                                         $"TotalEndCash={TotalEndCash}," +
                                                         $"Cash50={Cash50}," +
                                                         $"Cash20={Cash20}," +
                                                         $"Cash10={Cash10}," +
                                                         $"Cash5={Cash5}," +
                                                         $"Cash1={Cash1}," +
                                                         $"CashNon={CashNon}," +
                                                         $"SalesNote='{SalesNote}'," +
                                                         $"ImageCash={Image}," +
                                                         $"SYSID ='{SYSID}' " +
                                                         $"where IDNumber ={IDNumber}";
            Layer.Execute_Mannual(SQLStatement);
        }

        //ادخال وثيقة جديدة
        public void InsertZakah(string IDNumber, string ZakahAmount,string ZakahNote)
        {
            string Columns = " IDNumber,ZakahAmount,ZakahNote";

            
            string SQLStatement = $"insert into ZakahCash({Columns}) values({IDNumber}," +
                                                                            $"{ZakahAmount},'{ZakahNote}')";
            Layer.Execute_Mannual(SQLStatement);
        }

        public DataTable GetZakahPerID(string IDNumber)
        {
            string SQLStatement = $"select ZakahAmount,ZakahNote from ZakahCash where IDNumber={IDNumber}";

            return Layer.Select_Mannual(SQLStatement);
        }

        public DataTable GetZakah()
        {
            string SQLStatement = $"select * from ZakahCash ";

            return Layer.Select_Mannual(SQLStatement);
        }

        public DataTable GetSalesmanName()
        {
            string SQLStatement = $"select Username,SYSID from NameSalesMan  order by Username";

            return Layer.Select_Mannual(SQLStatement);
        }

        public DataTable GetSalesmanNameActive()
        {
            string SQLStatement = $"select Username from NameSalesMan where State='Active' order by Username";

            return Layer.Select_Mannual(SQLStatement);
        }
        public string GetSalesmanNameBySYSID(string SysID)
        {
           
            string SQLStatement = $"select Username from NameSalesMan where State='Active' and SYSID ={SysID}";
            DataTable DT = Layer.Select_Mannual(SQLStatement);

            string Name ="";
            if (DT.Rows.Count>0)
            {
                if (DT.Rows[0][0].ToString() == "")
                {
                    Name = "";
                }
                if (DT.Rows[0][0].ToString() != "")
                {
                    Name = DT.Rows[0][0].ToString();
                }
            }
            

            return Name;
            
        }

        public DataTable GetSalesmanNameAll()
        {
            string SQLStatement = $"select * from NameSalesMan order by Username";

            return Layer.Select_Mannual(SQLStatement);
        }


        //عرض مصغر للمعلقات للمناديب
        public DataTable GetLessCash(string SalesMan)
        {
            string SQLStatement = $"select IDNumber, TotalEndCash,SalesNote,FORMAT(DateSales, 'Short Date') as DateSales from SalesMan where (SalesManName like '{SalesMan}' and TotalEndCash < -1) and DateSales >=#{DateTime.Now.AddDays(-14)}# order by DateSales desc ";

            return Layer.Select_Mannual(SQLStatement);
        }
        public void UpdateSalesManState(string UserID, string State)
        {
           
            string SQLStatement = $"update NameSalesMan set State='{State}' where UserID={UserID} ";

            Layer.Execute_Mannual(SQLStatement);
           
        }

        public void ChangeStateForAll( string State)
        {

            string SQLStatement = $"update NameSalesMan set State='{State}'";

            Layer.Execute_Mannual(SQLStatement);

        }

        public void UpdateSalesManName(string UserID, string Name)
        {

            string SQLStatement = $"update NameSalesMan set Username='{Name}' where UserID={UserID} ";

            Layer.Execute_Mannual(SQLStatement);

        }
        public void DeleteSaleCash(string IDNumber)
        {

            string SQLStatement = $"delete from SalesMan where IDNumber={IDNumber} ";
           
            Layer.Execute_Mannual(SQLStatement);
            DeleteZakah(IDNumber);
        }

        public void DeleteZakah(string IDNumber)
        {

            
            string SQLStatement = $"delete from ZakahCash where IDNumber={IDNumber}";
           
            Layer.Execute_Mannual(SQLStatement);
        }

        public DataTable CheckDuplicate(string SalesMan,string date)
        {
            DateTime DateFrom = Convert.ToDateTime(date);
            string SQLStatement = $"select IDNumber, TotalEndCash,SalesNote,DateSales from SalesMan where SalesManName like '{SalesMan}' and DateSales =#{DateFrom.ToString(CL.PassParameters.DateFormat)}# ";

            return Layer.Select_Mannual(SQLStatement);
        }


        public static string GetUserID(string Username)
        {
            string SQLStatement = $"select SYSID from NameSalesMan where Username like '{Username}'";

            DataTable dt = new CL.Layer_Data().Select_Mannual(SQLStatement);

            string result = "";
            if (dt.Rows.Count>0)
            {
                result = dt.Rows[0][0].ToString();
            }
            return result;
        }

        public static string CreateUserNameFromSYSID(string SYSID)
        {
            string SQLStatement = $"select Username from NameSalesMan where SYSID like '{SYSID}'";

            DataTable dt = new CL.Layer_Data().Select_Mannual(SQLStatement);

            string result = "";
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count==1)
                {
                    result = "'" + dt.Rows[0][0].ToString() + "'";
                }
               else if (dt.Rows.Count == 2)
                {
                    result ="'"+ dt.Rows[0][0].ToString()+"'"+",'" + dt.Rows[1][0].ToString() + "'";
                }
                else if (dt.Rows.Count == 3)
                {
                    result = "'" + dt.Rows[0][0].ToString() + "'" + ",'" + dt.Rows[1][0].ToString() + "'" + ",'" + dt.Rows[2][0].ToString() + "'";
                }
                else if (dt.Rows.Count == 4)
                {
                    result = "'" + dt.Rows[0][0].ToString() + "'" + ",'" + dt.Rows[1][0].ToString() + "'" + ",'" + dt.Rows[2][0].ToString() + "'" + ",'" + dt.Rows[3][0].ToString() + "'";
                }
                else if (dt.Rows.Count == 5)
                {
                    result = "'" + dt.Rows[0][0].ToString() + "'" + ",'" + dt.Rows[1][0].ToString() + "'" + ",'" + dt.Rows[2][0].ToString() + "'" + ",'" + dt.Rows[3][0].ToString() + "'" + ",'" + dt.Rows[4][0].ToString() + "'";
                }
                else if (dt.Rows.Count == 6)
                {
                    result = "'" + dt.Rows[0][0].ToString() + "'" + ",'" + dt.Rows[1][0].ToString() + "'" + ",'" + dt.Rows[2][0].ToString() + "'" + ",'" + dt.Rows[3][0].ToString() + "'" + ",'" + dt.Rows[4][0].ToString() + "'" + ",'" + dt.Rows[5][0].ToString() + "'";
                }
            }
            result = "(" + result + ")";
            return result;
        }



    }
}
