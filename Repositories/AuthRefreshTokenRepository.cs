using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Models;
using ERPAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ERPAPI.Repositories
{
    public class AuthRefreshTokenRepository : IAuthRefreshTokenRepository
    {
        private ERPContext _context;

        public AuthRefreshTokenRepository(ERPContext context)
        {
            _context = context;
        }

        public bool AddToken(AuthRefreshToken token)
        {
            _context.AuthRefreshTokens.Add(token);
            return _context.SaveChanges() > 0;
        }

        public bool ExpireToken(AuthRefreshToken token)
        {
            _context.AuthRefreshTokens.Update(token);
            return _context.SaveChanges() > 0;
        }

        public async Task<AuthRefreshToken> GetTokenAsync(string refresh_token, string client_id)
        {
            var authRefreshToken = await _context.AuthRefreshTokens.FirstOrDefaultAsync(e
                         => e.IsActive == true && e.Expires >= DateTimeOffset.UtcNow
                         && e.ClientId == client_id && e.Value == refresh_token);
            return authRefreshToken;
        }
    }
}
