using ERPAPI.ViewModels.Branchs;
using ERPAPI.ViewModels.ItemGroups;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.Branchs
{
    public class BranchViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new BranchViewModel()
            {
                Code = "01",
                CreatedDateTime = DateTimeOffset.UtcNow,
                Id = new Random().Next(1000, 999999),
                Name = "فرع 1",
                Note = "",
                ParentBranchCode = "",
                ParentBranchId = null,
                ParentBranchName = ""
            };
        }
    }
}
