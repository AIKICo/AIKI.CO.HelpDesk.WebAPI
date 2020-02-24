using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public class User
    {
        public Guid id { get; set; }
        public Guid companyid { get; set; }
        public string membername { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string roles { get; set; }
        public string email { get; set; }
        public string token { get; set; }
    }
}
