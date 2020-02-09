using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ERPAPI.Repositories
{
    public class CustomerRepository : DefaultEntityRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ERPContext context) : base(context)
        {

        }
        
        public void LoadReferences(Customer customer)
        {
            Context.Entry(customer).Reference(c => c.CustomerType).Load();
        }

        public IQueryable<Customer> Search(string key)
        {
            var customers = NativeGetAllNoTracking().Include(a => a.CustomerType).AsQueryable();

            if (!string.IsNullOrEmpty(key))
            {
                customers = customers.Where(
                                e => e.Number.ToString().Contains(key)
                                || e.Name.Contains(key)
                                || e.Number.ToString().Contains(key)

                                || (e.Number.ToString() + " " + e.CustomerType.Name + " " + e.Name).Contains(key)
                                || (e.Number.ToString() + "-" + e.CustomerType.Name + " " + e.Name).Contains(key)

                                || (e.Number.ToString() + " " + e.Name).Contains(key)
                                || (e.Number.ToString() + "-" + e.Name).Contains(key));
            }

            customers = customers.OrderBy(e => e.Number.ToString()).ThenBy(e => e.Name);
            customers = customers.Skip(0).Take(25);
            return customers;
        }
    }
}
