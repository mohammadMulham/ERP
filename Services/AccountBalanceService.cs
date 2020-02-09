using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Models;
using ERPAPI.Repositories;

namespace ERPAPI.Services
{
    public class AccountBalanceService : IAccountBalanceService
    {
        private IAccountBalanceRepository _accountBalanceRepo;
        private IAccountRepository _accountRepo;
        public AccountBalanceService(IAccountBalanceRepository accountBalanceRepo, IAccountRepository accountRepo)
        {
            _accountBalanceRepo = accountBalanceRepo;
            _accountRepo = accountRepo;
        }
        public async Task<bool> PostEntryToAccounts(Entry entry, bool rollBack = false)
        {
            var accountsId = entry.Items.Select(a => a.AccountId).Distinct();
            var accounts = _accountRepo.NativeGetAll().Where(e => accountsId.Contains(e.Id));
            foreach (var item in accounts)
            {
                var accountEntryItems = entry.Items.Where(e => e.AccountId == item.Id);
                var totalDebit = accountEntryItems.Sum(e => e.Debit);
                var totalCredit = accountEntryItems.Sum(e => e.Credit);
                var accountBalance = await _accountBalanceRepo.GetAccountBalanceAsync(item.Id);

                if (rollBack)
                {
                    totalDebit = totalDebit * -1;
                    totalCredit = totalCredit * -1;
                }

                if (!rollBack && accountBalance == null)
                {
                    accountBalance = new AccountBalance { AccountId = item.Id, Debit = totalDebit, Credit = totalCredit };
                    await _accountBalanceRepo.AddAsync(accountBalance, false);
                }
                else
                {
                    accountBalance.Debit += totalDebit;
                    accountBalance.Credit += totalCredit;
                    _accountBalanceRepo.Edit(accountBalance, false);
                }

            }

            if (!rollBack)
            {
                entry.IsPosted = true;
            }

            return await Task.FromResult(true);
        }
    }

}
