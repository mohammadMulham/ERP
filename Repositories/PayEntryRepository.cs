using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ERPAPI.Repositories
{
    public class PayEntryRepository : BaseEntityRepository<PayEntry> , IPayEntryRepository
    {
        public PayEntryRepository(ERPContext context) : base(context)
        {

        }
        public long GetNextNumber(Guid payTypeId)
        {
          var count = GetAll().Where(p => p.PayTypeId == payTypeId).Count();
           if (count == 0)
            {
                    return 1;
            }
            var maxNumber = NativeGetAllNoTracking().Max(e => e.Number);
            return maxNumber + 1;
        }

       public async Task<long> GetNextNumberAsync(Guid payTypeId)
        {
            var count = await GetAll().Where(p => p.PayTypeId == payTypeId).CountAsync();
            if (count == 0)
            {
                return 1;
            }
            var maxNumber = await NativeGetAllNoTracking().MaxAsync(e => e.Number);
            return maxNumber + 1;
        }

        public IQueryable<PayEntry> GetAllByTypeId(Guid typeId)
        {
            var payEntries = GetAllNoTracking().Where(e => e.PayTypeId == typeId);
            return payEntries;
        }


        public PayEntry Get(Guid payTypeId, long number)
        {
            var entity = GetAll().Include(e=>e.Entry).FirstOrDefault(e => e.PayTypeId == payTypeId && e.Number == number);
            return entity;
        }

        
        public async Task<PayEntry> GetAsync(Guid payTypeId, long number)
        {
            var entity = await GetAll().Include(e => e.Entry).FirstOrDefaultAsync(e => e.PayTypeId == payTypeId && e.Number == number);
            return entity;
        }

        public void LoadReferences(PayEntry payEntry)
        {
           
        }

       
    }
}
