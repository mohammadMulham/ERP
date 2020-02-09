using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.Sellers
{
    public class SellerViewModel : CardViewModel
    {
        public long? ParentSellerId { get; set; }
        public string ParentSellerName { get; set; }
        public string ParentSellerCode { get; set; }
    }
}
