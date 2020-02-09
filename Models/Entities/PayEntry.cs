using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("PayEntries", Schema = "Accounting")]
    public class PayEntry : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override long Number { get; set; }

        public Guid EntryId { get; set; }
        public virtual Entry Entry { get; set; }
        public Guid PayTypeId { get; set; }
        public virtual PayType PayType { get; set; }
        public Guid? PayAccountId { get; set; }
        public Account PayAccount { get; set; }
      
    }
}
