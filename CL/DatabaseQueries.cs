using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electricity_Subscriber.CL
{
    class DatabaseQueries
    {
        string TableName = "SubscriberTBL";

        CL.Data_Layer Layer = new Data_Layer();
        //جلب الرقم التسلسلي الجديد
        public int GetLastSerialID()
        {
            string SQLStatement = $"select Max(ID_SYS) from {TableName} ";
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

        public DataTable GetInfoAll()
        {
            string SQLStatement = $"select *,DCount(\"[ID_SYS]\",\"[SubscriberTBL]\",\"[ID_SYS] <= \" & [ID_SYS]) as Serial from {TableName}";


            //string SQLStatement = $"select * from {TableName}";

            return Layer.Select_Mannual(SQLStatement);
        }

        public DataTable GetInfoAllWithWhere(string Command)
        {
            string SQLStatement = $"select *,DCount(\"[ID_SYS]\",\"[SubscriberTBL]\",\"[ID_SYS] <= \" & [ID_SYS]) as Serial from {TableName} {Command} order by ID_SYS";

            return Layer.Select_Mannual(SQLStatement);
        }



        public DataTable GetInfo(string text)
        {
            string SQLStatement = $"select * from {TableName} where TypeSubscriber like '{text}'";

            return Layer.Select_Mannual(SQLStatement);
        }

        public DataTable GetInfoByNumber(string text)
        {
            string SQLStatement = $"select * from {TableName} where NumberSubscriber like '%{text}%'";


          


            return Layer.Select_Mannual(SQLStatement);
        }



        public DataTable GetTypeSubscriber()
        {
            string SQLStatement = $"select distinct (TypeSubscriber) from {TableName} where TypeSubscriber <>'-' order by TypeSubscriber ";

            return Layer.Select_Mannual(SQLStatement);
        }



        public void Update(string data,string id)
        {
            string SQLStatement = $"update {TableName} set NoteSubscriber='{data}' where ID_SYS={id}";

           

             Layer.Select_Mannual(SQLStatement);
        }




    }
}
