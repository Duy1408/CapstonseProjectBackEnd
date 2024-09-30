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
    public class FloorConfiguration : IEntityTypeConfiguration<Floor>
    {
        public void Configure(EntityTypeBuilder<Floor> builder)
        {
            builder.ToTable("Floor");
            builder.HasKey(x => x.FloorID);
            builder.Property(x => x.NumFloor).IsRequired();
            builder.Property(x => x.ImageFloor);
            builder.HasMany(x => x.Properties).WithOne(x => x.Floor).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
