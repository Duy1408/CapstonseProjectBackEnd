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
        public string Title { get; set; }
        public string Subtiltle { get; set; }
        public string Body { get; set; }
        public string DeepLink { get; set; }
        public Guid? CustomerID { get; set; }
        public Guid? OpeningForSaleID { get; set; }
    }
}
