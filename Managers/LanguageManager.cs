using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Managers
{
    public class LanguageManager : ILanguageManager
    {
        private IHttpContextAccessor _httpContextAccessor;

        public LanguageManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetLang()
        {
            var country = _httpContextAccessor.HttpContext.Request.Headers["Accept-Language"];
            return country;
        }
    }
}
