using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("EntryItems", Schema = "Accounting")]
    public class EntryItem : BaseClass
    {
        public EntryItem()
        {
            Initialize();
        }

        public EntryItem(Guid accountId,double debit,double credit, Guid currencyId, double currencyValue,Guid? costCenterId,DateTime date,string note)
        {
            Initialize();
            AccountId = accountId;
            Debit = debit;
            Credit = credit;
            CurrencyId = currencyId;
            CurrencyValue = currencyValue;
            CostCenterId = costCenterId;
            Date = date;
            Note = note;
        }

        private void Initialize()
        {

        }

        public Guid EntryId { get; set; }
        public virtual Entry Entry { get; set; }

        public DateTimeOffset Date { get; set; }

        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }

        public Guid CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
        public double CurrencyValue { get; set; }

        public Guid? CostCenterId { get; set; }
        public virtual CostCenter CostCenter { get; set; }

        public double Debit { get; set; } // مدين
        public double Credit { get; set; } // دائن
        public string Note { get; set; }

    }
}
