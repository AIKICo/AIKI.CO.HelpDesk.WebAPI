using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;

namespace AIKI.CO.HelpDesk.WebAPI.Services.Interface
{
    public interface IMemberService : IService<Member, MemberResponse>
    {
        MemberResponse Authenticate(string username, string password);

        Task<MemberResponse> GetSingleWithPassword(Expression<Func<Member, bool>> predicate,
            bool ignoreQueryFilters = false);
    }
}