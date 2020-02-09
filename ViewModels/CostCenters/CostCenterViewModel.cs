using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.CostCenters
{
    public class CostCenterViewModel : CardViewModel
    {
        public long? ParentCostCenterId { get; set; }
        public string ParentCostCenterName { get; set; }
        public string ParentCostCenterCode { get; set; }
        public string ParentCostCenterCodeName { get; set; }
    }
}
