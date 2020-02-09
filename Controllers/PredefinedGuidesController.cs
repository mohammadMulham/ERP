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

namespace ERPAPI.Controllers
{
    [Route("api/[controller]")]
    public class PredefinedGuidesController : BaseController
    {
        #region variables
        private readonly IConfiguration _configuration;
        private readonly IFileManager _fileManager;
        private readonly IWritableOptions<DefaultKeysOptions> _defaultKeysOptions;
        private readonly IPredefinedGuideService _predefinedGuideService;
        private readonly IAccountRepository _accountRepository;
        private readonly ICurrencyRepository _currencyRepository;
        #endregion

        public PredefinedGuidesController(
            IConfiguration configuration,
            ILanguageManager languageManager,
            IWritableOptions<DefaultKeysOptions> defaultKeysOptions,
            IFileManager fileManager,
            IPredefinedGuideService predefinedGuideService,
            IAccountRepository accountRepository,
            ICurrencyRepository currencyRepository)
            : base(languageManager)
        {
            _configuration = configuration;
            _fileManager = fileManager;
            _defaultKeysOptions = defaultKeysOptions;
            _predefinedGuideService = predefinedGuideService;
            _accountRepository = accountRepository;
            _currencyRepository = currencyRepository;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return base.OnActionExecutionAsync(context, next);
        }

        [AllowAnonymous]
        [HttpGet("AccountGuides", Name = "GetAccountGuides")]
        public IActionResult AccountGuides()
        {
            var viewModels = _predefinedGuideService.GetAccountGuides();
            return Ok(viewModels);
        }

        [AllowAnonymous]
        [HttpPost("generatguide/{id}", Name = "GenerateAccountsGuide")]
        public async Task<IActionResult> GenerateAccountsGuide(int id)
        {
            if (await _currencyRepository.CountAsync() == 0)
            {
                return BadRequest("you should to set default currency before.");
            }

            if (await _accountRepository.CountAsync() > 0)
            {
                return BadRequest("you already created accounts guide");
            }

            var accounts = _predefinedGuideService.GetGuideAccounts(id);

            foreach (var accountModel in accounts)
            {
                var finalAccountId = accountModel.FinalAccountId == Guid.Empty ? null : accountModel.FinalAccountId;
                var parentAccountId = accountModel.ParentId == Guid.Empty ? null : accountModel.ParentId;
                var account =
                    new Account(accountModel.GetName(_language), accountModel.Code, parentAccountId, finalAccountId,
                    (AccountType)accountModel.Type, AccountDirectionType.Both, _defaultKeysOptions.Value.CurrencyId);
                account.Id = accountModel.Id;
                _accountRepository.Add(account, false);
            }

            var affectedRows = await _accountRepository.SaveAllAsync();
            if (affectedRows > 0)
            {
                return Ok();
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("generatcurrency/{id}", Name = "GenerateDefaultCurrency")]
        public async Task<IActionResult> GenerateDefaultCurrency(int id)
        {
            if (await _currencyRepository.CountAsync() > 0)
            {
                return BadRequest("you already created currency");
            }

            var currencyModel = _predefinedGuideService.GetCurrecny(id);
            if (currencyModel == null)
            {
                return NotFound();
            }

            var currency = new Currency(currencyModel.Code, currencyModel.GetName(_language), 1, currencyModel.GetPartName(_language), currencyModel.PartRate, currencyModel.ISOCode);

            var affectedRows = await _currencyRepository.AddAsync(currency);
            if (affectedRows > 0)
            {
                _defaultKeysOptions.Update(a => a.CurrencyId = currency.Id);
                return Ok();
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("Currencies", Name = "GetPredefinedCurrencies")]
        public IActionResult Currencies()
        {
            var viewModels = _predefinedGuideService.GetCurrencies();
            return Ok(viewModels);
        }
    }
}
