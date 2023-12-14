using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Models.Repositories
{
    class GasCylinderInventoryRepository : IDatabaseRepository<GasCylinderInventoryTbl>
    {
        DbContext db;

        public GasCylinderInventoryRepository()
        {
            db = new DbContext();


        }
        public Task<int> Add(GasCylinderInventoryTbl entity)
        {
            return db._database.InsertAsync(entity);
        }

        public Task<int> Delete(GasCylinderInventoryTbl entity)
        {
            return db._database.DeleteAsync(entity);
        }

        public Task<GasCylinderInventoryTbl> Find(int id)
        {
            return db._database.Table<GasCylinderInventoryTbl>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<GasCylinderInventoryTbl>> List()
        {
            return db._database.Table<GasCylinderInventoryTbl>().ToListAsync();
        }

        public Task<List<GasCylinderInventoryTbl>> Search(string term)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(GasCylinderInventoryTbl entity)
        {
            return db._database.UpdateAsync(entity);
        }
    }

}
