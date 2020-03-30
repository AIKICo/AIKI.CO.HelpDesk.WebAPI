﻿using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Services
{
    public class SLASettingService : BaseService<SLASetting, SLASettingResponse>, IService<SLASetting, SLASettingResponse>
    {
        public SLASettingService(
            IMapper map,
            IUnitOfWork unitofwork,
            IOptions<AppSettings> appSettings) : base(map, unitofwork, appSettings)
        {

        }
    }
}
