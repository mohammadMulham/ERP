using ERPAPI.Managers;
using ERPAPI.PredefinedGuides;
using ERPAPI.ViewModels.PredefinedGuides;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Services
{
    public class PredefinedGuideService : IPredefinedGuideService
    {
        private readonly IFileManager _fileManager;
        private readonly PredefinedGuidesContent _predefinedGuidesContent;
        private readonly ILanguageManager _languageManager;
        private string _language;

        public PredefinedGuideService(
            IFileManager fileManager,
            ILanguageManager languageManager
            )
        {
            _fileManager = fileManager;
            _predefinedGuidesContent = GetPredefinedGuidesContent();
            _languageManager = languageManager;
            _language = _languageManager.GetLang();
        }

        private PredefinedGuidesContent GetPredefinedGuidesContent()
        {
            string content = _fileManager.ReadFile("predefinedGuides.json");
            var predefinedGuidesContent = JsonConvert.DeserializeObject<PredefinedGuidesContent>(content);
            return predefinedGuidesContent;
        }

        public List<AccountGuideViewModel> GetAccountGuides()
        {
            setLanguage();
            PredefinedGuidesContent predefinedGuidesContent = GetPredefinedGuidesContent();
            var viewModels = predefinedGuidesContent.AccountGuides.OrderBy(a => a.Order).Select(e => new AccountGuideViewModel
            {
                Id = (int)e.Id,
                Name = e.GetName(_language)
            }).ToList();
            return viewModels;
        }

        public List<PredefinedAccount> GetGuideAccounts(int guideId)
        {
            setLanguage();
            PredefinedGuidesContent predefinedGuidesContent = GetPredefinedGuidesContent();
            var viewModels = predefinedGuidesContent.AccountGuides.FirstOrDefault(e => (int)e.Id == guideId).Accounts;
            return viewModels;
        }

        public PredefinedCurrency GetCurrecny(int currencyId)
        {
            setLanguage();
            PredefinedGuidesContent predefinedGuidesContent = GetPredefinedGuidesContent();
            var viewModel = predefinedGuidesContent.Currencies.FirstOrDefault(e => e.Id == currencyId);
            return viewModel;
        }

        private void setLanguage()
        {
            _language = _languageManager.GetLang();
        }

        public List<CurrencyViewModel> GetCurrencies()
        {
            setLanguage();
            PredefinedGuidesContent predefinedGuidesContent = GetPredefinedGuidesContent();
            var viewModels = predefinedGuidesContent.Currencies.ToList().OrderBy(a => a.Order)
                .Select(e => new CurrencyViewModel
                {
                    Id = e.Id,
                    Code = e.Code,
                    Name = e.GetName(_language),
                    ISOCode = e.ISOCode,
                    PartName = e.GetPartName(_language),
                    PartRate = e.PartRate
                }).ToList();
            return viewModels;
        }
    }
}
