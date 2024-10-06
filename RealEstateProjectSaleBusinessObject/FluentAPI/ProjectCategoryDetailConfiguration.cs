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
            builder.HasKey(x => new { x.ProjectID, x.PropertyCategoryID });
            builder.HasOne(x => x.Project)
                 .WithMany(o => o.ProjectCategoryDetails)
                 .HasForeignKey(x => x.ProjectID);
            builder.HasOne(x => x.PropertyCategory)
                .WithMany(o => o.ProjectCategoryDetails)
                .HasForeignKey(x => x.PropertyCategoryID);
        }
    }
}
