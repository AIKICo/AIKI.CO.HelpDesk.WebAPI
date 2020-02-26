using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomersController : BaseApiController<Customer, CustomerResponse, Guid>
    {
        public CustomersController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<Customer, CustomerResponse, Guid> service) : base(map, appSettings, service)
        {
        }
    }
}