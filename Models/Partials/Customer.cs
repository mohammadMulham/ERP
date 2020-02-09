using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public partial class Customer
    {
        private string customerContactsWithPrefix;

        [NotMapped]
        public string CustomerContactsWithPrefix
        {
            get
            {
                customerContactsWithPrefix = "";
                foreach (var contact in CustomerContacts)
                {
                    if (contact == CustomerContacts.Last())
                    {
                        customerContactsWithPrefix += string.Format("{0}", contact.ContactWithPrefix);
                    }
                    else
                    {
                        customerContactsWithPrefix += string.Format("{0}, ", contact.ContactWithPrefix);
                    }
                }
                return customerContactsWithPrefix;
            }
        }

        private string accountsNames;

        [NotMapped]
        public string AccountsNames
        {
            get
            {
                accountsNames = "";
                foreach (var account in Accounts)
                {
                    if (account.Code == Accounts.Last().Code)
                    {
                        accountsNames += string.Format("{0}", account.Name);
                    }
                    else
                    {
                        accountsNames += string.Format("{0}, ", account.Name);
                    }
                }
                return accountsNames;
            }
        }


        private string accountsCodeNames;

        [NotMapped]
        public string AccountsCodeNames
        {
            get
            {
                accountsCodeNames = "";
                foreach (var account in Accounts)
                {
                    if (account.Code == Accounts.Last().Code)
                    {
                        accountsCodeNames += string.Format("{0}", account.CodeName);
                    }
                    else
                    {
                        accountsCodeNames += string.Format("{0}, ", account.CodeName);
                    }
                }
                return accountsCodeNames;
            }
        }


        // private string customerTypeGroupsNames;

        //[NotMapped]
        //public string CustomerTypeGroupsNames
        //{
        //    get
        //    {
        //        customerTypeGroupsNames = CustomerType.CustomerTypeGroupNames;
        //        return customerTypeGroupsNames;
        //    }
        //}

        //private string customerTypeGroupsCodeNames;

        //[NotMapped]
        //public string CustomerTypeGroupsCodeNames
        //{
        //    get
        //    {
        //        customerTypeGroupsCodeNames = CustomerType.CustomerTypeGroupCodeNames;
        //        return customerTypeGroupsCodeNames;
        //    }
        //}

        [NotMapped]
        public string NumberFullName
        {
            get
            {
                numberFullName = string.Format("{0}-{1}", Number, FullName);
                return numberFullName;
            }
        }

        private string numberFullName;
        
        private string fullName;

        [NotMapped]
        public string FullName
        {
            get
            {
                fullName = string.Format("{0} {1}", CustomerType?.Name, Name);
                return fullName;
            }
        }
    }
}
