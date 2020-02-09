using ERPAPI.ViewModels.Currencies;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.Currencies
{
    public class AddCurrencyViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new AddCurrencyViewModel
            {
                Code = "$",
                Name = "عملة 1",
                Value = 1,
                ISOCode = "USD",
                PartName = "جزء العملة",
                PartRate = 100,
                Note = "",
            };
        }
    }
}
