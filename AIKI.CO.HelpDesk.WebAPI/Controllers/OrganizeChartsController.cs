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
        
        [HttpGet("GetByCompanyId/{id:guid}")]
        public async Task<IActionResult> GetByCompanyId(Guid id)
        {
            var result = await _service.GetAll(predicate: q=>q.companyid == id);
            if (result != null)
                return Ok(result);
            else return NotFound();
        }
    }
}