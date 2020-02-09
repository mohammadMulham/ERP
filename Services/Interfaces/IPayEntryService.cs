using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Services
{
    public interface IPayEntryService
    {
        long GetNextNumber(long payTypeNumber);

        Task<long> GetNextNumberAsync(long payTypeNumber);
    }
}
