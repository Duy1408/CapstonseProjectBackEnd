using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class Contract
    {
        public Guid ContractID { get; set; }
        public DateTime? DateSigned { get; set; }
        public DateTime? UpdateUsAt { get; set; }
        public DateTime CreatedStAt { get; set; }
        public string ContractType { get; set; }
        public bool Status { get; set; }
        public Guid BookingID { get; set; }
        public Booking? Booking { get; set; }
        public List<ContractPaymentDetail>? ContractPaymentDetails { get; set; }
        public Guid PaymentProcessID { get; set; }
        public PaymentProcess? PaymentProcess { get; set; }

    }
}
