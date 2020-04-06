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

namespace AIKI.CO.HelpDesk.WebAPI.BuilderExtensions
{
    public static class AddTransientExtensions
    {
        public static  IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IMemberService, MemberService>();
            services.AddTransient<IService<Customer, CustomerResponse>, BaseService<Customer, CustomerResponse>>();
            services.AddTransient<IMemberService, MemberService>();
            services.AddTransient<IService<OperatingHour, OperatingHoureResponse>, BaseService<OperatingHour, OperatingHoureResponse>>();
            services.AddTransient<IService<SLASetting, SLASettingResponse>, BaseService<SLASetting, SLASettingResponse>>();
            services.AddTransient<IJWTService, JWTService>();

            services.AddHttpContextAccessor();
            return services;
        }
    }
}
