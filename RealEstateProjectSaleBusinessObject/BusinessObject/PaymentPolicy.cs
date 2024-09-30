using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class PaymentPolicy
    {
        public string PaymentPolicyID { get; set; }
        public string PaymentPolicyName { get; set; }
        public double PercentEarly { get; set; }
        public int EarlyDate { get; set; }
        public int LateDate { get; set; }
        public double PercentLate{ get; set; }

    }
}
