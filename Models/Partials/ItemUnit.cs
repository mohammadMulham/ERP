using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public partial class ItemUnit
    {
        private string name;

        [NotMapped]
        public string Name
        {
            get
            {
                name = Unit.Name;
                return name;
            }
        }

        private string itemUnitName;
        [NotMapped]
        public string ItemUnitName
        {
            get
            {
                if (Item != null && Unit != null)
                {
                    itemUnitName = string.Format("{0} ({1})", Item.Name, Unit.Name);
                }
                return itemUnitName;
            }
        }

        private string itemUnitCodeName;
        [NotMapped]
        public string ItemUnitCodeName
        {
            get
            {
                if (Item != null && Unit != null)
                {
                    itemUnitCodeName = string.Format("{0} ({1})", Item.CodeName, Unit.Name);
                }
                return itemUnitCodeName;
            }
        }
    }
}
