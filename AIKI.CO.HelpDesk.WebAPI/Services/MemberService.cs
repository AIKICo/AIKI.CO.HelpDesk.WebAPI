using AIKI.CO.HelpDesk.WebAPI.Extensions;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
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
    public sealed class MemberService :BaseService<Member,MemberResponse>, IMemberService
    {
        private readonly IJWTService _jwtService;
        public MemberService(
            IMapper map, 
            IUnitOfWork unitofwork,
            IOptions<AppSettings> appSettings,
            IJWTService jwtService) :base(map,unitofwork, appSettings)
        {
            _jwtService = jwtService;
        }
        public MemberResponse Authenticate(string username, string password)
        {
            var user = _map.Map<MemberResponse>(_unitofwork.GetRepository<Member>().GetFirstOrDefault(predicate: x => x.username == username && x.password == password));
            if (user == null)
                return null;

            user.token = _jwtService.GenerateSecurityToken(user.id.ToString().ToString());
            return user.WithoutPassword();
        }

        public override async Task<IEnumerable<MemberResponse>> GetAll()
        {
            return _map.Map<IEnumerable<MemberResponse>>(await _unitofwork.GetRepository<Member>().GetAllAsync()).WithoutPasswords();
        }
    }
}
