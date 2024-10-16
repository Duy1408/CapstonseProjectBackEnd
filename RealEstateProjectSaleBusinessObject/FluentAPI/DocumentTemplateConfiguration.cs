﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.FluentAPI
{
    public class DocumentTemplateConfiguration : IEntityTypeConfiguration<DocumentTemplate>
    {
        public void Configure(EntityTypeBuilder<DocumentTemplate> builder)
        {
            builder.ToTable("DocumentTemplate");
            builder.HasKey(x => x.DocumentTemplateID);
            builder.Property(x => x.DocumentName).IsRequired();
            builder.Property(x => x.DocumentFile);
            builder.Property(x => x.Status);
            builder.HasMany(x => x.Contracts).WithOne(x => x.DocumentTemplate).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.Bookings).WithOne(x => x.DocumentTemplate).OnDelete(DeleteBehavior.NoAction);




        }
    }
}
