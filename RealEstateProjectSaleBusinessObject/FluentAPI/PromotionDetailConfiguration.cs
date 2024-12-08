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
    public class PromotionDetailConfiguration : IEntityTypeConfiguration<PromotionDetail>
    {
        public void Configure(EntityTypeBuilder<PromotionDetail> builder)
        {
            builder.ToTable("PromotionDetail");
            builder.HasKey(x => x.PromotionDetailID);
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Amount).IsRequired();
            builder.HasMany(x => x.Contracts).WithOne(x => x.PromotionDetail).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
