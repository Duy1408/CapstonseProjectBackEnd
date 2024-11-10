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
    public class ContractConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.ToTable("Contract");
            builder.HasKey(x => x.ContractID);
            builder.Property(x => x.ContractCode).IsRequired();
            builder.Property(x => x.ContractType).IsRequired();
            builder.Property(x => x.CreatedTime).IsRequired();
            builder.Property(x => x.UpdatedTime);
            builder.Property(x => x.ExpiredTime);
            builder.Property(x => x.TotalPrice).IsRequired();
            builder.Property(x => x.Description);
            builder.Property(x => x.ContractDepositFile);
            builder.Property(x => x.ContractSaleFile);
            builder.Property(x => x.PriceSheetFile);
            builder.Property(x => x.Status).IsRequired();
            builder.HasOne(x => x.Booking);
            builder.HasMany(x => x.ContractPaymentDetails).WithOne(x => x.Contract);

        }
    }
}
