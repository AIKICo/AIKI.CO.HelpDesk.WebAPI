using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using AutoMapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    [Authorize(Roles = "admin")]
    public class OperationHoursController : BaseCRUDApiController<OperatingHour, OperatingHoureResponse>
    {
        public OperationHoursController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<OperatingHour, OperatingHoureResponse> service) : base(map, appSettings, service)
        {
        }
    }
}