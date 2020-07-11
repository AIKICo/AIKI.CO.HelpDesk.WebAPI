using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration
{
    public class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseObject
    {
        protected Guid _companyid { get; set; }

        public BaseConfiguration(Guid companyid)
        {
            _companyid = companyid;
        }

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(c => c.id);
            builder.Property(c => c.companyid).HasDefaultValue(_companyid);
            builder.Property(c => c.id)
                .ValueGeneratedNever();
        }
    }
}