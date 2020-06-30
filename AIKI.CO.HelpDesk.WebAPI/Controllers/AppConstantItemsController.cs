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
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AppConstantItemsController : BaseCRUDApiController<AppConstantItem, AppConstantItemResponse>
    {
        public AppConstantItemsController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<AppConstantItem, AppConstantItemResponse> service) : base(map, appSettings, service)
        {
        }

        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public override async Task<IActionResult> Get()
        {
            return await base.Get();
        }

        [Authorize(Roles = "admin, user")]
        [HttpGet("GetByParentId/{id:guid}")]
        public async Task<IActionResult> GetByParentId(Guid id)
        {
            var result = await _service.GetAll(q => q.appconstantid == id);
            if (result != null)
                return Ok(result);
            else return NotFound();
        }

        public override async Task<IActionResult> Put(AppConstantItemResponse request)
        {
            var itemInfo = await _service.GetById(request.id);
            if (itemInfo.allowdelete != null && !(bool)itemInfo.allowdelete) return BadRequest("Can not edit record");
            return await base.Put(request);
        }

        [HttpDelete("{id:guid}")]
        public override async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var itemInfo = await _service.GetById(id);
            if (itemInfo.allowdelete != null && !(bool)itemInfo.allowdelete) return BadRequest("Can not delete record");
            return await base.Delete(id);
        }
    }
}