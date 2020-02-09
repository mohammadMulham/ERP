using ERPAPI.ViewModels.Card;
using ERPAPI.ViewModels.PayEntries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.PayTypes
{
    public class AddPayTypeViewModel : AddCardViewModel
    {
        public AddPayTypeViewModel()
        {
            
        }
        public int PayTypeOrder { get; set; }

        #region defaultValues
        public Guid? DefaultAccountId { get; set; }
        public Guid? DefaultCostCenterId { get; set; }
        public Guid? DefaultBranchId { get; set; }
        public Guid? DefaultCurrencyId { get; set; }
        #endregion

        #region view configurations
        public bool ShowCostCenterldField { get; set; }
        public bool ShowBranchField { get; set; }
        public string DebitFieldName { get; set; }
        public string CreditFieldName { get; set; }
        #endregion

        #region items configurations
        public bool ShowItemDebitField { get; set; }
        public bool ShowItemCreditField { get; set; }
        public bool ShowItemCostCenterldField { get; set; }
        public bool ShowItemNoteField { get; set; }
        public bool ShowItemCurrencyField { get; set; }
        #endregion

        public bool IsBeginEntry { get; set; }

        public bool AutoPostToAccounts { get; set; }

        public int? Color1 { get; set; }
        public int? Color2 { get; set; }

        public ICollection<PayEntryViewModel> PayEntries { get; set; } = new HashSet<PayEntryViewModel>();
    }
}
