using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Models.Entities
{
    public class Ticket:BaseObject
    {
        public DateTime registerdate { get; set; }
        public DateTime? enddate { get; set; }
        public string description { get; set; }
        public Guid tickettype { get; set; }
        public Guid ticketcategory { get; set; }
        public Guid tickettags { get; set; }
        public Guid? asset { get; set; }
        public Company Company { get; set; }
    }
}
