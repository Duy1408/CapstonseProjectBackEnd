using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class OpenForSaleDetail
    {
        public Guid OpenForSaleDetailID { get; set; }

        public double Price { get; set; }
        public double? Discount { get; set; }
        public string? Note { get; set; }
        public Guid OpeningForSaleID { get; set; }
        public OpeningForSale? OpeningForSale { get; set; }
        public Guid PropertyID { get; set; }
        public Property? Property { get; set; }

    }
}
