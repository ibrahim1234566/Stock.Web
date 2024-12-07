using Stock.Data.Context;
using Stock.Data.Models;
using Stock.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StockDbContext _context;
        private IGenericRepository<Store> _stores;
        private IGenericRepository<Item> _items;
        private IGenericRepository<StoreItem> _storeItems;

        public UnitOfWork(StockDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Store> Stores => _stores ??= new GenericRepository<Store>(_context);
        public IGenericRepository<Item> Items => _items ??= new GenericRepository<Item>(_context);
        public IGenericRepository<StoreItem> StoreItems => _storeItems ??= new GenericRepository<StoreItem>(_context);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
