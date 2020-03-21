using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.Extensions.Options;

namespace AIKI.CO.HelpDesk.WebAPI.Services
{
    public sealed class CustomerService : BaseService<Customer, CustomerResponse>, IService<Customer, CustomerResponse>
    {
        public CustomerService(
            IMapper map,
            IUnitOfWork unitofwork,
            IOptions<AppSettings> appSettings) : base(map, unitofwork, appSettings)
        {

        }
    }
}
