using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Repositories
{
    public interface IBillRepository : IBaseEntityRepository<Bill>
    {
        IQueryable<Bill> GetAllByTypeId(Guid typeId);

        long GetNextNumber(Guid billTypeId);
        Task<long> GetNextNumberAsync(Guid billTypeId);

        bool CheckIfExist(long number, Guid billTypeId);

        Task<Bill> GetAsync(Guid billTypeId, long number);

        Bill Get(Guid billTypeId, long number);

        Task<bool> CheckIfExistAsync(long number, Guid billTypeId);
    }
}
