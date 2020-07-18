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
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> GetByCustomerID(Guid id)
        {
            var customerInfo = await _service.GetAnotherTableRecords<Customer, CustomerResponse>(q => q.id == id);
            if (customerInfo == null) return NotFound();
            var slaSetting = await _service.GetSingle(q => q.id == customerInfo.FirstOrDefault().operatinghourid);
            if (slaSetting == null) return NotFound();
            return Ok(slaSetting);
        }
    }
}