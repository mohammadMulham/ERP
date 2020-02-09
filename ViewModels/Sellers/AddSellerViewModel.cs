using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.Sellers
{
    public class AddSellerViewModel : AddCardViewModel
    {
        public long? ParentSellerId { get; set; }
    }
}
