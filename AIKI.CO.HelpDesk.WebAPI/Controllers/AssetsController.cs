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
    public class AssetsController : BaseCRUDApiController<Asset, AssetResponse>
    {
        public AssetsController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<Asset, AssetResponse> service) : base(map, appSettings, service)
        {
        }

        [HttpGet("isAssetExists/{id}")]
        public async Task<IActionResult> isAssetExists([FromRoute]string id)
        {
            return Ok(await _service.isExists(q => q.assetnumber == id));
        }
    }
}