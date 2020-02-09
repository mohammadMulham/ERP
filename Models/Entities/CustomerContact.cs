using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("CustomerContacts", Schema = "Marketing")]
    public partial class CustomerContact : BaseEntity
    {
        public CustomerContact()
        {
            //Calls = new HashSet<Call>();
        }

        public CustomerContact(Guid contactTypeId, string contact, bool isDefault = false)
        {
            ContactTypeId = contactTypeId;
            Contact = contact;
        }

        [StringLength(128)]
        public string FirstName { get; set; }
        [StringLength(128)]
        public string LastName { get; set; }

        [StringLength(128)]
        public string Adjective { get; set; }

        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public Guid ContactTypeId { get; set; }
        public virtual ContactType ContactType { get; set; }
        public string Contact { get; set; }
        public bool IsDefault { get; set; }
        // public ICollection<Call> Calls { get; set; }
    }
}
