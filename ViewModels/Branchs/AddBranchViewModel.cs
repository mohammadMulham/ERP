using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.Branchs
{
    public class AddBranchViewModel : AddCardViewModel
    {
        public long? ParentBranchId { get; set; }
    }
}
