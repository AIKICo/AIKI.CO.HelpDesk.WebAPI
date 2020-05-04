using System.ComponentModel.DataAnnotations;

namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public sealed class AuthenticateModel
    {
        [Required] public string Username { get; set; }

        [Required] public string Password { get; set; }
    }
}