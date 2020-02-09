using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Repositories
{
    public interface ICardRepository<T> : IDefaultEntityRepository<T>
    {
        bool IsExisCode(string Code);
        bool IsExisCode(Guid id, string Code);
        Task<bool> IsExistCodeAsync(string Code);
        Task<bool> IsExistCodeAsync(Guid id, string Code);
    }
}
