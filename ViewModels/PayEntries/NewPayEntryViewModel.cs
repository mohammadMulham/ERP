using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.PayEntries
{
    public class NewPayEntryViewModel
    {
        public DateTimeOffset Date { get; set; }
        [Required]
        public long CurrencyId { get; set; }
        [Required]
        public double CurrencyValue { get; set; }

        public long? CostCenterId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyISOCode { get; set; }

        public long? BranchId { get; set; }
        public string BranchCodeName { get; set; }

        public long? PayAccountId { get; set; }
        [Required]
        public long PayTypeId { get; set; }
        public string PayTypeName { get; set; }
    }
}
