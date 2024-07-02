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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
            builder.HasKey(x => x.CustomerID);
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.DateOfBirth).IsRequired();
            builder.Property(x => x.PersonalEmail).IsRequired();
            builder.Property(x => x.PhoneNumber).IsRequired();
            builder.Property(x => x.IdentityCardNumber).IsRequired();
            builder.Property(x => x.Nationality).IsRequired();
            builder.Property(x => x.Taxcode);
            builder.Property(x => x.BankName);
            builder.Property(x => x.BankNumber);
            builder.Property(x => x.Address).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.HasOne(x => x.Account);
            builder.HasMany(x=>x.Comments).WithOne(x=>x.Customer).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.Bookings).WithOne(x => x.Customer).OnDelete(DeleteBehavior.NoAction);



        }
    }
}
