using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.Reports.CustomerAccountStatement
{
    public class CusAccStatmentOptions
    {
        public Guid? CustomerId { get; set; }
        public Guid? CusAccountId { get; set; }
        public IQueryable<Guid> CustomerAcounts { get; set; }
    }
    
}
