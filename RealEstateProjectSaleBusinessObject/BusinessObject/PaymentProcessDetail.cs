using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class PaymentProcessDetail
    {
        public Guid PaymentProcessDetailID { get; set; }
        public string DetailName { get; set; }
        public string PeriodType { get; set; }
        public string? Period { get; set; }
        public double? PaymentRate { get; set; }
        public string PaymentType { get; set; }
        public double Amount { get; set; }
        public string? Note { get; set; }
        public Guid PaymentProcessID { get; set; }
        public PaymentProcess? PaymentProcess { get; set; }
    }
}
