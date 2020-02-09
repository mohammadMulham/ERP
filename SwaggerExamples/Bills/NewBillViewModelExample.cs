using ERPAPI.ViewModels.Bills;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.Bills
{
    public class NewBillViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new NewBillViewModel
            {
                Number = new Random().Next(1000, 999999),
                BillTypeId = new Random().Next(1, 9),
                BillTypeName = "نوع فاتورة 1",
                CanEditItemPrice = true,
                ShowNoteField = true,
                AccountCodeName = "حساب 1",
                AccountId = new Random().Next(1000, 999999),
                BranchCodeName = "فرع 1",
                BranchId = new Random().Next(1000, 999999),
                CostCenterId = new Random().Next(1000, 999999),
                CostCenterCodeName = "مركز كلفة 1",
                StoreId = new Random().Next(1000, 999999),
                StoreCodeName = "مستودع 1",
            };
        }
    }
}
