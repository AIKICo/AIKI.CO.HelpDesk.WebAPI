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
            => Ok(await _service.GetAll(q => q.customerid == id));


        [Authorize(Roles = "admin")]
        public override async Task<IActionResult> Put(OrganizeChartResponse request)
            => await base.Put(request);

        [Authorize(Roles = "admin")]
        public override async Task<IActionResult> Patch(Guid id, JsonPatchDocument<OrganizeChartResponse> patchDoc)
            => await base.Patch(id, patchDoc);

        [Authorize(Roles = "admin")]
        public override async Task<IActionResult> Delete(Guid id)
            => await base.Delete(id);
    }
}