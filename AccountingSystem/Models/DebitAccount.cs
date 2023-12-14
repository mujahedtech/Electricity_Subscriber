using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Models
{
    public class DebitAccount
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int IdCustomer { get; set; }

        public double Amount { get; set; }

        public DateTime DateEnter { get; set; }
        public string Note { get; set; }
    }
}
