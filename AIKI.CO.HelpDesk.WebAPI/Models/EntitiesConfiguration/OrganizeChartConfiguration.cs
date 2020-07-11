using System;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration
{
    public class OrganizeChartConfiguration : BaseConfiguration<OrganizeChart>, IEntityTypeConfiguration<OrganizeChart>
    {
        public OrganizeChartConfiguration(Guid companyid) : base(companyid)
        {
        }

        public override void Configure(EntityTypeBuilder<OrganizeChart> builder)
        {
            base.Configure(builder);
            builder.Property(c => c.additionalinfo).HasColumnType("jsonb");
            builder.HasOne(c => c.Company)
                .WithMany(c => c.OrganizeCharts)
                .HasForeignKey(c => c.companyid);

            builder.HasOne(c => c.Customer)
                .WithMany(c => c.OrganizeCharts)
                .HasForeignKey(c => c.customerid);
            
            builder.HasOne(c => c.ParentOrganizeChart)
                .WithMany(c => c.OrganizeCharts)
                .HasForeignKey(c => c.parent_id);
        }
    }
}