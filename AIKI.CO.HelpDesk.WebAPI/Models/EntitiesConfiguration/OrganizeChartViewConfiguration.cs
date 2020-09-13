using System;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration
{
    public class OrganizeChartViewConfiguration: BaseViewConfiguration<OrganizeChartView>
    {
        public OrganizeChartViewConfiguration(Guid companyid) : base(companyid)
        {
        }

        public override void Configure(EntityTypeBuilder<OrganizeChartView> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.children).HasColumnType("json");
        }
    }
}