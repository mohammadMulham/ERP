using ERPAPI.Data;
using ERPAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Repositories
{
    public abstract class DefaultEntityRepository<T> : BaseEntityRepository<T>, IDefaultEntityRepository<T>
        where T : DefaultEntity
    {

        public DefaultEntityRepository(ERPContext context) : base(context)
        {

        }

        public override IQueryable<T> GetAllNoTracking()
        {
            return base.GetAllNoTracking().OrderByDescending(e => e.CreatedDateTime);
        }

        public virtual bool IsExistName(string Name)
        {
            var count = base.GetAll().LongCount(a => a.Name == Name);
            return count > 0;
        }

        public virtual bool IsExistName(Guid id, string Name)
        {
            var count = base.GetAll().LongCount(a => a.Name == Name && a.Id != id);
            return count > 0;
        }

        public virtual async Task<bool> IsExistNameAsync(string Name)
        {
            var count = await base.GetAll().LongCountAsync(a => a.Name == Name);
            return count > 0;
        }

        public virtual async Task<bool> IsExistNameAsync(Guid id, string Name)
        {
            var count = await base.GetAll().LongCountAsync(a => a.Name == Name && a.Id != id);
            return count > 0;
        }
    }
}
