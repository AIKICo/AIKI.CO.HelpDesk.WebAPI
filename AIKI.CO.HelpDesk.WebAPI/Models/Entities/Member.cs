using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public partial class Member:BaseObject
    {
        public string membername { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string roles { get; set; }
        public string email { get; set; }
        
        public virtual Company Company { get; set; }

        [NotMapped]
        public string token { get; set; }
    }
}
