using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public abstract class BaseEntity : BaseClass
    {
        public BaseEntity()
        {
            EntityStatus = EntityStatus.Active;
            CreatedDateTime = DateTimeOffset.UtcNow;
        }

        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual long Number { get; set; }

        public EntityStatus EntityStatus { get; set; }

        public DateTimeOffset CreatedDateTime { get; set; }
    }
}
