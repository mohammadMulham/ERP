using ERPAPI.Models;
using ERPAPI.ViewModels.Card;
using ERPAPI.ViewModels.DefaultEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.BillTypes
{
    public class BillTypeViewModel : CardViewModel
    {

        public BillsType Type { get; set; }
        /// <summary>
        /// not used yet!
        /// </summary>
        public short BillInOut { get; set; }
        /// <summary>
        /// for view in bill types list
        /// </summary>
        public int BillTypeOrder { get; set; }


        #region accounting configurations
        /// <summary>
        /// عدم توليد القيود
        /// </summary>
        public bool NotGenerateEntry { get; set; }

        /// <summary>
        /// توليد القيود بشكل الي
        /// </summary>
        public bool AutoGenerateEntry { get; set; }

        /// <summary>
        /// ترحيل القيود الى الحسابات بشكل الي
        /// </summary>
        public bool AutoPostEntryToAccounts { get; set; }

        /// <summary>
        /// منع الاخراج بالسالب
        /// </summary>
        public bool PreventNegativeOutput { get; set; }

        /// <summary>
        /// عدم ترحيل الفواتير الى المخازن
        /// </summary>
        public bool NoPostToStores { get; set; }

        /// <summary>
        /// ترحيل الي للفواتير الى المخازن
        /// </summary>
        public bool AutoPostToStores { get; set; }
        #endregion

        #region view configurations
        /// <summary>
        /// Can not dont show if there is no DefaultStoreId
        /// </summary>
        public bool ShowStoreField { get; set; }
        public bool ShowCostCenterldField { get; set; }
        public bool ShowCustomerAccountldField { get; set; }
        /// <summary>
        /// Can not dont show if there is no DefaultPayType
        /// </summary>
        public bool ShowPayTypeField { get; set; }
        #endregion

        #region Entry
        public Guid? DefaultItemsAccountId { get; set; }
        public Guid? DefaultExtraAccountId { get; set; }
        public Guid? DefaultDiscAccountId { get; set; }
        #endregion

        #region defaultValues
        /// <summary>
        /// for view right price
        /// </summary>
        public Guid? DefaultPriceId { get; set; }
        public Guid? DefaultCashAccountId { get; set; }
        public Guid? DefaultStoreId { get; set; }
        public Guid? DefaultCostCenterId { get; set; }
        public Guid? DefaultBranchId { get; set; }
        public PaysType DefaultPayType { get; set; }
        #endregion

        #region items configurations
        public bool ShowItemUnitField { get; set; }
        public bool ShowItemPriceFields { get; set; }
        public bool ShowItemStoreField { get; set; }
        public bool ShowItemCostCenterldField { get; set; }
        public bool ShowItemExpireDateField { get; set; }
        public bool ShowItemProductionDateField { get; set; }
        public bool ShowItemNoteField { get; set; }
        public bool ShowTotalPriceItemField { get; set; }
        public bool ShowExtraField { get; set; }
        public bool ShowDiscField { get; set; }
        public bool CanEditItemTotalPrice { get; set; }
        public bool CanEditItemPrice { get; set; }
        public bool ShowNoteField { get; set; }
        public bool ShowBranchField { get; set; }
        public bool ShowSellersFields { get; set; }
        #endregion

        public int? Color1 { get; set; }
        public int? Color2 { get; set; }
    }
}
