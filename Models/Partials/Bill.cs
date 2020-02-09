using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public partial class Bill
    {
        //private string billSellersCodeFullNames;
        // [NotMapped]
        //public string BillSellersCodeFullNames
        //{
        //    get
        //    {
        //        billSellersCodeFullNames = "";
        //        if (BillSellers != null && BillSellers.Count > 0)
        //        {
        //            foreach (var billSeller in BillSellers)
        //            {
        //                if (BillSellers.Last() == billSeller)
        //                {
        //                    billSellersCodeFullNames += billSeller.Employee != null ?
        //                        billSeller.Employee.CodeFullName : "";
        //                }
        //                else
        //                {
        //                    billSellersCodeFullNames += string.Format("{0}, ", billSeller.Employee != null
        //                        ? billSeller.Employee.CodeFullName : "");
        //                }
        //            }
        //        }
        //        return billSellersCodeFullNames;
        //    }
        //}

        private long itemsCount;

        [NotMapped]
        public long ItemsCount
        {
            get
            {
                if (BillItems != null)
                {
                    itemsCount = BillItems.LongCount();
                }
                return itemsCount;
            }
        }

        /// <summary>
        /// sum price of all items after extra and discount
        /// </summary>
        [NotMapped]
        public double TotalItems
        {
            get
            {
                return BillItems.Sum(e => e.Total);
            }
        }

        [NotMapped]
        public double TotalItemsExtra
        {
            get
            {
                return BillItems.Sum(e => e.Extra) + Extra;
            }
        }

        [NotMapped]
        public double TotalItemsDisc
        {
            get
            {
                return BillItems.Sum(e => e.Disc) + Disc;
            }
        }
    }
}
