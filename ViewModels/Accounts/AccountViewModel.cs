using ERPAPI.Models;
using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.Accounts
{
    public class AccountViewModel : CardViewModel
    {
        public long? ParentAccountId { get; set; }
        public string ParentAccountName { get; set; }
        public string ParentAccountCode { get; set; }

        public long? FinalAccountId { get; set; }
        public string FinalAccountName { get; set; }
        public string FinalAccountCode { get; set; }

        public long CurrencyId { get; set; }
        public string CurrencyName { get; set; }

        public AccountType AccountType { get; set; }
        public AccountDirectionType DirectionType { get; set; }

        //public long? CustomerId { get; set; }
        //public string CustomerName { get; set; }
    }
}
