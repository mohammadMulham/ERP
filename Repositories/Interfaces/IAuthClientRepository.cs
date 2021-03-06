﻿using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Repositories
{
    public interface IAuthClientRepository
    {
        Task<AuthClient> FindAsync(Guid id);
        Task<AuthClient> GetAsync(string name);
    }
}
