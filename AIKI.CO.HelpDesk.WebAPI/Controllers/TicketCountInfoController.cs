using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using AutoMapper;
using Microsoft.Extensions.Options;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    public class TicketCountInfoController: BaseRApiController<TicketCountInfo, TicketCountInfoResponse>
    {
        public TicketCountInfoController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<TicketCountInfo, TicketCountInfoResponse> service) : base(map, appSettings, service)
        {
        }
    }
}