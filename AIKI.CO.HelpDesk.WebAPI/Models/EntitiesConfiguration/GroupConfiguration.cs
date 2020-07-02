using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration
{
    public sealed class GroupConfiguration : BaseConfiguration<Entities.Group>, IEntityTypeConfiguration<Entities.Group>
    {
        public GroupConfiguration(Guid companyid) : base(companyid)
        {
        }

        public override void Configure(EntityTypeBuilder<Entities.Group> builder)
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