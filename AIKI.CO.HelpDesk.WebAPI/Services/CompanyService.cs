using System;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
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
            return request;
        }
    }
}