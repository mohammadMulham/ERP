
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.Bills
{
    public class AddPayViewModel
    {
        public DateTimeOffset Date { get; set; }
        public Guid AccountId { get; set; }
        public Guid CurrencyId { get; set; }
        public double CurrencyValue { get; set; }
        public Guid? CostCenterId { get; set; }
        /// <summary>
        /// مدين: قبض
        /// </summary>
        public double Debit { get; set; }
        /// <summary>
        /// دائن: دفع
        /// </summary>
        public double Credit { get; set; }
        public string Note { get; set; }
    }
}
