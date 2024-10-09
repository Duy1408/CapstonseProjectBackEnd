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
    public class ZoneConfiguration : IEntityTypeConfiguration<Zone>
    {
        public void Configure(EntityTypeBuilder<Zone> builder)
        {
            builder.ToTable("Zone");
            builder.HasKey(x => x.ZoneID);
            builder.Property(x => x.ZoneName).IsRequired();
            builder.Property(x => x.ImageZone);
            builder.Property(x => x.Status).IsRequired();
            builder.HasMany(x => x.Properties).WithOne(x => x.Zone).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.Blocks).WithOne(x => x.Zone).OnDelete(DeleteBehavior.NoAction);


        }
    }
}
