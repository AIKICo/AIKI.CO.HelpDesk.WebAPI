using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    public class OrganizeCharts_JsonViewController : BaseCRUDApiController<OrganizeCharts_JsonView, OrganizeCharts_JsonViewResponse>
    {
        public OrganizeCharts_JsonViewController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<OrganizeCharts_JsonView, OrganizeCharts_JsonViewResponse> service) : base(map, appSettings, service,isReadOnly:true)
        {
        }
    }
}