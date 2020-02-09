using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Models;

namespace ERPAPI.ViewModels.Items
{
    public class AddItemViewModel : AddCardViewModel
    {
        public long ItemGroupId { get; set; }
        public ItemType Type { get; set; }
        public string UnitBarcode { get; set; }
        public long UnitId { get; set; }
    }
}
