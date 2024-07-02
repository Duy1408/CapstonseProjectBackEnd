using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.FluentAPI
{
    public class SalespolicyConfiguration : IEntityTypeConfiguration<Salespolicy>
    {
        public void Configure(EntityTypeBuilder<Salespolicy> builder)
        {
            builder.ToTable("Salespolicy");
            builder.HasKey(x => x.SalesPolicyID);
            builder.Property(x => x.ExpressTime).IsRequired();
            builder.Property(x => x.PeopleApplied).IsRequired();
            builder.Property(x => x.SalesPolicyType).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.HasMany(x => x.Promotions).WithOne(x => x.Salespolicy).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.PaymentProcesses).WithOne(x => x.Salespolicy).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
