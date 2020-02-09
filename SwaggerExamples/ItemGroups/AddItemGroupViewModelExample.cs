using ERPAPI.ViewModels.ItemGroups;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.ItemGroups
{
    public class AddItemGroupViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new AddItemGroupViewModel
            {
                Code = "01",
                Name = "مجموعة مواد 1",
                ParentItemGroupId = new Random().Next(1000, 999999),
                Note = "",
            };
        }
    }
}
