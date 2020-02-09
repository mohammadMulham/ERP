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
    public abstract class GenericRepository<T> : IGenericRepository<T>
            where T : BaseClass
    {
        private ERPContext _context;

        public ERPContext Context
        {
            get { return _context; }
        }

        private Guid _perdioId;
        public Guid PerdioId { get => _perdioId; set => _perdioId = value; }

        public GenericRepository(ERPContext context)
        {
            _context = context;
        }

        public virtual IQueryable<T> NativeGetAllNoTracking()
        {
            var entities = NativeGetAll().AsNoTracking();
            return entities;
        }

        public virtual IQueryable<T> GetAllNoTracking()
        {
            var entities = GetAll().AsNoTracking();
            return entities;
        }

        public virtual IQueryable<T> NativeGetAll()
        {
            var entities = _context.Set<T>();
            return entities;
        }

        public virtual IQueryable<T> GetAll()
        {
            var entities = NativeGetAll();
            return entities;
        }

        public virtual T Find(params object[] keyValues)
        {
            var entity = _context.Set<T>().Find(keyValues);
            return entity;
        }

        public virtual async Task<T> FindAsync(params object[] keyValues)
        {
            var entity = await _context.Set<T>().FindAsync(keyValues);
            return entity;
        }

        public virtual int Add(T entity, bool autoSaveAll = true)
        {
            SetCreateProps(entity);
            Context.Set<T>().Add(entity);
            if (autoSaveAll)
            {
                var rowsEffected = _context.SaveChanges();
                return rowsEffected;
            }
            return 0;
        }

        public virtual async Task<int> AddAsync(T entity, bool autoSaveAll = true)
        {
            SetCreateProps(entity);
            await _context.Set<T>().AddAsync(entity);
            if (autoSaveAll)
            {
                var rowsEffected = await _context.SaveChangesAsync();
                return rowsEffected;
            }
            return 0;
        }

        private void BaseEdit(T entity, byte[] rowVersion)
        {
            if (rowVersion != null)
            {
                _context.Entry(entity).Property("RowVersion").OriginalValue = rowVersion;
            }
            _context.Entry<T>(entity).State = EntityState.Modified;
        }

        private static T HandleConcurrency(DbUpdateConcurrencyException ex)
        {
            var exceptionEntry = ex.Entries.Single();
            var clientValues = (T)exceptionEntry.Entity;
            var databaseEntry = exceptionEntry.GetDatabaseValues();
            T databaseEntity = null;
            if (databaseEntry != null)
            {
                databaseEntity = (T)databaseEntry.ToObject();
            }
            return databaseEntity;
        }

        public virtual int Edit(T entity, bool autoSaveAll = true, byte[] rowVersion = null)
        {
            BaseEdit(entity, rowVersion);
            if (autoSaveAll)
            {
                int rowsEffected = -1;
                try
                {
                    rowsEffected = _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    T databaseEntity = HandleConcurrency(ex);
                }
                return rowsEffected;
            }
            return 0;
        }

        public virtual async Task<int> EditAsync(T entity, bool autoSaveAll = true, byte[] rowVersion = null)
        {
            BaseEdit(entity, rowVersion);
            if (autoSaveAll)
            {
                int rowsEffected = -1;
                try
                {
                    rowsEffected = await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    T databaseEntity = HandleConcurrency(ex);
                }
                return rowsEffected;
            }
            return 0;
        }

        public virtual int Delete(T entity, bool autoSaveAll = true)
        {
            _context.Set<T>().Remove(entity);
            if (autoSaveAll)
            {
                try
                {
                    var rowsEffected = _context.SaveChanges();
                    return rowsEffected;
                }
                catch (DbUpdateException)
                {
                    return -1;
                }
            }
            return 0;
        }

        public virtual async Task<int> DeleteAsync(T entity, bool autoSaveAll = true)
        {
            _context.Set<T>().Remove(entity);
            if (autoSaveAll)
            {
                try
                {
                    var rowsEffected = await _context.SaveChangesAsync();
                    return rowsEffected;
                }
                catch (DbUpdateException)
                {
                    return -1;
                }
            }
            return 0;
        }

        protected virtual string GenerateNewCode(string lastCode, string parentCode = "", int boxesNumber = 2, char firstCode = '0')
        {
            try
            {
                var newCode = "";
                parentCode = parentCode ?? "";

                #region there are children for this parent account

                if (lastCode != null)
                {
                    var lastChildCodeString = lastCode.Substring(parentCode.Length); // get only last child code without parent code
                    long lastChildCode;
                    if (long.TryParse(lastChildCodeString, out lastChildCode))
                    {
                        var tempLastChildCode = long.Parse("9" + lastChildCodeString);
                        if ((tempLastChildCode + 1).ToString().Length == tempLastChildCode.ToString().Length
                                                                        && (tempLastChildCode + 1) > tempLastChildCode)
                        {
                            // increase code number for parents and concate new code with parent code
                            newCode = parentCode + (tempLastChildCode + 1).ToString().Substring(1);
                        }
                        else
                        {
                            // can't  generate new code because there is no empty code in within this account
                            newCode = "-1";
                        }
                    }
                }
                #endregion

                #region no Children for this parent Account
                else
                {
                    newCode = parentCode;
                    // newCode = parentCode + "00"; // Decimal place equal 2
                    for (int i = 0; i < boxesNumber - 1; i++)
                    {
                        newCode += "0";
                    }
                    newCode += firstCode;
                }
                #endregion

                return newCode;
            }
            catch (Exception)
            {
                return "-2";
            }
        }

        protected long GetSequenceNextValue(string sequenceName)
        {
            var command = Context.Database.GetDbConnection().CreateCommand();
            command.CommandText = "SELECT NEXT VALUE FOR " + sequenceName;
            Context.Database.OpenConnection();
            var @object = command.ExecuteScalar();
            return (long)@object;
        }

        protected async Task<long> GetSequenceNextValueAsync(string sequenceName)
        {
            var command = Context.Database.GetDbConnection().CreateCommand();
            command.CommandText = "SELECT NEXT VALUE FOR " + sequenceName;
            Context.Database.OpenConnection();
            var @object = await command.ExecuteScalarAsync();
            return (long)@object;
        }

        public long Count()
        {
            var count = GetAll().LongCount();
            return count;
        }

        public long Count(Expression<Func<T, bool>> predicate)
        {
            var count = GetAll().LongCount(predicate);
            return count;
        }

        public async Task<long> CountAsync()
        {
            var count = await GetAll().LongCountAsync();
            return count;
        }

        public async Task<long> CountAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var count = await GetAll().LongCountAsync(predicate);
                return count;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
            }
        }

        protected bool _IsAdmin;
        public bool IsAdmin
        {
            get
            {
                return _IsAdmin;
            }
        }
        public void SetIsAdmin(bool IsAdmin)
        {
            _IsAdmin = IsAdmin;
        }

        protected string loggedInUserName;
        public string LoggedInUserName
        {
            set { loggedInUserName = value; }
        }
        public void SetLoggedInUserName(string userName)
        {
            loggedInUserName = userName;
        }

        protected Guid loggedInUserId;
        public Guid LoggedInUserId
        {
            set { loggedInUserId = value; }
        }
        public void SetLoggedInUserId(Guid usurId)
        {
            loggedInUserId = usurId;
        }

        public int SaveAll()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAllAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public virtual void SetPeriodId(Guid perdioId)
        {
            _perdioId = perdioId;
        }

        public Guid GetPeriodId()
        {
            return _perdioId;
        }

        public virtual void SetCreateProps(T entity)
        {
        }
    }
}
