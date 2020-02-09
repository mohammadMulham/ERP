using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ERPAPI.Repositories
{
    public class FinancialPeriodRepository : DefaultEntityRepository<FinancialPeriod>, IFinancialPeriodRepository
    {
        public FinancialPeriodRepository(ERPContext context) : base(context)
        {

        }

        public override IQueryable<FinancialPeriod> GetAll()
        {
            return base.GetAll().Include(e => e.Company);
        }

        public void LoadReferences(FinancialPeriod FinancialPeriod)
        {
            Context.Entry(FinancialPeriod).Reference(c => c.Company).Load();
        }

    }
}
