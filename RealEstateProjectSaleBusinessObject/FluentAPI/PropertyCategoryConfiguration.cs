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
    public class PropertyCategoryConfiguration : IEntityTypeConfiguration<PropertyCategory>
    {
        public void Configure(EntityTypeBuilder<PropertyCategory> builder)
        {
            builder.ToTable("PropertyCategory");
            builder.HasKey(x => x.PropertyCategoryID);
            builder.Property(x => x.PropertyCategoryName);
            builder.HasMany(x => x.PropertyTypes).WithOne(x => x.PropertyCategory).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.Bookings).WithOne(x => x.PropertyCategory).OnDelete(DeleteBehavior.NoAction);



        }
    }
}
