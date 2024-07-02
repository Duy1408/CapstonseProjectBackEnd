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
            builder.Property(x => x.ContractPaymentDetailID).IsRequired();
            builder.Property(x => x.Paymentprogress).IsRequired();
            builder.Property(x => x.Paymentduedate).IsRequired();
            builder.Property(x => x.Customervaluepaid).IsRequired();
            builder.Property(x => x.Note);
            builder.HasMany(x => x.Payments).WithOne(x => x.ContractPaymentDetail);

        }
    }
}
