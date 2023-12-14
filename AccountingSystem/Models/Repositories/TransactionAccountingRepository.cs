using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Models.Repositories
{
    class TransactionAccountingRepository : IDatabaseRepository<TransactionAccounting>
    {
        DbContext db;

        public TransactionAccountingRepository()
        {
            db = new DbContext();


        }

        public Task<int> Add(TransactionAccounting entity)
        {
            return db._database.InsertAsync(entity);
        }

        public Task<int> Delete(TransactionAccounting entity)
        {
            return db._database.DeleteAsync(entity);
        }

        public Task<TransactionAccounting> Find(int id)
        {
            return db._database.Table<TransactionAccounting>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<TransactionAccounting>> List()
        {
            return db._database.Table<TransactionAccounting>().ToListAsync();
        }
        public Task<List<TransactionAccounting>> ListPerTransType(int Id)
        {
            return db._database.Table<TransactionAccounting>().Where(i => i.TransType == Id).ToListAsync();
        }
        public Task<List<TransactionAccounting>> ListIdAccount(int Id)
        {
            return db._database.Table<TransactionAccounting>().Where(i => i.AccountId == Id).OrderBy(i => i.DateEntered).ToListAsync();
        }
        public Task<List<TransactionAccounting>> ListPerDate(int month, int year)
        {

            DateTime From = new DateTime(year, month, 1);
            DateTime To = new DateTime(year, month, DateTime.DaysInMonth(year, month));


            return db._database.Table<TransactionAccounting>().Where(i => i.DateEntered <= To).OrderBy(i => i.DateEntered).ToListAsync();
        }

        public Task<List<TransactionAccounting>> ListPerDateRange(int month, int year)
        {

            DateTime From = new DateTime(year, month, 1);
            DateTime To = new DateTime(year, month, DateTime.DaysInMonth(year, month));


            return db._database.Table<TransactionAccounting>().Where(i => i.DateEntered >= From && i.DateEntered <= To).OrderBy(i => i.DateEntered).ToListAsync();
        }

        public Task<List<TransactionAccounting>> Search(string term)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(TransactionAccounting entity)
        {
            return db._database.UpdateAsync(entity);
        }
    }

}
