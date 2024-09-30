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
    public class PaymentPolicyConfiguration : IEntityTypeConfiguration<PaymentPolicy>
    {
        public void Configure(EntityTypeBuilder<PaymentPolicy> builder)
        {
            builder.ToTable("PaymentPolicy");
            builder.HasKey(x => x.PaymentPolicyID);
            builder.Property(x => x.PaymentPolicyName).IsRequired();
            builder.Property(x => x.PercentEarly);
            builder.Property(x => x.EarlyDate);
            builder.Property(x => x.LateDate);
            builder.Property(x => x.PercentLate);

        }
    }
}
