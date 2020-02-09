using ERPAPI.Models;
using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.Items
{
    public class ItemUnitViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string BarCode { get; set; }
        public double Factor { get; set; }
    }
}
