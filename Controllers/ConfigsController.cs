using System;
using System.Threading.Tasks;
using ERPAPI.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using ERPAPI.Services;
using ERPAPI.Repositories;
using ERPAPI.Models;
using ERPAPI.Options;
using Microsoft.Extensions.Configuration;
using ERPAPI.ViewModels.Configs;
using System.Collections.Generic;

namespace ERPAPI.Controllers
{
    [Route("api/[controller]")]
    public class ConfigsController : BaseController
    {
        #region variables
        private readonly IConfiguration _configuration;
        private readonly IFileManager _fileManager;
        private readonly IWritableOptions<DefaultKeysOptions> _defaultKeysOptions;
        private readonly IPredefinedGuideService _predefinedGuideService;
        private readonly ICompanyRepository _companyRepo;
        private readonly IFinancialPeriodRepository _financialPeriodRepo;
        private readonly IAccountRepository _accountRepo;
        private readonly ICurrencyRepository _currencyRepo;
        #endregion

        public ConfigsController(
            IConfiguration configuration,
            ILanguageManager languageManager,
            IWritableOptions<DefaultKeysOptions> defaultKeysOptions,
            IFileManager fileManager,
            IPredefinedGuideService predefinedGuideService,
            ICompanyRepository companyRepo,
            IFinancialPeriodRepository financialPeriodRepo,
            IAccountRepository accountRepo,
            ICurrencyRepository currencyRepo
            )
            : base(languageManager)
        {
            _configuration = configuration;
            _fileManager = fileManager;
            _defaultKeysOptions = defaultKeysOptions;
            _predefinedGuideService = predefinedGuideService;
            _companyRepo = companyRepo;
            _accountRepo = accountRepo;
            _currencyRepo = currencyRepo;
            _financialPeriodRepo = financialPeriodRepo;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return base.OnActionExecutionAsync(context, next);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Check()
        {
            return Ok(_defaultKeysOptions.Value.Initialized);
        }

        [AllowAnonymous]
        [HttpPost("init", Name = "InitConfig")]
        public async Task<IActionResult> Init(InitConfigViewModel model)
        {
            // check if it's initialized
            if (_defaultKeysOptions.Value.Initialized)
            {
                return BadRequest("Already Initialized!");
            }
            // 1. create company 2. finanital period
            string financialPeriodName = string.Format(Resources.Global.Common.FinancialPeriodName, DateTime.Now.Year, model.CompanyName);
            var financialPeriod = new FinancialPeriod(financialPeriodName, model.StartDate, model.EndtDate);
            financialPeriod.Company = new Company(model.CompanyName);
            _financialPeriodRepo.Add(financialPeriod, false);
            // 3. currency
            Currency currency = new Currency();

            if (model.CurrencyId.HasValue)
            {
                var preDefinedCurrency = _predefinedGuideService.GetCurrecny(model.CurrencyId.Value);
                currency = new Currency(preDefinedCurrency.Code, preDefinedCurrency.GetName(_language), 1, preDefinedCurrency.GetPartName(_language), preDefinedCurrency.PartRate, preDefinedCurrency.ISOCode);
            }
            else
            {
                var preDefinedCurrency = _predefinedGuideService.GetCurrecny(1); // USD
                currency = new Currency(preDefinedCurrency.Code, preDefinedCurrency.GetName(_language), 1, preDefinedCurrency.GetPartName(_language), preDefinedCurrency.PartRate, preDefinedCurrency.ISOCode);
            }

            _currencyRepo.Add(currency, false);

            // 4. accounts
            var accounts = _predefinedGuideService.GetGuideAccounts(model.AccoutGuideId);

            foreach (var accountModel in accounts)
            {
                var finalAccountId = accountModel.FinalAccountId == Guid.Empty ? null : accountModel.FinalAccountId;
                var parentAccountId = accountModel.ParentId == Guid.Empty ? null : accountModel.ParentId;
                var account =
                    new Account(accountModel.GetName(_language), accountModel.Code, parentAccountId, finalAccountId,
                    (AccountType)accountModel.Type, AccountDirectionType.Both, currency.Id);
                account.Id = accountModel.Id;
                _accountRepo.Add(account, false);
            }

            // 5. billTypes

            // 6. payTypes

            // 7. check and complete defulat items accountId and default cash accountId in predefienedGuides file

            var affectedRows = await _accountRepo.SaveAllAsync();
            if (affectedRows > 0)
            {
                // update appSettings
                //      1. update initialized prop
                _defaultKeysOptions.Update(a => a.Initialized = true);
                //      2. update default currency
                _defaultKeysOptions.Update(a => a.CurrencyId = currency.Id);

                return Ok();
            }
            return BadRequest();
        }
    }
}
