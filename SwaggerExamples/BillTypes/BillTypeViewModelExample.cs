using ERPAPI.ViewModels.Items;
using ERPAPI.ViewModels.BillTypes;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.BillTypes
{
    public class BillTypeViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new BillTypeViewModel()
            {
                CreatedDateTime = DateTimeOffset.UtcNow,
                Id = new Random().Next(1, 99),
                Name = "نوع فاتورة 1",
            };
        }
    }
}
