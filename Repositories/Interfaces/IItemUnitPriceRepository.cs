using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Repositories
{
    public interface IItemUnitPriceRepository : IGenericRepository<ItemUnitPrice>
    {
        ItemUnitPrice Get(Guid itemUnitId, long priceNumber);
        Task<ItemUnitPrice> GetAsync(Guid itemUnitId, long priceNumber);
        bool IsExist(Guid itemUnitId, Guid priceId);
        Task<bool> IsExistAsync(Guid itemUnitId, Guid priceId);
        IQueryable<ItemUnitPrice> GetItemUnitPrices(long itemNumber, long unitNumber);
        Task<double> GetItemUnitPriceAsync(long itemNumber, long unitNumber, long priceNumber);
    }
}
