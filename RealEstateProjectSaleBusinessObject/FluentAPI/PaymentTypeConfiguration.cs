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
    public class PaymentTypeConfiguration : IEntityTypeConfiguration<PaymentType>
    {
        public void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            builder.ToTable("PaymentType");
            builder.HasKey(x => x.PaymentTypeID);
            builder.Property(x => x.PaymentName).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.HasMany(x => x.Payments).WithOne(x => x.PaymentType).OnDelete(DeleteBehavior.NoAction); 

        }
    }
}
