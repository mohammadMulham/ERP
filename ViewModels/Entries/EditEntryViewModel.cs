using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.Entries
{
    public class EditEntryViewModel : IValidatableObject
    {
        public EditEntryViewModel()
        {
            EntryItems = new HashSet<EditEntryItemViewModel>();
        }
        public long Id { get; set; }
        [Required]
        public DateTimeOffset Date { get; set; }
        [Required]
        public long CurrencyId { get; set; }
        public double CurrencyValue { get; set; }

        public long? CostCenterId { get; set; }

        public long? ParentNumber { get; set; }

        public long? BranchId { get; set; }

        public string Note { get; set; }

        public virtual ICollection<EditEntryItemViewModel> EntryItems { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EntryItems.Count == 0)
            {
                yield return new ValidationResult("يجب اضافة نفدات القيد ", new[] { "Items" });
            }

            var isbalance = EntryItems.Sum(x => x.Credit) != EntryItems.Sum(y => y.Debit);
            if (isbalance)
                yield return new ValidationResult("القيد غير متوازن ", new[] { "entry non balanced" });
        }
    }
}
