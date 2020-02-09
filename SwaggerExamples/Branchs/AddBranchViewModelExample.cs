using ERPAPI.ViewModels.Branchs;
using ERPAPI.ViewModels.ItemGroups;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.Branchs
{
    public class AddBranchViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new AddBranchViewModel
            {
                Code = "01",
                Name = "فرع 1",
                ParentBranchId = new Random().Next(1000, 999999),
                Note = "",
            };
        }
    }
}
