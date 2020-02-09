using ERPAPI.ViewModels.DefaultEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.Card
{
    public class CardViewModel : DefaultEntityViewModel
    {
        public string Code { get; set; }
        public string Note { get; set; }
        public string CodeName { get; set; }
    }
}
