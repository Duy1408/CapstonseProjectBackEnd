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
            builder.Property(x => x.PaymentPolicyID);
            builder.Property(x => x.PaymentPolicyName).IsRequired();
            builder.Property(x => x.LateDate).IsRequired();
            builder.Property(x => x.PercentLate).IsRequired();
            builder.Property(x => x.Status);
            builder.HasMany(x => x.ContractPaymentDetails).WithOne(x => x.PaymentPolicy).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.Projects).WithOne(x => x.PaymentPolicy).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
