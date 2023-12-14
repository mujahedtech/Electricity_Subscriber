using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Models
{
    public class AccountClass
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string ClassName { get; set; }

        public DateTime DateEntered { get; set; } = DateTime.Now;

    }
}
