using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("Orders", Schema = "Marketing")]
    public partial class Order : BaseEntity
    {
        private void Initilize()
        {
            OrderItems = new HashSet<OrderItem>();
            //OrderMarketers = new HashSet<OrderMarketer>();
            //VisitOrders = new HashSet<VisitOrder>();
            Date = DateTime.Now;
            DueDate = Date;
        }

        public Order()
        {
            Initilize();
        }

        public Order(long number, Guid orderTypeId, DateTime date, DateTime dueDate, Guid customerId, Guid? branchId, IEnumerable<OrderItem> items, IEnumerable<Guid> marketersIds)
        {
            Initilize();

            Number = number;
            OrderTypeId = orderTypeId;
            Date = date;
            DueDate = dueDate;
            CustomerId = customerId;
            BranchId = branchId;
            foreach (var item in items)
            {
                OrderItems.Add(new OrderItem()
                {
                    ItemUnitId = item.ItemUnitId,
                    Quantity = item.Quantity
                });
            }
            //foreach (var marketerId in marketersIds)
            //{
            //    OrderMarketers.Add(new OrderMarketer()
            //    {
            //        EmployeeId = marketerId
            //    });
            //}
        }

        // number is separated for each type
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override long Number { get; set; }

        public Guid OrderTypeId { get; set; }
        public virtual OrderType OrderType { get; set; }

        public Guid FinancialPeriodId { get; set; }
        public virtual FinancialPeriod FinancialPeriod { get; set; }

        public DateTimeOffset Date { get; set; }

        public DateTimeOffset DueDate { get; set; } // => now or equal Date

        public DateTimeOffset? DeliveryDate { get; set; } // => when order delivred by saler

        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public Guid? BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        public Guid? StoreId { get; set; }
        public virtual Store Store { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
        //public virtual ICollection<OrderMarketer> OrderMarketers { get; set; }
        //public virtual ICollection<VisitOrder> VisitOrders { get; set; }
    }
}
