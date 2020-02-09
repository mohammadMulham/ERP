using ERPAPI.Models;
using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.Items
{
    public class ItemViewModel : CardViewModel
    {
        public long DefaultUnitId { get; set; }
        public string DefaultUnitName { get; set; }
        public string DefaultUnitBarCode { get; set; }
        public long ItemGroupId { get; set; }
        public string ItemGroupName { get; set; }
        public string ItemGroupCode { get; set; }
        public ItemType Type { get; set; }
        public List<ItemUnitViewModel> Units { get; set; } = new List<ItemUnitViewModel>();
    }
}
