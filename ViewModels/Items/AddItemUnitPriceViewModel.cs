using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ERPAPI.ViewModels.Items
{
    public class AddItemUnitPriceViewModel
    {
        public long ItemId { get; set; }
        public long UnitId { get; set; }
        public long PriceId { get; set; }
        [Range(1, double.MaxValue)]
        public double Price { get; set; }
    }
}
