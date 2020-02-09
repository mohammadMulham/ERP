using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Repositories
{
    public interface IDefaultEntityRepository<T> : IBaseEntityRepository<T>
    {
        bool IsExistName(string Name);
        bool IsExistName(Guid id, string Name);
        Task<bool> IsExistNameAsync(string Name);
        Task<bool> IsExistNameAsync(Guid id, string Name);
    }
}
