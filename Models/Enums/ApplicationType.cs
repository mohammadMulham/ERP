using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public enum ApplicationType
    {
        JavaScript = 0, // my application, can only sned clientId
        NativeConfidential = 1 // other applications and shuld send secret and clientId
    };

}
