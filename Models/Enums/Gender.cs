using ERPAPI.Attributes;
using ERPAPI.Resources.Gender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [EnumResouce(typeof(GenderResource))]
    public enum Gender
    {
        Male,
        Female,
        Unspecified
    }
}
