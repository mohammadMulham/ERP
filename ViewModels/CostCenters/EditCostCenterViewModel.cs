using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.CostCenters
{
    public class EditCostCenterViewModel : EditCardViewModel
    {
        public long? ParentCostCenterId { get; set; }
    }
}
