using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    [Authorize]
    public sealed class UsersController : BaseCRUDApiController<Member, MemberResponse>
    {
        private readonly IMemberService _userService;

        public UsersController(
            IMemberService userService,
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<Member, MemberResponse> service) : base(map, appSettings, service)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public override async Task<IActionResult> Get()
        {
            return await base.Get();
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);
            if (user == null)
                return BadRequest(new {message = "Username or password is incorrect"});
            return Ok(user);
        }
        
        [AllowAnonymous]
        [HttpGet("IsEmailExists/{id}")]
        public async Task<IActionResult> IsEmailExists(string id)
        {
            return Ok(await _service.isExists(q => q.email == id));
        }
    }
}