using ERPAPI.ViewModels.Items;
using ERPAPI.ViewModels.FinancialPeriods;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.FinancialPeriods
{
    public class FinancialPeriodViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new FinancialPeriodViewModel()
            {
                CreatedDateTime = DateTimeOffset.UtcNow,
                Id = new Random().Next(1, 99),
                Name = "ملفات عام 2017",
            };
        }
    }
}
