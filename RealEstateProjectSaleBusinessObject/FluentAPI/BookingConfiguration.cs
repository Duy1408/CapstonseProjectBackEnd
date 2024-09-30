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
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Booking");
            builder.HasKey(x => x.BookingID);
            builder.Property(x => x.DepositedTimed);
            builder.Property(x => x.DepositedPrice);
            builder.Property(x => x.CreatedTime).IsRequired();
            builder.Property(x => x.UpdatedTime);
            builder.Property(x => x.BookingFile);
            builder.Property(x => x.Note);
            builder.Property(x => x.Status).IsRequired();
            builder.HasMany(x => x.Payments).WithOne(x => x.Booking).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.Contracts).WithOne(x => x.Booking).OnDelete(DeleteBehavior.NoAction);


        }
    }
}
