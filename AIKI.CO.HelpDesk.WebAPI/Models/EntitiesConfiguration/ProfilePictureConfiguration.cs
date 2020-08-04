using System;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration
{
    public class ProfilePictureConfiguration: BaseConfiguration<ProfilePicture>, IEntityTypeConfiguration<ProfilePicture>
    {
        public ProfilePictureConfiguration(Guid companyid) : base(companyid)
        {
        }

        public override void Configure(EntityTypeBuilder<ProfilePicture> builder)
        {
            base.Configure(builder);
            builder.Property(c => c.filecontent).HasColumnType("bytea");
            
            builder.HasOne(c => c.Company)
                .WithMany(c => c.ProfilePictures)
                .HasForeignKey(c => c.companyid);
            
            builder.HasOne(c => c.Member)
                .WithMany(c => c.ProfilePictures)
                .HasForeignKey(c => c.memberid);
        }
    }
}