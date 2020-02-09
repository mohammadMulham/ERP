using ERPAPI.Models;
using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.ViewModels.BaseEntity;

namespace ERPAPI.ViewModels.Bills
{
    public class BillItemViewModel
    {
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string ItemCodeName { get; set; }
        public long UnitId { get; set; }
        public string UnitName { get; set; }
        public long? StoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreCode { get; set; }
        public string StoreCodeName { get; set; }
        public long? CostCenterId { get; set; }
        public string CostCenterName { get; set; }
        public string CostCenterCode { get; set; }
        public string CostCenterCodeName { get; set; }
        public DateTime? ExpireDate { get; set; }
        public DateTime? ProductionDate { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double Extra { get; set; }
        public double Disc { get; set; }
        public double Total { get; set; }
        public string Note { get; set; }
    }
}
