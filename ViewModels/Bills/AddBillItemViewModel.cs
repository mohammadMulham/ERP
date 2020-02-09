using ERPAPI.ViewModels.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ERPAPI.ViewModels.Bills
{
    public class AddBillItemViewModel
    {
        [Required]
        public long ItemId { get; set; }
        [Required]
        public long UnitId { get; set; }
        [Required]
        [Range(1, double.MaxValue)]
        public double Quantity { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "يجب تحديد السعر")]
        public double Price { get; set; }
        public double Extra { get; set; }
        public double Disc { get; set; }

        public long? StoreId { get; set; }
        public long? CostCenterId { get; set; }
        public string Note { get; set; }
    }
}
