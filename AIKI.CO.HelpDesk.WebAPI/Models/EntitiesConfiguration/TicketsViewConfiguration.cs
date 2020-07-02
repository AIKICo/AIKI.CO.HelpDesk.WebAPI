using System;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;

namespace AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration
{
    public class TicketsViewConfiguration : BaseViewConfiguration<TicketsView>
    {
        public TicketsViewConfiguration(Guid companyid) : base(companyid)
        {
        }
    }
}