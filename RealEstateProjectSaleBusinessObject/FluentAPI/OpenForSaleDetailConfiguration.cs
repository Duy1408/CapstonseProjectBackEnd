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
            builder.HasKey(x => x.OpenForSaleDetailID);
            builder.Property(x => x.Block).IsRequired();
            builder.Property(x => x.Floor).IsRequired();
            builder.Property(x => x.TypeRoom).IsRequired();
            builder.Property(x => x.Price).IsRequired();

        }
    }
}
