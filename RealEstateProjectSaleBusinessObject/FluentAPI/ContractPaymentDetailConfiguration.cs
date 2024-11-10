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
    public class ContractPaymentDetailConfiguration : IEntityTypeConfiguration<ContractPaymentDetail>
    {
        public void Configure(EntityTypeBuilder<ContractPaymentDetail> builder)
        {
            builder.ToTable("ContractPaymentDetail");
            builder.HasKey(x => x.ContractPaymentDetailID);
            builder.Property(x => x.PaymentRate).IsRequired();
            builder.Property(x => x.Description);
            builder.Property(x => x.Period);
            builder.Property(x => x.PaidValue);
            builder.Property(x => x.PaidValueLate);
            builder.Property(x => x.RemittanceOrder);
            builder.Property(x => x.Status).IsRequired();

        }
    }
}
