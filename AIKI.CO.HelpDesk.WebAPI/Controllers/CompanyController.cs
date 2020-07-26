using System.Collections.Generic;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using AutoMapper;
using MailKit;
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
        private readonly IEmailService _emailService;

        public CompanyController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            ICompanyService service,
            IService<Member, MemberResponse> memberService,
            IEmailService emailService)
        {
            _map = map;
            _appSettings = appSettings.Value;
            _service = service;
            _memberService = memberService;
            _emailService = emailService;
        }

        [HttpPost]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CompanyResponse request)
        {
            if (!ModelState.IsValid) return BadRequest(new {model=ModelState, message="خطا در ویرایش اطلاعات"});
            var duplicateRecord = await _memberService.GetSingle(q => q.username == request.email);
            if (duplicateRecord != null) return BadRequest("نام کاربری تکراری است");
            var result = await _service.AddRecord(request);
            var adminUser = new MemberResponse
            {
                membername = "مدیریت",
                password = new Password(includeLowercase: true, includeUppercase: true, includeNumeric: true, includeSpecial: true, passwordLength: 21).Next(),
                roles = "admin",
                email = request.email,
                companyid = request.id,
                allowdelete = false,
                disabled = false
            };
            await _memberService.AddRecord(adminUser, request.id);
            _emailService.Send(new EmailMessage
            {
                Subject = "میز کار خدمات رایانه ای AiKi",
                Content = $"<p dir='rtl' style='font-family:tahoma'> با سلام </br> رمز عبور شما جهت ورود به میزکار خدمات رایانه ای عبارت است از: <span dir='ltr'><b>{adminUser.password}</b></span> <br/> جهت ورود <a href='https://aiki-helpdesk-v1.firebaseapp.com/'>اینجا</a> کلیک نمایید</p>",
                FromAddresses = new List<EmailAddress>()
                    {new EmailAddress() {Name = "Mohammad Mehrnia", Address = "qermezkon@gmail.com"}},
                ToAddresses = new List<EmailAddress>()
                    {new EmailAddress() {Name = request.title, Address = request.email}},
            });
            return CreatedAtAction(nameof(Post), request);
        }
    }
}