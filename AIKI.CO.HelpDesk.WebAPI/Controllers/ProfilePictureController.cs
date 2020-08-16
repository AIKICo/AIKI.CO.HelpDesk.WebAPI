using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    [Authorize]
    public class ProfilePictureController : BaseCRUDApiController<ProfilePicture, ProfilePictureResponse>
    {
        public ProfilePictureController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<ProfilePicture, ProfilePictureResponse> service) : base(map, appSettings, service)
        {
        }

        [HttpPost(nameof(PostProfilePicture))]
        public async Task<IActionResult> PostProfilePicture(
            IFormFile profilePic,
            [FromBody] ProfilePictureResponse request,
            CancellationToken cancellationToken)
        {
            if (CheckIfExcelFile(profilePic))
            {
                if (profilePic.Length > 0)
                {
                    await using var ms = new MemoryStream();
                    await profilePic.CopyToAsync(ms, cancellationToken);
                    var fileBytes = ms.ToArray();
                    request.filecontent = fileBytes;
                    return await base.Post(request);
                }
                else
                {
                    return BadRequest(new {message = "فایل حاوی محتوی نمی باشد"});
                }
            }
            else
            {
                return BadRequest(new {message = "پسوند فایل مورد تایید نمی باشد"});
            }
        }

        private bool CheckIfExcelFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension == ".jpg" || extension == ".png"); // Change the extension based on your need
        }
    }
}