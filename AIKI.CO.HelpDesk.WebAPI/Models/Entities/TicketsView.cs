using System;
using Microsoft.OData.Edm;

namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public class TicketsView : BaseObject
    {
        public DateTime registerdate { get; set; }
        public DateTime? enddate { get; set; }
        public string description { get; set; }
        public string tickettype { get; set; }
        public string ticketcategory { get; set; }
        public string tickettags { get; set; }
        public string asset { get; set; }
        public string ticketfriendlynumber { get; set; }
        public string agentname { get; set; }
        public double? ticketrate { get; set; }
        public double? mandays { get; set; }
        public Guid? operateid { get; set; }
        public string requester { get; set; }
        public string requestpriority { get; set; }
        public string lasthistorycomment { get; set; }
        public Guid? customerid { get; set; }
        public string customertitle { get; set; }
    }
}