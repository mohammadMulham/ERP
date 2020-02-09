using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public partial class Item
    {
        private ItemUnit defaultitemUnit;

        [NotMapped]
        public ItemUnit DefaultUnit
        {
            get
            {
                defaultitemUnit = ItemUnits.FirstOrDefault(i => i.IsDefault);
                return defaultitemUnit;
            }
        }
    }
}
