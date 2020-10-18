using System;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.DTO;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    public class AssetsController : BaseCRUDApiController<Asset, AssetResponse>
    {
        public AssetsController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<Asset, AssetResponse> service,
            IStringLocalizer<AssetsController> localizer) : base(map, appSettings, service, localizer)
        {
        }

        [HttpGet("isAssetExists/{id}")]
        public async Task<IActionResult> isAssetExists([FromRoute] string id)
            => Ok(await _service.isExists(q => q.assetnumber == id));

        [HttpPost]
        [Authorize(Roles = "admin, backupuser")]
        [Produces("application/json")]
        public override async Task<IActionResult> Post(AssetResponse request)
            => await base.Post(request);

        [HttpPut]
        [Authorize(Roles = "admin, backupuser")]
        [Produces("application/json")]
        public override async Task<IActionResult> Put(AssetResponse request)
            => await base.Put(request);

        [HttpPatch("{id:guid}")]
        [Authorize(Roles = "admin, backupuser")]
        public override async Task<IActionResult> Patch(Guid id, JsonPatchDocument<AssetResponse> patchDoc)
            => await base.Patch(id, patchDoc);

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "admin, backupuser")]
        public override async Task<IActionResult> Delete(Guid id)
            => await base.Delete(id);
    }
}