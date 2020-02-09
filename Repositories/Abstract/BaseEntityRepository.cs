using ERPAPI.Data;
using ERPAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ERPAPI.Repositories
{
    public abstract class BaseEntityRepository<T> : GenericRepository<T>, IBaseEntityRepository<T>
            where T : BaseEntity
    {
        public BaseEntityRepository(ERPContext context) : base(context)
        {

        }

        public override IQueryable<T> NativeGetAll()
        {
            return base.NativeGetAll().Where(t => t.EntityStatus == EntityStatus.Active);
        }

        public override int Add(T entity, bool autoSaveAll = true)
        {
            SetCreateProps(entity);
            return base.Add(entity, autoSaveAll);
        }

        public override Task<int> AddAsync(T entity, bool autoSaveAll = true)
        {
            SetCreateProps(entity);
            return base.AddAsync(entity, autoSaveAll);
        }

        public virtual int SetUnDeleted(T entity, bool autoSaveAll = true)
        {
            entity.EntityStatus = EntityStatus.Active;
            int rowsEffected = Edit(entity, autoSaveAll);
            return rowsEffected;
        }

        public virtual async Task<int> SetUnDeletedAsync(T entity, bool autoSaveAll = true)
        {
            entity.EntityStatus = EntityStatus.Active;
            int rowsEffected = await EditAsync(entity, autoSaveAll);
            return rowsEffected;
        }

        public virtual int SetDeleted(T entity, bool autoSaveAll = true)
        {
            entity.EntityStatus = EntityStatus.Deleted;
            int rowsEffected = Edit(entity, autoSaveAll);
            return rowsEffected;
        }

        public virtual async Task<int> SetDeletedAsync(T entity, bool autoSaveAll = true)
        {
            entity.EntityStatus = EntityStatus.Deleted;
            int rowsEffected = await EditAsync(entity, autoSaveAll);
            return rowsEffected;
        }
        
        public virtual T Get(long number)
        {
            var entity = GetAll().FirstOrDefault(e => e.Number == number);
            return entity;
        }

        public virtual async Task<T> GetAsync(long number)
        {
            var entity = await GetAll().FirstOrDefaultAsync(e => e.Number == number);
            return entity;
        }

        public virtual bool CheckIfNotExist(long number)
        {
            var count = GetAll().Count(e => e.Number == number);
            return count > 0;
        }

        public virtual T GetEntity(long number)
        {
            var entity = base.NativeGetAll().FirstOrDefault(e => e.Number == number);
            return entity;
        }

        public virtual async Task<T> GetEntityAsync(long number)
        {
            var entity = await base.NativeGetAll().FirstOrDefaultAsync(e => e.Number == number);
            return entity;
        }

        public virtual async Task<bool> CheckIfNotExistAsync(long number)
        {
            var count = await GetAll().CountAsync(e => e.Number == number);
            return count > 0;
        }
    }
}
