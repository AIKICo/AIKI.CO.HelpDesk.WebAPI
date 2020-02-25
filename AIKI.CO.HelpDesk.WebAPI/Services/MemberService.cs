using AIKI.CO.HelpDesk.WebAPI.Extensions;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Services
{
    public class MemberService : IMemberService
    {
        private readonly AppSettings _appSettings;
        private readonly IUnitOfWork _unitofwork;

        private List<Member> _members = new List<Member>
        {
            new Member { id = Guid.NewGuid(),companyid=Guid.NewGuid(), membername = "test", username = "test", roles = "admin", password = "test", email="qermezkon@gmail.com" }
        };
        public MemberService(IUnitOfWork unitofwork,IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _unitofwork = unitofwork;
        }
        public Member Authenticate(string username, string password)
        {
            var user = _members.SingleOrDefault(x => x.username == username && x.password == password);

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

        public async Task<IEnumerable<Member>> GetAll()
        {
            return (await _unitofwork.GetRepository<Member>().GetAllAsync()).WithoutPasswords();
        }
    }
}
