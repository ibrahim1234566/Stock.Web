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
    public class StoreService : IStoreService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StoreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Store>> GetAllStoresAsync()
        {
            return await _unitOfWork.Stores.GetAllAsync();
        }

        public async Task<Store> GetStoreByIdAsync(int storeId)
        {
            return await _unitOfWork.Stores.GetByIdAsync(storeId);
        }

        public async Task AddStoreAsync(Store store)
        {
            await _unitOfWork.Stores.AddAsync(store);
            await _unitOfWork.SaveAsync();
        }
        public async Task UpdateStoreAsync(Store store)
        {
            var existingStore = (await _unitOfWork.Stores.FindAsync(s => s.StoreId == store.StoreId)).FirstOrDefault();
            if (existingStore != null)
            {
                existingStore.Name = store.Name;
                _unitOfWork.Stores.Update(existingStore);
                await _unitOfWork.SaveAsync();
            }
        }


        public async Task DeleteStoreAsync(int storeId)
        {
            var store = await _unitOfWork.Stores.GetByIdAsync(storeId);
            if (store != null)
            {
                _unitOfWork.Stores.Delete(store);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
