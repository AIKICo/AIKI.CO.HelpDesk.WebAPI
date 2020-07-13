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
    public class
        OrganizeChartsJsonViewController : BaseRApiController<OrganizeCharts_JsonView, OrganizeCharts_JsonViewResponse>
    {
        public OrganizeChartsJsonViewController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<OrganizeCharts_JsonView, OrganizeCharts_JsonViewResponse> service) : base(map, appSettings,
            service)
        {
        }

        [HttpGet("GetByCompanyId")]
        public async Task<IActionResult> GetByCompanyId()
        {
            var parent = await _service.GetSingle<OrganizeChart>(q => q.parent_id == null);
            if (parent != null)
            {
                var result = _service.GetRawSQL("SELECT * FROM organizecharts_jsonview({0})", parent.id);
                if (result != null)
                    return Ok(result);
                else return NotFound();
            }
            else
            {
                return BadRequest();
            }
        }
        
        [HttpGet("GetByCustomerId/{id}")]
        public async Task<IActionResult>GetByCustomerId([FromRoute] string id)
        {
            var result =  await _service.GetRawSQL("SELECT * FROM organizecharts_jsonview({0})", Guid.Parse(id));
            if (result != null)
                return Ok(result);
            else return NotFound();
        }
    }
}