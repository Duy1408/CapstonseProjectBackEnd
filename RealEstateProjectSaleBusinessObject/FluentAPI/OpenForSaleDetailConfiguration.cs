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
    public class OpenForSaleDetailConfiguration : IEntityTypeConfiguration<OpenForSaleDetail>
    {
        public void Configure(EntityTypeBuilder<OpenForSaleDetail> builder)
        {
            builder.ToTable("OpenForSaleDetail");

            builder.HasKey(x => new { x.OpeningForSaleID, x.PropertyID });
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Note);
            builder.HasOne(x => x.OpeningForSale)
                   .WithMany(o => o.OpenForSaleDetails)
                   .HasForeignKey(x => x.OpeningForSaleID);
            builder.HasOne(x => x.Property)
                   .WithMany(p => p.OpenForSaleDetails)
                   .HasForeignKey(x => x.PropertyID);



        }
    }
}
