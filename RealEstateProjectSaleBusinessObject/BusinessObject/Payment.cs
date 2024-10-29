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
        public double Amount { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? PaymentTime { get; set; }
        public bool Status { get; set; }
        public Guid? BookingID { get; set; }
        public Booking? Booking { get; set; }
        public Guid CustomerID { get; set; }
        public Customer? Customer { get; set; }
    }
}
