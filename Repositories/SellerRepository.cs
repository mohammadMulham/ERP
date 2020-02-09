using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ERPAPI.Repositories
{
    public class SellerRepository : CardRepository<Seller>, ISellerRepository
    {
        public SellerRepository(ERPContext context) : base(context)
        {
        }

        public override IQueryable<Seller> GetAll()
        {
            return base.GetAll().Include(e => e.ParentSeller);
        }

        public string GetNewCode(Guid? ParentId = null, int BoxesNumber = 2, char FirstCode = '0')
        {
            var lastCode = NativeGetAll().Where(a => a.ParentId == ParentId).Max(e => e.Code);

            var parentCode = NativeGetAll().Where(a => a.Id == ParentId).Select(e => e.Code).SingleOrDefault();

            return GenerateNewCode(lastCode, parentCode, BoxesNumber);
        }

        public async Task<string> GetNewCodeAsync(Guid? ParentId = null, int BoxesNumber = 2, char FirstCode = '0')
        {
            var lastCode = await NativeGetAll().Where(e => e.ParentId == ParentId).MaxAsync(e => e.Code);

            var parentCode = await NativeGetAll().Where(e => e.Id == ParentId).Select(e => e.Code).SingleOrDefaultAsync();

            return GenerateNewCode(lastCode, parentCode, BoxesNumber);
        }

        public void LoadReferences(Seller seller)
        {
            Context.Entry(seller).Reference(c => c.ParentSeller).Load();
        }
    }
}
