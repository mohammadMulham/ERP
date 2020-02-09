using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.Reports.CustomerAccountStatement
{
  
    public class CustomerAccountStatementViewModel
    {
        public List<CusEntryViewModel> Items { get; set; }
        public double DebitTotal { get { return Items.Sum(e=>e.Debit); } set { } }
        public double CreditTotal { get { return Items.Sum(e => e.Credit); } set { } }
        public double Balance { get { return DebitTotal - CreditTotal; } set { } }
        public DateTimeOffset? From { get; set; }
        public DateTimeOffset? To { get; set; }
        public string CurrencyCodeName { get; set; }

    }
    public class CusEntryViewModel
    {
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset Date { get; set; }
        public long EntryNo { get; set; }
        public string EntryTypeNoName { get; set; }
        public long? EntryOriginNo { get; set; }
        public EntryOrigin EntryOrigin { get; set; }
        public double Debit { get; set; } // مدين
        public double Credit { get; set; } // دائن
        public string ContraAccount { get; set; } // دائن
        public string Note { get; set; }
        public List<CusBillItemViewModel> BillItems { get; set; }
      

    }
    public class CusBillItemViewModel
    {
  
        public string ItemUnitCodeName { get; set; }
        public string StoreCodeName { get; set; }
        public string CostCenterCodeName { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double Extra { get; set; }
        public double Disc { get; set; }
        public double Total { get; set; }
        public string Note { get; set; }
    }
    public class CusEntyItemViewModel
    {
      
    }

}
