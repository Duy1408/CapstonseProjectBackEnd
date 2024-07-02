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
    public class ContactConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.ToTable("Contact");
            builder.HasKey(x => x.ContractID);
            builder.Property(x => x.DateSigned);
            builder.Property(x => x.UpdateUsAt);
            builder.Property(x => x.CreatedStAt).IsRequired();
            builder.Property(x => x.ContractType).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.HasOne(x => x.Booking);
            builder.HasMany(x => x.ContractPaymentDetails).WithOne(x => x.Contract);

        }
    }
}
