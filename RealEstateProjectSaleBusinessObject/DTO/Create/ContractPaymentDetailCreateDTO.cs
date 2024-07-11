using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class ContractPaymentDetailCreateDTO
    {
        [JsonIgnore]
        public Guid ContractPaymentDetailID { get; set; }
        public string DetailName { get; set; }
        [JsonIgnore]
        public DateTime CreatedTime { get; set; }
        public int PaymentRate { get; set; }
        public double? Amountpaid { get; set; }
        public int? TaxRate { get; set; }
        public double? MoneyTax { get; set; }
        public double? MoneyReceived { get; set; }
        public int? NumberDayLate { get; set; }
        public int? InterestRate { get; set; }
        public double? MoneyInterestRate { get; set; }
        public double? MoneyExist { get; set; }
        public string? Description { get; set; }
        public Guid ContractID { get; set; }
    }
}
