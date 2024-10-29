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
        public Guid PaymentID { get; set; }
        public DateTime CreatedTime { get; set; }
        public Guid BookingID { get; set; }
        public Guid CustomerID { get; set; }
    }
}
