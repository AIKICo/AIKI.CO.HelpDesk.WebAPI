namespace AIKI.CO.HelpDesk.WebAPI.Services.Interface
{
    public interface IJWTService
    {
        string GenerateSecurityToken(string Id);
    }
}