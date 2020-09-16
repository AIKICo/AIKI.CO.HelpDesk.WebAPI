using System;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration
{
    public class OperatingHourConfiguration : BaseConfiguration<OperatingHour>, IEntityTypeConfiguration<OperatingHour>
    {
        public OperatingHourConfiguration(Guid companyid) : base(companyid)
        {
        }

        public override void Configure(EntityTypeBuilder<OperatingHour> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.workdays).HasColumnType("jsonb");
            builder.Property(c => c.holidays).HasColumnType("jsonb");

            builder.HasOne(c => c.Company)
                .WithMany(c => c.OperatingsHour)
                .HasForeignKey(c => c.companyid);
        }
    }
}