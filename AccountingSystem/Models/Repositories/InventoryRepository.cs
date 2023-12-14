using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Models.Repositories
{
    public class InventoryRepository : IDatabaseRepository<Inventory>
    {
        DbContext db;

        public InventoryRepository()
        {
            db = new DbContext();


        }
        public Task<int> Add(Inventory entity)
        {
            return db._database.InsertAsync(entity);
        }

        public Task<int> Add(List<Inventory> entity)
        {


            return db._database.InsertAllAsync(entity);
        }

        public Task<int> Delete(Inventory entity)
        {
            return db._database.DeleteAsync(entity);
        }

        public Task<Inventory> Find(int id)
        {
            return db._database.Table<Inventory>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<Inventory>> List()
        {
            return db._database.Table<Inventory>().ToListAsync();
        }

        public Task<List<Inventory>> Search(string term)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(Inventory entity)
        {


            return db._database.UpdateAsync(entity);
        }
    }

}
