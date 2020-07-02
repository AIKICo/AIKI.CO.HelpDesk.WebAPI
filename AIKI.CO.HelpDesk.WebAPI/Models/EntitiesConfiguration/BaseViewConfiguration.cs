using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration
{
    public class BaseViewConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseObject
    {
        protected Guid _companyid { get; set; }

        public BaseViewConfiguration(Guid companyid)
        {
            _companyid = companyid;
        }

        public virtual void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<T> builder)
        {
            builder.HasNoKey();
        }
    }
}