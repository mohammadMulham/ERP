using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ERPAPI
{
    public partial class Startup
    {
        private void ConfigureJwtAuthService(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                            .AddJwtBearer(options =>
                            {
                                options.RequireHttpsMetadata = false;
                                options.SaveToken = true;
                                options.TokenValidationParameters = new TokenValidationParameters()
                                {
                                    // The signing key must match!
                                    ValidateIssuerSigningKey = true,
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SignKey"])),

                                    // Validate the JWT Audience (aud) claim
                                    ValidateAudience = true,
                                    ValidAudience = Configuration["JWT:ValidAudience"],

                                    // Validate the JWT Issuer (iss) claim
                                    ValidateIssuer = true,
                                    ValidIssuer = Configuration["JWT:ValidIssuer"],

                                    // Validate the token expiry 
                                    ValidateLifetime = true,

                                    ClockSkew = TimeSpan.Zero
                                };
                            });
        }
    }
}
