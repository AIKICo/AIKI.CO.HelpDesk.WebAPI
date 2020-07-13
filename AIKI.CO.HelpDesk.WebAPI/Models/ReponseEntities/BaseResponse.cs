using System;

namespace AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities
{
    public class BaseResponse
    {
        public Guid id { get; set; }
        public bool? allowdelete { get; set; }
    }
}