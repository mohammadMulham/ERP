using ERPAPI.ViewModels.BillTypes;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.BillTypes
{
    public class AddBillTypeViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new AddBillTypeViewModel
            {
                Name = "نوع فاتورة 1",
                Code  = "ب.م"
            };
        }
    }
}
