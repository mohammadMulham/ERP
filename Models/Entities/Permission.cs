using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("Permissions", Schema = "CMS")]
    public partial class Permission : BaseClass
    {
        private void Initilize()
        {
            PermissionGroupPermissons = new HashSet<PermissionGroupPermisson>();
        }

        public Permission()
        {
            Initilize();
        }

        public TableType TableType { get; set; }
        public Guid TableId { get; set; }
        public Guid UserId { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }

        public virtual ICollection<PermissionGroupPermisson> PermissionGroupPermissons { get; set; }
    }
}
