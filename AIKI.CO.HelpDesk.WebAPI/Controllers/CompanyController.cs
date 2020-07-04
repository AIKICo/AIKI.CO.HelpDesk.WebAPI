using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    public class CompanyController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IMapper _map;
        private readonly ICompanyService _service;
        
        public CompanyController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            ICompanyService service)
        {
            _map = map;
            _appSettings = appSettings.Value;
            _service = service;
        }
        
        [HttpPost]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CompanyResponse request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _service.AddRecord(request);
            if (result > 0)
                return CreatedAtAction(nameof(Post), request);
            return BadRequest(ModelState);
        }
    }
}