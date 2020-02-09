using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ERPAPI.ViewModels.Bills
{
    public class AddBillViewModel : IValidatableObject
    {
        public AddBillViewModel()
        {
            Date = DateTimeOffset.UtcNow;
            Items = new HashSet<AddBillItemViewModel>();
            Pays = new HashSet<AddPayViewModel>();
        }

        [Required]
        public long Number { get; set; }
        [Required]
        public long BillTypeId { get; set; }
        public long CurrencyId { get; set; }
        [Range(0.000000000000000001, double.MaxValue)]
        public double? CurrencyValue { get; set; }
        [Required]
        public DateTimeOffset Date { get; set; }
        [Required]
        public long AccountId { get; set; }
        public long? CustomerAccountId { get; set; }
        public string CustomerName { get; set; }
        public long? StoreId { get; set; }
        public long? CostCenterId { get; set; }
        public long? BranchId { get; set; }
        [Required]
        public PaysType PayType { get; set; }
        public double Extra { get; set; }
        public double Disc { get; set; }
        public double TotalPaid { get; set; }
        public string Note { get; set; }
        public ICollection<AddBillItemViewModel> Items { get; set; }
        public ICollection<AddPayViewModel> Pays { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Items.Count == 0)
            {
                yield return new ValidationResult("لازم تضيف مواد :D ", new[] { "Items" });
            }
            if (PayType != PaysType.Cash && !CustomerAccountId.HasValue)
            {
                yield return new ValidationResult("يحب تحديد الزبون في الفواتير الاجلة ", new[] { "CustomerAccountId" });
            }
            if (!StoreId.HasValue && Items.Count(i => i.StoreId == null) > 0)
            {
                yield return new ValidationResult("you have to set bill store if there is any item without store", new[] { "StoreId" });
            }

            // check if master store not found and there items stores not found
            if (!StoreId.HasValue && Items.Count(i => i.StoreId == null) > 0)
            {
                yield return new ValidationResult("you have to set bill store if there is any item without store", new[] { "StoreId" });
            }

            #region  if store and costcenter in items are null

            // if store has value => loop on stores of items to get null value the master value
            if (StoreId.HasValue)
            {
                foreach (var item in Items.Where(i => i.StoreId == null))
                {
                    item.StoreId = StoreId.Value;
                }
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
