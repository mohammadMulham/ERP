using ERPAPI.PredefinedGuides;
using ERPAPI.ViewModels.PredefinedGuides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Services
{
    public interface IPredefinedGuideService
    {
        List<AccountGuideViewModel> GetAccountGuides();
        List<CurrencyViewModel> GetCurrencies();
        List<PredefinedAccount> GetGuideAccounts(int guideId);
        PredefinedCurrency GetCurrecny(int currencyId);
    }
}
