using System;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpPost]
        [Authorize(Roles = "admin, backupuser")]
        [Produces("application/json")]
        public override Task<IActionResult> Post(AssetResponse request)
        {
            return base.Post(request);
        }

        [HttpPut]
        [Authorize(Roles = "admin, backupuser")]
        [Produces("application/json")]
        public override Task<IActionResult> Put(AssetResponse request)
        {
            return base.Put(request);
        }

        [HttpPatch("{id:guid}")]
        [Authorize(Roles = "admin, backupuser")]
        public override Task<IActionResult> Patch(Guid id, JsonPatchDocument<AssetResponse> patchDoc)
        {
            return base.Patch(id, patchDoc);
        }
        
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "admin, backupuser")]
        public override Task<IActionResult> Delete(Guid id)
        {
            return base.Delete(id);
        }
    }
}