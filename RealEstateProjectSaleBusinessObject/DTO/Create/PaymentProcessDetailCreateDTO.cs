using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class PaymentProcessDetailCreateDTO
    {
        [JsonIgnore]
        public Guid PaymentProcessDetailID { get; set; }
        public int PaymentStage { get; set; }
        public DateTime? Period { get; set; }
        public float? Percentage { get; set; }
        public double? Amount { get; set; }
        public Guid PaymentProcessID { get; set; }
    }
}
