using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ERPAPI.Repositories
{
    public class EntryRepository : BaseEntityRepository<Entry>, IEntryRepository
    {
        public EntryRepository(ERPContext context) : base(context)
        {

        }

        public long GetNextNumber()
        {
            var count = Count();
            if (count == 0)
            {
                return 1;
            }
            var maxNumber = GetAll().Max(e => e.Number);
            return maxNumber + 1;
        }

        public async Task<long> GetNextNumberAsync()
        {
            var count = await CountAsync();
            if (count == 0)
            {
                return 1;
            }
            var maxNumber = await GetAll().MaxAsync(e => e.Number);
            return maxNumber + 1;
        }

        public override IQueryable<Entry> NativeGetAll()
        {
            return base.NativeGetAll()
                .Include(e => e.Items)
                 .ThenInclude(b=>b.Account)
                     .ThenInclude(c=>c.ParentAccount)
                  .ThenInclude(e => e.Currency)
                .Include(e=>e.PayEntry)
                 .ThenInclude(c=>c.PayType)
                 .Include(e=>e.BillEntry)
                  .ThenInclude(e=>e.Bill)
                .Where(e => e.FinancialPeriodId == PerdioId);
        }

        public override void SetCreateProps(Entry entry)
        {
            base.SetCreateProps(entry);
            entry.FinancialPeriodId = PerdioId;
        }
    }
}
