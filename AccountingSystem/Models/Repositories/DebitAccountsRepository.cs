using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Models.Repositories
{
    public class DebitAccountsRepository : IDatabaseRepository<DebitAccount>
    {

        DbContext db;

        public DebitAccountsRepository()
        {
            db = new DbContext();


        }

        public Task<int> Add(DebitAccount entity)
        {
            return db._database.InsertAsync(entity);
        }

        public Task<int> Delete(DebitAccount entity)
        {
            return db._database.DeleteAsync(entity);
        }

        public Task<DebitAccount> Find(int id)
        {
            return db._database.Table<DebitAccount>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<DebitAccount>> List()
        {
            return db._database.Table<DebitAccount>().ToListAsync();
        }

        public Task<List<DebitAccount>> Search(string term)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(DebitAccount entity)
        {
            return db._database.UpdateAsync(entity);
        }
    }

}
