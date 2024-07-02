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
    public class PaymentProcessDetailConfiguration : IEntityTypeConfiguration<PaymentProcessDetail>
    {
        public void Configure(EntityTypeBuilder<PaymentProcessDetail> builder)
        {
            builder.ToTable("PaymentProcessDetail");
            builder.HasKey(x => x.PaymentProcessDetailID);
            builder.Property(x => x.PeriodType).IsRequired();
            builder.Property(x => x.Period);
            builder.Property(x => x.PaymentType).IsRequired();
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Maintenancecosts);
            builder.Property(x => x.Paymentprocessname).IsRequired();

        }
    }
}
