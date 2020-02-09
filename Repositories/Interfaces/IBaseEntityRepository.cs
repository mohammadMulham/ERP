using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ERPAPI.Repositories
{
    public interface IBaseEntityRepository<T> : IGenericRepository<T>
    {
        T Get(long number);
        Task<T> GetAsync(long number);
        Task<bool> CheckIfNotExistAsync(long number);
        bool CheckIfNotExist(long number);
        int SetDeleted(T entity, bool autoSaveAll = true);
        Task<int> SetDeletedAsync(T entity, bool autoSaveAll = true);
        int SetUnDeleted(T entity, bool autoSaveAll = true);
        Task<int> SetUnDeletedAsync(T entity, bool autoSaveAll = true);
        T GetEntity(long number);
        Task<T> GetEntityAsync(long number);
    }
}
