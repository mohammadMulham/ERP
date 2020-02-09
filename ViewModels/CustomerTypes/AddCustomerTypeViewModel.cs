using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Models;
using ERPAPI.ViewModels.DefaultEntity;

namespace ERPAPI.ViewModels.CustomerTypes
{
    public class AddCustomerTypeViewModel : AddDefaultEntityViewModel
    {
        public string Note { get; set; }
    }
}
