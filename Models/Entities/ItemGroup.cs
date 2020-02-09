using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("ItemGroups", Schema = "Production")]
    public partial class ItemGroup : Card
    {
        public ItemGroup()
        {
            Initialize();
        }

        public ItemGroup(string name, string code, string note)
        {
            Initialize();
            Name = name;
            Code = code;
            Note = note;
        }

        private void Initialize()
        {
            Groups = new HashSet<ItemGroup>();
            Items = new HashSet<Item>();
        }

        public Guid? ParentId { get; set; }
        public string Note { get; set; }
        public virtual ItemGroup ParentItemGroup { get; set; }
        public virtual ICollection<ItemGroup> Groups { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
