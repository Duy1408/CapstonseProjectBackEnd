using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.Model
{
    public class PaymentResponseModel
    {
        public string? PaymentIntent { get; set; }
        public string? EphemeralKey { get; set; }
        public string? Customer { get; set; }
        public string? PublishableKey { get; set; }
    }
}
