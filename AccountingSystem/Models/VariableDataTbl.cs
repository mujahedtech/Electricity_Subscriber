using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Models
{
    class VariableDataTbl
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string VariableName { get; set; }

        public double VairableValue { get; set; }
        public string Note { get; set; }
    }
}
