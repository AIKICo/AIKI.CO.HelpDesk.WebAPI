using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace AIKI.CO.HelpDesk.WebAPI.Models
{
    public sealed class dbContext : DbContext
    {
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _context;
        private Guid _companyid { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Customer> Customer { get; private set; }
        public DbSet<Member> Member { get; private set; }
        public DbSet<OperatingHour> OperatingHour { get; private set; }
        public DbSet<SLASetting> SLASetting { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<AppConstant> AppConstant { get; set; }
        public DbSet<AppConstantItem> AppConstantItem { get; set; }

        public dbContext(
            DbContextOptions options,
            IOptions<AppSettings> appSettings,
            IHttpContextAccessor context)
            : base(options)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _companyid = _appSettings.CompanyID;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
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
            modelBuilder.ApplyConfiguration<OperatingHour>(new OperatingHourConfiguration(_companyid));
            modelBuilder.ApplyConfiguration<SLASetting>(new SLASettingConfiguration(_companyid));
            modelBuilder.ApplyConfiguration<Group>(new GroupConfiguration(_companyid));
            modelBuilder.ApplyConfiguration<AppConstant>(new AppConstantConfiguration(_companyid));
            modelBuilder.ApplyConfiguration<AppConstantItem>(new AppConstantItemConfiguration(_companyid));
        }
    }
}
