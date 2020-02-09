using ERPAPI.ViewModels.Items;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.Items
{
    public class ItemViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new ItemViewModel()
            {
                Code = "01",
                CreatedDateTime = DateTimeOffset.UtcNow,
                Id = new Random().Next(1000, 999999),
                Name = "مجموعة مواد 1",
                Note = "",
                ItemGroupCode = "",
                ItemGroupId = new Random().Next(1000, 999999),
                ItemGroupName = ""
            };
        }
    }
}
