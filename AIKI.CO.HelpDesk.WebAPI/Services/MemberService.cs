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
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query;

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
                .GetFirstOrDefault(predicate: x => x.username == username && x.password == password,
                    ignoreQueryFilters: true));
            if (user == null)
                return null;
            user.token = _jwtService.GenerateSecurityToken(user);
            user.encryptedCompnayId = _protector.Protect(user.companyid.ToString());
            user.CompanyName = _unitofwork.GetRepository<Company>().Find(user.companyid).title;
            user.companyid = Guid.Empty;
            return user.WithoutPassword();
        }

        public override async Task<IEnumerable<MemberResponse>> GetAll()
        {
            return _map.Map<IEnumerable<MemberResponse>>(await _unitofwork.GetRepository<Member>()
                    .GetAllAsync(disableTracking: true))
                .WithoutPasswords().WithoutCompanyIds();
        }

        public override async Task<IEnumerable<MemberResponse>> GetAll(Expression<Func<Member, bool>> predicate = null,
            Func<IQueryable<Member>, IOrderedQueryable<Member>> orderBy = null,
            Func<IQueryable<Member>, IIncludableQueryable<Member, object>> include = null, bool disableTracking = true,
            bool ignoreQueryFilters = false)
        {
            return _map.Map<IEnumerable<MemberResponse>>(await _repository.GetAllAsync(predicate, orderBy, include,
                disableTracking,
                ignoreQueryFilters)).WithoutPasswords().WithoutCompanyIds();
        }

        public override async Task<IList<MemberResponse>> GetPagedList(Expression<Func<Member, bool>> predicate = null,
            Func<IQueryable<Member>, IOrderedQueryable<Member>> orderBy = null,
            Func<IQueryable<Member>, IIncludableQueryable<Member, object>> include = null, int pageIndex = 0,
            int pageSize = 20, bool disableTracking = true,
            CancellationToken cancellationToken = default(CancellationToken),
            bool ignoreQueryFilters = false)
        {
            return _map.Map<IList<MemberResponse>>((await _repository.GetPagedListAsync(predicate, orderBy,
                    include, pageIndex, pageSize, disableTracking, cancellationToken, ignoreQueryFilters)).Items)
                .WithoutPasswords().WithoutCompanyIds().ToList();
        }

        public override async Task<MemberResponse> GetById(Guid id)
        {
            return _map.Map<MemberResponse>(await _repository.FindAsync(id)).WithoutPassword().WithoutCompanyId();
        }

        public override async Task<MemberResponse> GetSingle(Expression<Func<Member, bool>> predicate, bool ignoreQueryFilters = false)
        {
            return _map.Map<MemberResponse>(await _repository.GetFirstOrDefaultAsync(predicate: predicate, ignoreQueryFilters:ignoreQueryFilters)).WithoutPassword().WithoutCompanyId();
        }
        
        public async Task<MemberResponse> GetSingleWithPassword(Expression<Func<Member, bool>> predicate, bool ignoreQueryFilters = false)
        {
            return _map.Map<MemberResponse>(await _repository.GetFirstOrDefaultAsync(predicate: predicate, ignoreQueryFilters:ignoreQueryFilters)).WithoutCompanyId();
        }
    }
}