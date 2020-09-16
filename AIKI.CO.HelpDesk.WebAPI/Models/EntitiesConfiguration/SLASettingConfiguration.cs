using System;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration
{
    public class SLASettingConfiguration : BaseConfiguration<SLASetting>, IEntityTypeConfiguration<SLASetting>
    {
        public SLASettingConfiguration(Guid companyid) : base(companyid)
        {
        }

        public override void Configure(EntityTypeBuilder<SLASetting> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.targetspriority).HasColumnType("jsonb");
            builder.Property(c => c.requesttypepriority).HasColumnType("jsonb");

            builder.HasOne(c => c.Company)
                .WithMany(c => c.SLASettings)
                .HasForeignKey(c => c.companyid);
        }
    }
}