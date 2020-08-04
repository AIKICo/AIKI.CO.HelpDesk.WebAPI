using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration;
using AIKI.CO.HelpDesk.WebAPI.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;

namespace AIKI.CO.HelpDesk.WebAPI.Models
{
    public sealed class dbContext : DbContext
    {
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _context;
        private readonly IDataProtector _protector;
        private readonly byte[] _encryptionKey = null;
        private readonly byte[] _encryptionIV = null;
        private readonly IEncryptionProvider _provider;
        private Guid _companyid { get; set; } = Guid.Empty;
        public DbSet<Company> Company { get; set; }
        public DbSet<Customer> Customer { get; private set; }
        public DbSet<Member> Member { get; private set; }
        public DbSet<OperatingHour> OperatingHour { get; private set; }
        public DbSet<SLASetting> SLASetting { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<AppConstant> AppConstant { get; set; }
        public DbSet<AppConstantItem> AppConstantItem { get; set; }
        public DbSet<OrganizeChart> OrganizeChart { get; set; }
        public DbSet<OrganizeCharts_JsonView> OrganizeCharts_JsonView { get; set; }
        public DbSet<Asset> Asset { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<AssetsView> AssetsView { get; set; }
        public DbSet<TicketsView> TicketsView { get; set; }
        public DbSet<TicketHistory> TicketHistory { get; set; }
        public DbSet<Last30Ticket> Last30Ticket { get; set; }
        public DbSet<TicketCountInfo> TicketCountInfo { get; set; }
        public DbSet<ProfilePicture> ProfilePicture { get; set; }

        public dbContext(
            DbContextOptions options,
            IOptions<AppSettings> appSettings,
            IHttpContextAccessor context,
            IDataProtectionProvider provider,
            IConfiguration Configuration)
            : base(options)
        {
            _encryptionKey =
                Encoding.Unicode.GetBytes(Environment.GetEnvironmentVariable("encryptionKey") ?? string.Empty);
            _encryptionIV =
                Encoding.Unicode.GetBytes(Environment.GetEnvironmentVariable("encryptionIV") ?? string.Empty);
            _context = context;
            _appSettings = appSettings.Value;
            _protector = provider.CreateProtector("MemberService.CompanyId");
            _provider = new AesProvider(_encryptionKey, _encryptionIV);

            if (_context.HttpContext.Request.Headers["CompanyID"].Any())
                _companyid =
                    Guid.Parse(_protector.Unprotect(_context.HttpContext.Request.Headers["CompanyID"].ToString()));
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
            modelBuilder.UseEncryption(_provider);
            modelBuilder.HasDefaultSchema("public");

            #region Apply Configuration

            modelBuilder.ApplyConfiguration<Customer>(new CustomerConfiguration(_companyid));
            modelBuilder.ApplyConfiguration<Member>(new MemberConfiguration(_companyid));
            modelBuilder.ApplyConfiguration<OperatingHour>(new OperatingHourConfiguration(_companyid));
            modelBuilder.ApplyConfiguration<SLASetting>(new SLASettingConfiguration(_companyid));
            modelBuilder.ApplyConfiguration<Group>(new GroupConfiguration(_companyid));
            modelBuilder.ApplyConfiguration<AppConstant>(new AppConstantConfiguration(_companyid));
            modelBuilder.ApplyConfiguration<AppConstantItem>(new AppConstantItemConfiguration(_companyid));
            modelBuilder.ApplyConfiguration<OrganizeChart>(new OrganizeChartConfiguration(_companyid));
            modelBuilder.ApplyConfiguration<Asset>(new AssetConfiguration(_companyid));
            modelBuilder.ApplyConfiguration<AssetsView>(new AssetsViewConfiguration(_companyid));
            modelBuilder.ApplyConfiguration<Ticket>(new TicketConfiguration(_companyid));
            modelBuilder.ApplyConfiguration<TicketsView>(new TicketsViewConfiguration(_companyid));
            modelBuilder.ApplyConfiguration<TicketHistory>(new TicketHistoryConfiguration(_companyid));
            modelBuilder.ApplyConfiguration<Last30Ticket>(new Last30TicketConfiguration(_companyid));
            modelBuilder.ApplyConfiguration<TicketCountInfo>(new TicketCountInfoConfiguration(_companyid));
            modelBuilder.ApplyConfiguration<ProfilePicture>(new ProfilePictureConfiguration(_companyid));

            #endregion

            #region Query Filter

            modelBuilder.Entity<Customer>().HasQueryFilter(q => q.companyid == _companyid);
            modelBuilder.Entity<Member>().HasQueryFilter(q => q.companyid == _companyid);
            modelBuilder.Entity<OperatingHour>().HasQueryFilter(q => q.companyid == _companyid);
            modelBuilder.Entity<SLASetting>().HasQueryFilter(q => q.companyid == _companyid);
            modelBuilder.Entity<Group>().HasQueryFilter(q => q.companyid == _companyid);
            modelBuilder.Entity<AppConstant>().HasQueryFilter(q => q.companyid == _companyid);
            modelBuilder.Entity<AppConstantItem>().HasQueryFilter(q => q.companyid == _companyid);
            modelBuilder.Entity<OrganizeChart>().HasQueryFilter(q => q.companyid == _companyid);
            modelBuilder.Entity<Asset>().HasQueryFilter(q => q.companyid == _companyid);
            modelBuilder.Entity<AssetsView>().HasQueryFilter(q => q.companyid == _companyid);
            modelBuilder.Entity<Ticket>().HasQueryFilter(q => q.companyid == _companyid);
            modelBuilder.Entity<TicketsView>().HasQueryFilter(q => q.companyid == _companyid);
            modelBuilder.Entity<TicketHistory>().HasQueryFilter(q => q.companyid == _companyid);
            modelBuilder.Entity<Last30Ticket>().HasQueryFilter(q => q.companyid == _companyid);
            modelBuilder.Entity<TicketCountInfo>().HasQueryFilter(q => q.companyid == _companyid);
            modelBuilder.Entity<ProfilePicture>().HasQueryFilter(q => q.companyid == _companyid);

            #endregion
        }
    }
}