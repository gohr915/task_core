using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSTest.NewFolder
{
    public interface IRepositoryItem
    {
        Task<int> add_item(Item item);

        Task<int> update_item(Item item );


        Task<bool> delete_item(int Id );

        Task<IQueryable<Item>> get_all_items();
    }
}
