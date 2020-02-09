using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ERPAPI.ViewModels.Bills
{
    public class NewBillViewModel
    {
        [Required]
        public long Number { get; set; }
        [Required]
        public long BillTypeId { get; set; }
        [Required]
        public string BillTypeName { get; set; }
        public long? PriceId { get; set; }
        public string PriceName { get; set; }
        public long? CurrencyId { get; set; }
        public long? AccountId { get; set; }
        public string AccountCodeName { get; set; }
        public long? StoreId { get; set; }
        public string StoreCodeName { get; set; }
        public long? CostCenterId { get; set; }
        public string CostCenterCodeName { get; set; }
        public long? BranchId { get; set; }
        public string BranchCodeName { get; set; }
        public PaysType? PayType { get; set; }
        // start bill type for view configuration
        [Required]
        public bool ShowItemStoreField { get; set; }
        [Required]
        public bool ShowItemCostCenterldField { get; set; }
        public bool ShowItemUnitField { get; set; }
        [Required]
        public bool ShowItemPriceFields { get; set; }
        [Required]
        public bool ShowItemNoteField { get; set; }
        public bool ShowItemExtraField { get; set; }
        public bool ShowItemDiscField { get; set; }
        [Required]
        public bool CanEditItemTotalPrice { get; set; }
        [Required]
        public bool CanEditItemPrice { get; set; }
        [Required]
        public bool ShowNoteField { get; set; }
        [Required]
        public bool ShowBranchField { get; set; }
        [Required]
        public bool ShowSellersFields { get; set; }
        [Required]
        public bool ShowCustomerAccountldField { get; set; }
        [Required]
        public bool ShowStoreField { get; set; }
        [Required]
        public bool ShowCostCenterldField { get; set; }
        [Required]
        public bool ShowPayTypeField { get; set; }
        [Required]
        public bool ShowDiscField { get; set; }
        [Required]
        public bool ShowItemExpireDateField { get; set; }
        [Required]
        public bool ShowExtraField { get; set; }
        [Required]
        public bool ShowItemProductionDateField { get; set; }
        [Required]
        public bool ShowTotalPriceItemField { get; set; }
        public int? Color1 { get; set; }
        public int? Color2 { get; set; }
    }
}
