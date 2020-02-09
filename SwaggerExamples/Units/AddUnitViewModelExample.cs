using ERPAPI.ViewModels.Units;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.Units
{
    public class AddUnitViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new AddUnitViewModel
            {
                Name = "وحدة 1",
            };
        }
    }
}
