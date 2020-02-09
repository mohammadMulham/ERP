using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("Companies", Schema = "Management")]
    public class Company : DefaultEntity
    {
        public Company()
        {
            Initialize();
        }

        public Company(string name)
        {
            Initialize();
            Name = name;
        }

        private void Initialize()
        {
            FinancialPeriods = new HashSet<FinancialPeriod>();
        }

        public virtual ICollection<FinancialPeriod> FinancialPeriods { get; set; }
    }
}
