using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("StoreItemUnits", Schema = "Production")]
    public class StoreItemUnit : BaseClass
    {
        public StoreItemUnit()
        {
            Initialize();
        }

        private void Initialize()
        {
            StartDate = DateTimeOffset.UtcNow;
        }

        public StoreItemUnit(Guid storeId, Guid itemUnitId, double quantity)
        {
            Initialize();
            StoreId = storeId;
            ItemUnitId = itemUnitId;
            Quantity = quantity;
        }

        public void SetEndDateNow()
        {
            EndDate = DateTimeOffset.UtcNow;
        }

        public Guid StoreId { get; set; }
        public Store Store { get; set; }
        public Guid ItemUnitId { get; set; }
        public ItemUnit ItemUnit { get; set; }
        public double Quantity { get; set; }
        /// <summary>
        /// Maximum limit for default Unit
        /// </summary>
        public double? MaxLimit { get; set; }
        /// <summary>
        /// Minimum limit for default Unit
        /// </summary>
        public double? MinLimit { get; set; }
        /// <summary>
        /// ReOrder limit for default Unit
        /// </summary>
        public double? ReOrderLimit { get; set; }
        public Guid FinancialPeriodId { get; set; }
        public virtual FinancialPeriod FinancialPeriod { get; set; }
        /// <summary>
        /// this will take date when enter first bill to this store
        /// </summary>
        public DateTimeOffset StartDate { get; set; }
        /// <summary>
        /// this will take date when the quantity equal zero
        /// </summary>
        public DateTimeOffset? EndDate { get; set; }
    }
}
