using System;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;

namespace AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration
{
    public class Last30TicketConfiguration : BaseViewConfiguration<Last30Ticket>
    {
        public Last30TicketConfiguration(Guid companyid) : base(companyid)
        {
        }
    }
}