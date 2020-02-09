using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public partial class User : IdentityUser<Guid>
    {
        public User()
        {
            Initialize();
        }

        public User(string userName) : base(userName)
        {
            Initialize();
        }

        private void Initialize()
        {
            Id = Guid.NewGuid();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}
