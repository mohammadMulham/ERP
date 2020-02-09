using ERPAPI.ViewModels.Stores;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.Stores
{
    public class StoreViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new StoreViewModel()
            {
                Code = "01",
                CreatedDateTime = DateTimeOffset.UtcNow,
                Id = new Random().Next(1000, 999999),
                Name = "مخزن 1",
                Note = "",
                ParentStoreCode = "",
                ParentStoreId = null,
                ParentStoreName = "",
                AccountId = null,
                AccountName = "",
                AccountCode = "",
                CostCenterId = null,
                CostCenterName = "",
                CostCenterCode = ""
            };
        }
    }
}
