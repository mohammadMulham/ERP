using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.Configs
{
    public class InitConfigViewModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(128)]
        public string CompanyName { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndtDate { get; set; }

        public int? CurrencyId { get; set; }

        public int AccoutGuideId { get; set; }
    }
}
