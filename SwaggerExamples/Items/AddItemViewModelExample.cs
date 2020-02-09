using ERPAPI.ViewModels.Items;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.Items
{
    public class AddItemViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new AddItemViewModel
            {
                Code = "01",
                Name = "مادة 1",
                ItemGroupId = new Random().Next(1000, 999999),
                Type = Models.ItemType.Product,
                UnitId = new Random().Next(1, 9),
                UnitBarcode = "",
                Note = "",
            };
        }
    }
}
