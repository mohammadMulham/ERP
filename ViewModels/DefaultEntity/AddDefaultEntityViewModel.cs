using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.DefaultEntity
{
    public class AddDefaultEntityViewModel
    {
        [Required]
        [StringLength(128, MinimumLength = 2)]
        public string Name { get; set; }
    }
}
