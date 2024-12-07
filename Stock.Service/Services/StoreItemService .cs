using Stock.Data.Models;
using Stock.Repository.Interfaces;
using Stock.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Service.Services
{
    public class StoreItemService:IStoreItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StoreItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task AddItemToStoreAsync(int storeId, int itemId, int quantity)
        {
            
            var storeItem = (await _unitOfWork.StoreItems.FindAsync(si => si.StoreId == storeId && si.ItemId == itemId))
                            .FirstOrDefault();

            if (storeItem != null)
            {
                storeItem.Quantity += quantity; 
                _unitOfWork.StoreItems.Update(storeItem);
            }
            else
            {
                storeItem = new StoreItem
                {
                    StoreId = storeId,
                    ItemId = itemId,
                    Quantity = quantity
                };
                await _unitOfWork.StoreItems.AddAsync(storeItem);
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateItemQuantityAsync(int storeId, int itemId, int quantity)
        {
            var storeItem = (await _unitOfWork.StoreItems.FindAsync(si => si.StoreId == storeId && si.ItemId == itemId)).FirstOrDefault();
            if (storeItem != null)
            {
                storeItem.Quantity = quantity;  
                _unitOfWork.StoreItems.Update(storeItem);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task<int> GetItemQuantityAsync(int storeId, int itemId)
        {
            var storeItem = (await _unitOfWork.StoreItems.FindAsync(si => si.StoreId == storeId && si.ItemId == itemId)).FirstOrDefault();
            return storeItem?.Quantity ?? 0;
        }
    }
}
