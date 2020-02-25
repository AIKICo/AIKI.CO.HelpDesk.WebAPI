using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration
{
    public class MemberConfiguration : BaseConfiguration<Member>, IEntityTypeConfiguration<Member>
    {
        public MemberConfiguration(Guid companyid):base(companyid)
        {

        }

        public override void Configure(EntityTypeBuilder<Member> builder)
        {
            base.Configure(builder);

            builder.HasOne(d => d.Company)
                .WithMany(p => p.Member)
                .HasForeignKey(d => d.companyid);
        }
    }
}
