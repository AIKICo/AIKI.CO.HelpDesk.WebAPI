using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.DTO;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;

namespace AIKI.CO.HelpDesk.WebAPI.Services.Interface
{
    public interface ICompanyService
    {
        Task<CompanyResponse> AddRecord(CompanyResponse request);
        Task<CompanyResponse> GetSingle(Expression<Func<Company, bool>> predicate, bool ignoreQueryFilters = false);
    }
}