using ERPAPI.Models;
using ERPAPI.ViewModels;
using ERPAPI.ViewModels.Currencies;
using ERPAPI.ViewModels.Items;
using ERPAPI.ViewModels.Reports.CustomerAccountStatement;
using ERPAPI.ViewModels.Reports.Journal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Factories
{
    public interface IModelFactory
    {
        JournalEntryViewModel CreateJournalEntryViewModel(Entry entry, JournalOptions op);
        JournalItemViewModel CreateJournalEntryItemViewModel(EntryItem entryItem);
        CusBillItemViewModel CreateCusBillItemViewModel(BillItem billItem);
        SearchViewModel CreateSearchViewModel(ItemUnitPrice itemUnitPrice);
        SearchWithChildViewModel CreateSearchWithChildViewModel(Account account);
        SearchWithChildViewModel CreateSearchWithChildViewModel(ItemUnit itemUnit);
        CurrencyViewModel CreateViewModel(Currency currency, Guid defaultCurrencyId);
    }
   

}
