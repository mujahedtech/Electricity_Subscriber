using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Models
{
    public class AccountsTable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int idclassAccount { get; set; }

        public string AccountName { get; set; }
        public string AccountMobile { get; set; }
        public string AccountNote { get; set; }



        public DateTime DateEntered { get; set; } = DateTime.Now;
    }
}
