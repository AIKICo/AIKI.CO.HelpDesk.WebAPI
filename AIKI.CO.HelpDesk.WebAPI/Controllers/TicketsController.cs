using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    public class TicketsController : BaseCRUDApiController<Ticket, TicketResponse>
    {
        public TicketsController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<Ticket, TicketResponse> service) : base(map, appSettings, service)
        {
        }

        [HttpGet("GetLast30Ticket")]
        public async Task<IActionResult> GetLast30Ticket()
        {
            return Ok(await _service.GetAnotherTableRecords<Last30Ticket, Last30TicketResponse>());
        }
    }
}