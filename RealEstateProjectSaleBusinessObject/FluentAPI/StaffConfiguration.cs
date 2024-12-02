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
    public class StaffConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.ToTable("Staff");
            builder.HasKey(x => x.StaffID);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.PersonalEmail).IsRequired();
            builder.Property(x => x.DateOfBirth).IsRequired();
            builder.Property(x => x.Image);
            builder.Property(x => x.IdentityCardNumber);
            builder.Property(x => x.Nationality).IsRequired();
            builder.Property(x => x.PlaceOfOrigin);
            builder.Property(x => x.PlaceOfResidence);
            builder.Property(x => x.Status).IsRequired();
            builder.HasOne(x => x.Account);
            builder.HasMany(x => x.Bookings).WithOne(x => x.Staff).OnDelete(DeleteBehavior.NoAction);


        }
    }
}
