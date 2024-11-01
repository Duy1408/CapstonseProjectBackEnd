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
    public class ProjectCategoryDetailConfiguration : IEntityTypeConfiguration<ProjectCategoryDetail>
    {
        public void Configure(EntityTypeBuilder<ProjectCategoryDetail> builder)
        {
            builder.ToTable("ProjectCategoryDetail");
            builder.HasKey(x => x.ProjectCategoryDetailID);
            builder.HasMany(x => x.Properties).WithOne(x => x.ProjectCategoryDetail).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.OpeningForSales).WithOne(x => x.ProjectCategoryDetail).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.Bookings).WithOne(x => x.ProjectCategoryDetail).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
