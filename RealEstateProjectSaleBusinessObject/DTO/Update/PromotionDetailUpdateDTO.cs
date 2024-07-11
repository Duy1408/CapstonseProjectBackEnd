using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Update
{
    public class PromotionDetailUpdateDTO
    {
        [JsonIgnore]
 
        public string Description { get; set; }
        public string PromotionType { get; set; }
        public double? DiscountPercent { get; set; }
        public double? DiscountAmount { get; set; }
        public double? Amount { get; set; }
     
  
    }
}
