using ERPAPI.ViewModels.PayTypes;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.PayTypes
{
    public class EditPayTypeViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new EditPayTypeViewModel
            {

                AutoPostToAccounts = true,
                DebitFieldName = "المقبوضات",
                CreditFieldName = "المدفوعات",
                DefaultAccountId = Guid.NewGuid(),
                PayTypeOrder = 1,
                DefaultBranchId = Guid.NewGuid(),
                IsBeginEntry = false,
                DefaultCostCenterId = Guid.NewGuid(),
                ShowItemCostCenterldField = false,
                ShowBranchField = false,
                DefaultCurrencyId = Guid.NewGuid(),
                ShowItemCreditField = false,
                ShowCostCenterldField = false,
                ShowItemCurrencyField = false,
                ShowItemDebitField = false,
                ShowItemNoteField = false,
                Color1 = 0,
                Color2 = 1,

            };
        }
    }
}
