using System;

namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public class ProfilePicture:BaseObject
    {
        public Guid memberid { get; set; }
        public byte[] filecontent { get; set; }
        public string filextension { get; set; }
        
        public Company Company { get; set; }
        public Member Member { get; set; }
    }
}