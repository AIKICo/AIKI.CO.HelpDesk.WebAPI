using System;

namespace AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities
{
    public class BaseResponse
    {
        public Guid id { get; set; }
        public Guid companyid { get; set; }
    }
}