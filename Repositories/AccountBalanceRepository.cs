using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ERPAPI.Data;

namespace ERPAPI.Repositories
{
    public class AccountBalanceRepository : GenericRepository<AccountBalance>, IAccountBalanceRepository
    {
        public AccountBalanceRepository(ERPContext context) : base(context)
        {
        }

        public override IQueryable<AccountBalance> NativeGetAll()
        {
            return base.NativeGetAll().Where(e => e.FinancialPeriodId == PerdioId);
        }

        public override IQueryable<AccountBalance> GetAll()
        {
            return base.GetAll();
        }

        public AccountBalance GetAccountBalance(Guid accountId)
        {
            return NativeGetAll().FirstOrDefault(e => e.AccountId == accountId);
        }

        public async Task<AccountBalance> GetAccountBalanceAsync(Guid accountId)
        {
            return await NativeGetAll().FirstOrDefaultAsync(e => e.AccountId == accountId);
        }

  

        public override void SetCreateProps(AccountBalance accountBalance)
        {
            base.SetCreateProps(accountBalance);
            accountBalance.FinancialPeriodId = PerdioId;
        }
    }
}
