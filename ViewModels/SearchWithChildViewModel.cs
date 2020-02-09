using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels
{
    public class SearchWithChildViewModel : SearchViewModel
    {
        public bool ShowChild { get; set; }
        public SearchViewModel Child { get; set; }
    }
}
