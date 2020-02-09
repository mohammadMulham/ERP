using ERPAPI.ViewModels.Items;
using ERPAPI.ViewModels.Companies;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.Companies
{
    public class CompanyViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new CompanyViewModel()
            {
                CreatedDateTime = DateTimeOffset.UtcNow,
                Id = new Random().Next(1, 99),
                Name = "شركة 1",
            };
        }
    }
}
