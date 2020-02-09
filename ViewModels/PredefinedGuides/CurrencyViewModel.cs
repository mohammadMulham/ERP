using ERPAPI.Models;
using ERPAPI.ViewModels.Card;
using ERPAPI.ViewModels.DefaultEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.PredefinedGuides
{
    public class CurrencyViewModel : CardViewModel
    {
        public string ISOCode { get; set; }
        public string PartName { get; set; }
        public double PartRate { get; set; }
    }
}
