using System;

namespace AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities
{
    public class CompanyResponse
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public string email { get; set; }
    }
}