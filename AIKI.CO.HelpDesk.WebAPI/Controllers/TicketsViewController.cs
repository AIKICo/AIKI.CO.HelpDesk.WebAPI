using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    public class TicketsViewController: BaseRApiController<TicketsView, TicketsViewResponse>
    {
        public TicketsViewController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<TicketsView, TicketsViewResponse> service) : base(map, appSettings, service)
        {
        }

        [HttpGet]
        public override async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAll(q => q.enddate == null));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }
    }
}