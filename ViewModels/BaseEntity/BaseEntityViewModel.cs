using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels.BaseEntity
{
    public class BaseEntityViewModel
    {
        public long Id { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
    }
}
