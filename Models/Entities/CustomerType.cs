using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("CustomerTypes", Schema = "Marketing")]
    public partial class CustomerType : DefaultEntity
    {
        private void Initialize()
        {
            Customers = new HashSet<Customer>();
            //CustomerTypeGroupDetails = new HashSet<CustomerTypeGroupDetail>();
            //EmployeeUserCustomerTypes = new HashSet<EmployeeUserCustomerType>();
            DefaultWithCreateAccount = true;
            DefaultWithCreateContact = true;
            PreventRepeatCustomer = true;
            PreventRepeatCustomerContact = true;
            GenerateCustomerAccountNameAutomatically = true;
            AllowEditCustomerAccountName = true;
        }

        public CustomerType()
        {
            Initialize();
        }

        public CustomerType(string name, string note)
        {
            Initialize();
            Name = name;
            Note = note;
        }

        public CustomerType(string name, string note, bool defaultWithCreateAccount, Guid? defaultParentAccountId,
            bool defaultWithCreateContact, bool preventRepeateCustomerContact, bool preventRepeateCustomer,
            short? preventRepeateCustomerInAreaType)
        {
            Initialize();
            Name = name;
            Note = note;
            DefaultWithCreateAccount = defaultWithCreateAccount;
            DefaultParentAccountId = defaultParentAccountId;
            DefaultWithCreateContact = defaultWithCreateContact;
            PreventRepeatCustomerContact = preventRepeateCustomerContact;
            PreventRepeatCustomer = preventRepeateCustomer;
            PreventRepeatCustomerInAreaType = preventRepeateCustomerInAreaType;
        }

        public CustomerType(string name, string note, bool defaultWithCreateAccount, Guid? defaultParentAccountId, bool defaultWithCreateContact, bool preventRepeateCustomerContact, bool preventRepeateCustomer, short? preventRepeateCustomerInAreaType, bool generateCusotmerAccountNameAutomatically, bool allowEditCustomerAccountName, bool generateCusotmerAccountNameWithCustomerTypeName, bool generateCusotmerAccountNameWithProvinceName, bool generateCusotmerAccountNameWithCityName, bool generateCusotmerAccountNameWithNeighborhoodName, bool generateCusotmerAccountNameWithStreetName)
        {
            Name = name;
            Note = note;
            DefaultWithCreateAccount = defaultWithCreateAccount;
            DefaultParentAccountId = defaultParentAccountId;
            DefaultWithCreateContact = defaultWithCreateContact;
            PreventRepeatCustomerContact = preventRepeateCustomerContact;
            PreventRepeatCustomer = preventRepeateCustomer;
            PreventRepeatCustomerInAreaType = preventRepeateCustomerInAreaType;
            GenerateCustomerAccountNameAutomatically = generateCusotmerAccountNameAutomatically;
            AllowEditCustomerAccountName = allowEditCustomerAccountName;
            GenerateCustomerAccountNameWithCustomerTypeName = generateCusotmerAccountNameWithCustomerTypeName;
            GenerateCustomerAccountNameWithProvinceName = generateCusotmerAccountNameWithProvinceName;
            GenerateCustomerAccountNameWithCityName = generateCusotmerAccountNameWithCityName;
            GenerateCustomerAccountNameWithNeighborhoodName = generateCusotmerAccountNameWithNeighborhoodName;
            GenerateCustomerAccountNameWithStreetName = generateCusotmerAccountNameWithStreetName;
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

        public bool DefaultWithCreateAccount { get; set; }
        public Guid? DefaultParentAccountId { get; set; }
        public bool DefaultWithCreateContact { get; set; }
        public bool PreventRepeatCustomerContact { get; set; }

        public bool GenerateCustomerAccountNameAutomatically { get; set; }
        public bool AllowEditCustomerAccountName { get; set; }
        public bool GenerateCustomerAccountNameWithCustomerTypeName { get; set; }
        public bool GenerateCustomerAccountNameWithProvinceName { get; set; }
        public bool GenerateCustomerAccountNameWithCityName { get; set; }
        public bool GenerateCustomerAccountNameWithNeighborhoodName { get; set; }
        public bool GenerateCustomerAccountNameWithStreetName { get; set; }

        public Guid? DefaultCustomerPriceId { get; set; }

        public bool PreventRepeatCustomer { get; set; }
        public short? PreventRepeatCustomerInAreaType { get; set; }

        public string Note { get; set; }

        public ICollection<Customer> Customers { get; set; }
        //public virtual ICollection<CustomerTypeGroupDetail> CustomerTypeGroupDetails { get; set; }
        //public ICollection<EmployeeUserCustomerType> EmployeeUserCustomerTypes { get; set; }
    }
}
