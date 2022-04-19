using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electricity_Subscriber.CL
{
    class Data_Layer
    {

        //private static string DBSource = CL.PassParameters.DecodeServerName(System.IO.File.ReadAllText(@"DataBase\Line.dll", System.Text.Encoding.UTF8));

        //private static string Password = CL.PassParameters.DecodeServerName(System.IO.File.ReadAllText(@"DataBase\Door.dll", System.Text.Encoding.UTF8));

        //private string MySQLOn = $"Server=db4free.net;" +
        //                         $" Database=khirat_db;" +
        //                         $"Uid=khiratmt; " +
        //                         $" Pwd=00000000;" +
        //                         $" Port=3306; " +
        //                         $" oldguids=true";


        //private string Local = $"Provider=Microsoft.ACE.OLEDB.12.0;" +
        //                       $"Data Source=Electricity_Subscriber_DB;" +
        //                       $"Jet OLEDB:Database Password={Password};";


        public static string Local = $"Provider=Microsoft.ACE.OLEDB.12.0;" +
                             $"Data Source=Electricity_Subscriber_DB.accdb;" +
                             $"Persist Security Info=True";
        public OleDbConnection Sqlconnection;

        //this for Connect the database

        public Data_Layer()
        {
            Sqlconnection = new OleDbConnection(Local);

        }

        //open connect
        public void Open()
        {
            if (Sqlconnection.State != ConnectionState.Open)
            {
                Sqlconnection.Open();
            }

        }
        // close connect 
        public void Close()
        {
            if (Sqlconnection.State == ConnectionState.Open)
            {
                Sqlconnection.Close();
            }
        }

        //Read from database


        //p`t 
        //Execute Mannual
        public void Execute_Mannual(string Code)
        {
            try
            {
                Sqlconnection.Open();
                OleDbCommand Command = new OleDbCommand();
                Command.Connection = Sqlconnection;
                Command.CommandText = Code;
                Command.ExecuteNonQuery();
                Sqlconnection.Close();
            }
            catch (System.Exception m)
            {
              

            }



        }

        // Mannual Select Data
        public DataTable Select_Mannual(string Code)
        {
            DataTable DS = new DataTable();

            Sqlconnection.Open();

            OleDbCommand Command = new OleDbCommand();
            Command.Connection = Sqlconnection;
            Command.CommandText = Code;
            OleDbDataAdapter DA = new OleDbDataAdapter(Code, Sqlconnection);
            Sqlconnection.Close();

            DA.Fill(DS);



            return DS;

        }


    }
}
