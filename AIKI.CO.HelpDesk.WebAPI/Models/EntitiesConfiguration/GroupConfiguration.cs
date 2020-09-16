using System;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration
{
    public sealed class GroupConfiguration : BaseConfiguration<Group>, IEntityTypeConfiguration<Group>
    {
        public GroupConfiguration(Guid companyid) : base(companyid)
        {
        }

        public override void Configure(EntityTypeBuilder<Group> builder)
        {
            base.Configure(builder);

            builder.HasKey(c => c.id);
            builder.Property(c => c.id)
                .ValueGeneratedNever();

            builder.HasOne(c => c.Company)
                .WithMany(c => c.Groups)
                .HasForeignKey(c => c.companyid);
        }
    }
}