using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class RealEstateProjectSaleSystemDBContext : DbContext
    {
        public RealEstateProjectSaleSystemDBContext()
        {

        }

        public RealEstateProjectSaleSystemDBContext(DbContextOptions<RealEstateProjectSaleSystemDBContext> opt) : base(opt) { }

        public virtual DbSet<Account>? Accounts { get; set; }
        public virtual DbSet<Booking>? Bookings { get; set; }
        public virtual DbSet<Payment>? Payments { get; set; }
        public virtual DbSet<ContractPaymentDetail>? ContractPaymentDetails { get; set; }
        public virtual DbSet<Comment>? Comments { get; set; }
        public virtual DbSet<Contract>? Contracts { get; set; }
        public virtual DbSet<Customer>? Customers { get; set; }

        public virtual DbSet<OpenForSaleDetail>? OpenForSaleDetails { get; set; }
        public virtual DbSet<OpeningForSale>? OpeningForSales { get; set; }
        public virtual DbSet<PaymentProcess>? PaymentProcesses { get; set; }
        public virtual DbSet<PaymentProcessDetail>? PaymentProcessDetails { get; set; }
        public virtual DbSet<PaymentType>? PaymentTypes { get; set; }
        public virtual DbSet<Project>? Projects { get; set; }
        public virtual DbSet<Promotion>? Promotions { get; set; }
        public virtual DbSet<PromotionDetail>? PromotionDetails { get; set; }
        public virtual DbSet<Property>? Properties { get; set; }
        public virtual DbSet<PropertyType>? PropertiesTypes { get; set; }
        public virtual DbSet<Role>? Roles { get; set; }
        public virtual DbSet<Salespolicy>? Salespolicies { get; set; }
        public virtual DbSet<Staff>? Staffs { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }

#if DEBUG
        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", true, true)
                .Build();
            return config["ConnectionStrings:DB"]!;
        }
#else
   private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            return config["ConnectionStrings:DB"]!;
        }
#endif

    }
}

