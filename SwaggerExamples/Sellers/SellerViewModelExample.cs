using ERPAPI.ViewModels.Sellers;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.Sellers
{
    public class SellerViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new SellerViewModel()
            {
                Code = "01",
                CreatedDateTime = DateTimeOffset.UtcNow,
                Id = new Random().Next(1, 99),
                Name = "بائع 1",
                Note = "",
                ParentSellerCode = "",
                ParentSellerId = null,
                ParentSellerName = ""
            };
        }
    }
}
