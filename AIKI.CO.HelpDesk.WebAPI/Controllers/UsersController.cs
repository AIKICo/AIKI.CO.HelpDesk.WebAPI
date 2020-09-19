using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Extensions;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Services.ServiceConfiguration;
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
        private readonly IEmailService _emailService;
        private readonly IMemberService _userService;

        public UsersController(
            IMemberService userService,
            IMapper map,
            IOptions<AppSettings> appSettings,
            IMemberService service, IEmailService emailService) : base(map, appSettings, service)
        {
            _userService = userService;
            _emailService = emailService;
        }

        [HttpGet]
        public override async Task<IActionResult> Get()
            => await base.Get();

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);
            if (user == null)
                return BadRequest(new {message = "نام کاربری و یا کلمه عبور اشتباه وارد شده است"});
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpGet("IsEmailExists/{id}")]
        public async Task<IActionResult> IsEmailExists([FromRoute] string id)
            => Ok(await _service.isExists(q => q.email == id, true));

        [HttpGet("IsUserNameExists/{id}")]
        public async Task<IActionResult> IsUserNameExists([FromRoute] string id)
            => Ok(await _service.isExists(q => q.username == id, true));

        [HttpPut]
        [Produces("application/json")]
        public override async Task<IActionResult> Put(MemberResponse request)
        {
            if (_isReadOnly) return BadRequest(new {message = "اطلاعات قابل ویرایش نیستند"});
            if (!ModelState.IsValid) return BadRequest(new {model = ModelState, message = "خطا در ویرایش اطلاعات"});
            var existsRecord = await _userService.GetSingleWithPassword(q => q.id == request.id);
            if (existsRecord == null) return NotFound();
            if (string.IsNullOrEmpty(request.password)) request.password = existsRecord.password;
            var result = await _service.UpdateRecord(request);
            if (result > 0) return Ok(request.WithoutPassword().WithoutCompanyId());
            return BadRequest(new {model = ModelState, message = "خطا در ویرایش اطلاعات"});
        }

        [AllowAnonymous]
        [HttpGet("ResendPassword/{id}")]
        public async Task<IActionResult> ResendPassword([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("آدرس ایمیل را وارد نمایید");
            var userInfo = await _userService.GetSingleWithPassword(q => q.email == id, true);
            if (userInfo == null) return BadRequest(new {message = "آدرس ایمیل ثبت نشده است"});
            _emailService.Send(new EmailMessage
            {
                Subject = "میز کار خدمات رایانه ای AiKi",
                Content =
                    $"<p dir='rtl' style='font-family:tahoma'> با سلام </br> رمز عبور شما جهت ورود به میزکار خدمات رایانه ای عبارت است از: <span dir='ltr'><b>{userInfo.password}</b></span> <br/> جهت ورود <a href='https://aiki-helpdesk-v1.firebaseapp.com/'>اینجا</a> کلیک نمایید</p>",
                FromAddresses = new List<EmailAddress>
                    {new EmailAddress {Name = "Mohammad Mehrnia", Address = "qermezkon@gmail.com"}},
                ToAddresses = new List<EmailAddress>
                    {new EmailAddress {Name = userInfo.CompanyName, Address = userInfo.email}}
            });
            return Ok();
        }
    }
}