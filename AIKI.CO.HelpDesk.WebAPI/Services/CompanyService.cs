using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.DTO;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using AutoMapper.Internal;

namespace AIKI.CO.HelpDesk.WebAPI.Services
{
    public sealed class CompanyService : ICompanyService
    {
        private readonly IMapper _map;
        private readonly IUnitOfWork _unitofwork;

        public CompanyService(IUnitOfWork unitofwork, IMapper map)
        {
            _unitofwork = unitofwork;
            _map = map ?? throw new NullReferenceException(nameof(map));
        }

        public async Task<CompanyResponse> AddRecord(CompanyResponse request)
        {
            request.id = Guid.NewGuid();
            request.allowdelete = false;
            await _unitofwork.GetRepository<Company>().InsertAsync(_map.Map<Company>(request));
            var effectedRow = await _unitofwork.SaveChangesAsync();
            if (effectedRow <= 0) return null;

            #region Add Constant

            var appConstantRepo = _unitofwork.GetRepository<AppConstant>();
            var appContants = await appConstantRepo.GetAllAsync(
                q => q.companyid == Guid.Parse("997afb89-9abf-4889-8e43-cc301a311a9f"), ignoreQueryFilters: true);
            appContants.ForAll(async record =>
            {
                record.companyid = request.id;
                await appConstantRepo.InsertAsync(record);
            });
            await _unitofwork.SaveChangesAsync();

            var appConstantItemRepo = _unitofwork.GetRepository<AppConstantItem>();
            var appContantItems = await appConstantItemRepo.GetAllAsync(
                q => q.companyid == Guid.Parse("997afb89-9abf-4889-8e43-cc301a311a9f"), ignoreQueryFilters: true);
            appContantItems.ForAll(async record =>
            {
                record.companyid = request.id;
                await appConstantItemRepo.InsertAsync(record);
            });
            await _unitofwork.SaveChangesAsync();

            #endregion

            #region Add Operating Hours

            var operatingHourRepo = _unitofwork.GetRepository<OperatingHour>();
            var operatingHourRecords = await operatingHourRepo.GetAllAsync(
                q => q.companyid == Guid.Parse("997afb89-9abf-4889-8e43-cc301a311a9f"), ignoreQueryFilters: true);
            operatingHourRecords.ForAll(async record =>
            {
                record.id = Guid.NewGuid();
                record.companyid = request.id;
                await operatingHourRepo.InsertAsync(record);
            });
            await _unitofwork.SaveChangesAsync();

            #endregion

            #region Add SLA

            var slaRepo = _unitofwork.GetRepository<SLASetting>();
            var slaRecords = await slaRepo.GetAllAsync(
                q => q.companyid == Guid.Parse("997afb89-9abf-4889-8e43-cc301a311a9f"), ignoreQueryFilters: true);
            slaRecords.ForAll(async record =>
            {
                record.id = Guid.NewGuid();
                record.companyid = request.id;
                await slaRepo.InsertAsync(record);
            });

            #endregion

            #region Add Groups

            var groupsRepo = _unitofwork.GetRepository<Group>();
            var groupRecords = await groupsRepo.GetAllAsync(
                q => q.companyid == Guid.Parse("997afb89-9abf-4889-8e43-cc301a311a9f"), ignoreQueryFilters: true);
            groupRecords.ForAll(async record =>
            {
                record.id = Guid.NewGuid();
                record.companyid = request.id;
                await groupsRepo.InsertAsync(record);
            });
            await _unitofwork.SaveChangesAsync();

            #endregion

            return request;
        }

        public async Task<CompanyResponse> GetSingle(Expression<Func<Company, bool>> predicate,
            bool ignoreQueryFilters = false)
            => _map.Map<CompanyResponse>(await _unitofwork.GetRepository<Company>().GetFirstOrDefaultAsync(
                predicate: predicate,
                ignoreQueryFilters: ignoreQueryFilters));
    }
}