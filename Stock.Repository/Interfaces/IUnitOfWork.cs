using Stock.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Store> Stores { get; }
        IGenericRepository<Item> Items { get; }
        IGenericRepository<StoreItem> StoreItems { get; }
        Task SaveAsync();
    }
}
