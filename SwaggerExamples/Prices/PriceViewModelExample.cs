using ERPAPI.ViewModels.Items;
using ERPAPI.ViewModels.Prices;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.Prices
{
    public class PriceViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new PriceViewModel()
            {
                CreatedDateTime = DateTimeOffset.UtcNow,
                Id = new Random().Next(1, 99),
                Name = "سعر 1",
            };
        }
    }
}
