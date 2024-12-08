using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class PromotionDetailVM
    {
        public Guid PromotionDetailID { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public Guid PromotionID { get; set; }
        public string PromotionName { get; set; }
        public Guid PropertyTypeID { get; set; }
        public string PropertyTypeName { get; set; }
    }
}
