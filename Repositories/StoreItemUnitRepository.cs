using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ERPAPI.Repositories
{
    public class StoreItemUnitRepository : GenericRepository<StoreItemUnit>, IStoreItemUnitRepository
    {
        public StoreItemUnitRepository(ERPContext context) : base(context)
        {
        }

        public override IQueryable<StoreItemUnit> NativeGetAll()
        {
            return base.NativeGetAll().Where(e => e.FinancialPeriodId == PerdioId);
        }

        public override IQueryable<StoreItemUnit> GetAll()
        {
            return base.GetAll().Include(e => e.ItemUnit).Include(e => e.Store);
        }

        public ItemUnit GetItemUnit(Guid itemUnitId, Guid storeId)
        {
            var itemUnit = GetAll().Where(e => e.ItemUnitId == itemUnitId && e.StoreId == storeId).Select(e => e.ItemUnit).FirstOrDefault();
            return itemUnit;
        }

        public StoreItemUnit Get(long itemId, long unitId, long storeId)
        {
            var storeItemUnit = NativeGetAllNoTracking().FirstOrDefault(e => e.ItemUnit.Item.Number == itemId && e.ItemUnit.Unit.Number == unitId && e.Store.Number == storeId);
            return storeItemUnit;
        }

        public async Task<StoreItemUnit> GetAsync(long itemId, long unitId, long storeId)
        {
            var storeItemUnit = await NativeGetAllNoTracking().FirstOrDefaultAsync(e 
                => e.ItemUnit.Item.Number == itemId
                && e.ItemUnit.Unit.Number == unitId 
                && e.Store.Number == storeId);
            return storeItemUnit;
        }

        public async Task<ItemUnit> GetItemUnitAsync(Guid itemUnitId, Guid storeId)
        {
            var itemUnit = await GetAll().Where(e => e.ItemUnitId == itemUnitId && e.StoreId == storeId).Select(e => e.ItemUnit).FirstOrDefaultAsync();
            return itemUnit;
        }

        public override void SetCreateProps(StoreItemUnit storeItemUnit)
        {
            base.SetCreateProps(storeItemUnit);
            storeItemUnit.FinancialPeriodId = PerdioId;
        }
    }
}
