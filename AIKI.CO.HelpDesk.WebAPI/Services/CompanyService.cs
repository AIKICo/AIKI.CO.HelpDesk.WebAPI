using System;
using System.Linq;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Authorization;

namespace AIKI.CO.HelpDesk.WebAPI.Services
{
    public class CompanyService:ICompanyService
    {
        protected IUnitOfWork _unitofwork { get; private set; }
        protected IMapper _map { get; private set; }

        public CompanyService(IUnitOfWork unitofwork, IMapper map)
        {
            _unitofwork = unitofwork;
            _map = map;
        }
        public async Task<CompanyResponse> AddRecord(CompanyResponse request)
        {
            request.id = Guid.NewGuid();
            await _unitofwork.GetRepository<Company>().InsertAsync(_map.Map<Company>(request));
            await _unitofwork.SaveChangesAsync();
            
            // Add Constant
            var AppConstantRepo = _unitofwork.GetRepository<AppConstant>();
            var AppContants = await AppConstantRepo.GetAllAsync(q => q.companyid == Guid.Parse("997afb89-9abf-4889-8e43-cc301a311a9f"), ignoreQueryFilters:true);
            AppContants.ForAll(async (record) =>
            {
                record.companyid = request.id;
                await AppConstantRepo.InsertAsync(record);
            });
            await _unitofwork.SaveChangesAsync();

            var AppConstantItemRepo = _unitofwork.GetRepository<AppConstantItem>();
            var AppContantItems = await AppConstantItemRepo.GetAllAsync(q => q.companyid == Guid.Parse("997afb89-9abf-4889-8e43-cc301a311a9f"), ignoreQueryFilters:true);
            AppContantItems.ForAll(async (record) =>
            {
                record.companyid = request.id;
                await AppConstantItemRepo.InsertAsync(record);
            });
            await _unitofwork.SaveChangesAsync();
            
            return request;
        }
    }
}