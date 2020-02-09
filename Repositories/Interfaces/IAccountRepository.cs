using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Repositories
{
    public interface IAccountRepository : ICardRepository<Account>, INewCode
    {
        new string GetNewCode(Guid? ParentId = null, int BoxesNumber = 2, char FirstCode = '0');
        new Task<string> GetNewCodeAsync(Guid? ParentId = null, int BoxesNumber = 2, char FirstCode = '0');
        IQueryable<Account> SearchCustomers(string key);
        IQueryable<Account> GetCustomerAccounts(Guid customerId);
        IQueryable<Account> Search(string key, bool? leavesOnly = false, AccountType? accountType = null);
        void LoadReferences(Account account);
        bool CheckIfCanBeDeleted(long id);
        Task<bool> CheckIfCanBeDeletedAsync(long id);
    }
}
