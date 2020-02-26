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
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BaseApiController<T,V> : ControllerBase 
        where T:BaseObject
        where V: BaseResponse
    {
        protected readonly AppSettings _appSettings;
        protected readonly IMapper _map;
        protected readonly IService<T, V> _service;
        public BaseApiController(
            IMapper map, 
            IOptions<AppSettings> appSettings,
            IService<T, V> service)
        {
            _map = map;
            _appSettings = appSettings.Value;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _service.GetById(id));
        }

        public async Task<IActionResult> Post([FromBody] V request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _service.AddRecord(request);
            if (result > 0)
                return CreatedAtAction(nameof(Post), request);
            else return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] V request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var existsRecord = await _service.GetById(request.id);
            if (existsRecord != null)
            {
                var result = await _service.UpdateRecord(request);
                if (result > 0) return Ok(request);
                else return BadRequest(ModelState);
            }
            else return NotFound(request);
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Patch([FromRoute] Guid id, [FromBody] JsonPatchDocument<V> patchDoc)
        {
            if (patchDoc == null) return BadRequest();
            var founded = await _service.GetById(id);
            if (founded == null) return NotFound();

            var foundedToPatch = _map.Map<V>(founded);
            patchDoc.ApplyTo(foundedToPatch, ModelState);
            TryValidateModel(foundedToPatch);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _service.PartialUpdateRecord(_map.Map(foundedToPatch, founded));
            if (result > 0)
                return Ok(_map.Map(foundedToPatch, founded));
            else return BadRequest();

        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteRecord(id);
            if (result > 0)
                return Ok(id);
            else
                return BadRequest();
        }

    }
}