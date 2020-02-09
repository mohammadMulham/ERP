using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ERPAPI.Repositories
{
    public class CurrencyRepository : CardRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(ERPContext context) : base(context)
        {
        }

        public Currency GetDefaultCurrency(long number)
        {
            return base.Get(number);
        }
    }
}
