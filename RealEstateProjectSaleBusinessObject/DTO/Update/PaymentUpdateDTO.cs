using RealEstateProjectSaleBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Update
{
    public class PaymentUpdateDTO
    {
        public double? Amount { get; set; }
        public string? Content { get; set; }
        public bool? Status { get; set; }
        public Guid? BookingID { get; set; }
        public Guid? CustomerID { get; set; }
    }
}
