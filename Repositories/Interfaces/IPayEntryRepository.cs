using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Repositories
{
    public interface IPayEntryRepository : IBaseEntityRepository<PayEntry>
    {
        IQueryable<PayEntry> GetAllByTypeId(Guid typeId);
        Task<PayEntry> GetAsync(Guid payTypeId, long number);
        PayEntry Get(Guid payTypeId, long number);
        long GetNextNumber(Guid payTypeId);

        Task<long> GetNextNumberAsync(Guid payTypeId);
        void LoadReferences(PayEntry payEntry);
    }
}
