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
    public class ItemService:IItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            return await _unitOfWork.Items.GetAllAsync();
        }

        public async Task<Item> GetItemByIdAsync(int itemId)
        {
            return await _unitOfWork.Items.GetByIdAsync(itemId);
        }

        public async Task AddItemAsync(Item item)
        {
            await _unitOfWork.Items.AddAsync(item);
            await _unitOfWork.SaveAsync();
        }
        public async Task UpdateItemAsync(Item item)
        {
            var existingItem = (await _unitOfWork.Items.FindAsync(i => i.ItemId == item.ItemId)).FirstOrDefault();
            if (existingItem != null)
            {
                existingItem.Name = item.Name;
                _unitOfWork.Items.Update(existingItem);
                await _unitOfWork.SaveAsync();
            }
        }


        public async Task DeleteItemAsync(int itemId)
        {
            var item = await _unitOfWork.Items.GetByIdAsync(itemId);
            if (item != null)
            {
                _unitOfWork.Items.Delete(item);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
