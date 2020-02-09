using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ERPAPI.Repositories
{
    public class CompanyRepository : DefaultEntityRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(ERPContext context) : base(context)
        {

        }

        public void LoadReferences(Company company)
        {

        }

    }
}
