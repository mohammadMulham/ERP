using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ERPAPI.Repositories
{
    public class ItemGroupRepository : CardRepository<ItemGroup>, IItemGroupRepository
    {
        public ItemGroupRepository(ERPContext context) : base(context)
        {
        }

        public override IQueryable<ItemGroup> GetAll()
        {
            return base.GetAll().Include(e => e.ParentItemGroup);
        }

        public string GetNewCode(Guid? ParentId = null, int BoxesNumber = 2, char FirstCode = '0')
        {
            var lastCode = NativeGetAll().Where(a => a.ParentId == ParentId).Max(e => e.Code);

            var parentCode = NativeGetAll().Where(a => a.Id == ParentId).Select(e => e.Code).SingleOrDefault();

            return GenerateNewCode(lastCode, parentCode, BoxesNumber);
        }

        public async Task<string> GetNewCodeAsync(Guid? ParentId = null, int BoxesNumber = 2, char FirstCode = '0')
        {
            var lastCode = await NativeGetAll().Where(e => e.ParentId == ParentId).MaxAsync(e => e.Code);

            var parentCode = await NativeGetAll().Where(e => e.Id == ParentId).Select(e => e.Code).SingleOrDefaultAsync();

            return GenerateNewCode(lastCode, parentCode, BoxesNumber);
        }

        public IQueryable<ItemGroup> Search(string key)
        {
            var itemGroups = NativeGetAllNoTracking();
            
            if (!string.IsNullOrEmpty(key))
            {
                itemGroups = itemGroups.Where(
                                e => e.Code.Contains(key)
                                || e.Name.Contains(key)
                                || e.Code.Contains(key)
                                || (e.Code + " " + e.Name).Contains(key)
                                || (e.Code + "-" + e.Name).Contains(key));
            }

            itemGroups = itemGroups.OrderBy(e => e.Code).ThenBy(e => e.Name);
            itemGroups = itemGroups.Skip(0).Take(25);
            return itemGroups;
        }

        public void LoadReferences(ItemGroup itemGroup)
        {
            Context.Entry(itemGroup).Reference(c => c.ParentItemGroup).Load();
        }
    }
}
