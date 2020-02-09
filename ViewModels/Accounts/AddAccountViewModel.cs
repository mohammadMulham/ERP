using ERPAPI.Models;
using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.Accounts
{
    public class AddAccountViewModel : AddCardViewModel
    {
        public long CurrencyId { get; set; }

        public long? ParentAccountId { get; set; }
        public long? FinalAccountId { get; set; }

        public long? CustomerId { get; set; }
        public AccountType AccountType { get; set; }
        public AccountDirectionType DirectionType { get; set; }
    }
}
