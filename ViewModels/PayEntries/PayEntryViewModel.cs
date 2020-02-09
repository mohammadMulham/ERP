using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.PayEntries
{
    public class PayEntryViewModel
    {
        public long Number { get; set; }
        public DateTimeOffset Date { get; set; }
        public long EntryId { get; set; }
        public long CurrencyId { get; set; }
        public double CurrencyValue { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyISOCode { get; set; }
        public long? CostCenterId { get; set; }
        public long? BranchId { get; set; }
        public string  BranchCodeName { get; set; }
        public long? PayAccountId { get; set; }

        public long PayTypeId { get; set; }
        public string PayTypeName { get; set; }

        public ICollection<EntryItem> Items { get; set; } = new HashSet<EntryItem>();
    }
}
