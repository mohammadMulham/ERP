using ERPAPI.ViewModels.Currencies;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.Currencies
{
    public class CurrencyViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new CurrencyViewModel()
            {
                Code = "$",
                ISOCode = "USD",
                CreatedDateTime = DateTimeOffset.UtcNow,
                Id = new Random().Next(1, 99),
                Name = "عملة 1",
                Note = "",
                PartName = "جزء العملة",
                PartRate = 100,
                Value = new Random().Next(1, 99),
            };
        }
    }
}
