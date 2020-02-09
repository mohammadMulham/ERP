using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Models;
using ERPAPI.ViewModels.DefaultEntity;

namespace ERPAPI.ViewModels.FinancialPeriods
{
    public class AddFinancialPeriodViewModel : AddDefaultEntityViewModel
    {
        public long CompanyId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndtDate { get; set; }
    }
}
