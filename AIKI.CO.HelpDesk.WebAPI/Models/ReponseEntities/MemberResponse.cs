using System.ComponentModel.DataAnnotations.Schema;

namespace AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities
{
    public sealed class MemberResponse : BaseResponse
    {
        [NotMapped]
        public string encryptedCompnayId { get; set; }
        public string membername { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string roles { get; set; }
        public string email { get; set; }
        public string token { get; set; }
    }
}