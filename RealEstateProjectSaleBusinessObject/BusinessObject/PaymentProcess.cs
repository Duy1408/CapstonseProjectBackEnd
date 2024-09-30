using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class PaymentProcess
    {
        public Guid PaymentProcessID { get; set; }
        public string PaymentProcessName { get; set; }
        public double? Discount { get; set; }
        public double? TotalPrice { get; set; }
        public Guid SalesPolicyID { get; set; }
        public Salespolicy? Salespolicy { get; set; }
        public List<Contract>? Contracts { get; set; }
        public List<PaymentProcessDetail>? PaymentProcessDetails { get; set; }


    }
}
