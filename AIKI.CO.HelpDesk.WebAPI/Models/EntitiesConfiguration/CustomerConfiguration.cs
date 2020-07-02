using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration
{
    public sealed class CustomerConfiguration : BaseConfiguration<Customer>, IEntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration(Guid companyid) : base(companyid)
        {
        }

        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.description)
                .HasMaxLength(4000);

            builder.Property(e => e.disabled)
                .HasDefaultValueSql("false");

            builder.Property(e => e.domains)
                .HasMaxLength(1000);

            builder.Property(e => e.schema);

            builder.Property(e => e.title)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(d => d.Company)
                .WithMany(p => p.Customers)
                .HasForeignKey(d => d.companyid);
        }
    }
}