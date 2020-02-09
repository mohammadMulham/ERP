using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("Accounts", Schema = "Accounting")]
    public partial class Account : Card
    {
        private void Initialize()
        {
            Accounts = new HashSet<Account>();
            FinalAccounts = new HashSet<Account>();
            AccountBills = new HashSet<Bill>();
            CustomerBills = new HashSet<Bill>();
            PayEntries = new HashSet<PayEntry>();
            EntryItems = new HashSet<EntryItem>();
            BillEntryItems = new HashSet<BillEntryItem>();
            AccountBalances = new HashSet<AccountBalance>();
        }
        
        public Account()
        {
            Initialize();
        }

        public Account(string name, string code, Guid? parentId, Guid? finalAccountId, AccountType accountType, AccountDirectionType directionType,
            Guid currencyId, Guid? customerId = null, string note = "")
        {
            Initialize();
            Name = name;
            Code = code;
            ParentId = parentId;
            FinalAccountId = finalAccountId;
            AccountType = accountType;
            DirectionType = directionType;
            CurrencyId = currencyId;
            CustomerId = customerId;
            Note = note;
        }

        public AccountDirectionType DirectionType { get; set; }

        public Guid? ParentId { get; set; }
        public virtual Account ParentAccount { get; set; }

        public Guid? FinalAccountId { get; set; }
        public virtual Account FinalAccount { get; set; }

        public Guid CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }

        public Guid? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public AccountType AccountType { get; set; }

        public string Note { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Account> FinalAccounts { get; set; }

        public virtual ICollection<Bill> AccountBills { get; set; }
        public virtual ICollection<Bill> CustomerBills { get; set; }

        public virtual ICollection<PayEntry> PayEntries { get; set; }
        public virtual ICollection<EntryItem> EntryItems { get; set; }
        public virtual ICollection<BillEntryItem> BillEntryItems { get; set; }
        public virtual ICollection<AccountBalance> AccountBalances { get; set; }
    }
}
