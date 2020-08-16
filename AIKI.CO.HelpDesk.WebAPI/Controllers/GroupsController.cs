using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    [Authorize(Roles = "admin")]
    public class GroupsController : BaseCRUDApiController<Group, GroupResponse>
    {
        public GroupsController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<Group, GroupResponse> service) : base(map, appSettings, service)
        {
        }
        
        [HttpPost]
        [Authorize(Roles = "admin")]
        [Produces("application/json")]
        public override Task<IActionResult> Post(GroupResponse request)
        {
            return base.Post(request);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        [Produces("application/json")]
        public override Task<IActionResult> Put(GroupResponse request)
        {
            return base.Put(request);
        }

        [HttpPatch("{id:guid}")]
        [Authorize(Roles = "admin")]
        public override Task<IActionResult> Patch(Guid id, JsonPatchDocument<GroupResponse> patchDoc)
        {
            return base.Patch(id, patchDoc);
        }
        
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "admin")]
        public override Task<IActionResult> Delete(Guid id)
        {
            return base.Delete(id);
        }
    }
}