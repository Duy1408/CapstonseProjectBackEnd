using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class OpenForSaleDetailVM
    {
        public Guid OpeningForSaleID { get; set; }
        public string OpeningForSaleName { get; set; }
        public Guid PropertyID { get; set; }
        public string PropertyName { get; set; }
        public double Price { get; set; }
        public string? Note { get; set; }

    }
}
