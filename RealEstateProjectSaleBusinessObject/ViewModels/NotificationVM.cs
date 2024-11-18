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
    public class NotificationVM
    {
        public Guid NotificationID { get; set; }
        public string Title { get; set; }
        public string Subtiltle { get; set; }
        public string Body { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreatedTime { get; set; }
        public bool Status { get; set; }
        public Guid BookingID { get; set; }
    }
}
