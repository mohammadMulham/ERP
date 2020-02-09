using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ERPAPI.Repositories
{
    public class CustomerTypeRepository : DefaultEntityRepository<CustomerType>, ICustomerTypeRepository
    {
        public CustomerTypeRepository(ERPContext context) : base(context)
        {

        }

        public void LoadReferences(CustomerType customerType)
        {

        }

    }
}
