using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.DataProtection;

namespace AIKI.CO.HelpDesk.WebAPI.Extensions
{
    public static class ExtensionMethods
    {
        public static IEnumerable<MemberResponse> WithoutPasswords(this IEnumerable<MemberResponse> members)
        {
            return members.Select(x => x.WithoutPassword());
        }

        public static IEnumerable<MemberResponse> WithoutCompanyIds(this IEnumerable<MemberResponse> members)
        {
            return members.Select(x => x.WithoutCompanyId());
        }
        public static MemberResponse WithoutPassword(this MemberResponse user)
        {
            user.password = string.Empty;
            return user;
        }
        
        public static MemberResponse WithoutCompanyId(this MemberResponse user)
        {
            user.companyid = null;
            return user;
        }
    }
}