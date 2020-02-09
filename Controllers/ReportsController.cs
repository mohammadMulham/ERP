using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Managers;
using Microsoft.AspNetCore.Mvc.Filters;
using ERPAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using ERPAPI.ViewModels.Card;
using ERPAPI.Models;
using Microsoft.EntityFrameworkCore;
using ERPAPI.Extentions;
using Swashbuckle.AspNetCore.Examples;
using Newtonsoft.Json.Converters;
using ERPAPI.ViewModels.Customers;
using ERPAPI.SwaggerExamples.Customers;
using ERPAPI.SwaggerExamples.Items;
using ERPAPI.ViewModels.Reports.CustomerAccountStatement;
using ERPAPI.Services;

namespace ERPAPI.Controllers
{
    [Route("api/[Controller]")]
    public class ReportsController : BaseController
    {
        #region variables
        private IPeriodManager _periodManager;
        private ICustomerRepository _customerRepo;
        private IAccountRepository _accountRepo;
        private ICostCenterRepository _costCenterRepo;
        private ICustomerTypeRepository _customerTypeRepo;
        private IEntryItemRepository _entryItemRepo;
        private IBillRepository _billRepo;
        private IReportService _reportService;
        #endregion

        public ReportsController(
            ILanguageManager languageManager,
            ICustomerRepository customerRepo,
            ICustomerTypeRepository customerTypeRepo,
            IAccountRepository accountRepo,
            ICostCenterRepository costCenterRepo,
            IEntryItemRepository entryItemRepo,
            IBillRepository billRepo,
            IPeriodManager periodManager,
            IReportService reportService
            ) : base(languageManager)
        {
            _periodManager = periodManager;
            _customerRepo = customerRepo;
            _billRepo = billRepo;
            _customerTypeRepo = customerTypeRepo;
            _accountRepo = accountRepo;
            _costCenterRepo = costCenterRepo;
            _entryItemRepo = entryItemRepo;
            _reportService = reportService;

        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                _customerRepo.SetLoggedInUserId(GetUserId());
                _customerTypeRepo.SetLoggedInUserId(GetUserId());

                _customerRepo.SetIsAdmin(GetIsUserAdmin());
                _customerTypeRepo.SetIsAdmin(GetIsUserAdmin());

                _entryItemRepo.SetLoggedInUserId(GetUserId());
                _entryItemRepo.SetIsAdmin(GetIsUserAdmin());
                _entryItemRepo.SetPeriodId(_periodManager.GetPeriod());

                _billRepo.SetLoggedInUserId(GetUserId());
                _billRepo.SetIsAdmin(GetIsUserAdmin());
                _billRepo.SetPeriodId(_periodManager.GetPeriod());
            }
            return base.OnActionExecutionAsync(context, next);
        }
        [HttpGet("AccountStatment", Name = "AccountStatmentReport")]
        [ProducesResponseType(typeof(IEnumerable<CustomerAccountStatementViewModel>), 200)]
        public async Task<IActionResult> AccountStatment(DateTimeOffset? from, DateTimeOffset? to, DateTimeOffset? fromReleaseDate, DateTimeOffset? toReleaseDate, long? customerId, long? accountId, long? costCenterId, bool ShowNotPostedEntries = false)
        {
            Guid? accountIdGuid = null, customerIdGuid = null, costCenterGuid = null;
            if (customerId.HasValue)
            {
                var customer = await _customerRepo.GetAsync(customerId.Value);
                if (customer == null)
                {
                    return NotFound("AcountNotFound");
                }
                customerIdGuid = customer.Id;
            }
            if (accountId.HasValue)
            {
                var account = await _accountRepo.GetAsync(accountId.Value);
                if (account == null)
                {
                    return NotFound("AcountNotFound");
                }
                accountIdGuid = account.Id;
            }


            if (costCenterId.HasValue)
            {
                var costCenter = await _costCenterRepo.GetAsync(costCenterId.Value);
                if (costCenter == null)
                {
                    return NotFound("CostCenterNotFound");
                }
                costCenterGuid = costCenter.Id;
            }

            var CusAccStatmentsViewModel = _reportService.GetCustomerAccountStatment(customerIdGuid, accountIdGuid, costCenterGuid, from, to, fromReleaseDate, toReleaseDate);

            return Ok(CusAccStatmentsViewModel);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _customerRepo.Dispose();
                _customerTypeRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
