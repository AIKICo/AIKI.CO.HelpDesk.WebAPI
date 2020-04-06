using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    public class MembersController : BaseCRUDApiController<Member, MemberResponse>
    {
        public MembersController(IMapper map,
            IOptions<AppSettings> appSettings,
            IService<Member, MemberResponse> service) : base(map, appSettings, service)
        {

        }
    }
}