using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Services.Interface
{
    public interface IService<T, V>
        where T : BaseObject
        where V : BaseResponse
    {
        Task<V> GetById(Guid id);
        Task<IEnumerable<V>> GetAll();

        Task<IEnumerable<V>> GetAll(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true,
            bool ignoreQueryFilters = false);

        Task<IList<V>> GetPagedList(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int pageIndex = 0,
            int pageSize = 20,
            bool disableTracking = true,
            CancellationToken cancellationToken = default(CancellationToken),
            bool ignoreQueryFilters = false);

        Task<V> GetSingle(Expression<Func<T, bool>> predicate, bool ignoreQueryFilters=false);
        Task<K> GetSingle<K>(Expression<Func<K, bool>> predicate, bool ignoreQueryFilters=false) where K : BaseObject;
        Task<List<T>> GetRawSQL(string sqlQuery, params object[] parameters);

        Task<IList<SR>> GetAnotherTableRecords<S, SR>(Expression<Func<S, bool>> predicate = null,
            Func<IQueryable<S>, IOrderedQueryable<S>> orderBy = null,
            Func<IQueryable<S>, IIncludableQueryable<S, object>> include = null, bool disableTracking = true,
            bool ignoreQueryFilters = false) where S : BaseObject where SR : BaseResponse;

        Task<bool> isExists(Expression<Func<T, bool>> predicate, bool ignoreQueryFilters = false);
        Task<int> AddRecord(V request, Guid? companyId = null);
        Task<V> AddRecordWithReturnRequest(V request);
        Task<int> UpdateRecord(V request);
        Task<int> PartialUpdateRecord(V request);
        Task<int> DeleteRecord(Guid id);
    }
}