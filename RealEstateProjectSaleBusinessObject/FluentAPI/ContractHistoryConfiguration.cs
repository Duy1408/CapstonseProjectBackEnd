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
    public class ContractHistoryConfiguration : IEntityTypeConfiguration<ContractHistory>

    {
        public void Configure(EntityTypeBuilder<ContractHistory> builder)
        {
            builder.ToTable("ContractHistory");
            builder.HasKey(x => x.ContractHistoryID);
            builder.Property(x => x.NotarizedContractCode).IsRequired();
            builder.Property(x => x.AttachFile).IsRequired();
            builder.Property(x => x.Note);
            builder.Property(x => x.CreatedTime).IsRequired();



        }
    }
}
