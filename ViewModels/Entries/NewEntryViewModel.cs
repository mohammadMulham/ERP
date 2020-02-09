using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.Entries
{
    public class NewEntryViewModel
    {
        public NewEntryViewModel()
        {
            Initialize();
        }
        
        private void Initialize()
        {
            Date = DateTimeOffset.UtcNow;
        }

        public long Number { get; set; }

        public DateTimeOffset Date { get; set; }

        public long CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public double CurrencyValue { get; set; }
    }
}
