using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;



namespace Mujahed_Package.OnlineDataBase
{
    class DataAccessOnline
    {

        private string MySQLOn = $"Server=db4free.net;" +
                                 $" Database=khirat_db;" +
                                 $"Uid=khiratmt; " +
                                 $" Pwd=00000000;" +
                                 $" Port=3306; " +
                                 $" oldguids=true";

        private string MySQLOn1 = $"Server=213.6.76.82;" +
                                 $" Database=mhmking_db;" +
                                 $"Uid=mhmking; " +
                                 $" Pwd=SDFnm@@91;" +
                                 $" Port=3306;" +
                                 $"CharSet=utf8; " +
                                 $" oldguids=true";


        private string Local = $"Provider=Microsoft.ACE.OLEDB.12.0;" +
                               $"Data Source=Khirat_DB.accdb;" +
                               $"Persist Security Info=True";
        public MySqlConnection Sqlconnection;

        //this for Connect the database

        public DataAccessOnline()
        {
            Sqlconnection = new MySqlConnection(MySQLOn1);

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
                MySqlCommand Command = new MySqlCommand();
                Command.Connection = Sqlconnection;
                Command.CommandText = Code;
                Command.ExecuteNonQuery();
                Sqlconnection.Close();
                MessageBox.Show("Complete", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (System.Exception m)
            {

                MessageBox.Show(m.Message);
            }
           

        }

        // Mannual Select Data
        public DataTable Select_Mannual(string Code)
        {
            Sqlconnection.Open();
            MySqlCommand Command = new MySqlCommand();
            Command.Connection = Sqlconnection;
            Command.CommandText = Code;
            MySqlDataAdapter DA = new MySqlDataAdapter(Code, Sqlconnection);
            Sqlconnection.Close();
            DataTable DS = new DataTable();
            DA.Fill(DS);
            return DS;


        }
    }
}
