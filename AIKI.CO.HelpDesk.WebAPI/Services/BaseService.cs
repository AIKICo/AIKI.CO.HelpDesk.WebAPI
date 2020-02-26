using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
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
    public class BaseService<T, V>:IService<T, V> 
        where T:BaseObject
        where V:BaseResponse
    {
        protected readonly AppSettings _appSettings;
        protected readonly IUnitOfWork _unitofwork;
        protected readonly IMapper _map;

        public BaseService(
            IMapper map,
            IUnitOfWork unitofwork, 
            IOptions<AppSettings> appSettings)
        {
            _map = map;
            _appSettings = appSettings.Value;
            _unitofwork = unitofwork;
        }

        public virtual async Task<IEnumerable<V>> GetAll()
        {
            return _map.Map<IEnumerable<V>>(await _unitofwork.GetRepository<T>().GetAllAsync());
        }
    }
}
