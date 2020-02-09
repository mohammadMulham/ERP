using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ERPAPI.Repositories
{
    public class UnitRepository : DefaultEntityRepository<Unit>, IUnitRepository
    {
        public UnitRepository(ERPContext context) : base(context)
        {

        }

        public IQueryable<Unit> Search(string key)
        {
            var units = NativeGetAllNoTracking();

            if (!string.IsNullOrEmpty(key))
            {
                units = units.Where(
                                e => e.Name.Contains(key));
            }

            units = units.OrderBy(e => e.Name);
            units = units.Skip(0).Take(25);
            return units;
        }

        public void LoadReferences(Unit unit)
        {

        }

    }
}
