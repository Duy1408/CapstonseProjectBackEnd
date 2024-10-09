using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.FluentAPI
{
    public class BlockConfiguration : IEntityTypeConfiguration<Block>
    {
        public void Configure(EntityTypeBuilder<Block> builder)
        {
            builder.ToTable("Block");
            builder.HasKey(x => x.BlockID);
            builder.Property(x => x.BlockName).IsRequired();
            builder.Property(x => x.ImageBlock);
            builder.Property(x => x.Status);
            builder.HasMany(x => x.Properties).WithOne(x => x.Block).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.Floors).WithOne(x => x.Block).OnDelete(DeleteBehavior.NoAction);


        }
    }
}
