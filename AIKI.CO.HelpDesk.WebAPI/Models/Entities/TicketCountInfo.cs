using System;

namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public class TicketCountInfo: BaseObject
    {
        public Guid tickettype { get; set; }
        public int count { get; set; }
    }
}