using ERPAPI.Extentions;
using ERPAPI.Managers;
using ERPAPI.Models;
using ERPAPI.Options;
using ERPAPI.Repositories;
using ERPAPI.Services;
using ERPAPI.SwaggerExamples.Entries;
using ERPAPI.ViewModels.Entries;
using ERPAPI.ViewModels.Reports.Journal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPAPI.Controllers
{
    [Route("api/[Controller]")]
    public class EntriesController : BaseController
    {
        #region variabls
        private IPeriodManager _periodManager;
        private IWritableOptions<DefaultKeysOptions> _defaultKeysOptions;
        private IEntryRepository _entryRepo;
        private IEntryItemRepository _entryItemRepo;
        private IAccountRepository _accountRepo;
        private IAccountBalanceRepository _accountBalanceRep;
        private IAccountBalanceService _accountBalanceService;
        private ICostCenterRepository _costCenterRepo;
        private IBranchRepository _branchRepo;
        private ICurrencyRepository _currencyRepo;
        private IReportService _reportService;
        private IFinancialPeriodRepository _financialPeriodRepo;
        #endregion

        public EntriesController(
            ILanguageManager languageManager,
            IPeriodManager periodManager,
            IWritableOptions<DefaultKeysOptions> defaultKeysOptions,
            IEntryRepository entryRepo,
            IEntryItemRepository entryItemRepo,
            IAccountRepository accountRepo,
            IAccountBalanceRepository accountBalanceRep,
            IAccountBalanceService accountBalanceService,
            ICostCenterRepository costCenterRepo,
            IBranchRepository branchRepo,
            ICurrencyRepository currencyRepo,
            IReportService reportService,
            IFinancialPeriodRepository financialPeriodRepo) : base(languageManager)
        {
            _periodManager = periodManager;
            _defaultKeysOptions = defaultKeysOptions;
            _entryRepo = entryRepo;
            _entryItemRepo = entryItemRepo;
            _accountRepo = accountRepo;
            _accountBalanceRep = accountBalanceRep;
            _accountBalanceService = accountBalanceService;
            _costCenterRepo = costCenterRepo;
            _branchRepo = branchRepo;
            _currencyRepo = currencyRepo;
            _reportService = reportService;
            _financialPeriodRepo = financialPeriodRepo;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                _entryRepo.SetLoggedInUserId(GetUserId());
                _entryRepo.SetIsAdmin(GetIsUserAdmin());
                _entryRepo.SetPeriodId(_periodManager.GetPeriod());

                _accountBalanceRep.SetLoggedInUserId(GetUserId());
                _accountBalanceRep.SetIsAdmin(GetIsUserAdmin());
                _accountBalanceRep.SetPeriodId(_periodManager.GetPeriod());

                _entryItemRepo.SetLoggedInUserId(GetUserId());
                _entryItemRepo.SetIsAdmin(GetIsUserAdmin());
                _entryItemRepo.SetPeriodId(_periodManager.GetPeriod());
            }
            return base.OnActionExecutionAsync(context, next);
        }

        [HttpGet("journal", Name = "journalReport")]
        [ProducesResponseType(typeof(IEnumerable<JournalItemViewModel>), 200)]
        public async Task<IActionResult> Getjournal(DateTimeOffset? from, DateTimeOffset? to, DateTimeOffset? fromReleaseDate, DateTimeOffset? toReleaseDate, long? accountId, long? costCenterId, bool ShowNotPostedEntries = false, bool ShowTotalEntries = true)
        {
            Guid? accountGuid = null, costCenterGuid = null;

            if (accountId.HasValue)
            {
                var account = await _accountRepo.GetAsync(accountId.Value);
                if (account == null)
                {
                    return NotFound("AcountNotFound");
                }
                accountGuid = account.Id;
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
            JournalOptions options = new JournalOptions
            {
                ShowNotPostedEntries = ShowNotPostedEntries,
                ShowTotalEntries = ShowTotalEntries,
            };
            var journalViewModel = _reportService.GetjournalEntry(accountGuid, costCenterGuid, from, to, fromReleaseDate, toReleaseDate, options);

            return Ok(journalViewModel);
        }

        [HttpGet(Name = "GetEntries")]
        [ProducesResponseType(typeof(IEnumerable<EntryViewModel>), 200)]
        [SwaggerResponseExample(200, typeof(EntryViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> GetAll()
        {
            var entries = await _entryRepo.GetAllNoTracking().ToListAsync();
            var viewModels = AutoMapper.Mapper.Map<IEnumerable<EntryViewModel>>(entries);
            return Ok(viewModels);
        }

        [HttpGet("{id}", Name = "GetEntry")]
        [ProducesResponseType(typeof(EntryViewModel), 200)]
        [SwaggerResponseExample(200, typeof(EntryViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Get(long id)
        {
            var entry = await _entryRepo.GetAsync(id);
            if (entry == null)
            {
                return NotFound("Not Found !!");
            }
            var viewModel = AutoMapper.Mapper.Map<EntryViewModel>(entry);
            return Ok(viewModel);
        }

        [HttpGet("new", Name = "NewEntry")]
        [ProducesResponseType(typeof(NewEntryViewModel), 200)]
        [SwaggerResponseExample(200, typeof(NewEntryViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> NewEntry()
        {
            if (_periodManager.GetPeriod() == Guid.Empty)
            {
                ModelState.AddModelError("", Resources.Global.Common.FinancialPeriodMustBeSpecified);
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            var financialPeriod = await _financialPeriodRepo.FindAsync(_periodManager.GetPeriod());
            if (financialPeriod == null)
            {
                return NotFound("FinancialPeriodNotFound");
            }
            var newEntry = new NewEntryViewModel();
            newEntry.Number = await _entryRepo.GetNextNumberAsync();
            Guid currencyId = _defaultKeysOptions.Value.CurrencyId;
            var curency = await _currencyRepo.FindAsync(currencyId);
            if (curency != null)
            {
                newEntry.CurrencyId = curency.Number;
                newEntry.CurrencyName = curency.Name;
                newEntry.CurrencyValue = curency.Value;
            }
            return Ok(newEntry);
        }

        [HttpPost]
        [ProducesResponseType(typeof(EntryViewModel), 200)]
        [SwaggerResponseExample(200, typeof(EntryViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerRequestExample(typeof(AddEntryViewModel), typeof(AddEntryViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Post(long Id, [FromBody]AddEntryViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            if (model == null)
            {
                return BadRequest();
            }

            if (_periodManager.GetPeriod() == Guid.Empty)
            {
                ModelState.AddModelError("", Resources.Global.Common.FinancialPeriodMustBeSpecified);
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            var financialPeriod = await _financialPeriodRepo.FindAsync(_periodManager.GetPeriod());
            if (financialPeriod == null)
            {
                return NotFound("FinancialPeriodNotFound");
            }
            if (!financialPeriod.CheckIfDateInPeriod(model.Date))
            {
                ModelState.AddModelError("Date", Resources.Global.Common.DateOutCurrenctPeriod);
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            #region checks

            Guid currencyId;
            var currency = await _currencyRepo.GetAsync(model.CurrencyId);
            if (currency != null)
            {
                currencyId = currency.Id;
                model.CurrencyValue = currency.Value;
            }
            else
            {
                currencyId = _defaultKeysOptions.Value.CurrencyId;
                model.CurrencyValue = 1;
            }

            Guid? costCenterId = null;
            if (model.CostCenterId.HasValue)
            {
                var costCenter = await _costCenterRepo.GetAsync(model.CostCenterId.Value);
                if (costCenter == null)
                {
                    return NotFound(Resources.CostCenters.CostCenterResource.CostCenterNotFound);
                }
                costCenterId = costCenter.Id;
            }

            Guid? branchId = null;
            if (model.BranchId.HasValue)
            {
                var branch = await _branchRepo.GetAsync(model.BranchId.Value);
                if (branch == null)
                {
                    return NotFound(Resources.Branchs.BranchResource.BranchNotFound);
                }
                branchId = branch.Id;
            }

            // get new number value if the value from model is exist
            if (!await _entryRepo.CheckIfNotExistAsync(model.Number))
            {
                model.Number = await _entryRepo.GetNextNumberAsync();
            }

            #endregion

            var entry = new Entry(model.Number, currencyId, model.CurrencyValue, branchId, model.Note);

            var itemsIndex = 0;
            // check form items if not found
            foreach (var item in model.Items)
            {
                itemsIndex++;

                Guid accountId;
                var account = await _accountRepo.GetAsync(item.AccountId);
                if (account == null)
                {
                    return NotFound($"account in entry item {itemsIndex} not found");
                }
                accountId = account.Id;

                Guid itemCurrencyId;
                if (model.CurrencyId == item.CurrencyId)
                {
                    itemCurrencyId = currencyId;
                    item.CurrencyValue = model.CurrencyValue;
                }
                else
                {
                    var itemCurrency = await _currencyRepo.GetAsync(item.CurrencyId.Value);
                    if (itemCurrency != null)
                    {
                        itemCurrencyId = itemCurrency.Id;
                        item.CurrencyValue = itemCurrency.Value;
                    }
                }

                Guid? itemCostCenterId = null;
                if (model.CostCenterId.HasValue && item.CostCenterId.HasValue && model.CostCenterId == item.CostCenterId)
                {
                    itemCostCenterId = costCenterId;
                }
                else
                {
                    if (item.CostCenterId.HasValue)
                    {
                        var costCenter = await _costCenterRepo.GetAsync(item.CostCenterId.Value);
                        if (costCenter == null)
                        {
                            return NotFound(Resources.CostCenters.CostCenterResource.CostCenterNotFound);
                        }
                        itemCostCenterId = costCenter.Id;
                    }
                }

                var entryItem = new EntryItem(accountId, item.Debit, item.Credit, itemCurrencyId, item.CurrencyValue.Value, costCenterId, item.Date.Value.UtcDateTime, item.Note);
                entry.Items.Add(entryItem);
            }
            await _accountBalanceService.PostEntryToAccounts(entry);
            var affectedRows = await _entryRepo.AddAsync(entry);
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<EntryViewModel>(entry);
                return CreatedAtRoute("GetEntry", new { id = entry.Number }, viewModel);
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EntryViewModel), 200)]
        public async Task<IActionResult> Put(long id, [FromBody]EditEntryViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (model.Id != id)
            {
                return BadRequest();
            }

            var entry = await _entryRepo.GetAsync(id);
            if (entry == null)
            {
                return NotFound("Entry Not Found!");
            }

            #region checks

            Guid currencyId;
            var currency = await _currencyRepo.GetAsync(model.CurrencyId);
            if (currency != null)
            {
                currencyId = currency.Id;
                model.CurrencyValue = currency.Value;
            }
            else
            {
                currencyId = _defaultKeysOptions.Value.CurrencyId;
                model.CurrencyValue = 1;
            }


            Guid? costCenterId = null;
            if (model.CostCenterId.HasValue)
            {
                var costCenter = await _costCenterRepo.GetAsync(model.CostCenterId.Value);
                if (costCenter == null)
                {
                    return NotFound(Resources.CostCenters.CostCenterResource.CostCenterNotFound);
                }
                costCenterId = costCenter.Id;
            }

            Guid? branchId = null;
            if (model.BranchId.HasValue)
            {
                var branch = await _branchRepo.GetAsync(model.BranchId.Value);
                if (branch == null)
                {
                    return NotFound(Resources.Branchs.BranchResource.BranchNotFound);
                }
                branchId = branch.Id;
            }

            #endregion

            await _accountBalanceService.PostEntryToAccounts(entry, true);

            entry.CurrencyId = currencyId;
            entry.CurrencyValue = model.CurrencyValue;
            entry.BranchId = branchId;
            entry.Note = model.Note;
            entry.Items = new HashSet<EntryItem>();

            var itemsIndex = 0;
            // check form items if not found
            foreach (var item in model.EntryItems)
            {
                itemsIndex++;

                Guid accountId;
                var account = await _accountRepo.GetAsync(item.AccountId);
                if (account == null)
                {
                    return NotFound("account not found");
                }
                accountId = account.Id;

                Guid itemCurrencyId;
                if (model.CurrencyId == item.CurrencyId)
                {
                    itemCurrencyId = currencyId;
                    item.CurrencyValue = model.CurrencyValue;
                }
                else
                {
                    var itemCurrency = await _currencyRepo.GetAsync(item.CurrencyId.Value);
                    if (itemCurrency != null)
                    {
                        itemCurrencyId = itemCurrency.Id;
                        item.CurrencyValue = itemCurrency.Value;
                    }
                }

                Guid? itemCostCenterId = null;
                if (model.CostCenterId.HasValue && item.CostCenterId.HasValue && model.CostCenterId == item.CostCenterId)
                {
                    itemCostCenterId = costCenterId;
                }
                else
                {
                    if (item.CostCenterId.HasValue)
                    {
                        var costCenter = await _costCenterRepo.GetAsync(item.CostCenterId.Value);
                        if (costCenter == null)
                        {
                            return NotFound(Resources.CostCenters.CostCenterResource.CostCenterNotFound);
                        }
                        itemCostCenterId = costCenter.Id;
                    }
                }

                var entryItem = new EntryItem(accountId, item.Debit, item.Credit, itemCurrencyId, item.CurrencyValue.Value, itemCostCenterId, item.Date.Value.UtcDateTime, item.Note);
                entry.Items.Add(entryItem);
            }

            await _accountBalanceService.PostEntryToAccounts(entry);
            var affectedRows = await _entryRepo.EditAsync(entry);
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<EntryViewModel>(entry);
                return CreatedAtRoute("GetEntry", new { id = entry.Number }, viewModel);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(EntryViewModel), 200)]
        [SwaggerResponseExample(200, typeof(EntryViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Delete(long id)
        {
            var entry = await _entryRepo.GetAsync(id);
            if (entry == null)
            {
                return NotFound("Not Found");
            }

            var affectedRows = await _entryRepo.DeleteAsync(entry);
            if (affectedRows == -1)
            {
                return BadRequest("Can't delete this entry !");
            }
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<EntryViewModel>(entry);
                return Ok(viewModel);
            }
            return BadRequest();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _entryRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}