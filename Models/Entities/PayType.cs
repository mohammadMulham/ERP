using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("PayTypes", Schema = "Accounting")]
    public class PayType : Card
    {
        public PayType()
        {
            Initialize();
        }

        public PayType(string name)
        {
            Initialize();
        }

        private void Initialize()
        {
            PayEntries = new HashSet<PayEntry>();
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

        public virtual ICollection<PayEntry> PayEntries { get; set; }

    }
}
