using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    /// <summary>
    /// Type of bill
    /// BillsType description will determine Store stock will increase or decrease
    /// </summary>
    public enum BillsType
    {
        /// <summary>
        /// بضاعة أول المدة
        /// </summary>
        [Description("1")]
        BeginningInventory = 0,

        /// <summary>
        /// Purchase bill will increase store stock
        /// (مشتريات)
        /// </summary>
        [Description("1")]
        Purchase = 1,

        /// <summary>
        /// Sales bill will increase store stock
        /// (مبيعات)
        /// </summary>
        [Description("-1")]
        Sales = 2,

        /// <summary>
        /// مرتجع شراء
        /// </summary>
        [Description("-1")]
        PurchaseReturn = 3,

        /// <summary>
        /// مرتجع مبيع
        /// </summary>
        [Description("1")]
        SalesReturn = 4,

        /// <summary>
        /// مناقلة
        /// </summary>
        Transfer = 5,

        /// <summary>
        /// بضاعة آخر المدة
        /// </summary>
        [Description("-1")]
        EndPeriodInventory = 6,

        //[Description("1")]
        //In,
        //// ادخال

        //[Description("-1")]
        //Out,
        //// اخراج
    }

}
