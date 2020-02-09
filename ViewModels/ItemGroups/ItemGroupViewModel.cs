using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.ItemGroups
{
    public class ItemGroupViewModel : CardViewModel
    {
        public long? ParentItemGroupId { get; set; }
        public string ParentItemGroupName { get; set; }
        public string ParentItemGroupCode { get; set; }
    }
}
