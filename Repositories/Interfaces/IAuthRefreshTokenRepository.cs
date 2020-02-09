using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Repositories
{
    public interface IAuthRefreshTokenRepository
    {
         bool AddToken(AuthRefreshToken token);
         bool ExpireToken(AuthRefreshToken token);
         Task<AuthRefreshToken> GetTokenAsync(string refresh_token, string client_id);
    }
}
