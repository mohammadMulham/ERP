using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ERPAPI.Repositories
{
    public class CostCenterRepository : CardRepository<CostCenter>, ICostCenterRepository
    {
        public CostCenterRepository(ERPContext context) : base(context)
        {
        }

        public override IQueryable<CostCenter> GetAll()
        {
            return base.GetAll().Include(e => e.ParentCostCenter);
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

        public IQueryable<CostCenter> Search(string key)
        {
            var costCenters = NativeGetAllNoTracking();

            if (!string.IsNullOrEmpty(key))
            {
                costCenters = costCenters.Where(
                                e => e.Code.Contains(key)
                                || e.Name.Contains(key)
                                || e.Code.Contains(key)
                                || (e.Code + " " + e.Name).Contains(key)
                                || (e.Code + "-" + e.Name).Contains(key));
            }

            costCenters = costCenters.OrderBy(e => e.Code).ThenBy(e => e.Name);
            costCenters = costCenters.Skip(0).Take(25);
            return costCenters;
        }

        public void LoadReferences(CostCenter costCenter)
        {
            Context.Entry(costCenter).Reference(c => c.ParentCostCenter).Load();
        }

        public bool CheckIfCanBeDeleted(long id)
        {
            var canBeDeleted = NativeGetAllNoTracking().LongCount(e => e.ParentCostCenter.Number == id) == 0;
            return canBeDeleted;
        }

        public async Task<bool> CheckIfCanBeDeletedAsync(long id)
        {
            var canBeDeleted = await NativeGetAllNoTracking().LongCountAsync(e => e.ParentCostCenter.Number == id) == 0;
            return canBeDeleted;
        }
    }
}
