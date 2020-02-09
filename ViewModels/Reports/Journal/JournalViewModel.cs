using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.Reports.Journal
{
    public class JournalViewModel
    {
        public List<JournalEntryViewModel> Items { get; set; }
        public double DebitTotal { get { return Items.Sum(e => e.Items.Sum(c => c.Debit)); } set { } }
        public double CreditTotal { get { return Items.Sum(e => e.Items.Sum(c => c.Credit)); } set { } }
        public double Balance { get { return DebitTotal - CreditTotal; } set { } }


    }
    public class JournalEntryViewModel
    {
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset Date { get; set; }
        public long EntryNo { get; set; }
        public long? EntryOriginNo { get; set; }
        public EntryOrigin EntryOrigin { get; set; }
        public List<JournalItemViewModel> Items { get; set; }
        public double? DebitSum { get; set; }
        public double? CreditSum { get; set; }

    }
    public class JournalItemViewModel
    {
        public string AccountCodeName { get; set; }
        public string ParentAccountCodeName { get; set; }
        public string CurrencyCode { get; set; }
        public double CurrencyValue { get; set; }
        public string CostCenterCodeName { get; set; }
        public double Debit { get; set; } // مدين
        public double Credit { get; set; } // دائن
        public string Note { get; set; }

    }

}
