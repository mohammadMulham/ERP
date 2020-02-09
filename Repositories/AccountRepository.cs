using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ERPAPI.Repositories
{
    public class AccountRepository : CardRepository<Account>, IAccountRepository
    {
        public AccountRepository(ERPContext context) : base(context)
        {
        }

        public override IQueryable<Account> GetAll()
        {
            return base.GetAll().Include(e => e.ParentAccount).Include(e => e.FinalAccount).Include(e => e.Customer).Include(e => e.Currency);
        }

        public string GetNewCode(Guid? ParentId = null, int BoxesNumber = 2, char FirstCode = '0')
        {
            var lastCode = NativeGetAll().Where(a => a.ParentId == ParentId).Max(e => e.Code);

            var parentCode = NativeGetAll().Where(a => a.Id == ParentId).Select(e => e.Code).SingleOrDefault();

            return GenerateNewCode(lastCode, parentCode, BoxesNumber);
        }

        public async Task<string> GetNewCodeAsync(Guid? ParentId = null, int BoxesNumber = 2, char FirstCode = '0')
        {
            var lastCode = await NativeGetAll().Where(e => e.ParentId == ParentId).MaxAsync(e => e.Code);

            var parentCode = await NativeGetAll().Where(e => e.Id == ParentId).Select(e => e.Code).SingleOrDefaultAsync();

            return GenerateNewCode(lastCode, parentCode, BoxesNumber);
        }

        public IQueryable<Account> Search(string key, bool? leavesOnly = false, AccountType? accountType = null)
        {
            var accounts = NativeGetAllNoTracking();

            if (leavesOnly.HasValue && leavesOnly.Value)
            {
                accounts = accounts.Where(e => e.Accounts.LongCount() == 0);
            }

            if (accountType.HasValue)
            {
                accounts = accounts.Where(a => a.AccountType == accountType);
            }

            if (!string.IsNullOrEmpty(key))
            {
                accounts = accounts.Where(
                                e => e.Code.Contains(key)
                                || e.Name.Contains(key)
                                || e.Code.Contains(key)
                                || (e.Code + " " + e.Name).Contains(key)
                                || (e.Code + "-" + e.Name).Contains(key));
            }

            accounts = accounts.OrderBy(e => e.Code).ThenBy(e => e.Name);
            accounts = accounts.Skip(0).Take(25);
            return accounts;
        }

        public IQueryable<Account> GetCustomerAccounts(Guid customerId)
        {
            var account = NativeGetAllNoTracking().Where(c => c.CustomerId == customerId);
            return account;
        }

        public IQueryable<Account> SearchCustomers(string key)
        {
            var accounts = NativeGetAllNoTracking().Include(a => a.Customer).ThenInclude(c => c.CustomerType)
                            .Where(a => a.CustomerId != null);

            if (!string.IsNullOrEmpty(key))
            {
                accounts = accounts.Where(
                                a => a.Code.Contains(key)
                                || a.Name.Contains(key)
                                || a.Code.Contains(key)
                                || (a.Code + " " + a.Name).Contains(key)
                                || (a.Code + "-" + a.Name).Contains(key)
                                || (a.Customer.Number + " " + a.Customer.CustomerType.Name + " " + a.Customer.Name).Contains(key)
                                || (a.Customer.Number + "-" + a.Customer.CustomerType.Name + " " + a.Customer.Name).Contains(key));
            }

            accounts = accounts.OrderBy(e => e.Customer.Number).ThenBy(e => e.Name);
            accounts = accounts.Skip(0).Take(25);
            return accounts;
        }

        public void LoadReferences(Account account)
        {
            Context.Entry(account).Reference(c => c.ParentAccount).Load();
            Context.Entry(account).Reference(c => c.FinalAccount).Load();
            Context.Entry(account).Reference(c => c.Customer).Load();
        }

        public bool CheckIfCanBeDeleted(long id)
        {
            var canBeDeleted = NativeGetAllNoTracking().LongCount(e => e.ParentAccount.Number == id) == 0;
            return canBeDeleted;
        }

        public async Task<bool> CheckIfCanBeDeletedAsync(long id)
        {
            var canBeDeleted = await NativeGetAllNoTracking().LongCountAsync(e => e.ParentAccount.Number == id) == 0;
            return canBeDeleted;
        }
    }
}
