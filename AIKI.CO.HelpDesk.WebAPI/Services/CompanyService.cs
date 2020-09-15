using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using AutoMapper.Configuration;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Authorization;

namespace AIKI.CO.HelpDesk.WebAPI.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _map;
        private readonly IConfiguration _configuration;
        private readonly ICloudFlareService _cloudFlareService;
        private readonly ICloudFlareConfiguration _cloudFlareConfiguration;

        public CompanyService(IUnitOfWork unitofwork, IMapper map, IConfiguration configuration,
            ICloudFlareService cloudFlareService,
            ICloudFlareConfiguration cloudFlareConfiguration)
        {
            _unitofwork = unitofwork;
            _map = map;
            _configuration = configuration;
            _cloudFlareService = cloudFlareService;
            _cloudFlareConfiguration = cloudFlareConfiguration;
        }

        public async Task<CompanyResponse> AddRecord(CompanyResponse request)
        {
            request.id = Guid.NewGuid();
            request.allowdelete = false;
            await _unitofwork.GetRepository<Company>().InsertAsync(_map.Map<Company>(request));
            var effectedRow = await _unitofwork.SaveChangesAsync();
            if (effectedRow > 0)
            {
                #region Add Constant

                var AppConstantRepo = _unitofwork.GetRepository<AppConstant>();
                var AppContants = await AppConstantRepo.GetAllAsync(
                    q => q.companyid == Guid.Parse("997afb89-9abf-4889-8e43-cc301a311a9f"), ignoreQueryFilters: true);
                AppContants.ForAll(async (record) =>
                {
                    record.companyid = request.id;
                    await AppConstantRepo.InsertAsync(record);
                });
                await _unitofwork.SaveChangesAsync();

                var AppConstantItemRepo = _unitofwork.GetRepository<AppConstantItem>();
                var AppContantItems = await AppConstantItemRepo.GetAllAsync(
                    q => q.companyid == Guid.Parse("997afb89-9abf-4889-8e43-cc301a311a9f"), ignoreQueryFilters: true);
                AppContantItems.ForAll(async (record) =>
                {
                    record.companyid = request.id;
                    await AppConstantItemRepo.InsertAsync(record);
                });
                await _unitofwork.SaveChangesAsync();

                #endregion

                #region Add Operating Hours

                var operatingHourRepo = _unitofwork.GetRepository<OperatingHour>();
                var operatingHourRecords = await operatingHourRepo.GetAllAsync(
                    q => q.companyid == Guid.Parse("997afb89-9abf-4889-8e43-cc301a311a9f"), ignoreQueryFilters: true);
                operatingHourRecords.ForAll(async (record) =>
                {
                    record.id = Guid.NewGuid();
                    record.companyid = request.id;
                    await operatingHourRepo.InsertAsync(record);
                });
                await _unitofwork.SaveChangesAsync();

                #endregion

                #region Add SLA

                var SLARepo = _unitofwork.GetRepository<SLASetting>();
                var SLARecords = await SLARepo.GetAllAsync(
                    q => q.companyid == Guid.Parse("997afb89-9abf-4889-8e43-cc301a311a9f"), ignoreQueryFilters: true);
                SLARecords.ForAll(async (record) =>
                {
                    record.id = Guid.NewGuid();
                    record.companyid = request.id;
                    await SLARepo.InsertAsync(record);
                });

                #endregion

                #region Add Groups

                var GroupsRepo = _unitofwork.GetRepository<Group>();
                var GroupRecords = await GroupsRepo.GetAllAsync(
                    q => q.companyid == Guid.Parse("997afb89-9abf-4889-8e43-cc301a311a9f"), ignoreQueryFilters: true);
                GroupRecords.ForAll(async (record) =>
                {
                    record.id = Guid.NewGuid();
                    record.companyid = request.id;
                    await GroupsRepo.InsertAsync(record);
                });
                await _unitofwork.SaveChangesAsync();

                #endregion

                #region Add subDomain To CloudFlare DNS Zone

                await _cloudFlareService.AddDnsRecord(request.subdomain, _cloudFlareConfiguration.IPAddress1);
                await _cloudFlareService.AddDnsRecord(request.subdomain, _cloudFlareConfiguration.IPAddress2);

                #endregion

                return request;
            }
            return null;
        }
    }
}