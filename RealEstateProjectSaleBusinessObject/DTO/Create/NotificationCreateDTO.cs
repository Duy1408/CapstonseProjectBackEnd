using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class NotificationCreateDTO
    {
        [JsonIgnore]
        public Guid NotificationID { get; set; }
        public string? Title { get; set; }
        public string? Subtiltle { get; set; }
        public string? Body { get; set; }
        [JsonIgnore]
        public DateTime CreatedTime { get; set; }
        [JsonIgnore]
        public bool Status { get; set; }
        public Guid BookingID { get; set; }
        [JsonIgnore]
        public Guid CustomerID { get; set; }

    }
}
