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
            builder.Property(x => x.CommericalName).IsRequired();
            builder.Property(x => x.ShortName).IsRequired();
            builder.Property(x => x.TypeOfProject).IsRequired();
            builder.Property(x => x.Address).IsRequired();
            builder.Property(x => x.Commune).IsRequired();
            builder.Property(x => x.District).IsRequired();
            builder.Property(x => x.DepositPrice).IsRequired();
            builder.Property(x => x.Summary);
            builder.Property(x => x.LicenseNo);
            builder.Property(x => x.DateOfIssue);
            builder.Property(x => x.CampusArea);
            builder.Property(x => x.PlaceofIssue);
            builder.Property(x => x.Code);
            builder.Property(x => x.Image);
            builder.Property(x => x.Status).IsRequired();
            builder.HasMany(x => x.Bookings).WithOne(x => x.Project).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.Properties).WithOne(x => x.Project).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.Salespolicies).WithOne(x => x.Project).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.OpeningForSales).WithOne(x => x.Project).OnDelete(DeleteBehavior.NoAction);




        }
    }
}
