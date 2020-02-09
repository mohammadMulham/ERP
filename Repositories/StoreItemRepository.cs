using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ERPAPI.Repositories
{
    public class StoreItemRepository : GenericRepository<StoreItem>, IStoreItemRepository
    {
        public StoreItemRepository(ERPContext context) : base(context)
        {
        }

        public override IQueryable<StoreItem> NativeGetAll()
        {
            return base.NativeGetAll().Where(e => e.FinancialPeriodId == PerdioId);
        }

        public override void SetCreateProps(StoreItem StoreItem)
        {
            base.SetCreateProps(StoreItem);
            StoreItem.FinancialPeriodId = PerdioId;
        }
    }
}
