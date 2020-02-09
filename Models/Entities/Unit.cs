using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("Units", Schema = "Production")]
    public class Unit : DefaultEntity
    {
        public Unit()
        {
            Initialize();
        }

        public Unit(string name)
        {
            Initialize();
            Name = name;
        }

        private void Initialize()
        {
            ItemUnits = new HashSet<ItemUnit>();
        }

        public virtual ICollection<ItemUnit> ItemUnits { get; set; }
    }
}
