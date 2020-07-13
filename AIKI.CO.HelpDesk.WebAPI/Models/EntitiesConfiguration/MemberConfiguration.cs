using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration
{
    public sealed class MemberConfiguration : BaseConfiguration<Member>, IEntityTypeConfiguration<Member>
    {
        public MemberConfiguration(Guid companyid) : base(companyid)
        {
        }

        public override void Configure(EntityTypeBuilder<Member> builder)
        {
            base.Configure(builder);
            
            builder.Property(c => c.id)
                .ValueGeneratedNever();

            builder.HasIndex(c => c.username).IsUnique();
            builder.HasIndex(c => c.email).IsUnique();
            builder.HasOne(c => c.Company)
                .WithMany(c => c.Members)
                .HasForeignKey(c => c.companyid);
        }
    }
}