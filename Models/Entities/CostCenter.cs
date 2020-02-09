using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("CostCenters", Schema = "Accounting")]
    public partial class CostCenter : Card
    {
        public CostCenter()
        {
            Initialize();
        }

        public CostCenter(string code, string name, string note)
        {
            Initialize();
            Code = code;
            Name = name;
            Note = note;
        }

        private void Initialize()
        {
            CostCenters = new HashSet<CostCenter>();
            EntryItems = new HashSet<EntryItem>();
            Bills = new HashSet<Bill>();
            BillItems = new HashSet<BillItem>();
        }

        public Guid? ParentId { get; set; }
        public virtual CostCenter ParentCostCenter { get; set; }
        public string Note { get; set; }
        public ICollection<CostCenter> CostCenters { get; set; }
        public ICollection<EntryItem> EntryItems { get; set; }
        public ICollection<Bill> Bills { get; set; }
        public ICollection<BillItem> BillItems { get; set; }
    }
}
