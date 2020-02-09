using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Repositories
{
    public interface IPeriod
    {
        Guid PerdioId { get; set; }
        void SetPeriodId(Guid perdioId);
        Guid GetPeriodId();
    }
}
