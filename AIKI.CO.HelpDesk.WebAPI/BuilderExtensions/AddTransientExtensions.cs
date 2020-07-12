using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Raven.Client;

namespace AIKI.CO.HelpDesk.WebAPI.BuilderExtensions
{
    public static class AddTransientExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IMemberService, MemberService>();
            services.AddTransient<IService<Member, MemberResponse>, BaseService<Member, MemberResponse>>();
            services.AddTransient<IService<Customer, CustomerResponse>, CustomerService>();
            services
                .AddTransient<IService<OperatingHour, OperatingHoureResponse>,
                    BaseService<OperatingHour, OperatingHoureResponse>>();
            services
                .AddTransient<IService<SLASetting, SLASettingResponse>, BaseService<SLASetting, SLASettingResponse>>();
            services.AddTransient<IService<Group, GroupResponse>, BaseService<Group, GroupResponse>>();
            services
                .AddTransient<IService<AppConstant, AppConstantResponse>, BaseService<AppConstant, AppConstantResponse>
                >();
            services
                .AddTransient<IService<AppConstantItem, AppConstantItemResponse>,
                    BaseService<AppConstantItem, AppConstantItemResponse>>();
            services
                .AddTransient<IService<OrganizeChart, OrganizeChartResponse>,
                    BaseService<OrganizeChart, OrganizeChartResponse>>();
            services
                .AddTransient<IService<OrganizeCharts_JsonView, OrganizeCharts_JsonViewResponse>,
                    BaseService<OrganizeCharts_JsonView, OrganizeCharts_JsonViewResponse>>();
            services.AddTransient<IJWTService, JWTService>();
            services
                .AddTransient<IService<Asset, AssetResponse>,
                    BaseService<Asset, AssetResponse>>();

            services
                .AddTransient<IService<Ticket, TicketResponse>, TicketService>();
            services
                .AddTransient<IService<AssetsView, AssetsViewResponse>,
                    BaseService<AssetsView, AssetsViewResponse>>();

            services
                .AddTransient<IService<TicketsView, TicketsViewResponse>,
                    BaseService<TicketsView, TicketsViewResponse>>();

            services
                .AddTransient<IService<TicketHistory, TicketHistoryResponse>,
                    TicketHistoryService>();

            services
                .AddTransient<IService<TicketCountInfo, TicketCountInfoResponse>,
                    BaseService<TicketCountInfo, TicketCountInfoResponse>>();
            
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddSingleton<IEmailConfiguration>(config.GetSection("EmailConfiguration").Get<EmailConfiguration>());

            services.AddTransient<IEmailService, EmailService>();
            services.AddHttpContextAccessor();
            return services;
        }
    }
}