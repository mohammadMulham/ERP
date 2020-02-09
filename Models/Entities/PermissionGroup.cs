using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("PermissionGroups", Schema = "CMS")]
    public partial class PermissionGroup : DefaultEntity
    {
        private void Initilize()
        {
            PermissionGroupRoles = new HashSet<PermissionGroupRole>();
            PermissionGroupPermissons = new HashSet<PermissionGroupPermisson>();
        }

        public PermissionGroup()
        {
            Initilize();
        }

        public virtual ICollection<PermissionGroupRole> PermissionGroupRoles { get; set; }
        public virtual ICollection<PermissionGroupPermisson> PermissionGroupPermissons { get; set; }

    }
}
