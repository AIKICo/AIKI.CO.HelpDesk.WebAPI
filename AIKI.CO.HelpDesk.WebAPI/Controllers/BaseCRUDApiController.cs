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
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Serilog;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class BaseCRUDApiController<T, V> : ControllerBase
        where T : BaseObject
        where V : BaseResponse
    {
        protected readonly AppSettings _appSettings;
        protected readonly IMapper _map;
        protected readonly IService<T, V> _service;
        protected bool _isReadOnly;

        public BaseCRUDApiController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<T, V> service, bool isReadOnly = false)
        {
            _map = map;
            _appSettings = appSettings.Value;
            _service = service;
            _isReadOnly = isReadOnly;
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

        [HttpPost]
        [Produces("application/json")]
        public virtual async Task<IActionResult> Post([FromBody] V request)
        {
            if (_isReadOnly) return BadRequest(new {message = "اطلاعات قابل ویرایش نیستند"});
            if (!ModelState.IsValid) return BadRequest(new {model=ModelState, message="خطا در ویرایش اطلاعات"});
            var result = await _service.AddRecord(request);
            if (result > 0)
                return CreatedAtAction(nameof(Post), request);
            return BadRequest(new {model=ModelState, message="خطا در ویرایش اطلاعات"});
        }

        [HttpPut]
        [Produces("application/json")]
        public virtual async Task<IActionResult> Put([FromBody] V request)
        {
            if (_isReadOnly) return BadRequest(new {message = "اطلاعات قابل ویرایش نیستند"});
            if (!ModelState.IsValid) return BadRequest(new {model=ModelState, message="خطا در ویرایش اطلاعات"});
            var existsRecord = await _service.GetSingle(q => q.id == request.id);
            if (existsRecord == null) return NotFound();
            var result = await _service.UpdateRecord(request);
            if (result > 0) return Ok(request);
            return BadRequest(new {model=ModelState, message="خطا در ویرایش اطلاعات"}); }

        [HttpPatch("{id:guid}")]
        public virtual async Task<IActionResult> Patch([FromRoute] Guid id, [FromBody] JsonPatchDocument<V> patchDoc)
        {
            if (_isReadOnly) return BadRequest(new {message = "اطلاعات قابل ویرایش نیستند"});
            if (patchDoc == null) return BadRequest(new {model=ModelState, message="خطا در ویرایش اطلاعات"});
            var founded = await _service.GetSingle(q => q.id == id);
            if (founded == null) return NotFound();

            var foundedToPatch = _map.Map<V>(founded);
            patchDoc.ApplyTo(foundedToPatch, ModelState);
            TryValidateModel(foundedToPatch);
            if (!ModelState.IsValid) return BadRequest(new {model=ModelState, message="خطا در ویرایش اطلاعات"});
            var result = await _service.PartialUpdateRecord(_map.Map(foundedToPatch, founded));
            if (result > 0)
                return Ok(_map.Map(foundedToPatch, founded));
            return  BadRequest(new {model=ModelState, message="خطا در ویرایش اطلاعات"});
        }

        [HttpDelete("{id:guid}")]
        public virtual async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (_isReadOnly) return BadRequest(new {model=ModelState, message="خطا در ویرایش اطلاعات"});
            var result = await _service.DeleteRecord(id);
            if (result > 0)
                return Ok(id);
            return BadRequest(new {model=ModelState, message="خطا در حذف اطلاعات"});
        }
    }
}