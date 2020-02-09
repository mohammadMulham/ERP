using ERPAPI.Models;
using ERPAPI.ViewModels;
using ERPAPI.ViewModels.Bills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Services
{
    public interface IBillService
    {
        Task<List<ErrorViewModel>> CheckAddBill(long typeId, AddBillViewModel model, BillType billType);
    }
}
