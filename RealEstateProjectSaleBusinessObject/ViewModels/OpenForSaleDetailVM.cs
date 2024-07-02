using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class OpenForSaleDetailVM
    {
        public Guid OpenForSaleDetailID { get; set; }
        public int Floor { get; set; }
        public string TypeRoom { get; set; }
        public double Price { get; set; }
        public Guid OpeningForSaleID { get; set; }
        public Guid PropertiesID { get; set; }
    }
}
