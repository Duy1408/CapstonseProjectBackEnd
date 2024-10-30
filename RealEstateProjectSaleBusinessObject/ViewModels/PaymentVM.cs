using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.JsonConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class PaymentVM
    {
        public Guid PaymentID { get; set; }
        public double Amount { get; set; }
        public string? Content { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreatedTime { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? PaymentTime { get; set; }
        public bool Status { get; set; }
        public Guid? BookingID { get; set; }
        public string BookingStatus { get; set; }
        public Guid CustomerID { get; set; }
        public string FullName { get; set; }

    }
}
