using System;

namespace AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities
{
    public class ProfilePictureResponse:BaseResponse
    {
        public Guid memberid { get; set; }
        public byte[] filecontent { get; set; }
        public string filextension { get; set; }
    }
}