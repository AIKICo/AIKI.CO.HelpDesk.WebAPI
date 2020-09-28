using System;
using System.Net;
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
    public class TicketHistoryController : BaseCRUDApiController<TicketHistory, TicketHistoryResponse>
    {
        public TicketHistoryController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<TicketHistory, TicketHistoryResponse> service) : base(map, appSettings, service)
        {
        }

        [HttpGet("TicketHistoryByTicketID/{id:guid}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> TicketHistoryByTicketID([FromRoute] Guid id)
        {
            var response = await _service.GetAll(q => q.ticketid == id);
            if (response != null)
                return Ok(response);
            return BadRequest();
        }
    }
}