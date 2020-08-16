using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.BuilderExtensions;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    [Authorize(Roles = "admin, backupuser")]
    public class AppConstantItemsController : BaseCRUDApiController<AppConstantItem, AppConstantItemResponse>
    {
        public AppConstantItemsController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<AppConstantItem, AppConstantItemResponse> service) : base(map, appSettings, service)
        {
        }

        [HttpGet]
        [Authorize(Roles = "admin, backupuser")]
        public override async Task<IActionResult> Get()
        {
            return await base.Get();
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "admin, backupuser")]
        public override Task<IActionResult> Get(Guid id)
        {
            return base.Get(id);
        }

        [Authorize(Roles = "admin, backupuser")]
        [HttpGet("GetByParentId/{id:guid}")]
        public async Task<IActionResult> GetByParentId([FromRoute]Guid id)
        {
            var result = await _service.GetAll(q => q.appconstantid == id);
            if (result != null)
                return Ok(result);
            else return NotFound();
        }
        
        [HttpPost]
        [Authorize(Roles = "admin, backupuser")]
        [Produces("application/json")]
        public override Task<IActionResult> Post(AppConstantItemResponse request)
        {
            return base.Post(request);
        }

        [HttpPut]
        [Authorize(Roles = "admin, backupuser")]
        [Produces("application/json")]
        public override Task<IActionResult> Put(AppConstantItemResponse request)
        {
            return base.Put(request);
        }

        [HttpPatch("{id:guid}")]
        [Authorize(Roles = "admin, backupuser")]
        public override Task<IActionResult> Patch(Guid id, JsonPatchDocument<AppConstantItemResponse> patchDoc)
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