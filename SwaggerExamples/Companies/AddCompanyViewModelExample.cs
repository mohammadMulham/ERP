using ERPAPI.ViewModels.Companies;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.Companies
{
    public class AddCompanyViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new AddCompanyViewModel
            {
                Name = "شركة 1",
            };
        }
    }
}
