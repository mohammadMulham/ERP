using ERPAPI.Data;
using ERPAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Repositories
{
    public class AuthClientRepository : IAuthClientRepository
    {
        private ERPContext _context;

        public AuthClientRepository(ERPContext context)
        {
            _context = context;
        }

        public async Task<AuthClient> FindAsync(Guid id)
        {
            var authClient = await _context.AuthClients.FindAsync(id);
            return authClient;
        }

        public async Task<AuthClient> GetAsync(string name)
        {
            var authClient = await _context.AuthClients.FirstOrDefaultAsync(e => e.Name == name);
            return authClient;
        }
    }
}
