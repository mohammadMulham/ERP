using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Repositories
{
    public interface ISellerRepository : ICardRepository<Seller>, INewCode
    {
        new string GetNewCode(Guid? ParentId = null, int BoxesNumber = 2, char FirstCode = '0');
        new Task<string> GetNewCodeAsync(Guid? ParentId = null, int BoxesNumber = 2, char FirstCode = '0');
        void LoadReferences(Seller seller);
    }
}
