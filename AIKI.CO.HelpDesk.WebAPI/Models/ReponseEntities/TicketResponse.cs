using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Models.ReponseEntities
{
    public class TicketResponse : BaseResponse
    {
        public DateTime? registerdate { get; set; }
        public DateTime? enddate { get; set; }
        [Required]
        public string description { get; set; }
        public Guid? tickettype { get; set; }
        public Guid? ticketcategory { get; set; }
        public Guid? tickettags { get; set; }
        public string asset { get; set; }
        public double? ticketrate { get; set; }
        public double? mandays { get; set; }
        public Guid? operateid { get; set; }
        public string requestpriority { get; set; }
        public Guid? customerid { get; set; }
        public Guid? requester { get; set; }
    }
}