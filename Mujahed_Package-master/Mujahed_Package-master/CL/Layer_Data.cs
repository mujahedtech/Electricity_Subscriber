using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Data;
using System.Data.OleDb;
namespace Mujahed_Package.CL
{
    class Layer_Data
    {
      public static string Path = "DBPath.txt";

        private string MySQLOn = $"Server=db4free.net;" +
                                 $" Database=khirat_db;" +
                                 $"Uid=khiratmt; " +
                                 $" Pwd=00000000;" +
                                 $" Port=3306; " +
                                 $" oldguids=true";

        private string MySQLMujahed = $"Server=213.6.76.82;" +
                                 $" Database=MujahedTech;" +
                                 $"Uid=mujahed; " +
                                 $" Pwd=SDFnm0091;" +
                                 $" Port=3306; " +
                                 $" oldguids=true";


        private string Local = $"Provider=Microsoft.ACE.OLEDB.12.0;" +
                               $"Data Source={System.IO.File.ReadAllText(Path)};" +
                               $"Persist Security Info=True";
        public OleDbConnection Sqlconnection;

        //this for Connect the database

        

        public Layer_Data()
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
            
            Sqlconnection.Open();
            OleDbCommand Command = new OleDbCommand();
            Command.Connection = Sqlconnection;
            Command.CommandText = Code;
            Command.ExecuteNonQuery();
            Sqlconnection.Close();

        }

        // Mannual Select Data
        public DataTable Select_Mannual(string Code)
        {
            Sqlconnection.Open();
            OleDbCommand Command = new OleDbCommand();
            Command.Connection = Sqlconnection;
            Command.CommandText = Code;
            OleDbDataAdapter DA = new OleDbDataAdapter(Code, Sqlconnection);
            Sqlconnection.Close();
            DataTable DS = new DataTable();
            DA.Fill(DS);
            return DS;


        }


    }
}
