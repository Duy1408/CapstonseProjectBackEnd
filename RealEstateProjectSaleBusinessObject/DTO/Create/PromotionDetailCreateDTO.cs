using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class PromotionDetailCreateDTO
    {
        [JsonIgnore]
        public Guid PromotionDetailID { get; set; }
        public string? Description { get; set; }
        public double? Amount { get; set; }
        public Guid PromotionID { get; set; }
        public Guid PropertyTypeID { get; set; }
    }
}
