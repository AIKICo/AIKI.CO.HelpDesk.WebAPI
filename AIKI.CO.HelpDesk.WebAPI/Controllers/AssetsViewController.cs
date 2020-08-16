using System;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Raven.Client.Http;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    public class AssetsViewController : BaseRApiController<AssetsView, AssetsViewResponse>
    {
        public AssetsViewController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<AssetsView, AssetsViewResponse> service) : base(map, appSettings, service)
        {
        }
        
        [HttpGet("GetByCustomerId/{id:guid}")]
        public async Task<IActionResult> GetByCustomerId([FromRoute] Guid id)
        {
            return Ok(await _service.GetAll(q=>q.customerid==id));
        }
    }
}