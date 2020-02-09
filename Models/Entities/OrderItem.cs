using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("OrderItems", Schema = "Marketing")]
    public partial class OrderItem : BaseClass
    {
        public OrderItem()
        {

        }

        public OrderItem(Guid itemUnitId, double quantity)
        {
            ItemUnitId = itemUnitId;
            Quantity = quantity;
        }

        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
        public Guid ItemUnitId { get; set; }
        public virtual ItemUnit ItemUnit { get; set; }

        [Range(1, double.MaxValue)]
        public double Quantity { get; set; }

        public Guid? BillItemId { get; set; }
        public virtual BillItem BillItem { get; set; }
    }
}
