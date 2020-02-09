using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ERPAPI.Repositories
{
    public interface IGenericRepository<T> : IDisposable, IPeriod
    {
        IQueryable<T> NativeGetAll();
        IQueryable<T> GetAll();
        IQueryable<T> NativeGetAllNoTracking();
        IQueryable<T> GetAllNoTracking();
        T Find(params object[] keyValues);
        Task<T> FindAsync(params object[] keyValues);
        int Add(T entity, bool autoSaveAll = true);
        Task<int> AddAsync(T entity, bool autoSaveAll = true);
        int Edit(T entity, bool autoSaveAll = true, byte[] rowVersion = null);
        Task<int> EditAsync(T entity, bool autoSaveAll = true, byte[] rowVersion = null);
        int Delete(T entity, bool autoSaveAll = true);
        long Count();
        Task<long> CountAsync();
        long Count(Expression<Func<T, bool>> predicate);
        Task<long> CountAsync(Expression<Func<T, bool>> predicate);
        Task<int> DeleteAsync(T entity, bool autoSaveAll = true);
        void SetLoggedInUserId(Guid userId);
        void SetLoggedInUserName(string userName);
        void SetIsAdmin(bool IsAdmin);
        int SaveAll();
        Task<int> SaveAllAsync();
        void SetCreateProps(T entity);
    }
}
