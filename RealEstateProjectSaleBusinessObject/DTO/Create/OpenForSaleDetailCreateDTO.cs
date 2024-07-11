using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class OpenForSaleDetailCreateDTO
    {
        [JsonIgnore]
        public Guid OpenForSaleDetailID { get; set; }
        public double Price { get; set; }
        public double? Discount { get; set; }
        public string? Note { get; set; }
        public Guid OpeningForSaleID { get; set; }
        public Guid PropertyID { get; set; }
    }
}
