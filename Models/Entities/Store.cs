using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("Stores", Schema = "Production")]
    public class Store : Card
    {
        public Store()
        {
            Initialize();
        }

        public Store(string name, string code, Guid? accountId, Guid? costCenterId, string note)
        {
            Initialize();
            Name = name;
            Code = code;
            AccountId = accountId;
            CostCenterId = costCenterId;
            Note = note;
        }

        private void Initialize()
        {
            Stores = new HashSet<Store>();
            StoreItems = new HashSet<StoreItem>();
            Bills = new HashSet<Bill>();
            BillItems = new HashSet<BillItem>();
            StoreItemUnits = new HashSet<StoreItemUnit>();
        }

        public Guid? ParentId { get; set; }
        public virtual Store ParentStore { get; set; }
        public Guid? AccountId { get; set; }
        public virtual Account Account { get; set; }
        public Guid? CostCenterId { get; set; }
        public virtual CostCenter CostCenter { get; set; }
        public string Note { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
        public virtual ICollection<StoreItem> StoreItems { get; set; }
        public virtual ICollection<StoreItemUnit> StoreItemUnits { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<BillItem> BillItems { get; set; }

    }
}
