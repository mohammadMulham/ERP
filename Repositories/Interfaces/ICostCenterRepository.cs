using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Repositories
{
    public interface ICostCenterRepository : ICardRepository<CostCenter>, INewCode
    {
        new string GetNewCode(Guid? ParentId = null, int BoxesNumber = 2, char FirstCode = '0');
        new Task<string> GetNewCodeAsync(Guid? ParentId = null, int BoxesNumber = 2, char FirstCode = '0');
        IQueryable<CostCenter> Search(string key);
        void LoadReferences(CostCenter costCenter);
        bool CheckIfCanBeDeleted(long id);
        Task<bool> CheckIfCanBeDeletedAsync(long id);
    }
}
