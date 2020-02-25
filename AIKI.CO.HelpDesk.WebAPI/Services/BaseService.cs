using AIKI.CO.HelpDesk.WebAPI.Settings;
using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Services
{
    public class BaseService
    {
        protected readonly AppSettings _appSettings;
        protected readonly IUnitOfWork _unitofwork;

        public BaseService(IUnitOfWork unitofwork, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _unitofwork = unitofwork;
        }
    }
}
