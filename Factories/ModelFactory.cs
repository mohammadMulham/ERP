using ERPAPI.Models;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.ViewModels.Reports.Journal;
using ERPAPI.ViewModels.Items;
using ERPAPI.ViewModels.Reports.CustomerAccountStatement;
using ERPAPI.ViewModels;
using ERPAPI.ViewModels.Currencies;

namespace ERPAPI.Factories
{
    public class ModelFactory : IModelFactory
    {
        public JournalEntryViewModel CreateJournalEntryViewModel(Entry entry, JournalOptions op)
        {
            var model = new JournalEntryViewModel();
            model.EntryNo = entry.Number;
            model.EntryOrigin = entry.EntryOrigin;
            switch (entry.EntryOrigin)
            {
                case EntryOrigin.None:
                    model.EntryOriginNo = entry.Number;
                    break;
                case EntryOrigin.Pay:
                    model.EntryOriginNo = entry.PayEntry.Number;
                    break;
                case EntryOrigin.Bill:
                    model.EntryOriginNo = entry.BillEntry.Bill.Number;
                    break;
            }
            model.Date = entry.Date;
            model.CreationDate = entry.CreatedDateTime;
            model.Items = entry.Items.Select(this.CreateJournalEntryItemViewModel).ToList();
            if (op.ShowTotalEntries)
            {
                model.DebitSum = model.Items.Sum(e => e.Debit);
                model.CreditSum = model.Items.Sum(e => e.Credit);
            }
            return model;
        }
        public JournalItemViewModel CreateJournalEntryItemViewModel(EntryItem entryItem)
        {
            var model = new JournalItemViewModel();
            model.AccountCodeName = entryItem.Account.CodeName;
            if (entryItem.Account.ParentAccount != null)
            {
                model.ParentAccountCodeName = entryItem.Account.ParentAccount.CodeName;
            }
            model.Debit = entryItem.Debit;
            model.Credit = entryItem.Credit;
            model.Note = entryItem.Note;
            model.CostCenterCodeName = entryItem.Currency.CodeName;
            model.CurrencyCode = entryItem.Currency.Code;
            model.CurrencyValue = entryItem.CurrencyValue;
            return model;
        }
        public List<CusEntryViewModel> CreateCusBillViewModel(Bill bill)
        {
            var model = new CusEntryViewModel();
            var Items = new List<CusEntryViewModel>();

            if (bill.PayType == PaysType.Credit)
            {

                model.Debit = bill.Total;
                model.Credit = 0.0;
                var billEntryItems = bill.BillEntryItems.Select(e => CreateCusBillEntryItemViewModel(e));
                Items.Add(model);
                Items.AddRange(billEntryItems);
            }
            else
            {
                model.Debit = model.Credit = bill.Total;
                Items.Add(model);
            }
            return Items;


        }
        public CusEntryViewModel CreateCusBillEntryItemViewModel(BillEntryItem billEntryItem)
        {
            var model = new CusEntryViewModel();
            model.Debit = billEntryItem.Credit;
            model.Credit = billEntryItem.Debit;
            return model;
        }
        public CusBillItemViewModel CreateCusBillItemViewModel(BillItem billItem)
        {
            var model = new CusBillItemViewModel();

            model.ItemUnitCodeName = billItem.ItemUnit.Item.CodeName;
            model.Quantity = billItem.Quantity;
            model.Price = billItem.Price;
            model.Total = billItem.Total;
            model.Extra = billItem.Extra;
            model.Disc = billItem.Disc;
            model.Note = billItem.Note;
            return model;
        }
        public SearchWithChildViewModel CreateSearchWithChildViewModel(Account account)
        {
            var viewModel = new SearchWithChildViewModel()
            {
                Id = account.Customer.Number,
                Label = account.Customer.NumberFullName,
                ShowChild = account.Customer.Accounts.LongCount() > 1,
                Child = new SearchViewModel
                {
                    Id = account.Number,
                    Label = account.CodeName
                }
            };
            return viewModel;
        }
        public SearchWithChildViewModel CreateSearchWithChildViewModel(ItemUnit itemUnit)
        {
            var viewModel = new SearchWithChildViewModel()
            {
                Id = itemUnit.Item.Number,
                Label = itemUnit.Item.CodeName,
                Child = new SearchViewModel
                {
                    Id = itemUnit.Unit.Number,
                    Label = itemUnit.Unit.Name
                }
            };
            return viewModel;
        }
        public SearchViewModel CreateSearchViewModel(ItemUnitPrice itemUnitPrice)
        {
            var viewModel = new SearchViewModel
            {
                Label = itemUnitPrice.Name,
                Id = itemUnitPrice.Value
            };
            return viewModel;
        }
        public CurrencyViewModel CreateViewModel(Currency currency, Guid defaultCurrencyId)
        {
            var model = new CurrencyViewModel()
            {
                Id = currency.Number,
                Code = currency.Code,
                Equivalent = currency.Equivalent,
                ISOCode = currency.ISOCode,
                Name = currency.Name,
                PartName = currency.PartName,
                PartRate = currency.PartRate,
                Value = currency.Value,
                IsDefault = currency.Id == defaultCurrencyId
            };
            return model;
        }
    }

}
