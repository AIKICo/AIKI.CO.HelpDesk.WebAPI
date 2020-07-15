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
    public class OrganizeChartsController : BaseCRUDApiController<OrganizeChart, OrganizeChartResponse>
    {
        public OrganizeChartsController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<OrganizeChart, OrganizeChartResponse> service) : base(map, appSettings, service)
        {
        }

        [HttpGet("GetByCustomerId/{id}")]
        public async Task<IActionResult> GetByCustomerId(Guid id)
        {
            return Ok(await _service.GetAll(q => q.customerid == id));
        }


        [Authorize(Roles = "admin")]
        public override Task<IActionResult> Put(OrganizeChartResponse request)
        {
            return base.Put(request);
        }

        [Authorize(Roles = "admin")]
        public override Task<IActionResult> Patch(Guid id, JsonPatchDocument<OrganizeChartResponse> patchDoc)
        {
            return base.Patch(id, patchDoc);
        }

        [Authorize(Roles = "admin")]
        public override Task<IActionResult> Delete(Guid id)
        {
            return base.Delete(id);
        }
    }
}