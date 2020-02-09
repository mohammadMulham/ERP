using ERPAPI.ViewModels.Items;
using ERPAPI.ViewModels.Bills;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.Bills
{
    public class BillViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new BillViewModel()
            {
                CreatedDateTime = DateTimeOffset.UtcNow,
                Id = new Random().Next(1, 99),
                Note = "فاتورة 1",
            };
        }
    }
}
