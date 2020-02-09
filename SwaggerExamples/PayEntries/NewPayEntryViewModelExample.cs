using ERPAPI.ViewModels.PayEntries;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.PayEntries
{
    
      public class NewPayEntryViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new NewPayEntryViewModel
            {
                Date = DateTimeOffset.UtcNow,
                PayAccountId = 5,
                PayTypeId = 7,
            };
        }
    }
}
