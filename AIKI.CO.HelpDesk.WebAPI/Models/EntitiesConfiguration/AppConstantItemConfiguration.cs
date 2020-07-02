using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration
{
    public class AppConstantItemConfiguration : BaseConfiguration<AppConstantItem>,
        IEntityTypeConfiguration<AppConstantItem>
    {
        public AppConstantItemConfiguration(Guid companyid) : base(companyid)
        {
        }

        public override void Configure(EntityTypeBuilder<AppConstantItem> builder)
        {
            builder.HasKey(c => new {c.id, c.companyid});
            builder.Property(c => c.id)
                .ValueGeneratedNever();

            builder.HasQueryFilter(c => c.companyid == _companyid);

            builder.Property(d => d.additionalinfo).HasColumnType("jsonb");
            builder.HasOne(d => d.Company)
                .WithMany(p => p.AppConstantItems)
                .HasForeignKey(d => d.companyid);

            builder.HasOne(d => d.AppConstant)
                .WithMany(p => p.AppConstantItems)
                .HasForeignKey(d => new {d.appconstantid, d.companyid});
        }
    }
}