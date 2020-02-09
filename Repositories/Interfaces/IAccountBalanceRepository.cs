using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Repositories
{
    public interface IAccountBalanceRepository : IGenericRepository<AccountBalance>
    {
        AccountBalance GetAccountBalance(Guid accountId);
        Task<AccountBalance> GetAccountBalanceAsync(Guid accountId);
    }
}
