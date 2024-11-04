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
    public class PanoramaImageConfiguration : IEntityTypeConfiguration<PanoramaImage>
    {
        public void Configure(EntityTypeBuilder<PanoramaImage> builder)
        {
            builder.ToTable("PanoramaImage");
            builder.HasKey(x => x.PanoramaImageID);
            builder.Property(x => x.Title);
            builder.Property(x => x.Image);

        }
    }
}
