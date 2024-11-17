using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Update
{
    public class PaymentPolicyUpdateDTO
    {
        public string? PaymentPolicyName { get; set; }
        public int? LateDate { get; set; }
        public bool? Status { get; set; }
        public double? PercentLate { get; set; }
    }
}
