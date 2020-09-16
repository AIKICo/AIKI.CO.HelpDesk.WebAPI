using System;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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
        private readonly AppSettings _appSettings;
        private readonly IMapper _map;
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
            => Ok(await _service.GetAll());

        [HttpGet("{pageSize:int}/{pageIndex:int}")]
        public virtual async Task<IActionResult> Get([FromRoute] int pageSize, [FromRoute] int pageIndex)
            => Ok(await _service.GetPagedList(pageSize: pageSize, pageIndex: pageIndex));

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