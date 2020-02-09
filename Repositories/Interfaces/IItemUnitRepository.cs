using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Repositories
{
    public interface IItemUnitRepository : IGenericRepository<ItemUnit>
    {
        ItemUnit Get(long itemNumber, long unitNumber);
        Task<ItemUnit> GetAsync(long itemNumber, long unitNumber);
        Task<bool> IsExistCodeAsync(string Code);
        bool IsExist(Guid itemId, Guid unitId);
        Task<bool> IsExistAsync(Guid itemId, Guid unitId);
        IQueryable<ItemUnit> Search(string key);
        IQueryable<Unit> GetItemUnits(long itemId, string key);
    }
}
