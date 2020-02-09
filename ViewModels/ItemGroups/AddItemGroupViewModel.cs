using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.ItemGroups
{
    public class AddItemGroupViewModel : AddCardViewModel
    {
        public long? ParentItemGroupId { get; set; }
    }
}
