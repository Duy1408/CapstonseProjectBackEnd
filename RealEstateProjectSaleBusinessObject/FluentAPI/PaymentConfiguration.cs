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
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payment");
            builder.HasKey(x => x.PaymentID);
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Content);
            builder.Property(x => x.CreatedTime).IsRequired();
            builder.Property(x => x.PaymentTime);
            builder.Property(x => x.Status).IsRequired();

        }
    }
}
