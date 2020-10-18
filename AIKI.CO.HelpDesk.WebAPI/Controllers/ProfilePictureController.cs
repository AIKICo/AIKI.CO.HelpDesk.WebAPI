using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.DTO;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    [Authorize]
    public class ProfilePictureController : BaseCRUDApiController<ProfilePicture, ProfilePictureResponse>
    {
        public ProfilePictureController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<ProfilePicture, ProfilePictureResponse> service,
            IStringLocalizer<ProfilePictureController> localizer) : base(map, appSettings, service, localizer)
        {
        }

        [HttpPost(nameof(PostProfilePicture))]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostProfilePicture(
            IFormFile profilePic,
            [FromBody] ProfilePictureResponse request,
            CancellationToken cancellationToken)
        {
            if (!CheckIfExcelFile(profilePic)) return BadRequest(new {message = "پسوند فایل مورد تایید نمی باشد"});
            if (profilePic.Length <= 0) return BadRequest(new {message = "فایل حاوی محتوی نمی باشد"});
            await using var ms = new MemoryStream();
            await profilePic.CopyToAsync(ms, cancellationToken);
            var fileBytes = ms.ToArray();
            request.filecontent = fileBytes;
            return await base.Post(request);

        }

        private static bool CheckIfExcelFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return extension == ".jpg" || extension == ".png"; // Change the extension based on your need
        }
    }
}