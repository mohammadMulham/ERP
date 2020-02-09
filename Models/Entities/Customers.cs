using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("Customers", Schema = "Marketing")]
    public partial class Customer : DefaultEntity
    {
        private void inisilize()
        {
            Status = CustomerStatus.Active;
            Accounts = new HashSet<Account>();
            CustomerContacts = new HashSet<CustomerContact>();
            Orders = new HashSet<Order>();
            //Visits = new HashSet<Visit>();
            //ZoneDayCustomers = new HashSet<ZoneDayCustomer>();
        }
        public Customer()
        {
            inisilize();
        }

        /// <summary>
        /// create new instance from customer with address has province only
        /// </summary>
        /// <param name="name">name of the customer</param>
        /// <param name="customerTypeId">customer type id</param>
        /// <param name="provinceId">area id with discriminator equal province wil create address has this province</param>
        /// <param name="note">not of customer</param>
        public Customer(string name, Guid customerTypeId, string note)
        {
            inisilize();
            Name = name;
            CustomerTypeId = customerTypeId;
            Note = note;
        }

        public override string Name
        {
            get
            {
                return base.Name;
            }

            set
            {
                base.Name = value;
            }
        }

        public Guid CustomerTypeId { get; set; }
        public virtual CustomerType CustomerType { get; set; }

        //public Guid AddressId { get; set; }
        //public virtual Address Address { get; set; }

        public CustomerStatus Status { get; set; }

        [DefaultValue(0)]
        public short VIP { get; set; } // vip level

        public string Note { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<CustomerContact> CustomerContacts { get; set; }
        public ICollection<Order> Orders { get; set; }
        //public ICollection<Visit> Visits { get; set; }
        //public ICollection<ZoneDayCustomer> ZoneDayCustomers { get; set; }

    }
}
