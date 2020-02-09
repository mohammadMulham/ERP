using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ERPAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Filters;
using ERPAPI.Managers;
using System.Security.Claims;
using System.Net;
using System.Globalization;
using ERPAPI.Extentions;

namespace ERPAPI.Controllers
{
    [ModelValidator]
    [Produces("application/json")]
    public class BaseController : Controller
    {
        private ILanguageManager _languageManager;

        public BaseController(ILanguageManager languageManager)
        {
            _languageManager = languageManager;
        }

        protected string _language { get; set; }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                _language = _languageManager.GetLang();
                CultureInfo.CurrentUICulture = new CultureInfo(_language);
            }
            catch (CultureNotFoundException)
            {
            }
            return base.OnActionExecutionAsync(context, next);
        }

        protected IActionResult BadRequestWithErrors(string error, string key = "")
        {
            ModelState.AddModelError(key, error);
            return BadRequest(ModelState.GetWithErrorsKey());
        }

        protected Guid GetUserId()
        {
            var iClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (iClaim != null)
            {
                var userId = iClaim.Value;
                return Guid.Parse(userId);
            }
            return Guid.Empty;
        }

        protected string GetUserUserName()
        {
            var username = "";
            if (User.Identity.IsAuthenticated)
            {
                username = User.Identity.Name;
            }
            return username;
        }

        protected bool GetIsUserAdmin()
        {
            var isAdmin = false;
            if (User.Identity.IsAuthenticated)
            {
                isAdmin = User.HasClaim(e => e.Type == "ADMIN" && e.Value == "true");
            }
            return isAdmin;
        }

        protected string GetUserHostName()
        {
            var httpContext = Request.HttpContext;
            var connection = httpContext.Connection;
            var remoteIpAddress = connection.RemoteIpAddress;
            return remoteIpAddress.ToString();
        }
    }
}