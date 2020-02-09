using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ERPAPI.Repositories
{
    public class PriceRepository : DefaultEntityRepository<Price>, IPriceRepository
    {
        public PriceRepository(ERPContext context) : base(context)
        {

        }

        public void LoadReferences(Price price)
        {

        }

    }
}
