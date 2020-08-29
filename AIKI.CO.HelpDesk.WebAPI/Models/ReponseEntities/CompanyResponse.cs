using System;

namespace AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities
{
    public class CompanyResponse
    {
        public Guid id { get; set; }
        public Guid companyid { get; set; }
        public string title { get; set; }
        public string email { get; set; }
        public bool? allowdelete { get; set; }
        public string subdomain { get; set; }
    }
}