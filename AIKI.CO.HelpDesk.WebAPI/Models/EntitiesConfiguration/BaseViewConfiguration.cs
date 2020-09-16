using System;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration
{
    public class BaseViewConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseObject
    {
        public BaseViewConfiguration(Guid companyid)
        {
            _companyid = companyid;
        }

        protected Guid _companyid { get; set; }

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasNoKey();
        }
    }
}