using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Update
{
    public class ContractPaymentDetailUpdateDTO
    {
        public string? DetailName { get; set; }
        public int? PaymentRate { get; set; }
        public double? Amountpaid { get; set; }
        public int? TaxRate { get; set; }
        public double? MoneyTax { get; set; }
        public double? MoneyReceived { get; set; }
        public int? NumberDayLate { get; set; }
        public int? InterestRate { get; set; }
        public double? MoneyInterestRate { get; set; }
        public double? MoneyExist { get; set; }
        public string? Description { get; set; }
    }
}
