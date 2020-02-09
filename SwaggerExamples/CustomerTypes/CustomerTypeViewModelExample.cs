using ERPAPI.ViewModels.Items;
using ERPAPI.ViewModels.CustomerTypes;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.CustomerTypes
{
    public class CustomerTypeViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new CustomerTypeViewModel()
            {
                CreatedDateTime = DateTimeOffset.UtcNow,
                Id = new Random().Next(1, 99),
                Name = "نوع زبون 1",
            };
        }
    }
}
