using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    public class AssetsViewController : BaseRApiController<AssetsView, AssetsViewResponse>
    {
        public AssetsViewController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<AssetsView, AssetsViewResponse> service) : base(map, appSettings,
            service, true)
        {
        }
    }
}