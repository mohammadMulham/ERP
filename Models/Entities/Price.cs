using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("Prices", Schema = "Production")]
    public class Price : DefaultEntity
    {
        public Price()
        {
            Initialize();
        }

        public Price(string name)
        {
            Initialize();
            Name = name;
        }

        private void Initialize()
        {
            ItemUnitPrices = new HashSet<ItemUnitPrice>();
        }

        public ICollection<ItemUnitPrice> ItemUnitPrices { get; set; }
    }
}
