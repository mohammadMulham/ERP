using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ERPAPI.Repositories
{
    public class ItemUnitRepository : GenericRepository<ItemUnit>, IItemUnitRepository
    {
        public ItemUnitRepository(ERPContext context) : base(context)
        {
        }

        public override IQueryable<ItemUnit> GetAll()
        {
            return base.GetAll().Include(i => i.Item).Include(i => i.Unit);
        }

        public ItemUnit Get(long itemNumber, long unitNumber)
        {
            var itemUnit = GetAll().FirstOrDefault(e => e.Item.Number == itemNumber && e.Unit.Number == unitNumber);
            return itemUnit;
        }

        public async Task<ItemUnit> GetAsync(long itemNumber, long unitNumber)
        {
            var itemUnit = await GetAll().FirstOrDefaultAsync(e => e.Item.Number == itemNumber && e.Unit.Number == unitNumber);
            return itemUnit;
        }

        public void LoadReferences(ItemUnit itemUnit)
        {

        }

        public async Task<bool> IsExistCodeAsync(string Code)
        {
            var count = await base.GetAll().LongCountAsync(a => a.Code == Code);
            return count > 0;
        }

        public bool IsExist(Guid itemId, Guid unitId)
        {
            var count = Count(e => e.ItemId == itemId && e.UnitId == unitId);
            return count > 0;
        }

        public async Task<bool> IsExistAsync(Guid itemId, Guid unitId)
        {
            var count = await CountAsync(e => e.ItemId == itemId && e.UnitId == unitId);
            return count > 0;
        }

        public IQueryable<ItemUnit> Search(string key)
        {
            var itemUnits = NativeGetAllNoTracking();
            itemUnits = itemUnits.Include(e => e.Item).Include(e => e.Unit);

            if (!string.IsNullOrEmpty(key))
            {
                itemUnits = itemUnits.Where(
                                e => e.Code.Contains(key)
                                || e.Item.Name.Contains(key)
                                || e.Item.Code.Contains(key)
                                || (e.Item.Code + " " + e.Item.Name).Contains(key)
                                || (e.Item.Code + "-" + e.Item.Name).Contains(key));
            }

            itemUnits = itemUnits.Where(e => e.IsDefault);
            itemUnits = itemUnits.OrderBy(e => e.Item.Code).ThenBy(e => e.Item.Name);
            itemUnits = itemUnits.Skip(0).Take(25);
            return itemUnits;
        }

        public IQueryable<Unit> GetItemUnits(long itemId, string key)
        {
            var itemUnits = NativeGetAllNoTracking().Where(e => e.Item.Number == itemId);
            var units = itemUnits.Select(e => e.Unit);

            if (!string.IsNullOrEmpty(key))
            {
                units = units.Where(
                                e => e.Name.Contains(key));
            }

            units = units.OrderBy(e => e.Name);
            units = units.Skip(0).Take(25);
            return units;
        }
    }
}
