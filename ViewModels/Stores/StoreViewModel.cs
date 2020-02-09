using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.Stores
{
    public class StoreViewModel : CardViewModel
    {
        public long? ParentStoreId { get; set; }
        public string ParentStoreName { get; set; }
        public string ParentStoreCode { get; set; }
        public long? AccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountCode { get; set; }
        public long? CostCenterId { get; set; }
        public string CostCenterName { get; set; }
        public string CostCenterCode { get; set; }
    }
}
