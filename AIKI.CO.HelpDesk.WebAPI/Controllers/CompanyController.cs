using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PasswordGenerator;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CompanyController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IMapper _map;
        private readonly ICompanyService _service;
        private readonly IService<Member, MemberResponse> _memberService;

        public CompanyController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            ICompanyService service,
            IService<Member, MemberResponse> memberService)
        {
            _map = map;
            _appSettings = appSettings.Value;
            _service = service;
            _memberService = memberService;
        }
        
        [HttpPost]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CompanyResponse request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var duplicateRecord = await _memberService.GetSingle(q => q.username == request.email);
            if (duplicateRecord != null) return BadRequest("نام کاربری تکراری است");
            var result = await _service.AddRecord(request);
            await _memberService.AddRecord(new MemberResponse
            {
                membername = "مدیر",
                username =  request.email,
                password = new Password().Next(),
                roles = "admin",
                email = request.email,
                companyid = request.id,
                allowdelete = false
            });
            return CreatedAtAction(nameof(Post), request);
        }
    }
}