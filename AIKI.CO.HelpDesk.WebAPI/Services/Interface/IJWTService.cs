using AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities;

namespace AIKI.CO.HelpDesk.WebAPI.Services.Interface
{
    public interface IJWTService
    {
        string GenerateSecurityToken(MemberResponse user);
    }
}