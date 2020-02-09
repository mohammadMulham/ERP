using ERPAPI.Extentions;
using ERPAPI.Helpers;
using ERPAPI.Managers;
using ERPAPI.Models;
using ERPAPI.Options;
using ERPAPI.Repositories;
using ERPAPI.Services;
using ERPAPI.SwaggerExamples.Bills;
using ERPAPI.ViewModels.Bills;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Controllers
{
    [Route("api/[Controller]/{typeId}/")]
    public class BillsController : BaseController
    {
        #region variabls
        private IPeriodManager _periodManager;
        private IWritableOptions<DefaultKeysOptions> _defaultKeysOptions;
        private IBillRepository _billRepo;
        private IEntryRepository _entryRepo;
        private IBillTypeRepository _billTypeRepo;
        private ICustomerRepository _customerRepo;
        private IAccountRepository _accountRepo;
        private IAccountBalanceRepository _accountBalanceRepo;
        private IAccountBalanceService _accountBalanceService;
        private IStoreRepository _storeRepo;
        private ICostCenterRepository _costCenterRepo;
        private IBranchRepository _branchRepo;
        private IItemRepository _itemRepo;
        private IItemUnitRepository _itemUnitRepo;
        private IStoreItemRepository _storeItemRepo;
        private IStoreItemUnitRepository _storeItemUnitRepo;
        private IPriceRepository _priceRepo;
        private ICurrencyRepository _currencyRepo;
        private IFinancialPeriodRepository _financialPeriodRepo;
        #endregion

        public BillsController(
            ILanguageManager languageManager,
            IPeriodManager periodManager,
            IWritableOptions<DefaultKeysOptions> defaultKeysOptions,
            IBillRepository billRepo,
            IEntryRepository entryRepo,
            IBillTypeRepository billTypeRepo,
            ICustomerRepository customerRepo,
            IAccountRepository accountRepo,
            IAccountBalanceRepository accountBalanceRepo,
            IAccountBalanceService accountBalanceService,
            IStoreRepository storeRepo,
            ICostCenterRepository costCenterRepo,
            IBranchRepository branchRepo,
            IItemRepository itemRepo,
            IItemUnitRepository itemUnitRepo,
            IStoreItemRepository storeItemRepo,
            IStoreItemUnitRepository storeItemUnitRepo,
            IPriceRepository priceRepo,
            ICurrencyRepository currencyRepo,
            IFinancialPeriodRepository financialPeriodRepo) : base(languageManager)
        {
            _periodManager = periodManager;
            _defaultKeysOptions = defaultKeysOptions;
            _billRepo = billRepo;
            _entryRepo = entryRepo;
            _billTypeRepo = billTypeRepo;
            _customerRepo = customerRepo;
            _accountRepo = accountRepo;
            _accountBalanceRepo = accountBalanceRepo;
            _accountBalanceService = accountBalanceService;
            _storeRepo = storeRepo;
            _costCenterRepo = costCenterRepo;
            _branchRepo = branchRepo;
            _itemRepo = itemRepo;
            _itemUnitRepo = itemUnitRepo;
            _storeItemRepo = storeItemRepo;
            _storeItemUnitRepo = storeItemUnitRepo;
            _priceRepo = priceRepo;
            _currencyRepo = currencyRepo;
            _financialPeriodRepo = financialPeriodRepo;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                _billRepo.SetLoggedInUserId(GetUserId());
                _billRepo.SetIsAdmin(GetIsUserAdmin());
                _billRepo.SetPeriodId(_periodManager.GetPeriod());

                _billTypeRepo.SetLoggedInUserId(GetUserId());
                _billTypeRepo.SetIsAdmin(GetIsUserAdmin());
                _billTypeRepo.SetPeriodId(_periodManager.GetPeriod());

                _entryRepo.SetLoggedInUserId(GetUserId());
                _entryRepo.SetIsAdmin(GetIsUserAdmin());
                _entryRepo.SetPeriodId(_periodManager.GetPeriod());

                _accountBalanceRepo.SetLoggedInUserId(GetUserId());
                _accountBalanceRepo.SetIsAdmin(GetIsUserAdmin());
                _accountBalanceRepo.SetPeriodId(_periodManager.GetPeriod());

                _customerRepo.SetLoggedInUserId(GetUserId());
                _customerRepo.SetIsAdmin(GetIsUserAdmin());
                _customerRepo.SetPeriodId(_periodManager.GetPeriod());

                _accountRepo.SetLoggedInUserId(GetUserId());
                _accountRepo.SetIsAdmin(GetIsUserAdmin());
                _accountRepo.SetPeriodId(_periodManager.GetPeriod());

                _storeRepo.SetLoggedInUserId(GetUserId());
                _storeRepo.SetIsAdmin(GetIsUserAdmin());
                _storeRepo.SetPeriodId(_periodManager.GetPeriod());

                _costCenterRepo.SetLoggedInUserId(GetUserId());
                _costCenterRepo.SetIsAdmin(GetIsUserAdmin());
                _costCenterRepo.SetPeriodId(_periodManager.GetPeriod());

                _branchRepo.SetLoggedInUserId(GetUserId());
                _branchRepo.SetIsAdmin(GetIsUserAdmin());
                _branchRepo.SetPeriodId(_periodManager.GetPeriod());

                _itemRepo.SetLoggedInUserId(GetUserId());
                _itemRepo.SetIsAdmin(GetIsUserAdmin());
                _itemRepo.SetPeriodId(_periodManager.GetPeriod());

                _itemUnitRepo.SetLoggedInUserId(GetUserId());
                _itemUnitRepo.SetIsAdmin(GetIsUserAdmin());
                _itemUnitRepo.SetPeriodId(_periodManager.GetPeriod());

                _storeItemRepo.SetLoggedInUserId(GetUserId());
                _storeItemRepo.SetIsAdmin(GetIsUserAdmin());
                _storeItemRepo.SetPeriodId(_periodManager.GetPeriod());

                _storeItemUnitRepo.SetLoggedInUserId(GetUserId());
                _storeItemUnitRepo.SetIsAdmin(GetIsUserAdmin());
                _storeItemUnitRepo.SetPeriodId(_periodManager.GetPeriod());

                _priceRepo.SetLoggedInUserId(GetUserId());
                _priceRepo.SetIsAdmin(GetIsUserAdmin());
                _priceRepo.SetPeriodId(_periodManager.GetPeriod());

                _currencyRepo.SetLoggedInUserId(GetUserId());
                _currencyRepo.SetIsAdmin(GetIsUserAdmin());
                _currencyRepo.SetPeriodId(_periodManager.GetPeriod());

                _financialPeriodRepo.SetLoggedInUserId(GetUserId());
                _financialPeriodRepo.SetIsAdmin(GetIsUserAdmin());
                _financialPeriodRepo.SetPeriodId(_periodManager.GetPeriod());
            }
            return base.OnActionExecutionAsync(context, next);
        }

        [HttpGet(Name = "GetBills")]
        [ProducesResponseType(typeof(IEnumerable<BillViewModel>), 200)]
        [SwaggerResponseExample(200, typeof(BillViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> GetAll(long typeId)
        {
            var billType = await _billTypeRepo.GetAsync(typeId);
            if (billType == null)
            {
                return NotFound(Resources.Bills.BillResource.BillTypeNotFound);
            }
            var bills = await _billRepo.GetAllByTypeId(billType.Id).ToListAsync();
            var viewModels = AutoMapper.Mapper.Map<IEnumerable<BillViewModel>>(bills);
            return Ok(viewModels);
        }

        [HttpGet("get/{id}", Name = "GetBill")]
        [ProducesResponseType(typeof(BillViewModel), 200)]
        [SwaggerResponseExample(200, typeof(BillViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Get(long typeId, long id)
        {
            var billType = await _billTypeRepo.GetAsync(typeId);
            if (billType == null)
            {
                return NotFound(Resources.Bills.BillResource.BillTypeNotFound);
            }
            var bill = await _billRepo.GetAsync(billType.Id, id);
            if (bill == null)
            {
                return NotFound(Resources.Bills.BillResource.BillNotFound);
            }
            var viewModel = AutoMapper.Mapper.Map<BillViewModel>(bill);
            return Ok(viewModel);
        }

        [HttpGet("new", Name = "NewBill")]
        [ProducesResponseType(typeof(NewBillViewModel), 200)]
        [SwaggerResponseExample(200, typeof(NewBillViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> NewBill(long typeId)
        {
            var billType = await _billTypeRepo.GetAsync(typeId);
            if (billType == null)
            {
                return NotFound(Resources.Bills.BillResource.BillTypeNotFound);
            }

            var newBill = new NewBillViewModel();

            switch (billType.Type)
            {
                case BillsType.Transfer:
                    return BadRequest("can't not add new transfare from here!");
                case BillsType.EndPeriodInventory:
                    return BadRequest("can't add End Period Inventory bill");
            }

            #region billType properties

            newBill.BillTypeId = billType.Number;
            newBill.BillTypeName = billType.Name;
            newBill.ShowBranchField = billType.ShowBranchField;
            newBill.ShowCostCenterldField = billType.ShowCostCenterldField;
            newBill.ShowCustomerAccountldField = billType.ShowCustomerAccountldField;
            newBill.ShowDiscField = billType.ShowDiscField;
            newBill.ShowItemExpireDateField = billType.ShowItemExpireDateField;
            newBill.ShowExtraField = billType.ShowExtraField;
            newBill.ShowItemDiscField = billType.ShowItemDiscField;
            newBill.ShowItemExtraField = billType.ShowItemExtraField;
            newBill.ShowItemUnitField = billType.ShowItemUnitField;
            newBill.ShowItemCostCenterldField = billType.ShowItemCostCenterldField;
            newBill.ShowItemPriceFields = billType.ShowItemPriceFields;
            newBill.ShowItemStoreField = billType.ShowItemStoreField;
            newBill.ShowNoteField = billType.ShowNoteField;
            newBill.ShowItemNoteField = billType.ShowItemNoteField;
            newBill.ShowPayTypeField = billType.ShowPayTypeField;
            newBill.ShowItemProductionDateField = billType.ShowItemProductionDateField;
            newBill.ShowSellersFields = billType.ShowSellersFields;
            newBill.ShowStoreField = billType.ShowStoreField;
            newBill.ShowTotalPriceItemField = billType.ShowTotalPriceItemField;
            newBill.CanEditItemPrice = billType.CanEditItemPrice;
            newBill.CanEditItemTotalPrice = billType.CanEditItemTotalPrice;
            newBill.Color1 = billType.Color1;
            newBill.Color2 = billType.Color2;

            #endregion

            newBill.Number = await _billRepo.GetNextNumberAsync(billType.Id);

            #region default values

            newBill.PayType = billType.DefaultPayType;

            if (billType.DefaultPriceId.HasValue)
            {
                var price = await _priceRepo.FindAsync(billType.DefaultPriceId.Value);
                if (price != null)
                {
                    newBill.PriceId = price.Number;
                    newBill.PriceName = price.Name;
                }
            }

            if (billType.DefaultCurrencyId.HasValue)
            {
                var curency = await _currencyRepo.FindAsync(billType.DefaultCurrencyId.Value);
                if (curency != null)
                {
                    newBill.CurrencyId = curency.Number;
                }
            }
            if (!newBill.CurrencyId.HasValue)
            {
                var curency = await _currencyRepo.FindAsync(_defaultKeysOptions.Value.CurrencyId);
                if (curency != null)
                {
                    newBill.CurrencyId = curency.Number;
                }
            }

            if (billType.DefaultStoreId.HasValue)
            {
                var store = await _storeRepo.FindAsync(billType.DefaultStoreId.Value);
                if (store != null)
                {
                    newBill.StoreId = store.Number;
                    newBill.StoreCodeName = store.CodeName;
                }
            }

            if (billType.DefaultCashAccountId.HasValue)
            {
                var account = await _accountRepo.FindAsync(billType.DefaultCashAccountId.Value);
                if (account != null)
                {
                    newBill.AccountId = account.Number;
                    newBill.AccountCodeName = account.CodeName;
                }
            }

            if (billType.DefaultCostCenterId.HasValue)
            {
                var costCenter = await _costCenterRepo.FindAsync(billType.DefaultCostCenterId.Value);
                if (costCenter != null)
                {
                    newBill.CostCenterId = costCenter.Number;
                    newBill.CostCenterCodeName = costCenter.CodeName;
                }
            }

            if (billType.DefaultBranchId.HasValue)
            {
                var branch = await _branchRepo.FindAsync(billType.DefaultBranchId.Value);
                if (branch != null)
                {
                    newBill.BranchId = branch.Number;
                    newBill.BranchCodeName = branch.CodeName;
                }
            }

            #endregion

            return Ok(newBill);
        }

        [HttpPost()]
        [ProducesResponseType(typeof(BillViewModel), 200)]
        [SwaggerResponseExample(200, typeof(BillViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerRequestExample(typeof(AddBillViewModel), typeof(AddBillViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Post(long typeId, [FromBody]AddBillViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (typeId != model.BillTypeId)
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

            var billType = await _billTypeRepo.GetAsync(typeId);
            if (billType == null)
            {
                return NotFound(Resources.Bills.BillResource.BillTypeNotFound);
            }

            switch (billType.Type)
            {
                case BillsType.Transfer:
                    ModelState.AddModelError("", "can't not add new transfare from here!");
                    return BadRequest(ModelState.GetWithErrorsKey());
                case BillsType.EndPeriodInventory:
                    ModelState.AddModelError("", "can't add End Period Inventory bill");
                    return BadRequest(ModelState.GetWithErrorsKey());
            }

            #region checks

            Guid currencyId;
            var currency = await _currencyRepo.GetAsync(model.CurrencyId);
            if (currency != null)
            {
                currencyId = currency.Id;
                if (!model.CurrencyValue.HasValue)
                {
                    model.CurrencyValue = currency.Value;
                }
            }
            else
            {
                currencyId = _defaultKeysOptions.Value.CurrencyId;
                model.CurrencyValue = 1;
            }


            Guid cashAccountId;
            var cashAccount = await _accountRepo.GetAsync(model.AccountId);
            if (cashAccount == null)
            {
                return NotFound("Cash account not found");
            }
            cashAccountId = cashAccount.Id;


            Guid? customerAccountId = null;
            if (model.CustomerAccountId.HasValue)
            {
                var customerAccount = await _accountRepo.GetAsync(model.CustomerAccountId.Value);
                if (customerAccount == null)
                {
                    return NotFound("Customer account not found");
                }
                if (customerAccount.CustomerId == null)
                {
                    ModelState.AddModelError("CustomerAccountId", "account not related to customer or supplier");
                    return BadRequest(ModelState.GetWithErrorsKey());
                }
                customerAccountId = customerAccount.Id;
            }

            Guid? storeId = null;
            if (model.StoreId.HasValue)
            {
                var store = await _storeRepo.GetAsync(model.StoreId.Value);
                if (store == null)
                {
                    return NotFound(Resources.Stores.StoreResource.StoreNotFound);
                }
                storeId = store.Id;
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

            var bill = new Bill(billType.Id, currencyId, model.CurrencyValue.Value, cashAccountId, customerAccountId, model.CustomerName, model.Date, model.PayType, storeId, costCenterId, branchId, model.Extra, model.Disc, model.TotalPaid, model.Note);

            // add the payemnt
            if (bill.PayType == PaysType.Credit && billType.DefaultCashAccountId.HasValue)
            {
                double credit = 0;
                double debit = 0;

                var inOrOut = int.Parse(EnumHelper.GetDescription(billType.Type));
                if (inOrOut == 1)
                {
                    // مشتريات
                    credit = bill.TotalPaid; // صندوق
                    debit = 0; // مورد
                }
                else
                {
                    // مبيعات
                    credit = 0; // زبون 
                    debit = bill.TotalPaid; // صندوق
                }

                var payEntryItem = new BillEntryItem(bill.Date, billType.DefaultCashAccountId.Value, bill.CurrencyId, bill.CurrencyValue, null, BillEntryItemType.Pay, debit, credit, "");
                bill.BillEntryItems.Add(payEntryItem);
            }

            var itemsIndex = 0;
            // check form items if not found
            foreach (var item in model.Items)
            {
                itemsIndex++;
                Guid itemStoreId;
                if (model.StoreId.HasValue && model.StoreId.Value == item.StoreId)
                {
                    itemStoreId = storeId.Value;
                }
                else
                {
                    var itemStore = await _storeRepo.GetAsync(item.StoreId.Value);
                    if (itemStore == null)
                    {
                        return NotFound($"store in item {itemsIndex} not found");
                    }
                    itemStoreId = itemStore.Id;
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
                        var itemCostCenter = await _storeRepo.GetAsync(item.CostCenterId.Value);
                        if (itemCostCenter == null)
                        {
                            return NotFound($"costCenter in item {0} not found");
                        }
                    }
                }

                var itemUnit = _itemUnitRepo.Get(item.ItemId, item.UnitId);

                if (itemUnit == null)
                {
                    ModelState.AddModelError($"Items[{itemsIndex}].ItemId", "المادة غير موجودة");
                    return BadRequest(ModelState.GetWithErrorsKey());
                }

                var billItem = new BillItem(itemUnit.Id, itemStoreId, itemCostCenterId, item.Quantity, item.Price, item.Extra, item.Disc, model.Note);
                bill.BillItems.Add(billItem);

                if (billType.AutoPostToStores)
                {
                    if (!await PostToStore(bill, billType, billItem, itemUnit))
                    {
                        ModelState.AddModelError($"Items[{itemsIndex}].Quantity", "لا يوجد كل هذه الكمية في المستودع");
                        return BadRequest(ModelState.GetWithErrorsKey());
                    }
                    bill.IsPosted = true;
                }
            }

            bill.CalcTotal();

            #region billEntryItem
            if (bill.Extra + bill.TotalItemsExtra > 0 && billType.DefaultExtraAccountId.HasValue)
            {
                var extraEntryItem = new BillEntryItem(bill.Date, billType.DefaultExtraAccountId.Value, bill.CurrencyId, bill.CurrencyValue, bill.CostCenterId, BillEntryItemType.ExtraDisc, 0, bill.Extra + bill.TotalItemsExtra, bill.Note);
                bill.BillEntryItems.Add(extraEntryItem);
            }

            if (bill.Disc + bill.TotalItemsDisc > 0 && billType.DefaultDiscAccountId.HasValue)
            {
                var discEntryItem = new BillEntryItem(bill.Date, billType.DefaultDiscAccountId.Value, bill.CurrencyId, bill.CurrencyValue, bill.CostCenterId, BillEntryItemType.ExtraDisc, bill.Disc + bill.TotalItemsDisc, 0, bill.Note);
                bill.BillEntryItems.Add(discEntryItem);
            }

            foreach (var pay in model.Pays)
            {
                var payEntryItem = new BillEntryItem(pay.Date, pay.AccountId, pay.CurrencyId, pay.CurrencyValue, pay.CostCenterId, BillEntryItemType.Pay, pay.Debit, pay.Credit, pay.Note);
                bill.BillEntryItems.Add(payEntryItem);
            }
            #endregion

            if (billType.AutoGenerateEntry)
            {
                if (bill.PayType == PaysType.Cash)
                    bill.AccountId = cashAccountId;
                else
                    bill.AccountId = customerAccountId;

                Entry entry = GenerateEntry(bill, billType);

                bill.BillEntry = new BillEntry(entry);

                _entryRepo.Add(entry, false);

                bill.IsEntryGenerated = true;

                if (billType.AutoPostEntryToAccounts)
                {
                    await _accountBalanceService.PostEntryToAccounts(entry);
                }
            }

            bill.Number = await _billRepo.GetNextNumberAsync(billType.Id);
            var affectedRows = await _billRepo.AddAsync(bill);
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<BillViewModel>(bill);

                return CreatedAtRoute("GetBill", new { typeId = bill.BillType.Number, id = bill.Number }, viewModel);
            }
            return BadRequest();
        }

        private List<EntryItem> GenerateEntryItems(Bill bill, Guid account1Id, Guid accountId2, double debit, double credit)
        {
            var entryItems = new List<EntryItem>();
            EntryItem entryItem1 = new EntryItem(account1Id, debit, 0, bill.CurrencyId, bill.CurrencyValue, bill.CostCenterId, bill.Date.UtcDateTime, "");
            EntryItem entryItem2 = new EntryItem(accountId2, 0, credit, bill.CurrencyId, bill.CurrencyValue, bill.CostCenterId, bill.Date.UtcDateTime, "");
            entryItems.Add(entryItem1);
            entryItems.Add(entryItem2);
            return entryItems;
        }

        private HashSet<EntryItem> GenerateBillEntryItems(Bill bill, bool IsIn)
        {
            var entryItems = new HashSet<EntryItem>();
            var billentryItems = bill.BillEntryItems;
            double debitSum = 0.0, creditSum = 0.0;
            if (IsIn)
            {
                debitSum = billentryItems.Sum(x => x.Credit);
                // Group entry Items that have same account and same debit ( Short Entry ) 
                var billPayEntryItemsGrouped = billentryItems.Where(a => a.Type == BillEntryItemType.Pay)
                                            .GroupBy(a => a.AccountId).Select(a => new { Credit = a.Sum(b => b.Credit), AccountId = a.Key });

                var billExtraDiscEntryItems = billentryItems.Where(a => a.Type == BillEntryItemType.ExtraDisc);


                foreach (var item in billExtraDiscEntryItems)
                {
                    EntryItem entryItemExtraDisc = new EntryItem(item.AccountId, item.Credit, item.Debit, bill.CurrencyId, bill.CurrencyValue, bill.CostCenterId, bill.Date.UtcDateTime, bill.Note);
                    entryItems.Add(entryItemExtraDisc);
                }

                foreach (var item in billPayEntryItemsGrouped)
                {
                    EntryItem entryItem1 = new EntryItem(item.AccountId, 0, item.Credit, bill.CurrencyId, bill.CurrencyValue, null, bill.Date.UtcDateTime, bill.Note);
                    entryItems.Add(entryItem1);
                }
                EntryItem entryItem2 = new EntryItem((Guid)bill.AccountId, debitSum, 0, bill.CurrencyId, bill.CurrencyValue, null, bill.Date.UtcDateTime, bill.Note);
                entryItems.Add(entryItem2);
            }
            else
            {
                creditSum = billentryItems.Sum(x => x.Debit);
                // Group entry Items that have same account and same Credit ( Short Entry ) 
                var billPayEntryItemsGrouped = billentryItems.Where(a => a.Type == BillEntryItemType.Pay)
                                            .GroupBy(a => a.AccountId).Select(a => new { Debit = a.Sum(b => b.Debit), AccountId = a.Key });

                var billExtraDiscEntryItems = billentryItems.Where(a => a.Type == BillEntryItemType.ExtraDisc);

                foreach (var item in billExtraDiscEntryItems)
                {
                    EntryItem entryItemExtraDisc = new EntryItem(item.AccountId, item.Debit, item.Credit, bill.CurrencyId, bill.CurrencyValue, bill.CostCenterId, bill.Date.UtcDateTime, bill.Note);
                    entryItems.Add(entryItemExtraDisc);
                }

                foreach (var item in billPayEntryItemsGrouped)
                {
                    EntryItem entryItem1 = new EntryItem(item.AccountId, item.Debit, 0, bill.CurrencyId, bill.CurrencyValue, bill.CostCenterId, bill.Date.UtcDateTime, bill.Note);
                    entryItems.Add(entryItem1);
                }
                EntryItem entryItem2 = new EntryItem((Guid)bill.AccountId, 0, creditSum, bill.CurrencyId, bill.CurrencyValue, bill.CostCenterId, bill.Date.UtcDateTime, bill.Note);
                entryItems.Add(entryItem2);
            }

            return entryItems;
        }

        private Entry GenerateEntry(Bill bill, BillType billType)
        {

            Guid account1Id, account2Id;
            double debit = 0.0, credit = 0.0;
            var billAccountId = (Guid)bill.AccountId;
            bool InOrOut = false;
            var billItemsAccountId = billType.DefaultItemsAccountId.Value;
            switch (billType.Type)
            {
                case BillsType.Sales:
                case BillsType.PurchaseReturn:
                case BillsType.EndPeriodInventory:
                    account1Id = billAccountId; // حساب الصندوق أو الزبون
                    account2Id = billItemsAccountId; // حساب المبيعات
                    InOrOut = false;
                    debit = bill.Total;
                    credit = bill.TotalItems;
                    break;
                case BillsType.Purchase:
                case BillsType.SalesReturn:
                case BillsType.BeginningInventory:
                    account1Id = billItemsAccountId; // حساب المشتريات
                    account2Id = billAccountId; // حساب الصندوق أو الزبون
                    InOrOut = true;
                    debit = bill.TotalItems;
                    credit = bill.Total;
                    break;
            }

            var Items = GenerateEntryItems(bill, account1Id, account2Id, debit, credit);

            if (bill.BillEntryItems.Count() > 0)
            {
                var billEntryItems = GenerateBillEntryItems(bill, InOrOut);
                Items.AddRange(billEntryItems);
            }

            Entry entry;

            if (bill.BillEntry != null)
            {
                entry = bill.BillEntry.Entry;
                entry.BranchId = bill.BranchId;
                entry.CurrencyId = bill.CurrencyId;
                entry.CurrencyValue = bill.CurrencyValue;
                entry.Note = bill.Note;
                entry.Items = new HashSet<EntryItem>();
            }
            else
            {
                entry = new Entry(_entryRepo.GetNextNumber(), bill.CurrencyId, bill.CurrencyValue, bill.BranchId, bill.Note);
            }

            entry.Items = Items;

            return entry;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BillViewModel), 200)]
        public async Task<IActionResult> Put(long typeId, long id, [FromBody]EditBillViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (model.Id != id)
            {
                return BadRequest();
            }

            var billType = await _billTypeRepo.GetAsync(typeId);
            if (billType == null)
            {
                return NotFound(Resources.Bills.BillResource.BillTypeNotFound);
            }

            var bill = await _billRepo.GetAsync(billType.Id, id);
            if (bill == null)
            {
                return NotFound(Resources.Bills.BillResource.BillNotFound);
            }

            #region checks

            Guid currencyId;
            var currency = await _currencyRepo.GetAsync(model.CurrencyId);
            if (currency != null)
            {
                currencyId = currency.Id;
                if (!model.CurrencyValue.HasValue)
                {
                    model.CurrencyValue = currency.Value;
                }
            }
            else
            {
                currencyId = _defaultKeysOptions.Value.CurrencyId;
                model.CurrencyValue = 1;
            }

            Guid cashAccountId;
            var cashAccount = await _accountRepo.GetAsync(model.AccountId);
            if (cashAccount == null)
            {
                return NotFound("Cash account not found");
            }
            cashAccountId = cashAccount.Id;


            Guid? customerAccountId = null;
            if (model.CustomerAccountId.HasValue)
            {
                var customerAccount = await _accountRepo.GetAsync(model.CustomerAccountId.Value);
                if (customerAccount == null)
                {
                    return NotFound("Customer account not found");
                }
                if (customerAccount.CustomerId == null)
                {
                    ModelState.AddModelError("CustomerAccountId", "account not related to customer or supplier");
                    return BadRequest(ModelState.GetWithErrorsKey());
                }
                customerAccountId = customerAccount.Id;
            }

            Guid? storeId = null;
            if (model.StoreId.HasValue)
            {
                var store = await _storeRepo.GetAsync(model.StoreId.Value);
                if (store == null)
                {
                    return NotFound(Resources.Stores.StoreResource.StoreNotFound);
                }
                storeId = store.Id;
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

            #region undo bill posting to stores

            if (bill.IsPosted)
            {
                foreach (var billItem in bill.BillItems)
                {
                    await PostToStore(bill, bill.BillType, billItem, billItem.ItemUnit, true);
                }
            }
            #endregion

            bill.AccountId = cashAccountId;
            bill.CurrencyId = currencyId;
            bill.CurrencyValue = model.CurrencyValue.Value;
            bill.CustomerAccountId = customerAccountId;
            bill.CustomerName = model.CustomerAccountCodeName;
            bill.Date = model.Date;
            bill.PayType = model.PayType;
            bill.StoreId = storeId;
            bill.CostCenterId = costCenterId;
            bill.BranchId = branchId;
            bill.Extra = model.Extra;
            bill.Disc = model.Disc;
            bill.TotalPaid = model.TotalPaid;
            bill.Note = model.Note;
            bill.BillItems = new HashSet<BillItem>();

            var itemsIndex = 0;
            // check form items if not found
            foreach (var item in model.Items)
            {
                itemsIndex++;
                Guid itemStoreId;
                if (model.StoreId.HasValue && model.StoreId.Value == item.StoreId)
                {
                    itemStoreId = storeId.Value;
                }
                else
                {
                    var itemStore = await _storeRepo.GetAsync(item.StoreId.Value);
                    if (itemStore == null)
                    {
                        return NotFound($"store in item {itemsIndex} not found");
                    }
                    itemStoreId = itemStore.Id;
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
                        var itemCostCenter = await _storeRepo.GetAsync(item.CostCenterId.Value);
                        if (itemCostCenter == null)
                        {
                            return NotFound($"costCenter in item {0} not found");
                        }
                    }
                }

                var itemUnit = _itemUnitRepo.Get(item.ItemId, item.UnitId);

                if (itemUnit == null)
                {
                    ModelState.AddModelError($"Items[{itemsIndex}].ItemId", "المادة غير موجودة");
                    return BadRequest(ModelState.GetWithErrorsKey());
                }

                var billItem = new BillItem(itemUnit.Id, itemStoreId, itemCostCenterId, item.Quantity, item.Price, item.Extra, item.Disc, model.Note);
                bill.BillItems.Add(billItem);

                if (bill.BillType.AutoPostToStores)
                {
                    if (!await PostToStore(bill, bill.BillType, billItem, itemUnit))
                    {
                        ModelState.AddModelError($"Items[{itemsIndex}].Quantity", "لا يوجد كل هذه الكمية في المستودع");
                        return BadRequest(ModelState.GetWithErrorsKey());
                    }
                }
            }

            bill.CalcTotal();

            #region billEntryItem
            if (bill.Extra + bill.TotalItemsExtra > 0 && bill.BillType.DefaultExtraAccountId.HasValue)
            {
                var extraEntryItem = new BillEntryItem(bill.Date, bill.BillType.DefaultExtraAccountId.Value, bill.CurrencyId, bill.CurrencyValue, bill.CostCenterId, BillEntryItemType.ExtraDisc, 0, bill.Extra + bill.TotalItemsExtra, bill.Note);
                bill.BillEntryItems.Add(extraEntryItem);
            }

            if (bill.Disc + bill.TotalItemsDisc > 0 && bill.BillType.DefaultDiscAccountId.HasValue)
            {
                var discEntryItem = new BillEntryItem(bill.Date, bill.BillType.DefaultDiscAccountId.Value, bill.CurrencyId, bill.CurrencyValue, bill.CostCenterId, BillEntryItemType.ExtraDisc, bill.Disc + bill.TotalItemsDisc, 0, bill.Note);
                bill.BillEntryItems.Add(discEntryItem);
            }

            foreach (var pay in model.Pays)
            {
                var payEntryItem = new BillEntryItem(pay.Date, pay.AccountId, pay.CurrencyId, pay.CurrencyValue, pay.CostCenterId, BillEntryItemType.Pay, pay.Debit, pay.Credit, pay.Note);
            }
            #endregion

            if (bill.IsEntryGenerated)
            {
                var entry = bill.BillEntry.Entry;
                if (entry.IsPosted)
                {
                    // rolback 
                    await _accountBalanceService.PostEntryToAccounts(entry, true);
                    //await PostEntryToAccounts(entry,true);
                }

                if (bill.PayType == PaysType.Cash)
                    bill.AccountId = cashAccountId;
                else
                    bill.AccountId = customerAccountId;
                entry = GenerateEntry(bill, bill.BillType);
                _entryRepo.Edit(entry, false);

                if (entry.IsPosted)
                {
                    // Post to Account
                    await _accountBalanceService.PostEntryToAccounts(entry);
                    //await PostEntryToAccounts(entry);
                }
            }

            var affectedRows = await _billRepo.EditAsync(bill);
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<BillViewModel>(bill);

                return CreatedAtRoute("GetBill", new { id = bill.Number }, viewModel);
            }
            return BadRequest();
        }

        [HttpPut("postToStore/{id}")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> PostToStores(long typeId, long id)
        {
            var billType = await _billTypeRepo.GetAsync(typeId);
            if (billType == null)
            {
                return NotFound(Resources.Bills.BillResource.BillTypeNotFound);
            }

            var bill = await _billRepo.GetAsync(billType.Id, id);
            if (bill == null)
            {
                return NotFound(Resources.Bills.BillResource.BillNotFound);
            }

            if (bill.IsPosted)
            {
                ModelState.AddModelError("", "تم ترحيل هذه الفاتورة مسبقا, لا يمكن ترحيلها مرة ثانية");
                return BadRequest(ModelState.GetWithErrorsKey());
            }

            var itemsIndex = 0;
            foreach (var billItem in bill.BillItems)
            {
                itemsIndex++;
                if (!await PostToStore(bill, bill.BillType, billItem, billItem.ItemUnit))
                {
                    ModelState.AddModelError($"Items[{itemsIndex}].Quantity", "لا يوجد كل هذه الكمية في المستودع");
                    return BadRequest(ModelState.GetWithErrorsKey());
                }
            }
            var affectedRows = await _billRepo.SaveAllAsync();
            if (affectedRows > 0)
            {
                return Ok(true);
            }
            return BadRequest();
        }

        private async Task<bool> PostEntryToAccounts(Entry entry, bool rollBack = false)
        {
            var accountsId = entry.Items.Select(a => a.AccountId).Distinct();
            var accounts = _accountRepo.NativeGetAll().Where(e => accountsId.Contains(e.Id));
            foreach (var item in accounts)
            {
                var accountEntryItems = entry.Items.Where(e => e.AccountId == item.Id);
                var totalDebit = accountEntryItems.Sum(e => e.Debit);
                var totalCredit = accountEntryItems.Sum(e => e.Credit);
                var accountBalance = await _accountBalanceRepo.GetAccountBalanceAsync(item.Id);

                if (rollBack)
                {
                    totalDebit = totalDebit * -1;
                    totalCredit = totalCredit * -1;
                }

                if (!rollBack && accountBalance == null)
                {
                    accountBalance = new AccountBalance { AccountId = item.Id, Debit = totalDebit, Credit = totalCredit };
                    await _accountBalanceRepo.AddAsync(accountBalance, false);
                }
                else
                {
                    accountBalance.Debit += totalDebit;
                    accountBalance.Credit += totalCredit;
                    _accountBalanceRepo.Edit(accountBalance, false);
                }

            }

            return await Task.FromResult(true);
        }

        private async Task<bool> PostToStore(Bill bill, BillType billtype, BillItem billItem, ItemUnit itemUnit, bool rollBack = false)
        {
            var inOrOut = int.Parse(EnumHelper.GetDescription(billtype.Type));
            var billItemQuantity = inOrOut * billItem.Quantity;
            if (rollBack)
            {
                billItemQuantity = billItemQuantity * -1;
            }
            #region storeItemUnit

            var storeItemUnit = await _storeItemUnitRepo.NativeGetAll().FirstOrDefaultAsync(e => e.StoreId == billItem.StoreId && e.ItemUnitId == billItem.ItemUnitId);
            if (!rollBack && storeItemUnit == null) // check if not exist => create one
            {
                if (billtype.PreventNegativeOutput && billItemQuantity < 0)
                {
                    return false;
                }
                storeItemUnit = new StoreItemUnit(billItem.StoreId, billItem.ItemUnitId, billItemQuantity);
                _storeItemUnitRepo.Add(storeItemUnit, false);
            }
            else // if exist => update quantity
            {
                if (!rollBack && billtype.PreventNegativeOutput && storeItemUnit.Quantity + billItemQuantity < 0)
                {
                    return false;
                }
                storeItemUnit.Quantity = storeItemUnit.Quantity + billItemQuantity;
                // if quantity equal zero set end date of this item unit store
                if (!rollBack && billItemQuantity <= 0)
                {
                    storeItemUnit.SetEndDateNow();
                }
                _storeItemUnitRepo.Edit(storeItemUnit, false);
            }

            #endregion

            #region storeItem

            var storeItem = await _storeItemRepo.NativeGetAll().FirstOrDefaultAsync(e => e.StoreId == billItem.StoreId && e.ItemId == itemUnit.ItemId);

            //
            if (!itemUnit.IsDefault)
            {
                billItemQuantity = billItemQuantity * itemUnit.Factor;
            }
            //

            if (!rollBack && storeItem == null) // check if not exist => create one 
            {
                storeItem = new StoreItem(billItem.StoreId, itemUnit.ItemId, billItemQuantity);
                _storeItemRepo.Add(storeItem, false);
            }
            else // if exist => update quantity
            {
                storeItem.Quantity = storeItem.Quantity + billItemQuantity;
                // if quantity equal zero set end date of this item unit store
                if (billItemQuantity <= 0)
                {
                    storeItem.SetEndDateNow();
                }
                _storeItemRepo.Edit(storeItem, false);
            }

            #endregion

            bill.IsPosted = !rollBack;

            return await Task.FromResult(true);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(BillViewModel), 200)]
        [SwaggerResponseExample(200, typeof(BillViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> Delete(long typeId, long id)
        {
            var billType = await _billTypeRepo.GetAsync(typeId);
            if (billType == null)
            {
                return NotFound(Resources.Bills.BillResource.BillTypeNotFound);
            }

            var bill = await _billRepo.GetAsync(billType.Id, id);
            if (bill == null)
            {
                return NotFound(Resources.Bills.BillResource.BillNotFound);
            }

            var affectedRows = await _billRepo.DeleteAsync(bill);
            if (affectedRows == -1)
            {
                return BadRequest(Resources.Bills.BillResource.CanNotDeleteBill);
            }
            if (affectedRows > 0)
            {
                var viewModel = AutoMapper.Mapper.Map<BillViewModel>(bill);
                return Ok(viewModel);
            }
            return BadRequest();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _billRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}