using Microsoft.EntityFrameworkCore;
using POSTest.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSTest.NewFolder
{
    public class ItemRepository : IRepositoryItem
    {
        private db _db { get; }
        public ItemRepository(db db)
        {
            _db = db;
        }

        

        public async  Task<int> add_item(Item item)
        {
           await   _db.items.AddAsync(item);
            await _db.SaveChangesAsync();
            return item.Id;
        }

        public async  Task<bool> delete_item(int Id)
        {
          var ietm  =  await  _db.items.Include(x=>x.Sizes).FirstOrDefaultAsync();
            _db.items.Remove(ietm);
           await  _db.SaveChangesAsync();
            return true;
        
        }

        public async Task<IQueryable<Item>> get_all_items()
        {
            var querable = _db.items.Include(x => x.Sizes);
            return querable; 
        }

        public async  Task<int> update_item(Item item)
        {
            _db.items.Update(item);

           await _db.SaveChangesAsync();

            return item.Id;
            
        }
    }
}
