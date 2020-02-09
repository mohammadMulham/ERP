using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("BillSellers", Schema = "Accounting")]
    public partial class BillSeller : BaseClass
    {
        public Guid BillId { get; set; }
        public virtual Bill Bill { get; set; }

        public Guid SellerId { get; set; }
        public virtual Seller Seller { get; set; }
    }
}
