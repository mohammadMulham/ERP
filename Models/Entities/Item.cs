using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("Items", Schema = "Production")]
    public partial class Item : Card
    {

        private void Initialize()
        {
            Type = ItemType.Product;
            ItemUnits = new HashSet<ItemUnit>();
            StoreItems = new HashSet<StoreItem>();
        }

        public Item()
        {
            Initialize();
        }

        public Item(string name, string code, ItemType type, Guid itemGroupId, bool status, string note, Guid unitId, string unitBarcode)
        {
            Initialize();
            Name = name;
            Code = code;
            Type = type;
            ItemGroupId = itemGroupId;
            Note = note;
            ItemUnits.Add(new ItemUnit(unitId, unitBarcode, 1, true));
        }

        public Guid ItemGroupId { get; set; }
        public virtual ItemGroup ItemGroup { get; set; }
        public ItemType Type { get; set; }
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
        public string Note { get; set; }
        public virtual ICollection<ItemUnit> ItemUnits { get; set; }
        public virtual ICollection<StoreItem> StoreItems { get; set; }
    }
}
