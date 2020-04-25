using System;
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
    public class OrganizeChartsController : BaseCRUDApiController<OrganizeChart, OrganizeChartResponse>
    {
        public OrganizeChartsController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<OrganizeChart, OrganizeChartResponse> service) : base(map, appSettings, service)
        {
        }
        
        [HttpGet("GetParentByCompanyId/{id:guid}")]
        public async Task<IActionResult> GetParentByCompanyId(Guid id)
        {
            var result = await _service.GetAll(predicate: q=> q.parent_id==null);
            if (result != null)
                return Ok(result);
            else return NotFound();
        }
        
        [HttpGet("GetChildByCompanyId/{id:guid}")]
        public async Task<IActionResult> GetChildByCompanyId(Guid id)
        {
            var result = await _service.GetAll(predicate: q=> q.parent_id==id );
            if (result != null)
                return Ok(result);
            else return NotFound();
        }
    }
}