using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Extensions
{
    public static class ExtensionMethods
    {
        public static IEnumerable<Member> WithoutPasswords(this IEnumerable<Member> members)
        {
            return members.Select(x => x.WithoutPassword());
        }

        public static Member WithoutPassword(this Member user)
        {
            user.password = string.Empty;
            return user;
        }
    }
}
