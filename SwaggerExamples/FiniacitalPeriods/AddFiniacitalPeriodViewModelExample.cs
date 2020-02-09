using ERPAPI.ViewModels.FinancialPeriods;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.FinancialPeriods
{
    public class AddFinancialPeriodViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new AddFinancialPeriodViewModel
            {
                Name = "ملفات عام 2017",
            };
        }
    }
}
