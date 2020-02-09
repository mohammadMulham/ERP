using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("Entries", Schema = "Accounting")]
    public class Entry : BaseEntity
    {
        public Entry()
        {
            Initialize();
        }

        public Entry(long number, Guid currencyId, double currencyValue, Guid? branchId, string note, bool isPosted = true)
        {
            Initialize();
            Number = number;
            CurrencyId = currencyId;
            CurrencyValue = currencyValue;
            BranchId = branchId;
            IsPosted = isPosted;
            Note = note;
        }

        private void Initialize()
        {
            Date = DateTimeOffset.UtcNow;
            Items = new HashSet<EntryItem>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override long Number { get; set; }

        public DateTimeOffset Date { get; set; }

        public Guid FinancialPeriodId { get; set; }
        public virtual FinancialPeriod FinancialPeriod { get; set; }

        public Guid CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
        public double CurrencyValue { get; set; }

        /// <summary>
        /// اصل السند من اين
        /// </summary>
        public EntryOrigin EntryOrigin { get; set; }

        public Guid? BranchId { get; set; }
        public virtual Branch Branch { get; set; }

        /// <summary>
        /// تم ترحيلها الى الحسابات
        /// </summary>
        public bool IsPosted { get; set; }

        public string Note { get; set; }

        public virtual PayEntry PayEntry { get; set; }
        public virtual BillEntry BillEntry { get; set; }
        public virtual ICollection<EntryItem> Items { get; set; }

    }
}
