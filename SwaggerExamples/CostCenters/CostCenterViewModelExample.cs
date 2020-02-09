using ERPAPI.ViewModels.CostCenters;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.CostCenters
{
    public class CostCenterViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new CostCenterViewModel()
            {
                Code = "01",
                CreatedDateTime = DateTimeOffset.UtcNow,
                Id = new Random().Next(1, 99),
                Name = "مركز كلفة 1",
                Note = "",
                ParentCostCenterCode = "",
                ParentCostCenterId = null,
                ParentCostCenterName = ""
            };
        }
    }
}
