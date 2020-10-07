using System;
using System.Net;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    [Route("{culture:culture}/[controller]")]
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
        protected readonly IStringLocalizer<BaseRApiController<T, V>> _localizer;

        public BaseRApiController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<T, V> service,
            IStringLocalizer<BaseRApiController<T, V>> localizer)
        {
            _map = map ?? throw new NullReferenceException(nameof(map));
            _appSettings = appSettings.Value;
            _service = service ?? throw new NullReferenceException(nameof(service));
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public virtual async Task<IActionResult> Get()
            => Ok(await _service.GetAll());

        [HttpGet("{pageSize:int}/{pageIndex:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public virtual async Task<IActionResult> Get([FromRoute] int pageSize, [FromRoute] int pageIndex)
            => Ok(await _service.GetPagedList(pageSize: pageSize, pageIndex: pageIndex));

        [HttpGet("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public virtual async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await _service.GetSingle(q => q.id == id);
            if (result != null)
                return Ok(result);
            return NotFound();
        }
    }
}