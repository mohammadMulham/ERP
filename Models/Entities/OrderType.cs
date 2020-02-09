using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("OrderTypes", Schema = "Marketing")]
    public partial class OrderType : DefaultEntity
    {
        public OrderType()
        {
            Orders = new HashSet<Order>();
            //VisitTypeOrderTypes = new HashSet<VisitTypeOrderType>();
        }
        public int OrderTypeOrder { get; set; }
        public int? MarketersNumber { get; set; }
        public bool IsCurrentUserMarketer { get; set; }
        public bool NoEditFirstLastCustomerOrderDate { get; set; }
        public bool ShowBranshField { get; set; }
        public Guid? DefaultBranchId { get; set; }
        /// <summary>
        /// if this true that must show store and select it in view or select set default store and hide it from view
        /// </summary>
        public bool WithAutoPostToStore { get; set; }
        public bool ShowStoreField { get; set; }
        public Guid? DefaultStoreId { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        //public virtual ICollection<VisitTypeOrderType> VisitTypeOrderTypes { get; set; }
    }
}
