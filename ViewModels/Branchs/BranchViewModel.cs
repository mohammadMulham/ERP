using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.Branchs
{
    public class BranchViewModel : CardViewModel
    {
        public long? ParentBranchId { get; set; }
        public string ParentBranchName { get; set; }
        public string ParentBranchCode { get; set; }
    }
}
