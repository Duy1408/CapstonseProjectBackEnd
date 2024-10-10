using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class PromotionCreateDTO
    {
        [JsonIgnore]
        public Guid PromotionID { get; set; }
        public string PromotionName { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }
        [JsonIgnore]
        public bool Status { get; set; }
        public Guid SalesPolicyID { get; set; }

    }
}
