using ERPAPI.Data;
using Microsoft.EntityFrameworkCore;
using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Repositories
{
    public class EntryItemRepository : GenericRepository<EntryItem>, IEntryItemRepository
    {
        public EntryItemRepository(ERPContext context) : base(context)
        {
        }
        public override IQueryable<EntryItem> NativeGetAll()
        {
            return base.NativeGetAll()
                .Include(e => e.Entry)
                   .ThenInclude(e=>e.BillEntry)
                .Include(e => e.Entry.PayEntry)
                .Include(e=>e.Account)
                 .ThenInclude(e=>e.ParentAccount)
                .Where(e => e.Entry.FinancialPeriodId == PerdioId);
        }
    }
}
