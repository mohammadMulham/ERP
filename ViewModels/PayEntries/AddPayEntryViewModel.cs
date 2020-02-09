using ERPAPI.Repositories;
using ERPAPI.ViewModels.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.PayEntries
{
    public class AddPayEntryViewModel : BaseEntityViewModel , IValidatableObject
    {
        
        public DateTimeOffset Date { get; set; }

        [Required]
        public long? PayAccountId { get; set; }
        [Required]
        public long PayTypeId { get; set; }

        [Required]
        public long CurrencyId { get; set; }
        public double CurrencyValue { get; set; }

        public long? CostCenterId { get; set; }

        public long? BranchId { get; set; }
        public string Note { get; set; }

        public ICollection<AddPayEntryItemViewModel> Items { get; set; } = new HashSet<AddPayEntryItemViewModel>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Items.Count == 0)
            {
                yield return new ValidationResult("لايوجد تفاصيل للسند", new[] { "Items" });
            }
            if(PayTypeId==0)
            {
                yield return new ValidationResult("يجب تحديد نوع السند");
            }
        }
    }
}
