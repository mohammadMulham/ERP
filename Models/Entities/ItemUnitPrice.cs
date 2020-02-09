using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("ItemUnitPrices", Schema = "Production")]
    public partial class ItemUnitPrice : BaseClass
    {
        public ItemUnitPrice()
        {
            Initialize();
        }

        public ItemUnitPrice(Guid priceId, double value)
        {
            Initialize();
            PriceId = priceId;
            Value = value;
        }

        private void Initialize()
        {
            StartDate = DateTime.UtcNow;
        }

        public Guid ItemUnitId { get; set; }
        public ItemUnit ItemUnit { get; set; }
        public Guid PriceId { get; set; }
        public Price Price { get; set; }
        public double Value { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
