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
            builder.Property(x => x.PaymentStage).IsRequired();
            builder.Property(x => x.Description);
            builder.Property(x => x.Durationdate);
            builder.Property(x => x.Percentage);
            builder.Property(x => x.Amount);

        }
    }
}
