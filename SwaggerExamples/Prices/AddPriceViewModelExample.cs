using ERPAPI.ViewModels.Prices;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.Prices
{
    public class AddPriceViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new AddPriceViewModel
            {
                Name = "سعر 1",
            };
        }
    }
}
