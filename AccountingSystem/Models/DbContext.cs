using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Models
{
    public class DbContext
    {
        public  SQLiteAsyncConnection _database;


        public static string BackUp = "MujahedTech_Bakup.db3";

        public static string Path = "MujahedTech.db3";

        static string FolderNameBackup = "BackupFiles";


        public static string BackupName = $"{AppDomain.CurrentDomain.BaseDirectory}{FolderNameBackup}\\{BackUp}";


        public DbContext(string dbpath = "")
        {
            dbpath = Path;



            _database = new SQLiteAsyncConnection(dbpath);
            _database.CreateTableAsync<Models.Categories>();
            _database.CreateTableAsync<Models.DailySlaughter>();
            _database.CreateTableAsync<Models.DailyTransaction>();
            _database.CreateTableAsync<Models.DebitAccount>();
            _database.CreateTableAsync<Models.AccountClass>();
            _database.CreateTableAsync<AccountsTable>();
            _database.CreateTableAsync<Inventory>();
            _database.CreateTableAsync<PurchaseList>();
            _database.CreateTableAsync<TransactionAccounting>();
            _database.CreateTableAsync<GasCylinderBalanceTBL>();
            _database.CreateTableAsync<GasCylinderInventoryTbl>();
            _database.CreateTableAsync<VariableDataTbl>();


        }




        public Task BackupDB()
        {
            if (System.IO.Directory.Exists(FolderNameBackup) == false)
            {
                System.IO.Directory.CreateDirectory(FolderNameBackup);
            }



            return _database.BackupAsync(BackupName);

        }

    }
}
