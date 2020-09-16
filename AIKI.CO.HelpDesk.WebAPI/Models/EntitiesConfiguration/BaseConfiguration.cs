using System;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration
{
    public class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseObject
    {
        protected Guid companyid { get; }

        public BaseConfiguration(Guid companyid)
        {
            this.companyid = companyid;
        }
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(c => c.id);
            builder.Property(c => c.companyid).HasDefaultValue(companyid);
            builder.Property(c => c.id)
                .ValueGeneratedNever();
        }
    }
}