using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public partial class Account
    {
        public string GetNodeType()
        {
            if (AccountType == AccountType.Final)
            {
                return "final";
            }
            else
            {
                if (CustomerId != null)
                {
                    return "customer";
                }
            }
            return "parent";
        }
    }
}
