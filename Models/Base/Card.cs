using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class Card : DefaultEntity
    {
        [Required]
        [StringLength(128)]
        [Column(Order = 3)]
        public virtual string Code { get; set; }

        private string codeName;

        [NotMapped]
        public string CodeName
        {
            get
            {
                codeName = string.Format("{0}-{1}", Code, Name);
                return codeName;
            }
        }
    }
}
