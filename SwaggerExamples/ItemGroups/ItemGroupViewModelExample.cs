using ERPAPI.ViewModels.ItemGroups;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.ItemGroups
{
    public class ItemGroupViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new ItemGroupViewModel()
            {
                Code = "01",
                CreatedDateTime = DateTimeOffset.UtcNow,
                Id = new Random().Next(1000, 999999),
                Name = "مجموعة مواد 1",
                Note = "",
                ParentItemGroupCode = "",
                ParentItemGroupId = null,
                ParentItemGroupName = ""
            };
        }
    }
}
