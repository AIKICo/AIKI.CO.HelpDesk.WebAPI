using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration
{
    public class AppConstantConfiguration : BaseConfiguration<AppConstant>, IEntityTypeConfiguration<AppConstant>
    {
        public AppConstantConfiguration(Guid companyid) : base(companyid)
        {
        }

        public override void Configure(EntityTypeBuilder<AppConstant> builder)
        {
            base.Configure(builder);

            builder.HasKey(c => new {c.id, c.companyid});
            builder.Property(c => c.id)
                .ValueGeneratedNever();

            builder.HasOne(c => c.Company)
                .WithMany(c => c.AppConstants)
                .HasForeignKey(c => c.companyid);
        }
    }
}