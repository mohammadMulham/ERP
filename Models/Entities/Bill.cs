using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("Bills", Schema = "Accounting")]
    public partial class Bill : BaseEntity
    {
        public Bill()
        {
            Initialize();
        }

        public Bill(Guid billTypeId, Guid currencyId, double currencyValue, Guid accountId, Guid? customerAccountId, string customerName, DateTimeOffset date, PaysType payType,
                Guid? storeId, Guid? costCenterId, Guid? branchId, double extra, double disc, double totalPaid, string note)
        {
            Initialize();
            BillTypeId = billTypeId;
            CurrencyId = currencyId;
            CurrencyValue = currencyValue;
            AccountId = accountId;
            CustomerAccountId = customerAccountId;
            CustomerName = customerName;
            Date = date;
            PayType = payType;
            StoreId = storeId;
            CostCenterId = costCenterId;
            BranchId = branchId;
            TotalPaid = totalPaid;
            Extra = extra;
            Disc = disc;
            Note = note;
        }

        private void Initialize()
        {
            Date = DateTimeOffset.UtcNow;
            BillItems = new HashSet<BillItem>();
            BillSellers = new HashSet<BillSeller>();
            BillEntryItems = new HashSet<BillEntryItem>();
        }

        public void CalcTotal()
        {
            Total = TotalItems + TotalItemsExtra - TotalItemsDisc;
        }

        /// <summary>
        /// Bill number separated is for each type with sequance
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override long Number { get; set; }

        public DateTimeOffset Date { get; set; }

        public Guid? AccountId { get; set; }
        public virtual Account Account { get; set; }

        public Guid FinancialPeriodId { get; set; }
        
        public virtual FinancialPeriod FinancialPeriod { get; set; }

        public Guid? CustomerAccountId { get; set; }
        public virtual Account CustomerAccount { get; set; }

        [StringLength(128)]
        public string CustomerName { get; set; }

        public Guid BillTypeId { get; set; }
        public virtual BillType BillType { get; set; }

        public Guid CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
        public double CurrencyValue { get; set; }

        public Guid? StoreId { get; set; }
        public virtual Store Store { get; set; }

        public Guid? CostCenterId { get; set; }
        public virtual CostCenter CostCenter { get; set; }

        public Guid? BranchId { get; set; }
        public virtual Branch Branch { get; set; }

        public PaysType PayType { get; set; }

        public string Note { get; set; }

        public double Extra { get; set; } // مجموع الزيادة على كامل الفاتورة 

        public double Disc { get; set; } // مجموع الخصم على كامل الفاتورة

        public double Total { get; set; } // مجموع كامل الفاتورة بعد الخصم و الزيادة

        public double TotalPaid { get; set; } // القيمة المدفوعة من الفاتورة

        #region accounting configurations

        /// <summary>
        /// تم توليد سند القيد
        /// </summary>
        public bool IsEntryGenerated { get; set; }

        /// <summary>
        /// تم ترحيلها الى المخازن
        /// </summary>
        public bool IsPosted { get; set; }
        /// <summary>
        /// تمت طباعتها
        /// </summary>
        public bool IsPrinted { get; set; }

        #endregion

        public virtual BillEntry BillEntry { get; set; }

        public virtual ICollection<BillItem> BillItems { get; set; }
        public virtual ICollection<BillSeller> BillSellers { get; set; }
        public virtual ICollection<BillEntryItem> BillEntryItems { get; set; }
    }
}
