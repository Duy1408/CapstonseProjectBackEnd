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
    public class PaymentProcessConfiguration : IEntityTypeConfiguration<PaymentProcess>
    {
        public void Configure(EntityTypeBuilder<PaymentProcess> builder)
        {
            builder.ToTable("PaymentProcess");
            builder.HasKey(x => x.PaymentProcessID);
            builder.Property(x => x.Paymentperiod).IsRequired();
            builder.Property(x => x.Duration).IsRequired();
            builder.Property(x => x.PaymentRate).IsRequired();
            builder.HasMany(x => x.PaymentProcessDetails).WithOne(x => x.PaymentProcess).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.Contracts).WithOne(x => x.PaymentProcess).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
