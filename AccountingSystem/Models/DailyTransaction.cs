using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Models
{
    public class DailyTransaction
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public Guid IdFromSlaughter { get; set; }


        public DateTime Date { get; set; }

        public double EndCash { get; set; }

    }
}
