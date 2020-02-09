using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ERPAPI.ViewModels.Currencies
{
    public class CurrencyViewModel : CardViewModel
    {
        public double Value { get; set; }

        public string PartName { get; set; }

        public double PartRate { get; set; }

        public string ISOCode { get; set; }

        public double Equivalent { get; set; }
        public bool IsDefault { get; set; }
    }
}
