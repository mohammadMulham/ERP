using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Models;

namespace ERPAPI.ViewModels.Items
{
    public class AddItemUnitViewModel
    {
        public long ItemId { get; set; }
        public string UnitBarcode { get; set; }
        public long UnitId { get; set; }
        public double UnitFactor { get; set; }
    }
}
