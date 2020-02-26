﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AIKI.CO.HelpDesk.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected readonly AppSettings _appSettings;
        private readonly IMapper _map;
        public BaseApiController(IMapper map, IOptions<AppSettings> appSettings)
        {
            _map = map;
            _appSettings = appSettings.Value;
        }
    }
}