using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Services.Interface
{
    public interface IMemberService:IService<Member, MemberResponse>
    {
        MemberResponse Authenticate(string username, string password);
    }
}
