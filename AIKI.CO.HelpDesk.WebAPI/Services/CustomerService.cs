using System;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace AIKI.CO.HelpDesk.WebAPI.Services
{
    public class CustomerService: BaseService<Customer, CustomerResponse>
    {
        private readonly IService<OrganizeChart, OrganizeChartResponse> _serviceOrgChart;

        public CustomerService(IMapper map,
            IUnitOfWork unitofwork,
            IOptions<AppSettings> appSettings,
            IService<TicketHistory, TicketHistoryResponse> serviceHistory,
            IHttpContextAccessor context,
            IDataProtectionProvider provider,
            IService<OrganizeChart, OrganizeChartResponse> serviceOrgChart) : base(map, unitofwork, appSettings,
            context, provider)
        {
            _serviceOrgChart = serviceOrgChart;
        }
        public override async Task<int> AddRecord(CustomerResponse request, Guid? companyId =null)
        {
            request.disabled = false;
            var customerInfo = await base.AddRecordWithReturnRequest(request);
            if (customerInfo != null)
            {
                return await _serviceOrgChart.AddRecord(new OrganizeChartResponse
                {
                    title = customerInfo.title,
                    allowdelete = false,
                    titletype = "6014fa01-1bb4-4313-be26-c692ca4c2556",
                    customerid = customerInfo.id,
                });
            }
            return 0;
        }
    }
}