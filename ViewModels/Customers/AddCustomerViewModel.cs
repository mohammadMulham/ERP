using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Models;
using ERPAPI.ViewModels.DefaultEntity;
using System.ComponentModel.DataAnnotations;

namespace ERPAPI.ViewModels.Customers
{
    public class AddCustomerViewModel : AddDefaultEntityViewModel
    {
        [StringLength(128, MinimumLength = 2)]
        public string ResponsibleName { get; set; }

        [StringLength(128, MinimumLength = 2)]
        public string ResponsibleAdjective { get; set; }
        public long CustomerTypeId { get; set; }
        public string Note { get; set; }
    }
}
