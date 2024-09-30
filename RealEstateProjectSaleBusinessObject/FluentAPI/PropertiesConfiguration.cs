
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
    public class PropertiesConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.ToTable("Property");
            builder.HasKey(x => x.PropertyID);
            builder.Property(x => x.PropertyCode).IsRequired();
            builder.Property(x => x.View);
            builder.Property(x => x.PriceSold);
            builder.Property(x => x.Status).IsRequired();
            builder.HasMany(x => x.Comments).WithOne(x => x.Property).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Booking);

            builder.HasMany(x => x.OpenForSaleDetails).WithOne(x => x.Property).OnDelete(DeleteBehavior.NoAction);
        



        }


    }
}
