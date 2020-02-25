using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Services.Interface
{
    public interface IMemberService
    {
        Member Authenticate(string username, string password);
        Task<IEnumerable<Member>> GetAll();
    }
}
