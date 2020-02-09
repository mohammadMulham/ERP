using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Managers
{
    public class PeriodManager : IPeriodManager
    {
        private IHttpContextAccessor _httpContextAccessor;

        public PeriodManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetPeriod()
        {
            var periodIdString = _httpContextAccessor.HttpContext.Request.Headers["Accept-Period"];
            var periodId = !string.IsNullOrWhiteSpace(periodIdString) ? Guid.Parse(periodIdString) : Guid.Empty;
            return periodId;
        }
    }
}
