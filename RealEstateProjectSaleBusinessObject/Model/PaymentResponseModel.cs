using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.Model
{
    public class PaymentResponseModel
    {
        public string? SessionId { get; set; }

        public string? PubKey { get; set; }
        public string? SessionUrl { get; set; }
    }
}
