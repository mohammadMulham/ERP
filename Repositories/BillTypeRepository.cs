using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ERPAPI.Repositories
{
    public class BillTypeRepository : DefaultEntityRepository<BillType>, IBillTypeRepository
    {
        public BillTypeRepository(ERPContext context) : base(context)
        {

        }

        public void LoadReferences(BillType billType)
        {

        }

    }
}
