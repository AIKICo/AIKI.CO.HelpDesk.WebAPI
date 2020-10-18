
using AIKI.CO.HelpDesk.WebAPI.Models.DTO;

namespace AIKI.CO.HelpDesk.WebAPI.Services.Interface
{
    public interface IJWTService
    {
        string GenerateSecurityToken(MemberResponse user);
    }
}