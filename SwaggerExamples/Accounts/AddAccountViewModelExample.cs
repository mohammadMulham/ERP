using ERPAPI.ViewModels.Accounts;
using ERPAPI.ViewModels.ItemGroups;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.Accounts
{
    public class AddAccountViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new AddAccountViewModel
            {
                Code = "01",
                Name = "حساب 1",
                ParentAccountId = new Random().Next(1000, 999999),
                Note = "",
                FinalAccountId = new Random().Next(1000, 999999),
                AccountType = Models.AccountType.Normal,
                CustomerId = null
            };
        }
    }
}
