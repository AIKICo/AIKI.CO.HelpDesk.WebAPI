using System.Data;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    [Authorize(Roles = "admin")]
    public sealed class CustomersController : BaseCRUDApiController<Customer, CustomerResponse>
    {
        public CustomersController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<Customer, CustomerResponse> service) : base(map, appSettings, service)
        {
        }
    }
}