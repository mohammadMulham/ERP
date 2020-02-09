using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ERPAPI.ViewModels.Currencies
{
    public class EditCurrencyViewModel : AddCurrencyViewModel
    {
        public long Id { get; set; }
    }
}
