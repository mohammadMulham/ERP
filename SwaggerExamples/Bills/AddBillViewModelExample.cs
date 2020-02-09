using ERPAPI.ViewModels.Bills;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.Bills
{
    public class AddBillViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new AddBillViewModel
            {
                Date = DateTimeOffset.UtcNow,
                BillTypeId = new Random().Next(1, 9),
                StoreId = new Random().Next(1, 9),
                CurrencyId = 1,
                CurrencyValue = 1,
                AccountId = new Random().Next(1000, 9999),
                Note = "فاتورة 1",
                Items = new HashSet<AddBillItemViewModel>() {
                     new AddBillItemViewModel() { ItemId = new Random().Next(1000, 999999), UnitId = new Random().Next(1000, 999999), Price = new Random().Next(100, 9999), Quantity = new Random().Next(1000, 999999), Extra = new Random().Next(1, 100), Disc = new Random().Next(1, 100)},
                     new AddBillItemViewModel() { ItemId = new Random().Next(1000, 999999), UnitId = new Random().Next(1000, 999999), Price = new Random().Next(100, 9999), Quantity = new Random().Next(1000, 999999), Extra = new Random().Next(1, 100), Disc = new Random().Next(1, 100)},
                     new AddBillItemViewModel() { ItemId = new Random().Next(1000, 999999), UnitId = new Random().Next(1000, 999999), Price = new Random().Next(100, 9999), Quantity = new Random().Next(1000, 999999), Extra = new Random().Next(1, 100), Disc = new Random().Next(1, 100)},
                 }
            };
        }
    }
}
