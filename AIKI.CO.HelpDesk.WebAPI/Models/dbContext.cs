using AIKI.CO.HelpDesk.WebAPI.Models.Entities;
using AIKI.CO.HelpDesk.WebAPI.Models.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIKI.CO.HelpDesk.WebAPI.Models
{
    public class dbContext:DbContext
    {
        private Guid _companyid { get; set; } = new Guid("997afb89-9abf-4889-8e43-cc301a311a9f");
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Customer> Customer { get; private set; }
        public virtual DbSet<Member> Member { get; private set; }

        public dbContext(DbContextOptions options)
            : base(options)
        {
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
