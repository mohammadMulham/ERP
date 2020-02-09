using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Table("FinancialPeriods", Schema = "Management")]
    public class FinancialPeriod : DefaultEntity
    {
        public FinancialPeriod()
        {
            Initialize();
        }

        public FinancialPeriod(string name, DateTimeOffset startDate, DateTimeOffset endtDate)
        {
            Initialize();
            Name = name;
            StartDate = startDate;
            EndtDate = endtDate;
            Year = StartDate.Year;
        }
        
        public FinancialPeriod(string name, Guid companyId, DateTimeOffset startDate, DateTimeOffset endtDate)
        {
            Initialize();
            Name = name;
            CompanyId = companyId;
            StartDate = startDate;
            EndtDate = endtDate;
            Year = StartDate.Year;
        }

        private void Initialize()
        {
            Bills = new HashSet<Bill>();
            Entries = new HashSet<Entry>();
            StoreItems = new HashSet<StoreItem>();
            StoreItemUnits = new HashSet<StoreItemUnit>();
            AccountBalances = new HashSet<AccountBalance>();
            Order = 1;
        }

        public bool CheckIfDateInPeriod(DateTimeOffset date)
        {
            return StartDate <= date && date <= EndtDate;
        }

        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public int Order { get; set; }
        public int Year { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndtDate { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Entry> Entries { get; set; }
        public virtual ICollection<StoreItem> StoreItems { get; set; }
        public virtual ICollection<StoreItemUnit> StoreItemUnits { get; set; }
        public virtual ICollection<AccountBalance> AccountBalances { get; set; }
    }
}
