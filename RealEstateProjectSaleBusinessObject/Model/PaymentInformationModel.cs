using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.Model
{
    public class PaymentInformationModel
    {
        [JsonIgnore]
        public Guid PaymentID { get; set; } = Guid.NewGuid();
        public double Amount { get; set; }
        public string? Content { get; set; }
        [JsonIgnore]
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public Guid PaymentTypeID { get; set; }
        public Guid BookingID { get; set; }
        public Guid CustomerID { get; set; }
    }
}
