using System;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration
{
    public class AssetConfiguration : BaseConfiguration<Asset>, IEntityTypeConfiguration<Asset>
    {
        public AssetConfiguration(Guid companyid) : base(companyid)
        {
        }

        public override void Configure(EntityTypeBuilder<Asset> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.assetadditionalinfo).HasColumnType("jsonb");

            builder.HasOne(c => c.Company)
                .WithMany(c => c.Assets)
                .HasForeignKey(c => c.companyid);
            
            builder.HasOne(c => c.Customer)
                .WithMany(c => c.Assets)
                .HasForeignKey(c => c.customerid);
        }
    }
}