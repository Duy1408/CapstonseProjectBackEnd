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
    public class TransferConfiguration : IEntityTypeConfiguration<Transfer>

    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.ToTable("Transfer");
            builder.HasKey(x => x.TransferID);
            builder.Property(x => x.Notarizedcontractcode).IsRequired();
            builder.Property(x => x.AttachFile).IsRequired();
            builder.Property(x => x.Note);


        }
    }
}
