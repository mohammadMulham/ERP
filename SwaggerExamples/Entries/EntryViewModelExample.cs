using ERPAPI.ViewModels.Entries;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.Entries
{
    public class EntryViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new EntryViewModel
            {
                Id = new Random().Next(1000, 999999),
                Date = DateTimeOffset.UtcNow,
                BranchId = null,

                Items = new HashSet<EntryItemViewModel>()
                {
                    new EntryItemViewModel
                        {
                            AccountId = 1,
                            CostCenterId = 1,
                            Credit = 0,
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
