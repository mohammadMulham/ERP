using ERPAPI.Models;
using ERPAPI.ViewModels.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.Entries
{
    public class EntryViewModel : BaseEntityViewModel
    {
        public DateTimeOffset Date { get; set; }

        public long CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public double CurrencyValue { get; set; }
        public string CurrencyISOCode { get; set; }

        /// <summary>
        /// اصل السند من اين
        /// </summary>
        public EntryOrigin EntryOrigin { get; set; }
        /// <summary>

        public long? BranchId { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }

        /// <summary>
        /// تم ترحيلها الى الحسابات
        /// </summary>
        public bool IsPosted { get; set; }

        public string Note { get; set; }

        public IEnumerable<EntryItemViewModel> Items { get; set; }
    }
}
