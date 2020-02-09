using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.CostCenters
{
    public class AddCostCenterViewModel : AddCardViewModel
    {
        public long? ParentCostCenterId { get; set; }
    }
}
