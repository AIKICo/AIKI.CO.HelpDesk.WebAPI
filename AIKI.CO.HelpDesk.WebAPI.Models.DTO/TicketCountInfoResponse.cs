using System;

namespace AIKI.CO.HelpDesk.WebAPI.Models.DTO
{
    public class TicketCountInfoResponse : BaseResponse
    {
        public Guid tickettype { get; set; }
        public int count { get; set; }
    }
}