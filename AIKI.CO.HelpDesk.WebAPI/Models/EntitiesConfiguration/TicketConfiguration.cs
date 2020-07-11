using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration
{
    public class TicketConfiguration : BaseConfiguration<Ticket>, IEntityTypeConfiguration<Ticket>
    {
        public TicketConfiguration(Guid companyid) : base(companyid)
        {
        }

        public override void Configure(EntityTypeBuilder<Ticket> builder)
        {
            base.Configure(builder);
            builder.Property(q => q.registerdate).IsRequired();
            builder.HasOne(c => c.Company)
                .WithMany(c => c.Tickets)
                .HasForeignKey(c => c.companyid);
            
            builder.HasOne(c => c.Customer)
                .WithMany(c => c.Tickets)
                .HasForeignKey(c => c.customerid);
        }
    }
}