using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.Entries
{
    public class EntryItemViewModel
    {
        public DateTimeOffset Date { get; set; }

        public long AccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountCode { get; set; }

        public long CurrencyId { get; set; }
        public string  CurrencyName { get; set; }
        public double CurrencyValue { get; set; }

        public long? CostCenterId { get; set; }
        public string CostCenterName { get; set; }
        public string CostCenterCode { get; set; }

        public double Debit { get; set; } // مدين
        public double Credit { get; set; } // دائن

        public string Note { get; set; }
    }
}
