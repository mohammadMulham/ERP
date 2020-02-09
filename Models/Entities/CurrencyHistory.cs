using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{

    [Table("CurrencyHistories", Schema = "Accounting")]
    public partial class CurrencyHistory : BaseClass
    {

        public CurrencyHistory()
        {
            StartDate = DateTimeOffset.UtcNow;
        }

        public Guid CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
        public double CurrencyValue { get; set; }

        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset? EndtDate { get; set; }
    }
}
