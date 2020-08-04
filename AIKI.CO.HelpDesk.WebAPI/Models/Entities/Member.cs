using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public sealed class Member : BaseObject
    {
        public Member()
        {
            ProfilePictures = new HashSet<ProfilePicture>();
        }
        public string membername { get; set; }
        public string username { get; set; }

        [Encrypted] public string password { get; set; }
        public string roles { get; set; }
        public string email { get; set; }
        public bool? disabled { get; set; }
        public Company Company { get; set; }
        
        public ICollection<ProfilePicture> ProfilePictures { get; set; }

    }
}