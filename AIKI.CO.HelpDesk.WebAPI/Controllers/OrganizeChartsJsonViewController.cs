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
        OrganizeChartsJsonViewController : BaseRApiController<OrganizeChartView, OrganizeChartViewResponse>
    {
        public OrganizeChartsJsonViewController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<OrganizeChartView, OrganizeChartViewResponse> service) : base(map, appSettings,
            service)
        {
        }


        [HttpGet("GetByCustomerId/{id}")]
        public async Task<IActionResult> GetByCustomerId([FromRoute] Guid id)
        {
            var result = await _service.GetSingle(q => q.customerid == id);
            if (result != null)
                return Ok(result);
            else return NotFound();
        }
    }
}