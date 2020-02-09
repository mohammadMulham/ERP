using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("ContactTypes", Schema = "Person")]
    public class ContactType : DefaultEntity
    {
        public ContactType()
        {
            CustomerContacts = new HashSet<CustomerContact>();
        }

        [StringLength(4)]
        [Required]
        public string Prefix { get; set; }

        public ICollection<CustomerContact> CustomerContacts { get; set; }
    }
}
