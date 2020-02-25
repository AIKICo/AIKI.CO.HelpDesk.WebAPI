using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Models
{
    public class dbContext:DbContext
    {
        private readonly AppSettings _appSettings;
        private Guid _companyid { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Customer> Customer { get; private set; }
        public virtual DbSet<Member> Member { get; private set; }

        public dbContext(DbContextOptions options, IOptions<AppSettings> appSettings)
            : base(options)
        {
            _appSettings = appSettings.Value;
            _companyid = _appSettings.CompanyID;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging(true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.ApplyConfiguration<Customer>(new CustomerConfiguration(_companyid));
            modelBuilder.ApplyConfiguration<Member>(new MemberConfiguration(_companyid));
        }
    }
}
