using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("BillEntryItems", Schema = "Accounting")]
    public class BillEntryItem : BaseClass
    {
        public BillEntryItem()
        {
            Initialize();
        }
        
        public BillEntryItem(DateTimeOffset date, Guid accountId, Guid currencyId, double currencyValue, Guid? costCenterId, BillEntryItemType type, double debit, double credit, string note)
        {
            Initialize();
            Date = date;
            AccountId = accountId;
            CurrencyId = currencyId;
            CurrencyValue = currencyValue;
            CostCenterId = costCenterId;
            Type = type;
            Debit = debit;
            Credit = credit;
            Note = note;
        }

        private void Initialize()
        {

        }

        public Guid BillId { get; set; }
        public virtual Bill Bill { get; set; }

        public DateTimeOffset Date { get; set; }

        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }

        public Guid CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
        public double CurrencyValue { get; set; }

        public Guid? CostCenterId { get; set; }
        public virtual CostCenter CostCenter { get; set; }

        public BillEntryItemType Type { get; set; }

        /// <summary>
        /// مدين: حسم
        /// </summary>
        public double Debit { get; set; }
        /// <summary>
        /// دائن اضافي
        /// </summary>
        public double Credit { get; set; }

        public string Note { get; set; }
    }
}
