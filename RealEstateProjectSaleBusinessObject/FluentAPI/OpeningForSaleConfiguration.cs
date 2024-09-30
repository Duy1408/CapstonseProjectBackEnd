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
            builder.Property(x => x.DescriptionName).IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            builder.Property(x => x.ReservationDate);
            builder.Property(x => x.Description);
            builder.Property(x => x.CheckinDate);
            builder.Property(x => x.Status).IsRequired();
            builder.HasMany(x => x.Bookings).WithOne(x => x.OpeningForSale).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.openForSaleDetails).WithOne(x => x.OpeningForSale).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
