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
        public bool? Status { get; set; }
        public Guid? SalesPolicyID { get; set; }

    }
}
