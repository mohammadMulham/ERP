using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public abstract class DefaultEntity : BaseEntity
    {
        [Required]
        [StringLength(128)]
        [Column(Order = 2)]
        public virtual string Name { get; set; }
    }
}
