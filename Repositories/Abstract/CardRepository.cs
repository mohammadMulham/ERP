using ERPAPI.Data;
using ERPAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Repositories
{
    public abstract class CardRepository<T> : DefaultEntityRepository<T>, ICardRepository<T>
        where T : Card
    {
        public CardRepository(ERPContext context) : base(context)
        {

        }

        public override IQueryable<T> GetAllNoTracking()
        {
            return base.GetAllNoTracking().OrderBy(c => c.Code);
        }

        public bool IsExisCode(string Code)
        {
            var count = base.GetAll().LongCount(a => a.Code == Code);
            return count > 0;
        }

        public bool IsExisCode(Guid id, string Code)
        {
            var count = base.GetAll().LongCount(a => a.Code == Code && a.Id != id);
            return count > 0;
        }

        public async Task<bool> IsExistCodeAsync(string Code)
        {
            var count = await base.GetAll().LongCountAsync(a => a.Code == Code);
            return count > 0;
        }

        public async Task<bool> IsExistCodeAsync(Guid id, string Code)
        {
            var count = await base.GetAll().LongCountAsync(a => a.Code == Code && a.Id != id);
            return count > 0;
        }
    }
}
