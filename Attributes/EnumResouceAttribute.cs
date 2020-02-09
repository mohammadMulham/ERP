using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Attributes
{
    public class EnumResouceAttribute : Attribute
    {
        public Type ResouceType { get; private set; }
        public EnumResouceAttribute(Type resouceType)
        {
            ResouceType = resouceType;
        }
    }
}
