using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Models.Repositories
{
    public class AccountClassRepository : IDatabaseRepository<AccountClass>
    {
        DbContext db;

        public AccountClassRepository()
        {
            db = new DbContext();


        }

        public Task<int> Add(AccountClass entity)
        {
            return db._database.InsertAsync(entity);
        }
        public Task<int> Add(List<AccountClass> entity)
        {
            return db._database.InsertAllAsync(entity);
        }

        public Task<int> Delete(AccountClass entity)
        {
            return db._database.DeleteAsync(entity);

        }

        public Task<AccountClass> Find(int id)
        {
            return db._database.Table<AccountClass>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<AccountClass>> List()
        {
             return db._database.Table<AccountClass>().ToListAsync();
        }

        public Task<List<AccountClass>> Search(string term)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(AccountClass entity)
        {
            return db._database.UpdateAsync(entity);
        }
    }

}
