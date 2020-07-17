using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.BuilderExtensions
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection AddTokenAuthentication(
            this IServiceCollection services, 
            IConfiguration config)
        {
            var secret = Environment.GetEnvironmentVariable("jwt_secret") ?? throw new Exception();
            var key = Encoding.ASCII.GetBytes(secret);
            var encryptKey = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("jwt_encryptionKey") ?? throw new Exception());

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero,
                        RequireSignedTokens = true,
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        TokenDecryptionKey = new SymmetricSecurityKey(encryptKey),
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            if (string.IsNullOrEmpty(accessToken) == false) {
                                context.Token = accessToken;
                                context.HttpContext.Request.Headers.Add("CompanyID", context.Request.Query["CompanyID"]);
                                context.HttpContext.Request.Headers.Add("MemberID", context.Request.Query["MemberID"]);
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }
    }
}