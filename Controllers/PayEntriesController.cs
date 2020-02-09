using ERPAPI.Extentions;
using ERPAPI.Managers;
using ERPAPI.Models;
using ERPAPI.Options;
using ERPAPI.Repositories;
using ERPAPI.SwaggerExamples.Entries;
using ERPAPI.SwaggerExamples.PayEntries;
using ERPAPI.ViewModels.Entries;
using ERPAPI.ViewModels.PayEntries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ERPAPI.Services;

namespace ERPAPI.Controllers
{
    [Route("api/[Controller]")]
    public class PayEntriesController : BaseController
    {
        #region variabls
        private IPeriodManager _periodManager;
        private IWritableOptions<DefaultKeysOptions> _defaultKeysOptions;
        private IEntryRepository _entryRepo;
        private IPayEntryRepository _payEntryRepo;
        private IPayTypeRepository _payTypeRep;
        private IPayEntryService _payTypeEntryService;
        private IAccountRepository _accountRepo;
        private IAccountBalanceRepository _accountBalanceRepo;
        private IAccountBalanceService _accountBalanceService;
        private ICostCenterRepository _costCenterRepo;
        private IBranchRepository _branchRepo;
        private ICurrencyRepository _currencyRepo;
        private IFinancialPeriodRepository _financialPeriodRepo;
        #endregion

        public PayEntriesController(
            ILanguageManager languageManager,
            IPeriodManager periodManager,
            IWritableOptions<DefaultKeysOptions> defaultKeysOptions,
            IEntryRepository entryRepo,
            IPayTypeRepository payTypeRepo,
            IPayEntryRepository payEntryRepo,
            IPayEntryService payTypeEntryService,
            IAccountRepository accountRepo,
            IAccountBalanceRepository accountBalanceRepo,
            IAccountBalanceService accountBalanceService,
            ICostCenterRepository costCenterRepo,
            IBranchRepository branchRepo,
            ICurrencyRepository currencyRepo,
            IFinancialPeriodRepository financialPeriodRepo) : base(languageManager)
        {
            _periodManager = periodManager;
            _defaultKeysOptions = defaultKeysOptions;
            _entryRepo = entryRepo;
            _payTypeRep = payTypeRepo;
            _payEntryRepo = payEntryRepo;
            _payTypeEntryService = payTypeEntryService;
            _accountRepo = accountRepo;
            _accountBalanceRepo = accountBalanceRepo;
            _accountBalanceService = accountBalanceService;
            _costCenterRepo = costCenterRepo;
            _branchRepo = branchRepo;
            _currencyRepo = currencyRepo;
            _financialPeriodRepo = financialPeriodRepo;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                _entryRepo.SetLoggedInUserId(GetUserId());
                _entryRepo.SetIsAdmin(GetIsUserAdmin());
                _entryRepo.SetPeriodId(_periodManager.GetPeriod());

                _accountBalanceRepo.SetLoggedInUserId(GetUserId());
                _accountBalanceRepo.SetIsAdmin(GetIsUserAdmin());
                _accountBalanceRepo.SetPeriodId(_periodManager.GetPeriod());
            }
            return base.OnActionExecutionAsync(context, next);
        }

        [HttpGet(Name = "GetPayEntries")]
        [ProducesResponseType(typeof(IEnumerable<EntryViewModel>), 200)]
        [SwaggerResponseExample(200, typeof(EntryViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> GetAll(long typeId)
        {
            var payType = await _payTypeRep.GetAsync(typeId);
            if (payType == null)
            {
                return NotFound("Pay Not Found !");
            }

            var payEntries = await _payEntryRepo.GetAllByTypeId(payType.Id).ToListAsync();
            var viewModels = AutoMapper.Mapper.Map<IEnumerable<PayEntryViewModel>>(payEntries);
            return Ok(viewModels);
        }

        [HttpGet("get/{id}", Name = "GetPayEntry")]
        [ProducesResponseType(typeof(PayEntryViewModel), 200)]
        [SwaggerResponseExample(200, typeof(PayEntryViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Get(long payTypeId, long id)
        {
            var payType = await _payTypeRep.GetAsync(payTypeId);
            if (payType == null)
            {
                return NotFound("Pay Not Found !");
            }
            var payEntry = await _payEntryRepo.GetAsync(payType.Id, id);
            if (payEntry == null)
            {
                return NotFound("Not Found !!");
            }
            var viewModel = AutoMapper.Mapper.Map<PayEntryViewModel>(payEntry);
            return Ok(viewModel);
        }

        [HttpGet("new", Name = "NewPayEntry")]
        [ProducesResponseType(typeof(NewPayEntryViewModel), 200)]
        [SwaggerResponseExample(200, typeof(NewPayEntryViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> NewPayEntry(long typeId)
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
            var payType = await _payTypeRep.GetAsync(typeId);
            if (payType == null)
            {
                return NotFound("Pay Type Not Found");
            }
            var newPayEntry = new NewPayEntryViewModel();
            Guid currencyId = _defaultKeysOptions.Value.CurrencyId;
            var curency = await _currencyRepo.FindAsync(currencyId);
            if (curency != null)
            {
                newPayEntry.CurrencyId = curency.Number;
                newPayEntry.CurrencyName = curency.Name;
                newPayEntry.CurrencyValue = curency.Value;
            }

            var payAccount = await _accountRepo.FindAsync(payType.DefaultAccountId);
            if (payAccount != null)
            {
                newPayEntry.PayAccountId = payAccount.Number;
            }

            if (payType.DefaultBranchId.HasValue)
            {
                var branch = await _branchRepo.FindAsync(payType.DefaultBranchId.Value);
                if (branch != null)
                {
                    newPayEntry.BranchId = branch.Number;
                    newPayEntry.BranchCodeName = branch.CodeName;
                }
            }
            newPayEntry.PayTypeId = payType.Number;
            newPayEntry.PayTypeName = payType.Name;
            return Ok(newPayEntry);
        }

        [HttpPost("{id}")]
        [ProducesResponseType(typeof(PayEntryViewModel), 200)]
        [SwaggerResponseExample(200, typeof(PayEntryViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerRequestExample(typeof(AddPayEntryViewModel), typeof(AddPayEntryViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Post(long id, [FromBody]AddPayEntryViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            if (id != model.PayTypeId)
            {
                return BadRequest();
            }

            var payType = await _payTypeRep.GetAsync(id);
            if (payType == null)
            {
                return NotFound("Not Found !");
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
            var debitSum = payType.ShowItemDebitField ? model.Items.Sum(p => p.Debit) : 0;
            var creditSum = payType.ShowItemDebitField ? model.Items.Sum(p => p.Credit) : 0;

            Guid? payAccountId = null;
            var payAccount = await _accountRepo.GetAsync(model.PayAccountId.Value);
            if (payAccount != null)
            {
                payAccountId = payAccount.Id;
            }

            #region checks

            var ShowItemDebitField = model.Items.ToList().All(e => e.Debit != 0);
            var ShowItemCreditField = model.Items.ToList().All(e => e.Credit != 0);

            if (payAccount != null && ((ShowItemDebitField && creditSum != 0) || (ShowItemCreditField && debitSum != 0)))
            {
                return NotFound("Error in writing entry Details ");
            }
            if (ShowItemDebitField && ShowItemCreditField && payAccount != null)
            {
                return NotFound("Error payAccount must be leaved empty in Daily or Beginning Entry ! ");
            }
            if ((payType.ShowItemDebitField && payType.ShowItemCreditField) && payAccount == null)
            {
                var isBalance = debitSum == creditSum ? true : false;
                return NotFound("Entry is not Balanced ! ");
            }
            if ((ShowItemDebitField || ShowItemCreditField) && payAccount == null)
            {
                return NotFound("Error payAccount must be Filled !! ");
            }

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

            long entryNumber = await _entryRepo.GetNextNumberAsync();
            var entry = new Entry(entryNumber, currencyId, model.CurrencyValue, branchId, model.Note);


            if ((payType.ShowItemDebitField && payType.ShowItemCreditField))
            {
                var itemsIndex = 0;
                // check form items if not found
                foreach (var item in model.Items)
                {
                    itemsIndex++;

                    Guid account1Id;
                    var account1 = await _accountRepo.GetAsync(item.AccountId);
                    if (account1 == null)
                    {
                        return NotFound($"account in entry item {itemsIndex} not found");
                    }
                    account1Id = account1.Id;



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

                    var entryItem = new EntryItem(account1Id, item.Debit, item.Credit, itemCurrencyId, item.CurrencyValue.Value, costCenterId, item.Date.Value.UtcDateTime, item.Note);
                    entry.Items.Add(entryItem);
                }

            }
            else if (payType.ShowItemDebitField || payType.ShowItemCreditField)
            {
                var itemsIndex = 0;
                // check form items if not found
                foreach (var item in model.Items)
                {
                    itemsIndex++;

                    Guid account1Id;
                    var account1 = await _accountRepo.GetAsync(item.AccountId);
                    if (account1 == null)
                    {
                        return NotFound($"account in entry item {itemsIndex} not found");
                    }
                    account1Id = account1.Id;



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


                    var entryItem = new EntryItem(account1Id, item.Debit, item.Credit, itemCurrencyId, item.CurrencyValue.Value, costCenterId, item.Date.Value.UtcDateTime, item.Note);

                    entry.Items.Add(entryItem);

                }

                var entryItem1 = new EntryItem(payAccountId.Value, creditSum, debitSum, currencyId, model.CurrencyValue, costCenterId, model.Date.UtcDateTime, model.Note);
                entry.Items.Add(entryItem1);
            }
            entry.PayEntry = new PayEntry
            {
                Number = await _payEntryRepo.GetNextNumberAsync(payType.Id),
                PayAccountId = payAccountId.Value,
                PayTypeId = payType.Id,
            };

            if (payType.AutoPostToAccounts)
            {
                await _accountBalanceService.PostEntryToAccounts(entry);
            }

            var affectedRows = await _entryRepo.AddAsync(entry);
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<EntryViewModel>(entry);
                return CreatedAtRoute("GetPayEntry", new { id = entry.Number }, viewModel);
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PayEntryViewModel), 200)]
        public async Task<IActionResult> Put(long id, [FromBody]EditPayEntryViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (model.Id != id)
            {
                return BadRequest();
            }

            // why ? you have to get PayEntry by number (id)
            var entry = await _entryRepo.GetAsync(id);
            if (entry == null)
            {
                return NotFound("Entry Not Found!");
            }

            var payType = entry.PayEntry.PayType;

            var debitSum = payType.ShowItemDebitField ? model.Items.Sum(p => p.Debit) : 0;
            var creditSum = payType.ShowItemDebitField ? model.Items.Sum(p => p.Credit) : 0;

            Guid? payAccountId = null;

            var payAccount = await _accountRepo.GetAsync(model.PayAccountId.Value);
            if (payAccount != null)
            {
                payAccountId = payAccount.Id;
            }

            #region checks

            var ShowItemDebitField = model.Items.ToList().All(e => e.Debit != 0);
            var ShowItemCreditField = model.Items.ToList().All(e => e.Credit != 0);

            if (payAccount != null && ((ShowItemDebitField && creditSum != 0) || (ShowItemCreditField && debitSum != 0)))
            {
                return NotFound("Error in writing entry Details ");
            }
            if (ShowItemDebitField && ShowItemCreditField && payAccount != null)
            {
                return NotFound("Error payAccount must be leaved empty in Daily or Beginning Entry ! ");
            }
            if ((payType.ShowItemDebitField && payType.ShowItemCreditField) && payAccount == null)
            {
                var isBalance = debitSum == creditSum ? true : false;
                return NotFound("Entry is not Balanced ! ");
            }
            if ((ShowItemDebitField || ShowItemCreditField) && payAccount == null)
            {
                return NotFound("Error payAccount must be Filled !! ");
            }

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

            if (payType.AutoPostToAccounts)
            {
                await _accountBalanceService.PostEntryToAccounts(entry, true);
            }

            entry.CurrencyId = currencyId;
            entry.CurrencyValue = model.CurrencyValue;
            entry.BranchId = branchId;
            entry.Note = model.Note;
            entry.Items = new HashSet<EntryItem>();

            if ((payType.ShowItemDebitField && payType.ShowItemCreditField))
            {
                var itemsIndex = 0;
                // check form items if not found
                foreach (var item in model.Items)
                {
                    itemsIndex++;

                    Guid account1Id;
                    var account1 = await _accountRepo.GetAsync(item.AccountId);
                    if (account1 == null)
                    {
                        return NotFound($"account in entry item {itemsIndex} not found");
                    }
                    account1Id = account1.Id;



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

                    var entryItem = new EntryItem(account1Id, item.Debit, item.Credit, itemCurrencyId, item.CurrencyValue.Value, costCenterId, item.Date.Value.UtcDateTime, item.Note);
                    entry.Items.Add(entryItem);
                }

            }
            else if (payType.ShowItemDebitField || payType.ShowItemCreditField)
            {
                var itemsIndex = 0;
                // check form items if not found
                foreach (var item in model.Items)
                {
                    itemsIndex++;

                    Guid account1Id;
                    var account1 = await _accountRepo.GetAsync(item.AccountId);
                    if (account1 == null)
                    {
                        return NotFound($"account in entry item {itemsIndex} not found");
                    }
                    account1Id = account1.Id;



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
                    var entryItem = new EntryItem(account1Id, item.Debit, item.Credit, itemCurrencyId, item.CurrencyValue.Value, costCenterId, item.Date.Value.UtcDateTime, item.Note);
                    entry.Items.Add(entryItem);
                }
                var entryItem1 = new EntryItem(payAccountId.Value, creditSum, debitSum, currencyId, model.CurrencyValue, costCenterId, model.Date.UtcDateTime, model.Note);
                entry.Items.Add(entryItem1);
            }



            if (payType.AutoPostToAccounts)
            {
                await _accountBalanceService.PostEntryToAccounts(entry);
            }

            var affectedRows = await _entryRepo.EditAsync(entry);
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<EntryViewModel>(entry);
                return CreatedAtRoute("GetPayEntry", new { id = entry.Number }, viewModel);
            }
            return BadRequest();
        }


        [HttpPut("postToAccounts/{id}")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> PostToAccounts(long typeId, long id)
        {
            var payType = await _payTypeRep.GetAsync(typeId);
            if (payType == null)
            {
                return NotFound("Pay Not Found !");
            }
            var payEntry = await _payEntryRepo.GetAsync(payType.Id, id);
            if (payEntry == null)
            {
                return NotFound("Not Found !!");
            }
            var entry = payEntry.Entry;
            if (entry.IsPosted)
            {
                ModelState.AddModelError("", "تم ترحيل القيد مسبقا, لا يمكن ترحيله مرة ثانية");
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            var result = await _accountBalanceService.PostEntryToAccounts(entry);
            var rowAffacted = await _entryRepo.SaveAllAsync();
            if (rowAffacted > 0)
            {
                return Ok("تم ترحيل السند بنجاح");
            }

            return BadRequest();
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(PayEntryViewModel), 200)]
        [SwaggerResponseExample(200, typeof(PayEntryViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Delete(long typeId, long id)
        {
            var payType = await _payTypeRep.GetAsync(typeId);
            if (payType == null)
            {
                return NotFound("Pay Not Found !");
            }
            var payEntry = await _payEntryRepo.GetAsync(payType.Id, id);
            if (payEntry == null)
            {
                return NotFound("Not Found !!");
            }
            var affectedRows = await _payEntryRepo.DeleteAsync(payEntry);
            if (affectedRows == -1)
            {
                return BadRequest("Can't delete this pay entry !");
            }
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<PayEntryViewModel>(payEntry);
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