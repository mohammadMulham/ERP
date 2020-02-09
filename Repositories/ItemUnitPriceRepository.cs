using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ERPAPI.Repositories
{
    public class ItemUnitPriceRepository : GenericRepository<ItemUnitPrice>, IItemUnitPriceRepository
    {
        public ItemUnitPriceRepository(ERPContext context) : base(context)
        {
        }

        public override IQueryable<ItemUnitPrice> GetAll()
        {
            return base.GetAll().Include(i => i.ItemUnit).Include(i => i.Price);
        }

        public ItemUnitPrice Get(Guid itemUnitId, long priceNumber)
        {
            var itemUnitPrice = GetAll().FirstOrDefault(e => e.ItemUnitId == itemUnitId && e.Price.Number == priceNumber);
            return itemUnitPrice;
        }

        public async Task<ItemUnitPrice> GetAsync(Guid itemUnitId, long priceNumber)
        {
            var itemUnitPrice = await GetAll().FirstOrDefaultAsync(e => e.ItemUnitId == itemUnitId && e.Price.Number == priceNumber);
            return itemUnitPrice;
        }

        public bool IsExist(Guid itemUnitId, Guid priceId)
        {
            var count = Count(e => e.ItemUnitId == itemUnitId && e.PriceId == priceId);
            return count > 0;
        }

        public async Task<bool> IsExistAsync(Guid itemUnitId, Guid priceId)
        {
            var count = await CountAsync(e => e.ItemUnitId == itemUnitId && e.PriceId == priceId);
            return count > 0;
        }

        public IQueryable<ItemUnitPrice> GetItemUnitPrices(long itemNumber, long unitNumber)
        {
            var itemUnitPrices = GetAll().Where(e => e.ItemUnit.Item.Number == itemNumber && e.ItemUnit.Unit.Number == unitNumber);
            itemUnitPrices = itemUnitPrices.OrderBy(e => e.StartDate);
            return itemUnitPrices;
        }

        public async Task<double> GetItemUnitPriceAsync(long itemNumber, long unitNumber, long priceNumber)
        {
            var itemUnitPrice = await GetAll().FirstOrDefaultAsync(e => e.ItemUnit.Item.Number == itemNumber && e.ItemUnit.Unit.Number == unitNumber && e.Price.Number == priceNumber);
            return itemUnitPrice != null ? itemUnitPrice.Value : -1;
        }
    }
}
