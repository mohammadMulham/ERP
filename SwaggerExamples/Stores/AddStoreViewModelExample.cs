using ERPAPI.ViewModels.Stores;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.Stores
{
    public class AddStoreViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new AddStoreViewModel
            {
                Code = "01",
                Name = "مخزن 1",
                ParentId = new Random().Next(1000, 999999),
                Note = "",
                AccountId = new Random().Next(1000, 999999),
                CostCenterId = new Random().Next(1000, 999999)
            };
        }
    }
}
