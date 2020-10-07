using System;
using System.Net;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    public class TicketsViewController : BaseRApiController<TicketsView, TicketsViewResponse>
    {
        public TicketsViewController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<TicketsView, TicketsViewResponse> service,
            IStringLocalizer<TicketsViewController> localizer) : base(map, appSettings, service, localizer)
        {
        }

        [HttpGet]
        public override async Task<IActionResult> Get()
            => Ok(await _service.GetAll(q => q.enddate == null));

        [HttpGet("GetAll")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAll(q => q.registerdate.Year == DateTime.Now.Year));

        [HttpGet("GetAll/{id:guid}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll([FromRoute] Guid id)
            => Ok(await _service.GetAll(q => q.customerid == id && q.registerdate.Year == DateTime.Now.Year));
    }
}