using ERPAPI.Models;
using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.ViewModels.BaseEntity;

namespace ERPAPI.ViewModels.Bills
{
    public class BillViewModel : BaseEntityViewModel
    {
        public DateTimeOffset Date { get; set; }

        public long? AccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountCode { get; set; }
        public string AccountCodeName { get; set; }

        public long? CustomerAccountId { get; set; }
        public string CustomerAccountName { get; set; }
        public string CustomerAccountCode { get; set; }

        public long BillTypeId { get; set; }
        public string BillTypeName { get; set; }

        public long CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyISOCode { get; set; }
        public double CurrencyValue { get; set; }

        public long? StoreId { get; set; }
        public string StoreCode { get; set; }
        public string StoreName { get; set; }
        public string StoreCodeName { get; set; }

        public long? CostCenterId { get; set; }
        public string CostCenterCode { get; set; }
        public string CostCenterName { get; set; }
        public string CostCenterCodeName { get; set; }

        public long? BranchId { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string BranchCodeName { get; set; }

        public PaysType PayType { get; set; }

        public double Total { get; set; } // مجموع كامل الفاتورة بعد الخصم و الزيادة

        public double TotalPaid { get; set; }

        public double TotalItems { get; set; }

        public double TotalExtra { get; set; }

        public double TotalDisc { get; set; }

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

        public string Note { get; set; }

        public IEnumerable<BillItemViewModel> Items { get; set; } = new List<BillItemViewModel>();
    }
}
