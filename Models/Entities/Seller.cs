using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{

    [Table("Sellers", Schema = "Accounting")]
    public class Seller : Card
    {
        public Seller()
        {
            Initialize();
        }

        public Seller(string code, string name, string note)
        {
            Initialize();
            Code = code;
            Name = name;
            Note = note;
        }

        private void Initialize()
        {
            Sellers = new HashSet<Seller>();
            BillSellers= new HashSet<BillSeller>();
        }

        public Guid? ParentId { get; set; }
        public virtual Seller ParentSeller { get; set; }
        public string Note { get; set; }
        public virtual ICollection<Seller> Sellers { get; set; }
        public virtual ICollection<BillSeller> BillSellers { get; set; }
    }
}
