using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Repositories
{
    public interface ICustomerRepository : IDefaultEntityRepository<Customer>
    {
        void LoadReferences(Customer customer);
        IQueryable<Customer> Search(string key);
    }
}
