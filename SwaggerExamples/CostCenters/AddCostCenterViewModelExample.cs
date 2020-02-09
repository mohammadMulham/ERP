using ERPAPI.ViewModels.CostCenters;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.CostCenters
{
    public class AddCostCenterViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new AddCostCenterViewModel
            {
                Code = "01",
                Name = "مركز كلفة 1",
                ParentCostCenterId = new Random().Next(1000, 999999),
                Note = "",
            };
        }
    }
}
