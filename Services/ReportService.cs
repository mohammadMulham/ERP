using ERPAPI.Factories;
using ERPAPI.Helpers;
using ERPAPI.Models;
using ERPAPI.Repositories;
using ERPAPI.ViewModels.Reports.CustomerAccountStatement;
using ERPAPI.ViewModels.Reports.Journal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Services
{
    public class ReportService : IReportService
    {
        #region variables
        IEntryItemRepository _entryItemrep;
        IEntryRepository _entryRep;
        IBillRepository _billRepo;
        IAccountRepository _accountRep;
        ICostCenterRepository _costCenterRep;
        ICurrencyRepository _currencyRep;
        IModelFactory _modelFactory;
        #endregion

        public ReportService(
            IEntryItemRepository entryItemrep,
             IEntryRepository entryRep,
             IBillRepository billRepo,
            IAccountRepository accountRep,
            ICostCenterRepository costCenterRep,
            ICurrencyRepository currencyRep,
            IModelFactory modelFactory)
        {
            _entryItemrep = entryItemrep;
            _entryRep = entryRep;
            _billRepo = billRepo;
            _accountRep = accountRep;
            _costCenterRep = costCenterRep;
            _currencyRep = currencyRep;
            _modelFactory = modelFactory;
        }

        public CustomerAccountStatementViewModel GetCustomerAccountStatment(Guid? customerId, Guid? cusAccountId, Guid? costCenterId, DateTimeOffset? from, DateTimeOffset? fromReleaseDate, DateTimeOffset? toReleaseDate, DateTimeOffset? to)
        {
            var billQuery = _billRepo.GetAllNoTracking();
            var entryItemsQuery = _entryItemrep.GetAllNoTracking();
            entryItemsQuery = entryItemsQuery.Where(e => e.Entry.EntryOrigin != EntryOrigin.Bill);
            if (customerId.HasValue)
            {
                var cusAccounts = _accountRep.GetCustomerAccounts(customerId.Value).Select(c => c.Id);
                billQuery = billQuery.Where(e => cusAccounts.Contains(e.CustomerAccountId.Value));
                entryItemsQuery = entryItemsQuery.Where(e => cusAccounts.Contains(e.AccountId));
            }
            if (cusAccountId.HasValue)
            {
                billQuery = billQuery.Where(e => e.CustomerAccountId == cusAccountId);
                entryItemsQuery = entryItemsQuery.Where(I => I.AccountId == cusAccountId);
            }
            if (costCenterId.HasValue)
            {
                billQuery = billQuery.Where(e => e.CostCenterId == costCenterId);
                entryItemsQuery = entryItemsQuery.Where(I => I.CostCenterId == costCenterId);
            }
            if (from.HasValue)
            {
                billQuery = billQuery.Where(e => e.Date.Date >= from);
                entryItemsQuery = entryItemsQuery.Where(e => e.Entry.Date.Date >= from);
            }

            if (to.HasValue)
            {
                billQuery = billQuery.Where(e => e.Date.Date <= to);
                entryItemsQuery = entryItemsQuery.Where(e => e.Entry.Date.Date <= to);
            }

            if (fromReleaseDate.HasValue)
            {
                billQuery = billQuery.Where(e => e.Date.Date >= fromReleaseDate);
                entryItemsQuery = entryItemsQuery.Where(e => e.Entry.Date.Date >= fromReleaseDate);
            }

            if (toReleaseDate.HasValue)
            {
                billQuery = billQuery.Where(e => e.Date.Date <= toReleaseDate);
                entryItemsQuery = entryItemsQuery.Where(e => e.Entry.Date.Date <= toReleaseDate);
            }

            var customerEntries = new List<CusEntryViewModel>();
            foreach (var item in billQuery.ToList())
            {
                var model = new CusEntryViewModel();
                model.EntryTypeNoName = item.Number + "-" + item.BillType.Name;
                model.Note = item.PayType.ToString();
                if (item.PayType == PaysType.Credit)
                {
                    var inOrOut = int.Parse(EnumHelper.GetDescription(item.BillType.Type));
                    if (inOrOut == 1)
                    {
                        model.Credit = item.Total;
                    }
                    else
                    {
                        model.Debit = item.Total;
                    }
                    foreach (var billEntryItem in item.BillEntryItems)
                    {
                        var payItem = new CusEntryViewModel();
                        payItem.Debit = billEntryItem.Credit;
                        payItem.Credit = billEntryItem.Debit;
                        payItem.ContraAccount = billEntryItem.Account.CodeName;
                        payItem.Date = billEntryItem.Date;
                        customerEntries.Add(payItem);
                    }
                }
                else
                {
                    model.Debit = model.Credit = item.Total;
                }
                model.BillItems = item.BillItems.Select(b => _modelFactory.CreateCusBillItemViewModel(b)).ToList();
                customerEntries.Add(model);
            }

            foreach (var item in entryItemsQuery.ToList())
            {
                var model = new CusEntryViewModel();
                var entry = item.Entry;
                model.CreationDate = entry.CreatedDateTime;
                model.Date = entry.Date;
                model.EntryNo = entry.Number;
                model.EntryOrigin = entry.EntryOrigin;
                model.Debit = item.Debit;
                model.Credit = item.Credit;
                model.Note = item.Note;
                switch (entry.EntryOrigin)
                {
                    case EntryOrigin.None:
                        model.EntryOriginNo = entry.Number;
                        break;
                    case EntryOrigin.Pay:
                        var payEntry = entry.PayEntry;
                        model.EntryOriginNo = payEntry.Number;
                        model.EntryTypeNoName = payEntry.Number + "-" + payEntry.PayType.Name;
                        break;
                }
                customerEntries.Add(model);
            }
            var cusAccountViewModel = new CustomerAccountStatementViewModel
            {
                Items = customerEntries.OrderBy(e => e.Date).ToList(),
                From = from.HasValue ? from.Value : (DateTimeOffset?)null,
                To = to.HasValue ? to.Value : (DateTimeOffset?)null,
            };
            return cusAccountViewModel;
        }

        public JournalViewModel GetjournalEntry(Guid? accountId, Guid? costCenterId, DateTimeOffset? from, DateTimeOffset? fromReleaseDate, DateTimeOffset? toReleaseDate, DateTimeOffset? to, JournalOptions options)
        {
            var dataQurey = _entryRep.NativeGetAll();

            if (from.HasValue)
            {
                dataQurey = dataQurey.Where(e => e.Date.Date.Date >= from);
            }

            if (to.HasValue)
            {
                dataQurey = dataQurey.Where(e => e.Date.Date <= to);
            }

            if (fromReleaseDate.HasValue)
            {
                dataQurey = dataQurey.Where(e => e.CreatedDateTime.Date.Date >= fromReleaseDate);
            }

            if (toReleaseDate.HasValue)
            {
                dataQurey = dataQurey.Where(e => e.CreatedDateTime.Date <= toReleaseDate);
            }

            if (accountId.HasValue)
            {
                dataQurey = dataQurey.Where(e => e.Items.Any(I => I.AccountId == accountId));
            }

            if (costCenterId.HasValue)
            {
                dataQurey = dataQurey.Where(e => e.Items.Any(I => I.CostCenterId == costCenterId));
            }

            if (!options.ShowNotPostedEntries)
            {
                dataQurey = dataQurey.Where(e => e.IsPosted);
            }

            var journalViewModel = new JournalViewModel();
            journalViewModel.Items = dataQurey.Select(e => _modelFactory.CreateJournalEntryViewModel(e, options)).ToList();

            return journalViewModel;

        }
    }
}
