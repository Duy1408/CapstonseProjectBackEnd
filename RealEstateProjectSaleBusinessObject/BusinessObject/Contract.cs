using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class Contract
    {
        public Guid ContractID { get; set; }
        public string ContractCode { get; set; }
        public string ContractName { get; set; }
        public string ContractType { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateSigned { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ExpiredTime { get; set; }
        public double? TotalPrice { get; set; }
        public string? Description { get; set; }
        public string? ContractDepositFile { get; set; }
        public string? ContractSaleFile { get; set; }
        public string Status { get; set; }
        public Guid DocumentTemplateID { get; set; }
        public DocumentTemplate? DocumentTemplate { get; set; }

        public Guid BookingID { get; set; }
        public Booking? Booking { get; set; }

        public List<ContractPaymentDetail>? ContractPaymentDetails { get; set; }
        public Guid? PaymentProcessID { get; set; }
        public PaymentProcess? PaymentProcess { get; set; }
        public Guid? PromotionDetaiID { get; set; }
        public PromotionDetail? PromotionDetail { get; set; }
        public Guid CustomerID { get; set; }
        public Customer? Customer { get; set; }
 

    }
}
