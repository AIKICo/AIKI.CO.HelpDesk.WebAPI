using System;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;

namespace AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration
{
    public class TicketCountInfoConfiguration: BaseViewConfiguration<TicketCountInfo>
    {
        public TicketCountInfoConfiguration(Guid companyid) : base(companyid)
        {
        }
    }
}