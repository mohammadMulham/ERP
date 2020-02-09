using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ERPAPI.Repositories
{
    public class StoreRepository : CardRepository<Store>, IStoreRepository
    {
        public StoreRepository(ERPContext context) : base(context)
        {
        }

        public override IQueryable<Store> GetAll()
        {
            return base.GetAll().Include(e => e.ParentStore).Include(e => e.Account).Include(e => e.CostCenter);
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

        public IQueryable<Store> Search(string key)
        {
            var stores = NativeGetAllNoTracking();
            
            if (!string.IsNullOrEmpty(key))
            {
                stores = stores.Where(
                                e => e.Code.Contains(key)
                                || e.Name.Contains(key)
                                || e.Code.Contains(key)
                                || (e.Code + " " + e.Name).Contains(key)
                                || (e.Code + "-" + e.Name).Contains(key));
            }

            stores = stores.OrderBy(e => e.Code).ThenBy(e => e.Name);
            stores = stores.Skip(0).Take(25);
            return stores;
        }

        public void LoadReferences(Store store)
        {
            Context.Entry(store).Reference(c => c.ParentStore).Load();
            Context.Entry(store).Reference(c => c.Account).Load();
            Context.Entry(store).Reference(c => c.CostCenter).Load();
        }
    }
}
