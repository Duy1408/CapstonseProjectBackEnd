using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class PaymentProcessVM
    {
        public Guid PaymentProcessID { get; set; }
        public string PaymentProcessName { get; set; }
        public double? Discount { get; set; }
        public double TotalPrice { get; set; }
        public Guid SalesPolicyID { get; set; }
        public string SalesPolicyType { get; set; }
    }
}
