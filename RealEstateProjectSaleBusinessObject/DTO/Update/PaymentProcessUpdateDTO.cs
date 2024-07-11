using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Update
{
    public class PaymentProcessUpdateDTO
    {
        public string? PaymentProcessName { get; set; }
        public double? Discount { get; set; }
        public double? TotalPrice { get; set; }
    }
}
