using System;
using System.ComponentModel.DataAnnotations;

namespace AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities
{
    public class TicketHistoryResponse : BaseResponse
    {
        public Guid ticketid { get; set; }
        public string historydate { get; set; }
        [Required]
        public string historycomment { get; set; }
        public string agentname { get; set; }
    }
}