using System;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration
{
    public class OrganizeCharts_JsonViewConfiguration: BaseConfiguration<OrganizeCharts_JsonView>, IEntityTypeConfiguration<OrganizeCharts_JsonView>
    {
        public OrganizeCharts_JsonViewConfiguration(Guid? companyid):base(companyid)
        {

        }
        public override void Configure(EntityTypeBuilder<OrganizeCharts_JsonView> builder)
        {
            builder.HasNoKey();
            builder.Property(c => c.organizecharts).HasColumnType("jsonb");
            builder.HasQueryFilter(c => c.companyid == _companyid);
        }
    }
}