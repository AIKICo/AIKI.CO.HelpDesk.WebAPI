using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.CustomActionFilters;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Services.ServiceConfiguration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasswordGenerator;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CompanyController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IService<Member, MemberResponse> _memberService;
        private readonly ICompanyService _service;

        public CompanyController(
            ICompanyService service,
            IService<Member, MemberResponse> memberService,
            IEmailService emailService)
        {
            _service = service;
            _memberService = memberService;
            _emailService = emailService;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [AllowAnonymous]
        [ModelValidation]
        public async Task<IActionResult> Post([FromBody] CompanyResponse request)
        {
            var duplicateRecord = await _service.GetSingle(q => q.email == request.email);
            if (duplicateRecord != null) return Conflict("آدرس پست الکترونیک تکراری است");

            duplicateRecord = await _service.GetSingle(q => q.subdomain == request.subdomain);
            if (duplicateRecord != null) return Conflict("عنوان زیر دامنه تکراری است");

            var result = await _service.AddRecord(request);
            if (result == null) return NotFound("شرکت شما به ثبت نرسید");
            var adminUser = new MemberResponse
            {
                membername = "مدیریت",
                password = new Password(true, true, true,
                    true, 21).Next(),
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
                Content =
                    $"<p dir='rtl' style='font-family:tahoma'> با سلام </br> رمز عبور شما جهت ورود به میزکار خدمات رایانه ای عبارت است از: <span dir='ltr'><b>{adminUser.password}</b></span> <br/> جهت ورود <a href='https://aiki-helpdesk-v1.firebaseapp.com/'>اینجا</a> کلیک نمایید</p>",
                FromAddresses = new List<EmailAddress>
                    {new EmailAddress {Name = "Mohammad Mehrnia", Address = "qermezkon@gmail.com"}},
                ToAddresses = new List<EmailAddress> {new EmailAddress {Name = request.title, Address = request.email}}
            });
            return CreatedAtAction(nameof(Post), request);
        }
    }
}