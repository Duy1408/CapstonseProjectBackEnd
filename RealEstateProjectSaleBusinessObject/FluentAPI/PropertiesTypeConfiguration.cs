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
    public class PropertiesTypeConfiguration : IEntityTypeConfiguration<PropertyType>
    {
        public void Configure(EntityTypeBuilder<PropertyType> builder)
        {
            builder.ToTable("PropertyType");
            builder.HasKey(x => x.PropertyTypeID);
            builder.Property(x => x.TypeName).IsRequired();
            builder.HasMany(x => x.Properties).WithOne(x => x.PropertyType).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.PromotionDetails).WithOne(x => x.PropertiesType).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
