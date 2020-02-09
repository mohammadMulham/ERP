using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("PermissionGroupRoles", Schema = "CMS")]
    public partial class PermissionGroupRole : BaseClass
    {
        public Guid PermissionGroupId { get; set; }
        public virtual PermissionGroup PermissionGroup { get; set; }

        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
