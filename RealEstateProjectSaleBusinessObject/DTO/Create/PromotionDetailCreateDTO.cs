using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class PromotionDetailCreateDTO
    {
        public Guid PromotionDetaiID { get; set; }
        public string Description { get; set; }
        public string PromotionType { get; set; }
        public double? DiscountPercent { get; set; }
        public double? DiscountAmount { get; set; }
        public double Amount { get; set; }
        public Guid PromotionID { get; set; }
        public Guid PropertiesTypeID { get; set; }
    }
}
