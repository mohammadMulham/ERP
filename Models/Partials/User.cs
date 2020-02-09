using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public partial class User
    {
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
    }
}
