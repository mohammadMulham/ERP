using ERPAPI.ViewModels.Sellers;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.Sellers
{
    public class AddSellerViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new AddSellerViewModel
            {
                Code = "01",
                Name = "بائع 1",
                ParentSellerId = new Random().Next(1000, 999999),
                Note = "",
            };
        }
    }
}
