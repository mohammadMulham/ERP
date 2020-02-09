using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("StoreItems", Schema = "Production")]
    public class StoreItem : BaseClass
    {
        public StoreItem()
        {
            Initialize();
        }

        public StoreItem(Guid storeId, Guid itemId, double quantity)
        {
            Initialize();
            StoreId = storeId;
            ItemId = itemId;
            Quantity = quantity;
        }

        private void Initialize()
        {
            StartDate = DateTimeOffset.UtcNow;
        }

        public void SetEndDateNow()
        {
            EndDate = DateTimeOffset.UtcNow;
        }

        public Guid StoreId { get; set; }
        public Store Store { get; set; }
        public Guid ItemId { get; set; }
        public Item Item { get; set; }
        /// <summary>
        /// Quantity for default unit
        /// </summary>
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
        public DateTimeOffset StartDate { get; set; }
        /// <summary>
        /// this will take date when the quantity equal zero
        /// </summary>
        public DateTimeOffset? EndDate { get; set; }
    }
}
