using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class ContractPaymentDetail
    {
        public Guid ContractPaymentDetailID { get; set; }
        public int PaymentRate { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Period { get; set; }
        public double? PaidValue { get; set; }
        public double? PaidValueLate { get; set; }
        public string? RemittanceOrder { get; set; }
        public bool Status { get; set; }
        public Guid ContractID { get; set; }
        public Contract? Contract { get; set; }
        public Guid PaymentPolicyID { get; set; }
        public PaymentPolicy? PaymentPolicy { get; set; }

    }
}
