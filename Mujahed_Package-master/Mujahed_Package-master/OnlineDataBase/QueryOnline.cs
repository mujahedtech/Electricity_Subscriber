using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Mujahed_Package.OnlineDataBase
{
    class QueryOnline
    {
        OnlineDataBase.DataAccessOnline Layer = new DataAccessOnline();












        public void UpdateDataBase(string SQLCommand)
        {

            Layer.Execute_Mannual(SQLCommand);
        }

        public void InsertDataBase()
        {

            Layer.Execute_Mannual("INSERT INTO `NameSalesMan` (`UserID`, `Username`, `State`,`SYSID`) VALUES(11111,'مجاهد','Null',1111)");
        }
    }
}
