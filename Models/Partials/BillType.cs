using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Helpers;

namespace ERPAPI.Models
{
    public partial class BillType
    {
        public int GetTypeValue()
        {
            var value = int.Parse(EnumHelper.GetDescription(Type));
            return value;
        }
    }
}
