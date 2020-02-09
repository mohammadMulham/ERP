using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.Stores
{
    public class AddStoreViewModel : AddCardViewModel
    {
        public long? ParentId { get; set; }
        public long? AccountId { get; set; }
        public long? CostCenterId { get; set; }
    }
}
