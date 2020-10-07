using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AIKI.CO.HelpDesk.WebAPI.Services
{
    public class JWTService : IJWTService
    {
        private readonly byte[] _encryptionkey;
        private readonly string _expDate;
        private readonly string _secret;

        public JWTService(IConfiguration config)
        {
            _secret = Environment.GetEnvironmentVariable("jwt_secret");
            _encryptionkey =
                Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("jwt_encryptionKey") ??
                                       throw new Exception());
            _expDate = config.GetSection("JwtConfig").GetSection("expirationInMinutes").Value;
        }

        public string GenerateSecurityToken(MemberResponse user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.username),
                new Claim("firstName", user.membername),
                new Claim("role", user.roles),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                IssuedAt = DateTime.Now,
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.username),
                    new Claim("firstName", user.membername),
                    new Claim(ClaimTypes.Role, user.roles),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_expDate)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                EncryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(_encryptionkey),
                    SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}