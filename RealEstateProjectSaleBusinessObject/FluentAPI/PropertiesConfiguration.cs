
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
            builder.Property(x => x.PropertyName).IsRequired();
            builder.Property(x => x.Block).IsRequired();
            builder.Property(x => x.Floor).IsRequired();
            builder.Property(x => x.SizeArea).IsRequired();
            builder.Property(x => x.BedRoom).IsRequired();
            builder.Property(x => x.BathRoom).IsRequired();
            builder.Property(x => x.LivingRoom).IsRequired();
            builder.Property(x => x.View);
            builder.Property(x => x.InitialPrice).IsRequired();
            builder.Property(x => x.Discount);
            builder.Property(x => x.MoneyTax);
            builder.Property(x => x.MaintenanceCost);
            builder.Property(x => x.TotalPrice).IsRequired();
            builder.Property(x => x.Image);
            builder.Property(x => x.Status);
            builder.HasMany(x => x.Comments).WithOne(x => x.Property).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.OpenForSaleDetails).WithOne(x => x.Property).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.Bookings).WithOne(x => x.Property).OnDelete(DeleteBehavior.NoAction);



        }


    }
}
