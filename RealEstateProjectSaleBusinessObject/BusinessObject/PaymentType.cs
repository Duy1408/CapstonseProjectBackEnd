using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class PaymentType
    {
        public Guid PaymentTypeID { get; set; }
        public string PaymentName { get; set; }
        public List<Payment>? Payments { get; set; }


    }
}
