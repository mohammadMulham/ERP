using ERPAPI.ViewModels.Accounts;
using ERPAPI.ViewModels.ItemGroups;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.Accounts
{
    public class AccountViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new AccountViewModel()
            {
                Code = "01",
                CreatedDateTime = DateTimeOffset.UtcNow,
                Id = new Random().Next(1000, 999999),
                Name = "حساب 1",
                Note = "",
                ParentAccountCode = "",
                ParentAccountId = null,
                ParentAccountName = "",
                FinalAccountCode = "",
                FinalAccountId = null,
                FinalAccountName = "",
                AccountType = Models.AccountType.Normal
            };
        }
    }
}
