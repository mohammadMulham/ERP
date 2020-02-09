using ERPAPI.ViewModels.PayEntries;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.PayEntries
{
    public class AddPayEntryViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new AddPayEntryViewModel()
            {
                Date = DateTimeOffset.UtcNow,
                PayAccountId = 5,
                PayTypeId = 7,
                Items = new HashSet<AddPayEntryItemViewModel>
                {
                    new AddPayEntryItemViewModel
                    {
                        AccountId=6,
                        Debit=100,
                        Credit=0,
                        CurrencyId=1,
                        CurrencyValue=1,
                        Date = DateTime.UtcNow,
                        Note="",
                    }
                    ,
                    new AddPayEntryItemViewModel
                    {
                        AccountId=1,
                        Debit=100,
                        Credit=0,
                        CurrencyId=1,
                        CurrencyValue=1,
                        Date = DateTime.UtcNow,
                        Note="",
                    }
                }
            };
        }
    }
}
