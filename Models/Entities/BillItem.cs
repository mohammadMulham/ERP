using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("BillItems", Schema = "Accounting")]
    public partial class BillItem : BaseClass
    {
        public BillItem()
        {
            Initialize();
        }

        public BillItem(Guid itemUnitId, Guid storeId, Guid? costCenterId, double quantity, double price, double extra, double disc, string note)
        {
            Initialize();
            ItemUnitId = itemUnitId;
            StoreId = storeId;
            CostCenterId = costCenterId;
            Quantity = quantity;
            Price = price;
            Extra = extra;
            Disc = disc;
            Note = note;
        }

        private void Initialize()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public Guid BillId { get; set; }
        public Bill Bill { get; set; }

        public Guid ItemUnitId { get; set; }
        public ItemUnit ItemUnit { get; set; }

        public Guid StoreId { get; set; }
        public Store Store { get; set; }

        public Guid? CostCenterId { get; set; }
        public CostCenter CostCenter { get; set; }

        public DateTime? ExpireDate { get; set; }
        public DateTime? ProductionDate { get; set; }

        [Range(1, double.MaxValue)]
        public double Quantity { get; set; }

        [Range(1, double.MaxValue)]
        public double Price { get; set; }

        public double Extra { get; set; }

        public double Disc { get; set; }

        public string Note { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

    }
}
