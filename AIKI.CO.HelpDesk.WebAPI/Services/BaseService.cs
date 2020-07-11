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
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

// ReSharper disable All

namespace AIKI.CO.HelpDesk.WebAPI.Services
{
    public class BaseService<T, V> : IService<T, V>
        where T : BaseObject
        where V : BaseResponse
    {
        private readonly IHttpContextAccessor _context;
        protected readonly Guid _companyId;
        private readonly IDataProtector _protector;
        protected AppSettings _appSettings { get; private set; }
        protected IUnitOfWork _unitofwork { get; private set; }
        protected IRepository<T> _repository { get; private set; }
        protected IMapper _map { get; private set; }

        public BaseService(
            IMapper map,
            IUnitOfWork unitofwork,
            IOptions<AppSettings> appSettings,
            IHttpContextAccessor context,
            IDataProtectionProvider provider)
        {
            _map = map;
            _appSettings = appSettings.Value;
            _unitofwork = unitofwork;
            _repository = _unitofwork.GetRepository<T>();
            _context = context;
            _protector = provider.CreateProtector("MemberService.CompanyId");
            if (_context.HttpContext.Request.Headers["CompanyID"].Any())
                _companyId =
                    Guid.Parse(_protector.Unprotect(_context.HttpContext.Request.Headers["CompanyID"].ToString()));
        }

        public virtual async Task<IEnumerable<V>> GetAll()
        {
            return _map.Map<IEnumerable<V>>(await _repository.GetAllAsync());
        }


        public virtual async Task<IEnumerable<V>> GetAll(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true,
            bool ignoreQueryFilters = false)
        {
            return _map.Map<IEnumerable<V>>(await _repository.GetAllAsync(predicate, orderBy, include, disableTracking,
                ignoreQueryFilters));
        }

        public virtual async Task<IList<V>> GetPagedList(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int pageIndex = 0,
            int pageSize = 20,
            bool disableTracking = true,
            CancellationToken cancellationToken = default(CancellationToken),
            bool ignoreQueryFilters = false)
        {
            return _map.Map<IList<V>>((await _repository.GetPagedListAsync(predicate, orderBy,
                include, pageIndex, pageSize, disableTracking, cancellationToken, ignoreQueryFilters)).Items);
        }

        public virtual async Task<V> GetById(Guid id)
        {
            return _map.Map<V>(await _repository.FindAsync(id));
        }

        public virtual async Task<IList<SR>> GetAnotherTableRecords<S, SR>(
            Expression<Func<S, bool>> predicate = null,
            Func<IQueryable<S>, IOrderedQueryable<S>> orderBy = null,
            Func<IQueryable<S>, IIncludableQueryable<S, object>> include = null, bool disableTracking = true,
            bool ignoreQueryFilters = false) where S : BaseObject where SR : BaseResponse
        {
            return _map.Map<IList<SR>>(await _unitofwork.GetRepository<S>()
                .GetAllAsync(predicate, orderBy, include, disableTracking, ignoreQueryFilters));
        }

        public virtual async Task<bool> isExists(Expression<Func<T, bool>> predicate, bool ignoreQueryFilters = false)
        {
            return (await _repository.ExistsAsync(predicate, ignoreQueryFilters));
        }

        public virtual async Task<int> AddRecord(V request)
        {
            request.id = Guid.NewGuid();
            var record = _map.Map<T>(request);
            record.companyid = _companyId;
            await _repository.InsertAsync(record);
            return await _unitofwork.SaveChangesAsync();
        }

        public virtual async Task<V> AddRecordWithReturnRequest(V request)
        {
            request.id = Guid.NewGuid();
            var record = _map.Map<T>(request);
            record.companyid = _companyId;
            await _repository.InsertAsync();
            await _unitofwork.SaveChangesAsync();
            return request;
        }
        
        public virtual async Task<int> UpdateRecord(V request)
        {
            _repository.Update(_map.Map<T>(request));
            return await _unitofwork.SaveChangesAsync();
        }

        public virtual async Task<int> DeleteRecord(Guid id)
        {
            var founded = _map.Map<T>(await GetSingle(q => q.id == id));
            if (founded == null) return 0;
            if (founded.allowdelete==null || (bool)founded.allowdelete)
            {
                _repository.Delete(founded);
                return await _unitofwork.SaveChangesAsync();
            }
            else return 0;
        }

        public virtual async Task<int> PartialUpdateRecord(V request)
        {
            _repository.ChangeEntityState(_map.Map<T>(request), Microsoft.EntityFrameworkCore.EntityState.Modified);
            return await _unitofwork.SaveChangesAsync();
        }

        public virtual async Task<V> GetSingle(Expression<Func<T, bool>> predicate, bool ignoreQueryFilters=false)
        {
            return _map.Map<V>(await _repository.GetFirstOrDefaultAsync(predicate: predicate, ignoreQueryFilters:ignoreQueryFilters));
        }

        public virtual async Task<K> GetSingle<K>(Expression<Func<K, bool>> predicate, bool ignoreQueryFilters=false) where K : BaseObject
        {
            return _map.Map<K>(await _unitofwork.GetRepository<K>().GetFirstOrDefaultAsync(predicate: predicate, ignoreQueryFilters:ignoreQueryFilters));
        }

        public virtual async Task<List<T>> GetRawSQL(string sqlQuery, params object[] parameters)
        {
            return await _repository.FromSql(sqlQuery, parameters).ToListAsync();
        }
    }
}