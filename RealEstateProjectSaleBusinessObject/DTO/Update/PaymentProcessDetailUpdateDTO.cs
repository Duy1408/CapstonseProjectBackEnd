using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Update
{
    public class PaymentProcessDetailUpdateDTO
    {
        public string? DetailName { get; set; }
        public string? PeriodType { get; set; }
        public string? Period { get; set; }
        public int? PaymentRate { get; set; }
        public string? PaymentType { get; set; }
        public double? Amount { get; set; }
        public string? Note { get; set; }
    }
}
