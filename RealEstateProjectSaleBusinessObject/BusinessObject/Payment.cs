using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class Payment
    {
        public Guid PaymentID { get; set; }
        public Guid CustomerID { get; set; }     
        public Guid ProjectID { get; set; }
        public int Deposittoholdproject { get; set; }
        public bool Status { get; set; }
        public Guid PaymentTypeID { get; set; }
        public PaymentType? PaymentType { get; set; }
        public Guid BookingID { get; set; }
        public Booking? Booking { get; set; }
        public Guid ContractPaymentDetailID { get; set; }
        public ContractPaymentDetail? ContractPaymentDetail { get; set; }
    }
}
