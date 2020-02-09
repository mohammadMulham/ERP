using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public partial class ItemUnitPrice
    {
        private string name;

        [NotMapped]
        public string Name
        {
            get
            {
                name = string.Format("{0}: {1}", Price.Name, Value);
                return name;
            }
        }
    }
}
