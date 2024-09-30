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
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Project");
            builder.HasKey(x => x.ProjectID);
            builder.Property(x => x.ProjectName).IsRequired();
            builder.Property(x => x.Location).IsRequired();
            builder.Property(x => x.Investor).IsRequired();
            builder.Property(x => x.GeneralContractor).IsRequired();
            builder.Property(x => x.DesignUnit).IsRequired();
            builder.Property(x => x.TotalArea).IsRequired();
            builder.Property(x => x.Scale).IsRequired();
            builder.Property(x => x.BuildingDensity).IsRequired();
            builder.Property(x => x.TotalNumberOfApartment);
            builder.Property(x => x.ProductType);
            builder.Property(x => x.LegalStatus);
            builder.Property(x => x.HandOver);
            builder.Property(x => x.Convenience);
            builder.Property(x => x.Image);
            builder.Property(x => x.Status);
            builder.HasMany(x => x.Bookings).WithOne(x => x.Project).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.Properties).WithOne(x => x.Project).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.Salespolicies).WithOne(x => x.Project).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.OpeningForSales).WithOne(x => x.Project).OnDelete(DeleteBehavior.NoAction);




        }
    }
}
