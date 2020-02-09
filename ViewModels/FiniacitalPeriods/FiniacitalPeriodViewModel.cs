using ERPAPI.Models;
using ERPAPI.ViewModels.Card;
using ERPAPI.ViewModels.DefaultEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.FinancialPeriods
{
    public class FinancialPeriodViewModel : DefaultEntityViewModel
    {
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }

        public int Year { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndtDate { get; set; }
    }
}
