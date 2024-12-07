using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Service.Interfaces
{
    public interface IStoreItemService
    {
        Task AddItemToStoreAsync(int storeId, int itemId, int quantity);
        Task UpdateItemQuantityAsync(int storeId, int itemId, int quantity);
        Task<int> GetItemQuantityAsync(int storeId, int itemId);
    }
}
