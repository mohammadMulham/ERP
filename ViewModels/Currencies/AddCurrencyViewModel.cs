using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ERPAPI.ViewModels.Currencies
{
    public class AddCurrencyViewModel : AddCardViewModel
    {
        [StringLength(128, MinimumLength = 1)]
        public override string Code { get => base.Code; set => base.Code = value; }

        [Range(0, double.MaxValue)]
        public double Value { get; set; }

        [StringLength(128, MinimumLength = 2)]
        public string PartName { get; set; }

        [Range(1, double.MaxValue)]
        public double PartRate { get; set; }

        [StringLength(128, MinimumLength = 2)]
        public string ISOCode { get; set; }
    }
}
