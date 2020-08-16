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
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    [Authorize(Roles = "admin")]
    public class SLASettingController : BaseCRUDApiController<SLASetting, SLASettingResponse>
    {
        public SLASettingController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<SLASetting, SLASettingResponse> service) : base(map, appSettings, service)
        {
        }

        [HttpGet("GetByCustomerID/{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetByCustomerID(Guid id)
        {
            var customerInfo = await _service.GetAnotherTableRecords<Customer, CustomerResponse>(q => q.id == id);
            if (customerInfo == null) return NotFound();
            var slaSetting = await _service.GetSingle(q => q.id == customerInfo.FirstOrDefault().operatinghourid);
            if (slaSetting == null) return NotFound();
            return Ok(slaSetting);
        }
        
        [HttpPost]
        [Authorize(Roles = "admin")]
        [Produces("application/json")]
        public override Task<IActionResult> Post(SLASettingResponse request)
        {
            return base.Post(request);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        [Produces("application/json")]
        public override Task<IActionResult> Put(SLASettingResponse request)
        {
            return base.Put(request);
        }

        [HttpPatch("{id:guid}")]
        [Authorize(Roles = "admin")]
        public override Task<IActionResult> Patch(Guid id, JsonPatchDocument<SLASettingResponse> patchDoc)
        {
            return base.Patch(id, patchDoc);
        }
        
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "admin")]
        public override Task<IActionResult> Delete(Guid id)
        {
            return base.Delete(id);
        }
    }
}