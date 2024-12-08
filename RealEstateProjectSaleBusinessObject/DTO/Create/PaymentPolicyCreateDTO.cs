using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class PaymentPolicyCreateDTO
    {
        [JsonIgnore]
        public Guid PaymentPolicyID { get; set; }
        public string? PaymentPolicyName { get; set; }
        public int? LateDate { get; set; }
        public double? PercentLate { get; set; }
        [JsonIgnore]
        public bool Status { get; set; }

    }
}
