using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ERPAPI.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using ERPAPI.ViewModels.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using ERPAPI.Managers;
using Newtonsoft.Json;
using ERPAPI.Repositories;
using ERPAPI.Resources;
using System.Resources;
using ERPAPI.Resources.Gender;
using Swashbuckle.AspNetCore.Examples;
using Newtonsoft.Json.Converters;
using ERPAPI.SwaggerExamples.Auth;
using ERPAPI.Options;
using ERPAPI.Extentions;

namespace ERPAPI.Controllers
{
    [Route("api/[Controller]")]
    public class AuthController : BaseController
    {
        #region variables
        private ILogger<AuthController> _logger;
        private SignInManager<User> _signInMgr;
        private UserManager<User> _userMgr;
        private IPasswordHasher<User> _hasher;
        private IOptions<JwtOptions> _tokenOptions;
        private IAuthClientRepository _authClientRepo;
        private IAuthRefreshTokenRepository _authRefreshTokenRepo;
        #endregion

        public AuthController(
          UserManager<User> userMgr,
          IPasswordHasher<User> hasher,
          SignInManager<User> signInMgr,
          ILogger<AuthController> logger,
          ILanguageManager languageManager,
          IOptions<JwtOptions> tokenOptions,
          IAuthClientRepository authClientRepo,
          IAuthRefreshTokenRepository authRefreshTokenRepo
            ) : base(languageManager)
        {
            _signInMgr = signInMgr;
            _logger = logger;
            _userMgr = userMgr;
            _hasher = hasher;
            _tokenOptions = tokenOptions;
            _authClientRepo = authClientRepo;
            _authRefreshTokenRepo = authRefreshTokenRepo;
        }

        [AllowAnonymous]
        [HttpPost("login", Name = "login")]
        public async Task<IActionResult> Login([FromBody] CredentialModel model)
        {
            try
            {
                var result = await _signInMgr.PasswordSignInAsync(model.UserName, model.Password, false, false);
                if (result.Succeeded)
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception thrown while logging in: {ex}");
            }

            return BadRequestWithErrors("Failed to login");
        }

        [AllowAnonymous]
        [HttpPost("token", Name = "token")]
        [SwaggerRequestExample(typeof(GenerateJwtViewModel), typeof(CreateTokenViewModelExample), jsonConverter: typeof(StringEnumConverter))]
        public async Task<IActionResult> CreateToken([FromBody] GenerateJwtViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            if (model.grant_type == "password")
            {
                return await GenerateToken(model);
            }

            else if (model.grant_type == "refresh_token")
            {
                return await RefreshToekn(model);
            }
            else
            {
                return BadRequestWithErrors("invalid_grant_type", "grant_type");
            }
        }

        private async Task<IActionResult> RefreshToekn(GenerateJwtViewModel model)
        {
            var client = await _authClientRepo.GetAsync(model.client_id);
            if (client == null)
            {
                // should be Unauthorized
                return BadRequestWithErrors("invalid_client", "client_id");
            }

            if (client.ApplicationType != ApplicationType.JavaScript && client.Secret != model.client_secret) // we should compare with hash, in future
            {
                return BadRequestWithErrors("invalid_secret", "client_secret");
            }

            var token = await _authRefreshTokenRepo.GetTokenAsync(model.refresh_token, model.client_id);

            if (token == null || client == null)
            {
                return BadRequestWithErrors("can_not_refresh_token");
            }

            if (!token.IsActive)
            {
                return BadRequestWithErrors("refresh_token_has_expired");
            }

            var refresh_token = Guid.NewGuid().ToString("n");

            token.IsActive = false;

            //expire the old refresh_token and add a new refresh_token
            var updateFlag = _authRefreshTokenRepo.ExpireToken(token);

            var addFlag = _authRefreshTokenRepo.AddToken(new AuthRefreshToken(model.client_id, token.Subject, refresh_token, DateTime.UtcNow.AddMinutes(client.RefreshTokenLifeTime)));

            var user = await _userMgr.FindByNameAsync(model.username);

            if (user != null && updateFlag && addFlag)
            {
                return Ok(await GetJwt(model.client_id, client.RefreshTokenLifeTime, refresh_token, null));
            }

            return BadRequest();
        }

        private async Task<object> GetJwt(string client_id, int refreshTokenLifeTime, string refresh_token, User user)
        {
            var userClaims = await _userMgr.GetClaimsAsync(user);

            var claims = new[]
            {
                             new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                             new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                             new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                             new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                             new Claim(JwtRegisteredClaimNames.Email, user.Email),
                             new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
                        }.Union(userClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.Value.SignKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                  issuer: _tokenOptions.Value.ValidIssuer,
                  audience: _tokenOptions.Value.ValidAudience,
                  claims: claims,
                  expires: DateTime.UtcNow.AddMinutes(refreshTokenLifeTime),
                  signingCredentials: creds
             );

            return new
            {
                token_type = "Bearer",
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                refresh_token = refresh_token
            };
        }

        private async Task<IActionResult> GenerateToken(GenerateJwtViewModel model)
        {
            var client = await _authClientRepo.GetAsync(model.client_id);
            if (client == null)
            {
                return BadRequestWithErrors("invalid_client", "client_id");
            }

            if (client.ApplicationType != ApplicationType.JavaScript && client.Secret != model.client_secret) // we should compare with hash, in future
            {
                return BadRequestWithErrors("invalid_secret", "client_secret");
            }

            var user = await _userMgr.FindByNameAsync(model.username);
            if (user == null)
            {
                return BadRequestWithErrors("user_not_found!", "username");
            }
            if (user != null && string.IsNullOrEmpty(model.password) || _hasher.VerifyHashedPassword(user, user.PasswordHash, model.password) == PasswordVerificationResult.Success)
            {
                var refreshTokenKey = Guid.NewGuid().ToString("n");

                var refreshToken = new AuthRefreshToken(model.client_id, model.username, refreshTokenKey, DateTime.UtcNow.AddMinutes(client.RefreshTokenLifeTime));

                if (_authRefreshTokenRepo.AddToken(refreshToken))
                {
                    return Ok(await GetJwt(model.client_id, client.RefreshTokenLifeTime, refreshTokenKey, user));
                }
                else
                {
                    return BadRequestWithErrors("can_not_add_token_to_database");
                }
            }

            return BadRequestWithErrors("wrong_password!", "password");
        }
    }

}