using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class Role : IdentityRole<Guid>
    {
        public Role() : base()
        {
            Initialize();
        }

        public Role(string name) : base(name)
        {
            Initialize();
        }

        private void Initialize()
        {
            PermissionGroupRoles = new HashSet<PermissionGroupRole>();
        }

        public virtual ICollection<PermissionGroupRole> PermissionGroupRoles { get; set; }
    }
}
