using Microsoft.EntityFrameworkCore;
using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration
{
    public class BaseViewConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseObject
    {
        protected Guid _companyid { get; set; }
        public BaseViewConfiguration(Guid companyid)
        {
            _companyid = companyid;
        }
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<T> builder)
        {
            builder.HasNoKey();
            builder.HasQueryFilter(p => p.companyid == _companyid);
        }
    }
}
