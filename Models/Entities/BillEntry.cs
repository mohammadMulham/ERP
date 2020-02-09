using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("BillEntries", Schema = "Accounting")]
    public class BillEntry : BaseClass
    {
        public BillEntry()
        {

        }

        public BillEntry(Entry entry)
        {
            Entry = entry;
        }

        public Guid EntryId { get; set; }
        public virtual Entry Entry { get; set; }

        public Guid BillId { get; set; }
        public Bill Bill { get; set; }
    }
}
