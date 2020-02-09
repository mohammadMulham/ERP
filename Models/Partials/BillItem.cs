using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public partial class BillItem
    {
        /// <summary>
        /// total price after extra and discount 
        /// </summary>
        [NotMapped]
        public double Total
        {
            get
            {
                return Price * Quantity;
            }
        }

    }
}