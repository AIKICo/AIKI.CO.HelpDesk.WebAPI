﻿using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AIKI.CO.HelpDesk.WebAPI.Services
{
    public class JWTService : IJWTService
    {
        private readonly string _secret;
        private readonly string _expDate;
        private readonly byte[] _encryptionkey;

        public JWTService(IConfiguration config)
        {
            _secret = config.GetSection("JwtConfig").GetSection("secret").Value;
            _encryptionkey = Encoding.UTF8.GetBytes(config.GetSection("JwtConfig").GetSection("encryptionKey").Value);
            _expDate = config.GetSection("JwtConfig").GetSection("expirationInMinutes").Value;
        }

        public string GenerateSecurityToken(string Id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                IssuedAt=DateTime.Now,
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, Id)
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_expDate)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                EncryptingCredentials= new EncryptingCredentials(new SymmetricSecurityKey(_encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}