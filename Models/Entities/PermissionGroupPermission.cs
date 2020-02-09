using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("PermissionGroupPermissions", Schema = "CMS")]
    public partial class PermissionGroupPermisson : BaseClass
    {
        public Guid PermissionGroupId { get; set; }
        public virtual PermissionGroup PermissionGroup { get; set; }

        public Guid PermissionId { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
