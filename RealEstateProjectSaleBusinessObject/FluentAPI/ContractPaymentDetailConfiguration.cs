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
            builder.Property(x => x.DetailName).IsRequired();
            builder.Property(x => x.CreatedTime).IsRequired();
            builder.Property(x => x.PaymentRate).IsRequired();
            builder.Property(x => x.Amountpaid);
            builder.Property(x => x.TaxRate);
            builder.Property(x => x.MoneyTax);
            builder.Property(x => x.MoneyReceived);
            builder.Property(x => x.NumberDayLate);
            builder.Property(x => x.InterestRate);
            builder.Property(x => x.MoneyInterestRate);
            builder.Property(x => x.MoneyExist);
            builder.Property(x => x.Description);
         

        }
    }
}
