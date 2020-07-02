using System;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration
{
    public class TicketHistoryConfiguration : BaseConfiguration<TicketHistory>, IEntityTypeConfiguration<TicketHistory>
    {
        public TicketHistoryConfiguration(Guid companyid) : base(companyid)
        {
        }

        public override void Configure(EntityTypeBuilder<TicketHistory> builder)
        {
            base.Configure(builder);

            builder.HasOne(c => c.Company)
                .WithMany(c => c.TicketHistory)
                .HasForeignKey(c => c.companyid);

            builder.HasOne(c => c.Ticket)
                .WithMany(c => c.TicketHistories)
                .HasForeignKey(c => c.ticketid);
        }
    }
}