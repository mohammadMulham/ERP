using ERPAPI.ViewModels.PayEntries;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.PayEntries
{
    public class PayEntryViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new PayEntryViewModel
            {

            };
        }
    }
}
