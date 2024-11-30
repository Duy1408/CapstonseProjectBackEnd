using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int? PaymentRate { get; set; }//dot may
        public string? Description { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Period { get; set; }//Thời hạn
        public double? PaidValue { get; set; }
        public double? PaidValueLate { get; set; }
        [JsonIgnore]
        public string? RemittanceOrder { get; set; }//upload chung nhan
        [JsonIgnore]
        public bool Status { get; set; }
        public Guid ContractID { get; set; }
        public Guid? PaymentPolicyID { get; set; }
    }
}
