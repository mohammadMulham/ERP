using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("Logs", Schema = "Management")]
    public partial class Log : BaseClass
    {
        private void Initilize()
        {

        }

        public Log()
        {
            Initilize();
        }

        public TableType TableType { get; set; }
        public Guid TableId { get; set; }
        public string Object { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public Guid CreatedUserId { get; set; }
        public DateTimeOffset? ModifiedDateTime { get; set; }
        public Guid? ModifiedUserId { get; set; }
        public DateTimeOffset? DeletedDateTime { get; set; }
        public Guid? DeletedUserId { get; set; }
        public EntityStatus EntityStatus { get; set; }
    }
}
