using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Repositories
{
    public interface ICustomerTypeRepository : IDefaultEntityRepository<CustomerType>
    {
        void LoadReferences(CustomerType customer);
    }
}
