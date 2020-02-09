using ERPAPI.ViewModels.Entries;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.Entries
{
    public class AddEntryViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new AddEntryViewModel
            {
                Number = new Random().Next(1000, 999999),
                Date = DateTimeOffset.UtcNow,
                BranchId = null,
                CostCenterId = null,
                Items = new HashSet<AddEntryItemViewModel>()
                {
                    new AddEntryItemViewModel
                    {
                        AccountId = 1,
                        CostCenterId = 1,
                        Credit = 0,
                        Debit = 100,
                        Date = DateTimeOffset.UtcNow,
                        Note = "",
                        CurrencyId = 1,
                        CurrencyValue = 1,
                    },
                    new AddEntryItemViewModel
                    {
                        AccountId = 1,
                        CostCenterId = 1,
                        Credit = 100,
                        Debit = 0,
                        Date = DateTimeOffset.UtcNow,
                        Note = "",
                        CurrencyId = 1,
                        CurrencyValue = 1,
                    }
                },

                CurrencyId = 1,
                CurrencyValue = 1,
                
                Note = "قيد 1",
            };
        }
    }
}
