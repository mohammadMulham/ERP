using ERPAPI.ViewModels.Items;
using ERPAPI.ViewModels.Units;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.Units
{
    public class UnitViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new UnitViewModel()
            {
                CreatedDateTime = DateTimeOffset.UtcNow,
                Id = new Random().Next(1, 99),
                Name = "وحدة 1",
            };
        }
    }
}
