using System;

namespace AIKI.CO.HelpDesk.WebAPI.Models.DTO
{
    public class BaseResponse
    {
        public Guid id { get; set; }
        public bool? allowdelete { get; set; }
    }
}