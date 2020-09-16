using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage;

namespace AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration
{
    public class CompanyConfiguration:IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(c => c.id);
            builder.Property(c => c.email).IsRequired();
            builder.Property(c => c.subdomain).IsRequired();
            
            builder.HasIndex(c => c.email).IsUnique();
            builder.HasIndex(c => c.subdomain).IsUnique();
        }
    }
}