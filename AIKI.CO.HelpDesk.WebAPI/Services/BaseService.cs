using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using AIKI.CO.HelpDesk.WebAPI.Services.Interface;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using Arch.EntityFrameworkCore.UnitOfWork;
using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Services
{
    public class BaseService<T, V> : IService<T, V>
        where T : BaseObject
        where V : BaseResponse
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


        public virtual async Task<IEnumerable<V>> GetAll(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true, bool ignoreQueryFilters = false)
        {
            return _map.Map<IEnumerable<V>>(await _unitofwork.GetRepository<T>().GetAllAsync(predicate, orderBy, include, disableTracking, ignoreQueryFilters));
        }

        public virtual async Task<IPagedList<V>> GetPagedList(Expression<Func<T, bool>> predicate = null,
                                                           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                                           int pageIndex = 0,
                                                           int pageSize = 20,
                                                           bool disableTracking = true,
                                                           CancellationToken cancellationToken = default(CancellationToken),
                                                           bool ignoreQueryFilters = false)
        {
            return _map.Map<IPagedList<V>>(await _unitofwork.GetRepository<T>().GetPagedListAsync(predicate, orderBy, include, pageIndex, pageSize, disableTracking, cancellationToken, ignoreQueryFilters));
        }
        public virtual async Task<V> GetById(Guid id)
        {
            return _map.Map<V>(await _unitofwork.GetRepository<T>().FindAsync(id));
        }

        public async Task<int> AddRecord(V request)
        {
            await _unitofwork.GetRepository<T>().InsertAsync(_map.Map<T>(request));
            return await _unitofwork.SaveChangesAsync();
        }

        public async Task<int> UpdateRecord(V request)
        {
            _unitofwork.GetRepository<T>().Update(_map.Map<T>(request));
            return await _unitofwork.SaveChangesAsync();
        }

        public async Task<int> DeleteRecord(Guid id)
        {
            _unitofwork.GetRepository<T>().Delete(id);
            return await _unitofwork.SaveChangesAsync();
        }

        public async Task<int> PartialUpdateRecord(V request)
        {
            _unitofwork.GetRepository<T>().ChangeEntityState(_map.Map<T>(request), Microsoft.EntityFrameworkCore.EntityState.Modified);
            return await _unitofwork.SaveChangesAsync();
        }
    }
}
