using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities
{
    public sealed class MemberResponse : BaseResponse
    {
        [NotMapped] public string encryptedCompnayId { get; set; }
        public Guid? companyid { get; set; }
        [Required] public string membername { get; set; }
        [Required] public string username => email;
        public string password { get; set; }
        [Required] public string roles { get; set; }
        [Required] public string email { get; set; }
        public bool? disabled { get; set; }
        [NotMapped] public string token { get; set; }
        [NotMapped] public string CompanyName { get; set; }
    }
}