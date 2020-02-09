using ERPAPI.ViewModels.Customers;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.Customers
{
    public class AddCustomerViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new AddCustomerViewModel
            {
                Name = "زبون 1",
                CustomerTypeId = new Random().Next(1000, 999999),
            };
        }
    }
}
