using AIKI.CO.HelpDesk.WebAPI.Models.DTO;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using AutoMapper;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    public class TicketCountInfoController : BaseRApiController<TicketCountInfo, TicketCountInfoResponse>
    {
        public TicketCountInfoController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<TicketCountInfo, TicketCountInfoResponse> service,
            IStringLocalizer<TicketCountInfoController> localizer) : base(map, appSettings, service, localizer)
        {
        }
    }
}