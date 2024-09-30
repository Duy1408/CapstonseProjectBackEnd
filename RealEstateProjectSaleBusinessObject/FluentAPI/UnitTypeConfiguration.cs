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
    public class UnitTypeConfiguration : IEntityTypeConfiguration<UnitType>
    {
        public void Configure(EntityTypeBuilder<UnitType> builder)
        {
            builder.ToTable("UnitType");
            builder.HasKey(x => x.UnitTypeID);
            builder.Property(x => x.BathRoom);
            builder.Property(x => x.Image);
            builder.Property(x => x.NetFloorArea);
            builder.Property(x => x.GrossFloorArea);
            builder.Property(x => x.BedRoom);
            builder.Property(x => x.KitchenRoom);
            builder.Property(x => x.LivingRoom);
            builder.Property(x => x.Basement);






        }
    }
}
