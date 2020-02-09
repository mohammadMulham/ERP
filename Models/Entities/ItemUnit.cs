using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("ItemUnits", Schema = "Production")]
    public partial class ItemUnit : BaseClass
    {
        private void Initialize()
        {
            BillItems = new HashSet<BillItem>();
            OrderItems = new HashSet<OrderItem>();
            StoreItemUnits = new HashSet<StoreItemUnit>();
            ItemUnitPrices = new HashSet<ItemUnitPrice>();
        }

        public ItemUnit()
        {
            Initialize();
        }

        public ItemUnit(Guid unitId, string code, double factor = 1, bool isDefault = false)
        {
            Initialize();
            UnitId = unitId;
            Code = code;
            Factor = factor;
            IsDefault = isDefault;
        }

        public Guid ItemId { get; set; } // 1
        public virtual Item Item { get; set; }

        public Guid UnitId { get; set; }
        public virtual Unit Unit { get; set; }

        public bool IsDefault { get; set; } // 2

        // 1 + 2 : must to have one default unit to each item
        public double Factor { get; set; }

        /// <summary>
        /// Barcode
        /// </summary>
        [StringLength(128)]
        public string Code { get; set; }

        public virtual ICollection<BillItem> BillItems { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<StoreItemUnit> StoreItemUnits { get; set; }
        public virtual ICollection<ItemUnitPrice> ItemUnitPrices { get; set; }
    }
}
