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
        public string Block { get; set; }
        public int Floor { get; set; }
        public string TypeRoom { get; set; }
        public double Price { get; set; }
        public Guid OpeningForSaleID { get; set; }
        public OpeningForSale? OpeningForSale { get; set; }
        public Guid PropertiesID { get; set; }
        public Property? Property { get; set; }

    }
}
