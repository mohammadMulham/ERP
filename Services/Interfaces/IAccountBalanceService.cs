using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Services
{
    public interface IAccountBalanceService
    {
        Task<bool> PostEntryToAccounts(Entry entry, bool rollBack = false);
    }
}
