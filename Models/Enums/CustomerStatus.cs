using ERPAPI.Attributes;
using ERPAPI.Resources.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [EnumResouce(typeof(CustomerStatusResource))]
    public enum CustomerStatus
    {
        Closed,
        Active,
        TemporarilyClosed
    }
}
