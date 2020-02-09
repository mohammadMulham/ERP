using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Repositories
{
    public interface IFinancialPeriodRepository : IDefaultEntityRepository<FinancialPeriod>
    {
        void LoadReferences(FinancialPeriod FinancialPeriod);
    }
}
