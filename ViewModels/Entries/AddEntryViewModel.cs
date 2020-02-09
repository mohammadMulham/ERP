using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ERPAPI.ViewModels.Entries
{
    public class AddEntryViewModel : IValidatableObject
    {
        public AddEntryViewModel()
        {
            Date = DateTimeOffset.UtcNow;
            Items = new HashSet<AddEntryItemViewModel>();
        }

        public long Number { get; set; }

        [Required]
        public DateTimeOffset Date { get; set; }

        [Required]
        public long CurrencyId { get; set; }
        public double CurrencyValue { get; set; }

        public long? CostCenterId { get; set; }

        public long? BranchId { get; set; }

        public string Note { get; set; }

        public ICollection<AddEntryItemViewModel> Items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Items.Count == 0)
            {
                yield return new ValidationResult("يجب اضافة نفدات القيد ", new[] { "Items" });
            }

            var isBalance = Items.Sum(x => x.Credit) != Items.Sum(y => y.Debit);
            if (isBalance)
            {
                yield return new ValidationResult("القيد غير متوازن ", new[] { "" });
            }

            #region  if store and costcenter in items are null

            // Currency loop on Currencies of items to get null value the master value
            foreach (var item in Items.Where(i => i.CurrencyId == null))
            {
                item.CurrencyId = CurrencyId;
            }

            // the same to costCenter
            if (CostCenterId.HasValue)
            {
                foreach (var item in Items.Where(i => i.CostCenterId == null))
                {
                    item.CostCenterId = CostCenterId.Value;
                }
            }

            #endregion
        }
    }
}
