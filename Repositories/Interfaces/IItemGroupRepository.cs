﻿using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Repositories
{
    public interface IItemGroupRepository : ICardRepository<ItemGroup>, INewCode
    {
        new string GetNewCode(Guid? ParentId = null, int BoxesNumber = 2, char FirstCode = '0');
        new Task<string> GetNewCodeAsync(Guid? ParentId = null, int BoxesNumber = 2, char FirstCode = '0');
        IQueryable<ItemGroup> Search(string key);
        void LoadReferences(ItemGroup itemGroup);
    }
}
