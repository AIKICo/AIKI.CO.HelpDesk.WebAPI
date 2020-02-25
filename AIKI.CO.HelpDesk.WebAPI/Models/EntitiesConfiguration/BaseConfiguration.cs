using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            builder.HasKey(p => p.id);
            builder.Property(e => e.id)
                    .ValueGeneratedNever();

            builder.HasQueryFilter(p => p.companyid == _companyid);
        }
    }
}
