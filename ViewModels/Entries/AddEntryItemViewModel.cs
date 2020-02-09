using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.Entries
{
    public class AddEntryItemViewModel : IValidatableObject
    {
        public DateTimeOffset? Date { get; set; }

        [Required]
        public long AccountId { get; set; }

        public long? CurrencyId { get; set; }
        public double? CurrencyValue { get; set; }

        public long? CostCenterId { get; set; }

        public double Debit { get; set; } // مدين
        public double Credit { get; set; } // دائن

        public string Note { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var isDuplicated = Debit == Credit;
            if (isDuplicated)
                yield return new ValidationResult("خطأ في كتابة قلم في القيد", new[] { "Debit", "Credit" });
        }
    }
}
