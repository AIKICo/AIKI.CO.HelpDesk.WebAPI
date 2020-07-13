using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using AutoMapper;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class BaseRApiController<T, V> : ControllerBase
        where T : BaseObject
        where V : BaseResponse
    {
        protected readonly AppSettings _appSettings;
        protected readonly IMapper _map;
        protected readonly IService<T, V> _service;

        public BaseRApiController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<T, V> service)
        {
            _map = map;
            _appSettings = appSettings.Value;
            _service = service;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{pageSize:int}/{pageIndex:int}")]
        public virtual async Task<IActionResult> Get([FromRoute] int pageSize, [FromRoute] int pageIndex)
        {
            return Ok(await _service.GetPagedList(pageSize: pageSize, pageIndex: pageIndex));
        }

        [HttpGet("{id:guid}")]
        public virtual async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await _service.GetSingle(q => q.id == id);
            if (result != null)
                return Ok(result);
            return NotFound();
        }
    }
}