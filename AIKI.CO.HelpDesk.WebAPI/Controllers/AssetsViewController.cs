using System;
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
    public class AssetsViewController : BaseRApiController<AssetsView, AssetsViewResponse>
    {
        public AssetsViewController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<AssetsView, AssetsViewResponse> service,
            IStringLocalizer<AssetsViewController> localizer) : base(map, appSettings, service, localizer)
        {
        }

        [HttpGet("GetByCustomerId/{id:guid}")]
        public async Task<IActionResult> GetByCustomerId([FromRoute] Guid id)
            => Ok(await _service.GetAll(q => q.customerid == id));
    }
}