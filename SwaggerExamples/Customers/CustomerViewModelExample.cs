using ERPAPI.ViewModels.Items;
using ERPAPI.ViewModels.Customers;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.Customers
{
    public class CustomerViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new CustomerViewModel()
            {
                CreatedDateTime = DateTimeOffset.UtcNow,
                Id = new Random().Next(1, 99),
                Name = "زبون 1",
            };
        }
    }
}
