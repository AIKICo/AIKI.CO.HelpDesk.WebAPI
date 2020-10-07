using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
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
    [Authorize(Roles = "admin, backupuser")]
    public class AppConstantItemsController : BaseCRUDApiController<AppConstantItem, AppConstantItemResponse>
    {
        public AppConstantItemsController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<AppConstantItem, AppConstantItemResponse> service,
            IStringLocalizer<AppConstantItemsController> localizer) : base(map, appSettings, service, localizer)
        {
        }

        [HttpGet]
        [Authorize(Roles = "admin, backupuser")]
        public override async Task<IActionResult> Get()
            => await base.Get();

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "admin, backupuser")]
        public override async Task<IActionResult> Get(Guid id)
            => await base.Get(id);

        [Authorize(Roles = "admin, backupuser")]
        [HttpGet("GetByParentId/{id:guid}")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetByParentId([FromRoute] Guid id)
        {
            var result = await _service.GetAll(q => q.appconstantid == id,
                q => q.OrderBy(c => c.value1).ThenBy(c => c.value2));
            if (result != null)
                return Ok(result);
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "admin, backupuser")]
        [Produces("application/json")]
        public override async Task<IActionResult> Post(AppConstantItemResponse request)
            => await base.Post(request);

        [HttpPut]
        [Authorize(Roles = "admin, backupuser")]
        [Produces("application/json")]
        public override async Task<IActionResult> Put(AppConstantItemResponse request)
            => await base.Put(request);

        [HttpPatch("{id:guid}")]
        [Authorize(Roles = "admin, backupuser")]
        public override async Task<IActionResult> Patch(Guid id, JsonPatchDocument<AppConstantItemResponse> patchDoc)
            => await base.Patch(id, patchDoc);

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "admin, backupuser")]
        public override async Task<IActionResult> Delete(Guid id)
            => await base.Delete(id);
    }
}