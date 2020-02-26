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
    public class CustomersController : BaseApiController
    {
        private readonly IService<Customer, CustomerResponse, Guid> _service;
        public CustomersController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<Customer, CustomerResponse, Guid> service) : base(map, appSettings)
        {
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

        public async Task<IActionResult> Post([FromBody] CustomerResponse request)
        {
            var result = await _service.AddRecord(request);
            if (result > 0)
                return CreatedAtAction(nameof(Post), request);
            else return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CustomerResponse request)
        {
            var existsRecord = await _service.GetById(request.id);
            if (existsRecord != null)
            {
                var result = await _service.UpdateRecord(request);
                if (result > 0) return Ok(request);
                else return BadRequest(ModelState);
            }
            else return NotFound(request);
        }

        [HttpPatch]
        public IActionResult Patch([FromBody] JsonPatchDocument<CustomerResponse> patchDoc)
        {
            if (patchDoc != null)
            {
                var customer = new CustomerResponse();
                patchDoc.ApplyTo(customer, ModelState);
                if (!ModelState.IsValid) return BadRequest(ModelState);
                return new ObjectResult(customer);
            }
            else return BadRequest(ModelState);
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