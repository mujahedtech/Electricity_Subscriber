using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Models.Repositories
{
    public class AccountsTableRepository : IDatabaseRepository<AccountsTable>
    {
        DbContext db;

        public AccountsTableRepository()
        {
            db = new DbContext();


        }

        public Task<int> Add(AccountsTable entity)
        {

            return db._database.InsertAsync(entity);
        }

        public Task<int> Delete(AccountsTable entity)
        {
            return db._database.DeleteAsync(entity);
        }

        public Task<AccountsTable> Find(int id)
        {
            return db._database.Table<AccountsTable>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<AccountsTable>> List()
        {
            return db._database.Table<AccountsTable>().ToListAsync();
        }

        public Task<List<AccountsTable>> Search(string term)
        {
            throw new NotImplementedException();
        }
        public Task<List<AccountsTable>> SearchClassid(int term)
        {
            return db._database.Table<AccountsTable>().Where(i => i.idclassAccount == term).ToListAsync();
        }

        public Task<int> Update(AccountsTable entity)
        {


            return db._database.UpdateAsync(entity);
        }
    }
}
