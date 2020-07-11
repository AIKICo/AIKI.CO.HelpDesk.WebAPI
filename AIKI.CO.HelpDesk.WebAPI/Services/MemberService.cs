using System;
using AIKI.CO.HelpDesk.WebAPI.Extensions;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;

namespace AIKI.CO.HelpDesk.WebAPI.Services
{
    public sealed class MemberService : BaseService<Member, MemberResponse>, IMemberService
    {
        private readonly IJWTService _jwtService;
        private readonly IDataProtector _protector;

        public MemberService(
            IMapper map,
            IUnitOfWork unitofwork,
            IOptions<AppSettings> appSettings,
            IJWTService jwtService,
            IHttpContextAccessor context,
            IDataProtectionProvider provider) : base(map, unitofwork, appSettings, context, provider)
        {
            _jwtService = jwtService;
            _protector = provider.CreateProtector("MemberService.CompanyId");
        }

        public MemberResponse Authenticate(string username, string password)
        {
            var user = _map.Map<MemberResponse>(_unitofwork.GetRepository<Member>()
                .GetFirstOrDefault(predicate: x => x.username == username && x.password == password, ignoreQueryFilters:true));
            if (user == null)
                return null;
            user.token = _jwtService.GenerateSecurityToken(user);
            user.encryptedCompnayId = _protector.Protect(user.companyid.ToString());
            user.companyid = Guid.Empty;
            return user.WithoutPassword();
        }

        public override async Task<IEnumerable<MemberResponse>> GetAll()
        {
            return _map.Map<IEnumerable<MemberResponse>>(await _unitofwork.GetRepository<Member>()
                    .GetAllAsync(disableTracking: true))
                .WithoutPasswords();
        }
    }
}