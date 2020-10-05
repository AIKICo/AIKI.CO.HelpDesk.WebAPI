using System;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    [Authorize(Roles = "admin")]
    public class GroupsController : BaseCRUDApiController<Group, GroupResponse>
    {
        public GroupsController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<Group, GroupResponse> service,
            IStringLocalizer<GroupsController> localizer) : base(map, appSettings, service, localizer)
        {
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [Produces("application/json")]
        public override async Task<IActionResult> Post(GroupResponse request)
            => await base.Post(request);

        [HttpPut]
        [Authorize(Roles = "admin")]
        [Produces("application/json")]
        public override async Task<IActionResult> Put(GroupResponse request)
            => await base.Put(request);

        [HttpPatch("{id:guid}")]
        [Authorize(Roles = "admin")]
        public override async Task<IActionResult> Patch(Guid id, JsonPatchDocument<GroupResponse> patchDoc)
            => await base.Patch(id, patchDoc);

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "admin")]
        public override async Task<IActionResult> Delete(Guid id)
            => await base.Delete(id);
    }
}