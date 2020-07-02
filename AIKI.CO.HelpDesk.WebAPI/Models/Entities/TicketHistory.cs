using System;

namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public class TicketHistory : BaseObject
    {
        public Guid ticketid { get; set; }
        public DateTime historydate { get; set; }
        public string historycomment { get; set; }
        public string agentname { get; set; }

        public Company Company { get; set; }
        public Ticket Ticket { get; set; }
    }
}