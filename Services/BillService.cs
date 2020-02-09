using ERPAPI.Managers;
using ERPAPI.Models;
using ERPAPI.Repositories;
using ERPAPI.ViewModels;
using ERPAPI.ViewModels.Bills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Services
{
    public class BillService : IBillService
    {
        private IPeriodManager _periodManager;
        private IFinancialPeriodRepository _financialPeriodRepo;
        private IBillTypeRepository _billTypeRepo;

        public BillService(
            IPeriodManager periodManager,
            IFinancialPeriodRepository financialPeriodRepo,
            IBillTypeRepository billTypeRepo
            )
        {
            _periodManager = periodManager;
            _financialPeriodRepo = financialPeriodRepo;
            _billTypeRepo = billTypeRepo;
        }

        public void SetPreActionProps()
        {

        }

        public async Task<List<ErrorViewModel>> CheckAddBill(long typeId, AddBillViewModel model, BillType billType)
        {
            var errors = new List<ErrorViewModel>();

            if (_periodManager.GetPeriod() == Guid.Empty)
            {
                errors.Add(new ErrorViewModel(ErrorType.BadRequest, "", Resources.Global.Common.FinancialPeriodMustBeSpecified));
                return errors;
            }

            var financialPeriod = await _financialPeriodRepo.FindAsync(_periodManager.GetPeriod());
            if (financialPeriod == null)
            {
                errors.Add(new ErrorViewModel(ErrorType.NotFound, "FinancialPeriodNotFound"));
                return errors;
            }

            if (!financialPeriod.CheckIfDateInPeriod(model.Date))
            {
                errors.Add(new ErrorViewModel(ErrorType.BadRequest, "Date", Resources.Global.Common.DateOutCurrenctPeriod));
                return errors;
            }

            billType = await _billTypeRepo.GetAsync(typeId);
            if (billType == null)
            {
                errors.Add(new ErrorViewModel(ErrorType.NotFound, Resources.Bills.BillResource.BillTypeNotFound));
                return errors;
            }

            switch (billType.Type)
            {
                case BillsType.Transfer:
                    errors.Add(new ErrorViewModel(ErrorType.BadRequest, "", "can't not add new transfare from here!"));
                    return errors;

                case BillsType.EndPeriodInventory:
                    errors.Add(new ErrorViewModel(ErrorType.BadRequest, "", "can't add End Period Inventory bill"));
                    return errors;
            }



            return errors;
        }
    }
}
