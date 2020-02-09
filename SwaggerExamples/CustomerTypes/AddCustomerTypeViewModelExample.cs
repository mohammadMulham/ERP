using ERPAPI.ViewModels.CustomerTypes;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.CustomerTypes
{
    public class AddCustomerTypeViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new AddCustomerTypeViewModel
            {
                Name = "نوع زبون 1",
            };
        }
    }
}
