﻿using System;
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
    public class AppConstantItemsController : BaseCRUDApiController<AppConstantItem, AppConstantItemResponse>
    {
        public AppConstantItemsController(
            IMapper map,
            IOptions<AppSettings> appSettings,
            IService<AppConstantItem, AppConstantItemResponse> service) : base(map, appSettings, service)
        {
        }

        [HttpGet("GetByParentId/{id:guid}")]
        public async Task<IActionResult> GetByParentId(Guid id)
        {
            var result = await _service.GetAll(q => q.appconstantid == id);
            if (result != null)
                return Ok(result);
            else return NotFound();
        }
    }
}