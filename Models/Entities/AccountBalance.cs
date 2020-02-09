using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("AccountBalances", Schema = "Accounting")]
    public class AccountBalance : BaseClass
    {
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }

        public Guid FinancialPeriodId { get; set; }
        public virtual FinancialPeriod FinancialPeriod { get; set; }

        public double Debit { get; set; } // مدين
        public double Credit { get; set; } // دائن
    }
}
