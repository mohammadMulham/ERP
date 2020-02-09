using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Repositories
{
    public interface IStoreItemUnitRepository : IGenericRepository<StoreItemUnit>
    {
        ItemUnit GetItemUnit(Guid itemUnitId, Guid storeId);
        Task<ItemUnit> GetItemUnitAsync(Guid itemUnitId, Guid storeId);
        StoreItemUnit Get(long itemId, long unitId, long storeId);
        Task<StoreItemUnit> GetAsync(long itemId, long unitId, long storeId);
    }
}
