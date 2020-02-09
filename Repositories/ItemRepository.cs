using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ERPAPI.Repositories
{
    public class ItemRepository : CardRepository<Item>, IItemRepository
    {
        private IItemGroupRepository _itemGroupRepo;

        public ItemRepository(ERPContext context, IItemGroupRepository itemGroupRepo) : base(context)
        {
            _itemGroupRepo = itemGroupRepo;
        }

        public override IQueryable<Item> GetAll()
        {
            return base.GetAll().Include(i => i.ItemUnits).ThenInclude(e => e.Unit).Include(i => i.ItemGroup);
        }

        public string GetNewCode(Guid? ParentId = null, int BoxesNumber = 2, char FirstCode = '0')
        {
            var lastCode = NativeGetAll().Where(a => a.ItemGroupId == ParentId).Max(e => e.Code);

            var parentCode = NativeGetAll().Where(a => a.Id == ParentId).Select(e => e.Code).SingleOrDefault();

            return GenerateNewCode(lastCode, parentCode, BoxesNumber, FirstCode);
        }

        public async Task<string> GetNewCodeAsync(Guid? ParentId = null, int BoxesNumber = 2, char FirstCode = '0')
        {
            var lastCode = await NativeGetAll().Where(e => e.ItemGroupId == ParentId).MaxAsync(e => e.Code);

            var parentCode = await _itemGroupRepo.NativeGetAll().Where(e => e.Id == ParentId).Select(e => e.Code).SingleOrDefaultAsync();

            return GenerateNewCode(lastCode, parentCode, BoxesNumber, FirstCode);
        }
    }
}
