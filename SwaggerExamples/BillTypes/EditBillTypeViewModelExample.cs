using ERPAPI.ViewModels.BillTypes;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.BillTypes
{
    public class EditBillTypeViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new EditBillTypeViewModel
            {

            };
        }
    }
}
