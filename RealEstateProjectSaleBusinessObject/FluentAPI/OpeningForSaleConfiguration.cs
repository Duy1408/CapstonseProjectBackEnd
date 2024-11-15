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
    public class OpeningForSaleConfiguration : IEntityTypeConfiguration<OpeningForSale>
    {
        public void Configure(EntityTypeBuilder<OpeningForSale> builder)
        {
            builder.ToTable("OpeningForSale");
            builder.HasKey(x => x.OpeningForSaleID);
            builder.Property(x => x.DecisionName).IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            builder.Property(x => x.ReservationPrice).IsRequired();
            builder.Property(x => x.Description);
            builder.Property(x => x.CheckinDate).IsRequired();
            builder.Property(x => x.SaleType).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.HasMany(x => x.Bookings).WithOne(x => x.OpeningForSale).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.OpenForSaleDetails).WithOne(x => x.OpeningForSale).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
